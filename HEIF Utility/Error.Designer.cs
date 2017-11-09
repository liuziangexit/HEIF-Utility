namespace HEIF_Utility
{
    partial class Error
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
            this.maintext = new System.Windows.Forms.Label();
            this.accept = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linklabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // maintext
            // 
            this.maintext.Font = new System.Drawing.Font("微软雅黑 Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maintext.ForeColor = System.Drawing.Color.Black;
            this.maintext.Location = new System.Drawing.Point(12, 9);
            this.maintext.Name = "maintext";
            this.maintext.Size = new System.Drawing.Size(414, 185);
            this.maintext.TabIndex = 0;
            this.maintext.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // accept
            // 
            this.accept.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.accept.Location = new System.Drawing.Point(351, 197);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(75, 23);
            this.accept.TabIndex = 1;
            this.accept.Text = "好";
            this.accept.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 202);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "关于此错误:";
            // 
            // linklabel
            // 
            this.linklabel.AutoSize = true;
            this.linklabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linklabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.linklabel.ForeColor = System.Drawing.Color.DarkOrange;
            this.linklabel.Location = new System.Drawing.Point(89, 202);
            this.linklabel.Name = "linklabel";
            this.linklabel.Size = new System.Drawing.Size(0, 12);
            this.linklabel.TabIndex = 4;
            this.linklabel.Click += new System.EventHandler(this.link_Click);
            // 
            // Error
            // 
            this.AcceptButton = this.accept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.accept;
            this.ClientSize = new System.Drawing.Size(438, 226);
            this.ControlBox = false;
            this.Controls.Add(this.linklabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.accept);
            this.Controls.Add(this.maintext);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Error";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "错误";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label maintext;
        public System.Windows.Forms.Label linklabel;
    }
}