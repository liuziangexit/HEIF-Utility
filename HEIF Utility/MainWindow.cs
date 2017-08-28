using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HEIF_Utility
{
    public partial class MainWindow : Form
    {
        private string filename;
        public MainWindow()
        {
            InitializeComponent();
            this.pictureBox1.AllowDrop = true;
        }

        public MainWindow(string filename)
        {
            InitializeComponent();
            this.pictureBox1.AllowDrop = true;
            open(filename);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new About();
            box.ShowDialog();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (!(System.IO.File.Exists(Application.StartupPath + "\\HUD.exe") || !System.IO.File.Exists(Application.StartupPath + "ffmpeg.exe")))
            {
                MessageBox.Show("缺少核心组件，HEIF 实用工具无法启动。");
                Environment.Exit(0);
            }
        }

        private void 选择HEIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var filepicker = new OpenFileDialog();
                filepicker.Title = "打开HEIF";
                filepicker.Multiselect = false;
                filepicker.ShowDialog();
                if (filepicker.FileName == "") return;
                filename = filepicker.FileName;

                open(filename);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("无法打开此文件。");
                return;
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var box = new SaveFileDialog();
                box.Title = "保存";
                box.Filter = "PNG|*.png|JPG|*.jpg";
                box.ShowDialog();
                if (box.FileName == "") return;

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = Application.StartupPath + "\\ffmpeg.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = "-y -i \"" + Application.StartupPath + "\\out.265\" \"" + box.FileName + "\"";
                p.Start();                
                p.StandardInput.AutoFlush = true;
                p.WaitForExit();
                p.Close();

                if (System.IO.File.Exists(box.FileName))
                {
                    MessageBox.Show("成功保存到 " + box.FileName);
                    return;
                }
                else
                {
                    MessageBox.Show("无法保存到 " + box.FileName);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        void open(string openthis)
        {
            try
            {
                if (openthis == "") return;
                filename = openthis;
                
                try
                {
                    System.IO.File.Delete(Application.StartupPath + "\\peek.jpg");
                }
                catch (Exception)
                { }
                try
                {
                    System.IO.File.Delete(Application.StartupPath + "\\out.265");
                }
                catch (Exception)
                { }
                
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = Application.StartupPath + "\\HUD.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.Arguments = "\"" + openthis + "\" " + "\"" + Application.StartupPath + "\\out.265" + "\"";
                p.Start();
                p.StandardInput.AutoFlush = true;
                p.WaitForExit();
                p.Close();

                p.StartInfo.FileName = Application.StartupPath + "\\ffmpeg.exe";
                p.StartInfo.Arguments = "-y -i " + "\"" + Application.StartupPath + "\\out.265\" " + "\"" + Application.StartupPath + "\\peek.jpg\"";
                p.Start();
                p.StandardInput.AutoFlush = true;
                p.WaitForExit();
                p.Close();

                Stream s = new FileStream(Application.StartupPath + "\\peek.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
                Image img = new Bitmap(s);
                pictureBox1.Image = img;
                s.Close();
                pictureBox1.Visible = true;
                详细信息ToolStripMenuItem.Enabled = true;
                另存为ToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("无法打开此文件。");
                return;
            }
        }

        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var fi = new FileInfo(filename);
                fi.OpenRead();

                MessageBox.Show("文件名: " + fi.Name + "\r\n创建日期: " + fi.CreationTime.ToLongDateString() + " " + fi.CreationTime.ToLongTimeString() +
                    "\r\n大小: " + fi.Length.ToString() + " byte\r\n分辨率: " + pictureBox1.Image.Width + "x" + pictureBox1.Image.Height);
            }
            catch (Exception) { }
        }

        private void MainWindow_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                open(((string[])e.Data.GetData(DataFormats.FileDrop, false))[0]);
            }
            catch (Exception) { }
        }
        
        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void 批量转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new BC();
            box.ShowDialog();
        }
    }
}
