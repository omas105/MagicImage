namespace MagicImage
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbOrg = new System.Windows.Forms.PictureBox();
            this.pbResult = new System.Windows.Forms.PictureBox();
            this.pbBefore = new System.Windows.Forms.PictureBox();
            this.pbAfter = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbDDPayload = new System.Windows.Forms.Label();
            this.lbDDImage = new System.Windows.Forms.Label();
            this.lbGetPayload = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbOrg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBefore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAfter)).BeginInit();
            this.SuspendLayout();
            // 
            // pbOrg
            // 
            this.pbOrg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbOrg.Location = new System.Drawing.Point(12, 12);
            this.pbOrg.Name = "pbOrg";
            this.pbOrg.Size = new System.Drawing.Size(300, 300);
            this.pbOrg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbOrg.TabIndex = 0;
            this.pbOrg.TabStop = false;
            this.pbOrg.DragDrop += new System.Windows.Forms.DragEventHandler(this.PbOrg_DragDrop);
            this.pbOrg.DragEnter += new System.Windows.Forms.DragEventHandler(this.PbOrg_DragEnter);
            // 
            // pbResult
            // 
            this.pbResult.AccessibleName = "";
            this.pbResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbResult.Location = new System.Drawing.Point(347, 12);
            this.pbResult.Name = "pbResult";
            this.pbResult.Size = new System.Drawing.Size(300, 300);
            this.pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbResult.TabIndex = 1;
            this.pbResult.TabStop = false;
            this.pbResult.DragDrop += new System.Windows.Forms.DragEventHandler(this.PbResult_DragDrop);
            this.pbResult.DragEnter += new System.Windows.Forms.DragEventHandler(this.PbResult_DragEnter);
            // 
            // pbBefore
            // 
            this.pbBefore.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbBefore.Location = new System.Drawing.Point(12, 349);
            this.pbBefore.Name = "pbBefore";
            this.pbBefore.Size = new System.Drawing.Size(300, 300);
            this.pbBefore.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbBefore.TabIndex = 2;
            this.pbBefore.TabStop = false;
            this.pbBefore.Click += new System.EventHandler(this.PbBefore_Click);
            // 
            // pbAfter
            // 
            this.pbAfter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbAfter.Location = new System.Drawing.Point(347, 349);
            this.pbAfter.Name = "pbAfter";
            this.pbAfter.Size = new System.Drawing.Size(300, 300);
            this.pbAfter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbAfter.TabIndex = 3;
            this.pbAfter.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(135, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Orginal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(468, 315);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Result";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(110, 652);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "LSB before";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(459, 652);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "LSB after";
            // 
            // lbDDPayload
            // 
            this.lbDDPayload.AutoSize = true;
            this.lbDDPayload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDDPayload.Location = new System.Drawing.Point(424, 22);
            this.lbDDPayload.Name = "lbDDPayload";
            this.lbDDPayload.Size = new System.Drawing.Size(150, 16);
            this.lbDDPayload.TabIndex = 6;
            this.lbDDPayload.Text = "Drag and Drop payload";
            // 
            // lbDDImage
            // 
            this.lbDDImage.AutoSize = true;
            this.lbDDImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDDImage.Location = new System.Drawing.Point(89, 22);
            this.lbDDImage.Name = "lbDDImage";
            this.lbDDImage.Size = new System.Drawing.Size(136, 16);
            this.lbDDImage.TabIndex = 7;
            this.lbDDImage.Text = "Drag and drop image";
            // 
            // lbGetPayload
            // 
            this.lbGetPayload.AutoSize = true;
            this.lbGetPayload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGetPayload.Location = new System.Drawing.Point(62, 360);
            this.lbGetPayload.Name = "lbGetPayload";
            this.lbGetPayload.Size = new System.Drawing.Size(196, 16);
            this.lbGetPayload.TabIndex = 8;
            this.lbGetPayload.Text = "Click to get payload from image";
            this.lbGetPayload.Click += new System.EventHandler(this.LbGetPayload_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 682);
            this.Controls.Add(this.lbGetPayload);
            this.Controls.Add(this.lbDDImage);
            this.Controls.Add(this.lbDDPayload);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbAfter);
            this.Controls.Add(this.pbBefore);
            this.Controls.Add(this.pbResult);
            this.Controls.Add(this.pbOrg);
            this.Name = "Form1";
            this.Text = "Magic Image";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbOrg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBefore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAfter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbOrg;
        private System.Windows.Forms.PictureBox pbResult;
        private System.Windows.Forms.PictureBox pbBefore;
        private System.Windows.Forms.PictureBox pbAfter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbDDPayload;
        private System.Windows.Forms.Label lbDDImage;
        private System.Windows.Forms.Label lbGetPayload;
    }
}

