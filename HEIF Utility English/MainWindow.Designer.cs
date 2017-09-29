namespace HEIF_Utility
{
    partial class MainWindow
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择HEIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.拷贝ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更多ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.批量转换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainPictureBox = new System.Windows.Forms.PictureBox();
            this.TopPanel = new System.Windows.Forms.Panel();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.DetailedButton = new System.Windows.Forms.Button();
            this.DragPicture = new System.Windows.Forms.PictureBox();
            this.SoftwareName = new System.Windows.Forms.Label();
            this.TopMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).BeginInit();
            this.TopPanel.SuspendLayout();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DragPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // TopMenuStrip
            // 
            this.TopMenuStrip.BackColor = System.Drawing.Color.Transparent;
            this.TopMenuStrip.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TopMenuStrip.BackgroundImage")));
            this.TopMenuStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.TopMenuStrip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.更多ToolStripMenuItem});
            this.TopMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.TopMenuStrip.Size = new System.Drawing.Size(725, 35);
            this.TopMenuStrip.TabIndex = 0;
            this.TopMenuStrip.Text = "menuStrip1";
            this.TopMenuStrip.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseDown);
            this.TopMenuStrip.MouseLeave += new System.EventHandler(this.menuStrip1_MouseLeave);
            this.TopMenuStrip.MouseMove += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseMove);
            this.TopMenuStrip.MouseUp += new System.Windows.Forms.MouseEventHandler(this.menuStrip1_MouseUp);
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.选择HEIFToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.拷贝ToolStripMenuItem});
            this.文件ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(39, 31);
            this.文件ToolStripMenuItem.Text = "File";
            // 
            // 选择HEIFToolStripMenuItem
            // 
            this.选择HEIFToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.选择HEIFToolStripMenuItem.Name = "选择HEIFToolStripMenuItem";
            this.选择HEIFToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.选择HEIFToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.选择HEIFToolStripMenuItem.Text = "Open(.heic)";
            this.选择HEIFToolStripMenuItem.Click += new System.EventHandler(this.选择HEIFToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Enabled = false;
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.另存为ToolStripMenuItem.Text = "Save(.jpg)";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // 拷贝ToolStripMenuItem
            // 
            this.拷贝ToolStripMenuItem.Enabled = false;
            this.拷贝ToolStripMenuItem.Name = "拷贝ToolStripMenuItem";
            this.拷贝ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.拷贝ToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.拷贝ToolStripMenuItem.Text = "Copy";
            this.拷贝ToolStripMenuItem.Click += new System.EventHandler(this.拷贝ToolStripMenuItem_Click);
            // 
            // 更多ToolStripMenuItem
            // 
            this.更多ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.批量转换ToolStripMenuItem,
            this.关于ToolStripMenuItem});
            this.更多ToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
            this.更多ToolStripMenuItem.Name = "更多ToolStripMenuItem";
            this.更多ToolStripMenuItem.Size = new System.Drawing.Size(52, 31);
            this.更多ToolStripMenuItem.Text = "More";
            // 
            // 批量转换ToolStripMenuItem
            // 
            this.批量转换ToolStripMenuItem.Name = "批量转换ToolStripMenuItem";
            this.批量转换ToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.批量转换ToolStripMenuItem.Text = "Batch Conversion";
            this.批量转换ToolStripMenuItem.Click += new System.EventHandler(this.批量转换ToolStripMenuItem_Click);
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.关于ToolStripMenuItem.Text = "About";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // MainPictureBox
            // 
            this.MainPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.MainPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPictureBox.ErrorImage = null;
            this.MainPictureBox.InitialImage = null;
            this.MainPictureBox.Location = new System.Drawing.Point(0, 0);
            this.MainPictureBox.Name = "MainPictureBox";
            this.MainPictureBox.Size = new System.Drawing.Size(725, 559);
            this.MainPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MainPictureBox.TabIndex = 1;
            this.MainPictureBox.TabStop = false;
            this.MainPictureBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.HU_DragDrop);
            this.MainPictureBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.HU_DragEnter);
            // 
            // TopPanel
            // 
            this.TopPanel.Controls.Add(this.TopMenuStrip);
            this.TopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TopPanel.Location = new System.Drawing.Point(0, 0);
            this.TopPanel.Name = "TopPanel";
            this.TopPanel.Size = new System.Drawing.Size(725, 35);
            this.TopPanel.TabIndex = 3;
            // 
            // BottomPanel
            // 
            this.BottomPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(110)))), ((int)(((byte)(110)))));
            this.BottomPanel.Controls.Add(this.DetailedButton);
            this.BottomPanel.Controls.Add(this.DragPicture);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 536);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(725, 23);
            this.BottomPanel.TabIndex = 5;
            // 
            // DetailedButton
            // 
            this.DetailedButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(107)))), ((int)(((byte)(149)))));
            this.DetailedButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.DetailedButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(135)))), ((int)(((byte)(135)))));
            this.DetailedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DetailedButton.ForeColor = System.Drawing.Color.White;
            this.DetailedButton.Location = new System.Drawing.Point(0, 0);
            this.DetailedButton.Name = "DetailedButton";
            this.DetailedButton.Size = new System.Drawing.Size(75, 23);
            this.DetailedButton.TabIndex = 1;
            this.DetailedButton.TabStop = false;
            this.DetailedButton.Text = "Image Info";
            this.DetailedButton.UseVisualStyleBackColor = false;
            this.DetailedButton.Visible = false;
            this.DetailedButton.Click += new System.EventHandler(this.详细信息ToolStripMenuItem_Click);
            // 
            // DragPicture
            // 
            this.DragPicture.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.DragPicture.Dock = System.Windows.Forms.DockStyle.Right;
            this.DragPicture.ErrorImage = null;
            this.DragPicture.InitialImage = null;
            this.DragPicture.Location = new System.Drawing.Point(700, 0);
            this.DragPicture.Name = "DragPicture";
            this.DragPicture.Size = new System.Drawing.Size(25, 23);
            this.DragPicture.TabIndex = 0;
            this.DragPicture.TabStop = false;
            this.DragPicture.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragPicture_MouseDown);
            this.DragPicture.MouseMove += new System.Windows.Forms.MouseEventHandler(this.dragPicture_MouseMove);
            this.DragPicture.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dragPicture_MouseUp);
            // 
            // SoftwareName
            // 
            this.SoftwareName.AllowDrop = true;
            this.SoftwareName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SoftwareName.Enabled = false;
            this.SoftwareName.Font = new System.Drawing.Font("微软雅黑", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SoftwareName.ForeColor = System.Drawing.Color.Silver;
            this.SoftwareName.Location = new System.Drawing.Point(0, 35);
            this.SoftwareName.Name = "SoftwareName";
            this.SoftwareName.Size = new System.Drawing.Size(725, 501);
            this.SoftwareName.TabIndex = 6;
            this.SoftwareName.Text = "HEIF Utility";
            this.SoftwareName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SoftwareName.DragDrop += new System.Windows.Forms.DragEventHandler(this.HU_DragDrop);
            this.SoftwareName.DragEnter += new System.Windows.Forms.DragEventHandler(this.HU_DragEnter);
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(725, 559);
            this.ControlBox = false;
            this.Controls.Add(this.SoftwareName);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.TopPanel);
            this.Controls.Add(this.MainPictureBox);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.TopMenuStrip;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "MainWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainPictureBox)).EndInit();
            this.TopPanel.ResumeLayout(false);
            this.TopPanel.PerformLayout();
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DragPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuStrip TopMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择HEIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.PictureBox MainPictureBox;
        private System.Windows.Forms.ToolStripMenuItem 更多ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 批量转换ToolStripMenuItem;
        private System.Windows.Forms.Panel TopPanel;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.PictureBox DragPicture;
        private System.Windows.Forms.Button DetailedButton;
        private System.Windows.Forms.Label SoftwareName;
        private System.Windows.Forms.ToolStripMenuItem 拷贝ToolStripMenuItem;
    }
}

