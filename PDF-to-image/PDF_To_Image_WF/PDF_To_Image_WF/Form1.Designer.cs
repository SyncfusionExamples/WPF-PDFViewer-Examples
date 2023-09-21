namespace PDF_To_Image_WF
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
            this.ChoosePDF = new System.Windows.Forms.Button();
            this.Convert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChoosePDF
            // 
            this.ChoosePDF.Location = new System.Drawing.Point(326, 157);
            this.ChoosePDF.Name = "ChoosePDF";
            this.ChoosePDF.Size = new System.Drawing.Size(120, 32);
            this.ChoosePDF.TabIndex = 0;
            this.ChoosePDF.Text = "Choose PDF";
            this.ChoosePDF.UseVisualStyleBackColor = true;
            this.ChoosePDF.Click += new System.EventHandler(this.ChoosePDF_Click);
            // 
            // Convert
            // 
            this.Convert.Enabled = false;
            this.Convert.Location = new System.Drawing.Point(326, 195);
            this.Convert.Name = "Convert";
            this.Convert.Size = new System.Drawing.Size(120, 32);
            this.Convert.TabIndex = 1;
            this.Convert.Text = "Convert";
            this.Convert.UseVisualStyleBackColor = true;
            this.Convert.Click += new System.EventHandler(this.Convert_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Convert);
            this.Controls.Add(this.ChoosePDF);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ChoosePDF;
        private System.Windows.Forms.Button Convert;
    }
}

