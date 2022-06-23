using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pbResult.AllowDrop = true;
            pbOrg.AllowDrop = true;
        }


        private void Form1_Shown(object sender, EventArgs e)
        {
            CheckCommandLineArgs(Environment.GetCommandLineArgs());
        }

        /// <summary>
        /// Handles drag & drop on icon
        /// </summary>
        /// <param name="args">Environment.GetCommandLineArgs()</param>
        private void CheckCommandLineArgs(string[] args)
        {
            if (args.Length > 1)
            {
                LoadImage(args[1]);
            }
        }
        public Bitmap SetSecret(Bitmap image, byte[] payload)
        {

            Bitmap org = image;
            Bitmap result = new Bitmap(org.Width, org.Height, PixelFormat.Format24bppRgb);
            Bitmap before = new Bitmap(org.Width, org.Height, PixelFormat.Format24bppRgb);
            Bitmap after = new Bitmap(org.Width, org.Height, PixelFormat.Format24bppRgb);
            Debug.WriteLine($"width {org.Width} heigth {org.Height}");
            LockBitmap orgLockBitmap = new LockBitmap(org);
            LockBitmap resultLockBitmap = new LockBitmap(result);
            LockBitmap beforeLockBitmap = new LockBitmap(before);
            LockBitmap afterLockBitmap = new LockBitmap(after);
            orgLockBitmap.LockBits();
            resultLockBitmap.LockBits();
            beforeLockBitmap.LockBits();
            afterLockBitmap.LockBits();

            BitSequence bitSequence = new BitSequence(payload);

            for (int y = 0; y < result.Height; y++)
            {
                for (int x = 0; x < result.Width; x++)
                {
                    Color color = orgLockBitmap.GetPixel(x, y);

                    int lsb = color.R & 1;
                    if (lsb == 1)
                    {
                        beforeLockBitmap.SetPixel(x, y, Color.PaleVioletRed);
                    }

                    int r = (color.R & ~1) | bitSequence.NextBit();
                    int g = (color.G & ~1) | bitSequence.NextBit();
                    int b = (color.B & ~1) | bitSequence.NextBit();
                    Color c = Color.FromArgb(r, g, b);
                    resultLockBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                    lsb = r & 1;
                    if (lsb == 1)
                    {
                        afterLockBitmap.SetPixel(x, y, Color.LightGreen);
                    }
                }
            }
            orgLockBitmap.UnlockBits();
            resultLockBitmap.UnlockBits();
            beforeLockBitmap.UnlockBits();
            afterLockBitmap.UnlockBits();

            lbGetPayload.BeginInvoke((Action)(() => lbGetPayload.Visible = false));
            pbBefore.Image = before;
            pbAfter.Image = after;
            pbResult.Image = result;
            Debug.WriteLine("SetSecret end");
            return result;
        }

        public byte[] GetSecret(Bitmap image)
        {
            BitSequence bitSequence = new BitSequence();
            Debug.WriteLine("getting secret...");
            Bitmap org = image;
            Debug.WriteLine($"width {org.Width} heigth {org.Height}");
            LockBitmap orgLockBitmap = new LockBitmap(org);
            orgLockBitmap.LockBits();

            for (int y = 0; y < org.Height; y++)
            {
                for (int x = 0; x < org.Width; x++)
                {
                    Color color = orgLockBitmap.GetPixel(x, y);
                    bitSequence.SetBit(color.R & 1);
                    bitSequence.SetBit(color.G & 1);
                    bitSequence.SetBit(color.B & 1);
                    if (bitSequence.endOfPaylod)
                    {
                        Debug.WriteLine("end of payload");
                        break;
                    }
                }
                if (bitSequence.endOfPaylod)
                {
                    break;
                }
            }
            orgLockBitmap.UnlockBits();
            Debug.WriteLine("end");
            return bitSequence.GetPaylod();
        }
        private byte[] LoadPayload(string path)
        {
            this.BeginInvoke((Action)(() => this.Text = "loading payload"));
            Debug.WriteLine("load payload");
            byte[] buffer = new byte[0];
            try
            {
                using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, (int)fileStream.Length);
                    fileStream.Close();
                }
            }
            catch (Exception)
            {
                this.BeginInvoke((Action)(() => this.Text = "File format error"));
                Debug.WriteLine("file format error");
            }
            return buffer;
        }
        private void SavePayload(byte[] payload, string path)
        {
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
                fileStream.Write(payload, 0, payload.Length);
            //using (BinaryWriter bw = new BinaryWriter(fileStream)) {
            //    bw.Write(payload);
            //}
        }

        private void LoadImage(string path)
        {
            try
            {
                lbDDImage.Visible = false;
                lbGetPayload.Visible = true;
                lbDDPayload.Visible = true;
                pbBefore.Image = null;
                pbAfter.Image = null;
                pbResult.Image = null;

                pbOrg.Image = new Bitmap(path);
                int imageBytes = pbOrg.Image.Width * pbOrg.Image.Height / 8;
                this.Text = $"Max payload {imageBytes / 1000}KB";
            }
            catch (Exception)
            {
                this.Text = "File format error";
                Debug.WriteLine("file format error");
            }
        }

        private void PbResult_DragDrop(object sender, DragEventArgs e)
        {
            if (pbOrg.Image == null)
            {
                this.Text = "Load image first";
                Debug.WriteLine("Load image first");
            }
            else
            {
                lbDDPayload.Visible = false;
                Debug.WriteLine("File drop");
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
                    string path = filePath[0];
                    this.Text = "Running...";
                    Task.Run(() =>
                    {
                        byte[] payload = LoadPayload(path);
                        int imageBytes = pbOrg.Image.Width * pbOrg.Image.Height / 8;
                        if (payload.Length + 4 < imageBytes)
                        {
                            Bitmap imageResult = SetSecret(new Bitmap(pbOrg.Image), payload);
                            imageResult.Save("secret.png", ImageFormat.Png);
                            this.BeginInvoke((Action)(() => this.Text = $"Done hiding {Path.GetFileName(path)}"));
                        }
                        else
                        {
                            this.BeginInvoke((Action)(() => this.Text = $"Payload to large. Max {imageBytes / 1000}KB"));
                        }
                    });
                }
            }
        }

        private void PbResult_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void PbOrg_DragDrop(object sender, DragEventArgs e)
        {
            Debug.WriteLine("File drop");
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePath = (string[])e.Data.GetData(DataFormats.FileDrop);
                LoadImage(filePath[0]);

            }
        }

        private void PbOrg_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void LbGetPayload_Click(object sender, EventArgs e)
        {
            GetPayload(new Bitmap(pbOrg.Image));
        }

        private void PbBefore_Click(object sender, EventArgs e)
        {
            GetPayload(new Bitmap(pbOrg.Image));
        }

        private void GetPayload(Bitmap image)
        {
            try
            {
                this.Text = "Getting payload...";
                SavePayload(GetSecret(image), "payload");
                this.Text = "Done";
            }
            catch (Exception)
            {
                this.Text = "no secret found?";
                Debug.WriteLine("no secret found?");
            }
        }

        //private static Image BinaryToImage(Binary binaryData)
        //{
        //    if (binaryData == null) return null;
        //    byte[] buffer = binaryData.ToArray();
        //    MemoryStream memStream = new MemoryStream();
        //    memStream.Write(buffer, 0, buffer.Length);
        //    return Image.FromStream(memStream);
        //}

        //private Bitmap MakeImage2(string path)
        //{
        //    Bitmap bmOut;
        //    unsafe
        //    {
        //        using (Bitmap bmOrg = new Bitmap(path))
        //        {
        //            bmOut = new Bitmap(bmOrg.Width, bmOrg.Height);
        //            Debug.WriteLine($"heigth {bmOrg.Height} width {bmOrg.Width}");
        //            for (int i = 0; i < bmOrg.Height; i++)
        //            {
        //                for (int j = 0; j < bmOrg.Width; j++)
        //                {
        //                    Color color = bmOrg.GetPixel(j, i);
        //                    int gray = Convert.ToInt32(new int[] { color.R, color.B, color.G }.Average());
        //                    bmOut.SetPixel(j, i, Color.FromArgb(gray, gray, gray));
        //                }
        //            }
        //            Debug.WriteLine("end");
        //        }
        //    }
        //    return bmOut;
        //}

        //private Bitmap MakeImage1(string path)
        //{
        //    Bitmap org = new Bitmap(path);
        //    Bitmap result = new Bitmap(org.Width, org.Height, PixelFormat.Format24bppRgb);
        //    Bitmap red = new Bitmap(org.Width, org.Height, PixelFormat.Format24bppRgb);
        //    Bitmap green = new Bitmap(org.Width, org.Height, PixelFormat.Format24bppRgb);
        //    Debug.WriteLine($"width {org.Width} heigth {org.Height}");
        //    LockBitmap orgLockBitmap = new LockBitmap(org);
        //    LockBitmap resultLockBitmap = new LockBitmap(result);
        //    LockBitmap redLockBitmap = new LockBitmap(red);
        //    LockBitmap greenLockBitmap = new LockBitmap(green);
        //    orgLockBitmap.LockBits();
        //    resultLockBitmap.LockBits();
        //    redLockBitmap.LockBits();
        //    greenLockBitmap.LockBits();

        //    for (int y = 0; y < result.Height; y++)
        //    {
        //        for (int x = 0; x < result.Width; x++)
        //        {
        //            Color color = orgLockBitmap.GetPixel(x, y);
        //            int lsb = color.R & 1;
        //            if (lsb == 1)
        //            {
        //                redLockBitmap.SetPixel(x, y, Color.PaleVioletRed);
        //            }
        //            lsb = color.G & 1;
        //            if (lsb == 1)
        //            {
        //                greenLockBitmap.SetPixel(x, y, Color.LightGreen);
        //            }
        //            //lsb = color.B & 1;
        //            //if (lsb == 1)
        //            //{
        //            //    blueLockBitmap.SetPixel(x, y, Color.LightBlue);
        //            //}
        //            color = Color.FromArgb(color.R & ~1, color.G & ~1, color.B & ~1);
        //            resultLockBitmap.SetPixel(x, y, color);
        //        }
        //    }
        //    orgLockBitmap.UnlockBits();
        //    resultLockBitmap.UnlockBits();
        //    redLockBitmap.UnlockBits();
        //    greenLockBitmap.UnlockBits();

        //    pbBefore.Image = red;
        //    pbAfter.Image = green;

        //    Debug.WriteLine("MakeImage1 end");
        //    return result;
        //}

        //public Bitmap MakeImage(string path, byte[] payload)
        //{
        //    Bitmap org = new Bitmap(path);
        //    Bitmap result = new Bitmap(org.Width, org.Height, PixelFormat.Format24bppRgb);
        //    Bitmap before = new Bitmap(org.Width, org.Height, PixelFormat.Format24bppRgb);
        //    Bitmap after = new Bitmap(org.Width, org.Height, PixelFormat.Format24bppRgb);
        //    Debug.WriteLine($"width {org.Width} heigth {org.Height}");
        //    LockBitmap orgLockBitmap = new LockBitmap(org);
        //    LockBitmap resultLockBitmap = new LockBitmap(result);
        //    LockBitmap beforeLockBitmap = new LockBitmap(before);
        //    LockBitmap afterLockBitmap = new LockBitmap(after);
        //    orgLockBitmap.LockBits();
        //    resultLockBitmap.LockBits();
        //    beforeLockBitmap.LockBits();
        //    afterLockBitmap.LockBits();

        //    BitSequence bitSequence = new BitSequence(payload);

        //    for (int y = 0; y < result.Height; y++)
        //    {
        //        for (int x = 0; x < result.Width; x++)
        //        {
        //            Color color = orgLockBitmap.GetPixel(x, y);

        //            int lsb = color.R & 1;
        //            if (lsb == 1)
        //            {
        //                beforeLockBitmap.SetPixel(x, y, Color.PaleVioletRed);
        //            }

        //            int r = (color.R & ~1) | bitSequence.NextBit();
        //            int g = (color.G & ~1) | bitSequence.NextBit();
        //            int b = (color.B & ~1) | bitSequence.NextBit();
        //            Color c = Color.FromArgb(r, g, b);
        //            resultLockBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
        //            lsb = r & 1;
        //            if (lsb == 1)
        //            {
        //                afterLockBitmap.SetPixel(x, y, Color.LightGreen);
        //            }
        //        }
        //    }
        //    orgLockBitmap.UnlockBits();
        //    resultLockBitmap.UnlockBits();
        //    beforeLockBitmap.UnlockBits();
        //    afterLockBitmap.UnlockBits();

        //    pbBefore.Image = before;
        //    pbAfter.Image = after;

        //    Debug.WriteLine("MakeImage1 end");
        //    return result;
        //}
    }
}
