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
        }

        public MainWindow(string openthis)
        {
            InitializeComponent();
            open(openthis);
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new About();
            box.ShowDialog();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            if (!(System.IO.File.Exists("HUD.exe") && System.IO.File.Exists("ffmpeg.exe")))
            {
                MessageBox.Show("缺少核心组件，HEIF 实用工具无法启动。");
                Environment.Exit(0);
            }
            this.pictureBox1.AllowDrop = true;
        }

        private void 选择HEIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var filepicker = new OpenFileDialog();
                filepicker.ShowDialog();
                if (filepicker.FileName == "") return;
                filename = filepicker.FileName;

                open(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var box = new SaveFileDialog();
                box.Filter = "PNG|*.png|JPG|*.jpg|All Files|*.*";
                box.ShowDialog();
                if (box.FileName == "") return;

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine("ffmpeg -y -i out.265 " + box.FileName + "&exit");
                p.StandardInput.AutoFlush = true;
                p.WaitForExit();
                p.Close();

                if (System.IO.File.Exists(box.FileName))
                {
                    MessageBox.Show("成功导出到" + box.FileName);
                    return;
                }
                else
                {
                    MessageBox.Show("无法导出到" + box.FileName);
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
                    System.IO.File.Delete("src.heic");
                }
                catch (Exception)
                { }
                try
                {
                    System.IO.File.Delete("peek.jpg");
                }
                catch (Exception)
                { }
                try
                {
                    System.IO.File.Delete("out.265");
                }
                catch (Exception)
                { }

                try
                {
                    System.IO.File.Copy(openthis, "src.heic");
                }
                catch (Exception)
                {
                    MessageBox.Show("无法复制文件到临时目录。");
                    return;
                }

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardInput = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.CreateNoWindow = true;
                p.Start();
                p.StandardInput.WriteLine("HUD src.heic out.265");
                p.StandardInput.WriteLine("ffmpeg -y -i out.265 peek.jpg&exit");
                p.StandardInput.AutoFlush = true;
                p.WaitForExit();
                p.Close();

                Stream s = new FileStream("peek.jpg", FileMode.Open, FileAccess.Read, FileShare.Read);
                Image img = new Bitmap(s);
                pictureBox1.Image = img;
                s.Close();
                pictureBox1.Visible = true;
                详细信息ToolStripMenuItem.Enabled = true;
                另存为ToolStripMenuItem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fi = new FileInfo(filename);
            fi.OpenRead();

            MessageBox.Show("文件名: " + fi.Name + "\r\n创建日期: " + fi.CreationTime.ToLongDateString() + " " + fi.CreationTime.ToLongTimeString() +
                "\r\n大小: " + fi.Length.ToString() + " byte\r\n分辨率: " + pictureBox1.Image.Width + "x" + pictureBox1.Image.Height);
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
    }
}
