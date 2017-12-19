namespace HEIF_Utility
{
    partial class Batch_Conversion
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
            this.start = new System.Windows.Forms.Button();
            this.set_output_folder = new System.Windows.Forms.Button();
            this.add_file = new System.Windows.Forms.Button();
            this.Title = new System.Windows.Forms.Label();
            this.pop_file = new System.Windows.Forms.Button();
            this.FilelistPanel = new System.Windows.Forms.Panel();
            this.ClearFileList = new System.Windows.Forms.Button();
            this.set_output_quality = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.start.Location = new System.Drawing.Point(425, 382);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(75, 23);
            this.start.TabIndex = 1;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = true;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // set_output_folder
            // 
            this.set_output_folder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.set_output_folder.AutoSize = true;
            this.set_output_folder.Location = new System.Drawing.Point(12, 382);
            this.set_output_folder.Name = "set_output_folder";
            this.set_output_folder.Size = new System.Drawing.Size(111, 23);
            this.set_output_folder.TabIndex = 2;
            this.set_output_folder.Text = "Output Directory";
            this.set_output_folder.UseVisualStyleBackColor = true;
            this.set_output_folder.Click += new System.EventHandler(this.set_output_folder_Click);
            // 
            // add_file
            // 
            this.add_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.add_file.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(107)))), ((int)(((byte)(149)))));
            this.add_file.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.add_file.FlatAppearance.BorderSize = 0;
            this.add_file.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.add_file.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.add_file.ForeColor = System.Drawing.Color.White;
            this.add_file.Location = new System.Drawing.Point(444, 22);
            this.add_file.Name = "add_file";
            this.add_file.Size = new System.Drawing.Size(25, 23);
            this.add_file.TabIndex = 3;
            this.add_file.Text = "+";
            this.add_file.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.add_file.UseVisualStyleBackColor = false;
            this.add_file.Click += new System.EventHandler(this.add_file_Click);
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("微软雅黑", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title.ForeColor = System.Drawing.Color.DimGray;
            this.Title.Location = new System.Drawing.Point(5, 5);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(215, 41);
            this.Title.TabIndex = 4;
            this.Title.Text = "Convert Files";
            // 
            // pop_file
            // 
            this.pop_file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pop_file.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(107)))), ((int)(((byte)(149)))));
            this.pop_file.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.pop_file.FlatAppearance.BorderSize = 0;
            this.pop_file.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pop_file.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pop_file.ForeColor = System.Drawing.Color.White;
            this.pop_file.Location = new System.Drawing.Point(475, 22);
            this.pop_file.Name = "pop_file";
            this.pop_file.Size = new System.Drawing.Size(25, 23);
            this.pop_file.TabIndex = 5;
            this.pop_file.Text = "-";
            this.pop_file.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.pop_file.UseVisualStyleBackColor = false;
            this.pop_file.Click += new System.EventHandler(this.pop_file_Click);
            // 
            // FilelistPanel
            // 
            this.FilelistPanel.AllowDrop = true;
            this.FilelistPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FilelistPanel.Location = new System.Drawing.Point(12, 51);
            this.FilelistPanel.Name = "FilelistPanel";
            this.FilelistPanel.Size = new System.Drawing.Size(488, 318);
            this.FilelistPanel.TabIndex = 6;
            // 
            // ClearFileList
            // 
            this.ClearFileList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearFileList.AutoSize = true;
            this.ClearFileList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(107)))), ((int)(((byte)(149)))));
            this.ClearFileList.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.ClearFileList.FlatAppearance.BorderSize = 0;
            this.ClearFileList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearFileList.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClearFileList.ForeColor = System.Drawing.Color.White;
            this.ClearFileList.Location = new System.Drawing.Point(386, 22);
            this.ClearFileList.Name = "ClearFileList";
            this.ClearFileList.Size = new System.Drawing.Size(52, 24);
            this.ClearFileList.TabIndex = 7;
            this.ClearFileList.Text = "Clear";
            this.ClearFileList.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.ClearFileList.UseVisualStyleBackColor = false;
            this.ClearFileList.Click += new System.EventHandler(this.ClearFileList_Click);
            // 
            // set_output_quality
            // 
            this.set_output_quality.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.set_output_quality.AutoSize = true;
            this.set_output_quality.ForeColor = System.Drawing.Color.ForestGreen;
            this.set_output_quality.Location = new System.Drawing.Point(129, 382);
            this.set_output_quality.Name = "set_output_quality";
            this.set_output_quality.Size = new System.Drawing.Size(123, 23);
            this.set_output_quality.TabIndex = 8;
            this.set_output_quality.Text = "Set Output Quality";
            this.set_output_quality.UseVisualStyleBackColor = true;
            this.set_output_quality.Click += new System.EventHandler(this.set_output_quality_Click);
            // 
            // Batch_Conversion
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(512, 417);
            this.Controls.Add(this.set_output_quality);
            this.Controls.Add(this.ClearFileList);
            this.Controls.Add(this.FilelistPanel);
            this.Controls.Add(this.pop_file);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.add_file);
            this.Controls.Add(this.set_output_folder);
            this.Controls.Add(this.start);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(350, 221);
            this.Name = "Batch_Conversion";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch Conversion";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HandleFormClosing);
            this.Resize += new System.EventHandler(this.Batch_Conversion_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button set_output_folder;
        private System.Windows.Forms.Button add_file;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button pop_file;
        private System.Windows.Forms.Panel FilelistPanel;
        private System.Windows.Forms.Button ClearFileList;
        private System.Windows.Forms.Button set_output_quality;
    }
}