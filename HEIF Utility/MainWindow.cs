using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HEIF_Utility
{
    public partial class MainWindow : Form
    {
        private string filename;
        public MainWindow(Size s)
        {
            InitializeComponent();
            var backup = this.Size;
            try
            {
                if (s.Height <= 0 || s.Width <= 0)
                    throw new Exception();
                this.Size = s;
            }
            catch (Exception)
            {
                this.Size = backup;
            }
            this.pictureBox1.AllowDrop = true;            
        }

        /*
        public MainWindow(string filename, Size s)
        {
            InitializeComponent();
            var backup = this.Size;
            try
            {
                if (s.Height <= 0 || s.Width <= 0)
                    throw new Exception();
                this.Size = s;
            }
            catch (Exception)
            {
                this.Size = backup;
            }
            this.pictureBox1.AllowDrop = true;

            var box = new processing();
            Thread T;
            T = new Thread(new ThreadStart(new Action(() =>
            {
                box.ShowDialog();
            })));
            T.IsBackground = true;
            T.Start();

            open(filename, 50);

            box.Invoke(new Action(() =>
            {
                box.Close();
            }));
            this.Focus();
        }
        */

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new About();
            box.ShowDialog();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(Application.StartupPath + "\\HUD.exe") || !System.IO.File.Exists(Application.StartupPath + "\\ffmpeg.exe"))
                {
                    MessageBox.Show("缺少核心组件，HEIF 实用工具无法启动。");
                    Environment.Exit(0);
                }
            }
            catch (Exception) { }
        }

        private void 选择HEIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var filepicker = new OpenFileDialog();
                filepicker.Title = "打开";
                filepicker.Multiselect = false;
                filepicker.Filter = "HEIF(.heic)|*.heic|任意文件|*.*";
                filepicker.ShowDialog();
                if (filepicker.FileName == "") return;
                filename = filepicker.FileName;

                var box = new processing();
                Thread T;
                T = new Thread(new ThreadStart(new Action(() =>
                {
                    box.ShowDialog();
                })));
                T.IsBackground = true;
                T.Start();

                open(filename, 50);

                box.Invoke(new Action(() =>
                {
                    box.Close();                    
                }));
                this.Focus();
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
                box.Filter = "JPG|*.jpg";
                box.ShowDialog();
                if (box.FileName == "") return;

                var box2 = new processing();
                Thread T;
                T = new Thread(new ThreadStart(new Action(() =>
                {
                    box2.ShowDialog();
                })));
                T.IsBackground = true;
                T.Start();

                createpeek(filename, 100);

                System.IO.File.Move("peek.jpg", box.FileName);

                box2.Invoke(new Action(() =>
                {
                    box2.Close();
                }));
                this.Focus();

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

        void createpeek(string openthis, int jpgquality)
        {
            try
            {
                System.IO.File.Delete(Application.StartupPath + "\\peek.jpg");
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
            p.StartInfo.Arguments = "iOS-11 " + "\"" + openthis + "\" " + "peek" + " jpg imageinfo.json " + jpgquality.ToString();
            p.Start();
            p.StandardInput.AutoFlush = true;
            p.WaitForExit();
            p.Close();
        }

        void open(string openthis, int jpgquality)
        {
            try
            {
                if (openthis == "") return;
                filename = openthis;

                createpeek(openthis, jpgquality);

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
                var box = new processing();
                Thread T;
                T = new Thread(new ThreadStart(new Action(() =>
                {
                    box.ShowDialog();
                })));
                T.IsBackground = true;
                T.Start();

                open(((string[])e.Data.GetData(DataFormats.FileDrop, false))[0], 50);

                box.Invoke(new Action(() =>
                {
                    box.Close();
                }));
                this.Focus();
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

        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                System.IO.File.WriteAllText(Application.StartupPath + "//MainWindowSize", this.Size.Width.ToString() + "\r\n" + this.Size.Height.ToString());
            }
            catch (Exception)
            {
                try
                {
                    System.IO.File.Delete(Application.StartupPath + "//MainWindowSize");
                }
                catch (Exception) {
                }
            }
        }
    }
}
