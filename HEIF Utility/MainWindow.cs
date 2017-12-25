using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        private string filename, exifinfo;
        private byte[] heicfile;
        private Point mouseOff;

        private bool isMouseDown = false;

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
            this.MainPictureBox.AllowDrop = true;
        }

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
            this.MainPictureBox.AllowDrop = true;

            this.Show();

            var box = new Processing();
            try
            {
                Thread T;
                T = new Thread(new ThreadStart(new Action(() =>
                {
                    box.ShowDialog();
                })));
                T.IsBackground = true;
                T.Start();
                
                open(filename);
                
                box.Invoke(new Action(() =>
                {
                    box.Close();
                }));
                this.Focus();
            }
            catch (Exception)
            {
                box.Invoke(new Action(() =>
                {
                    box.Close();
                }));
                this.Focus();

                var errorbox = new Error();
                errorbox.maintext.Text = "无法打开此文件。";
                errorbox.linklabel.Text = "转到在线帮助";
                errorbox.link = "https://liuziangexit.com/HEIF-Utility/Help/No1.html";
                errorbox.ShowDialog();

                return;
            }
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new About();
            box.ShowDialog();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            try
            {
                if (!System.IO.File.Exists(Application.StartupPath + "\\HUD.dll") || !System.IO.File.Exists(Application.StartupPath + "\\opencv_ffmpeg330_64.dll") || !System.IO.File.Exists(Application.StartupPath + "\\opencv_world330.dll") || !System.IO.File.Exists(Application.StartupPath + "\\Newtonsoft.Json.dll"))
                {
                    var errorbox = new Error();
                    errorbox.maintext.Text = "缺少核心组件，HEIF 实用工具无法启动。";
                    errorbox.linklabel.Text = "下载页面";
                    errorbox.link = "https://liuziangexit.com/HEIF-Utility";
                    errorbox.ShowDialog();

                    Environment.Exit(0);
                }

                var bar = new TitleBar();
                bar.Dock = DockStyle.Right;
                bar.Name = "LASControlBox";
                bar.OnClickExitButton += new TitleBar.ClickHandler(TitleClicked);
                TopPanel.Controls.Add(bar);
            }
            catch (Exception) { }
        }

        private void TitleClicked(object sender, TitleClickArgs e)
        {
            switch (e.which)
            {
                case 1: { Close(); Environment.Exit(0); } break;
                case 2:
                    {
                        if (WindowState == FormWindowState.Normal)
                            WindowState = FormWindowState.Maximized;
                        else WindowState = FormWindowState.Normal;
                    }
                    break;
                case 3:
                    {
                        WindowState = FormWindowState.Minimized;
                    }
                    break;
                default: { throw new Exception(); };
            }
        }
        
        private void menuStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || WindowState == FormWindowState.Maximized) return;
            mouseOff = new Point(-e.X, -e.Y);
            isMouseDown = true;
        }

        private void menuStrip1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
                isMouseDown = false;
        }

        private void menuStrip1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown) return;
            Point temp = Control.MousePosition;
            temp.Offset(mouseOff.X, mouseOff.Y);
            Location = temp;
        }

        private void menuStrip1_MouseLeave(object sender, EventArgs e)
        {
            if (this.isMouseDown)
                isMouseDown = false;
        }

        private void dragPicture_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            mouseOff = new Point(e.X, e.Y);
            isMouseDown = true;
        }

        private void dragPicture_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown) return;
            Size temp = new Size(Control.MousePosition.X - mouseOff.X - Location.X + 22, Control.MousePosition.Y - mouseOff.Y - Location.Y + 22);
            if (temp.Width < MinimumSize.Width || temp.Height < MinimumSize.Height) return;
            Size = temp;
        }

        private void dragPicture_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isMouseDown) return;
            isMouseDown = false;
        }

        private void 选择HEIFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box = new Processing();
            try
            {
                var filepicker = new OpenFileDialog();
                filepicker.Title = "打开";
                filepicker.Multiselect = false;
                filepicker.Filter = "HEIF(.heic)|*.heic|任意文件|*.*";
                filepicker.ShowDialog();
                if (filepicker.FileName == "") return;                

                
                Thread T;
                T = new Thread(new ThreadStart(new Action(() =>
                {
                    box.ShowDialog();
                })));
                T.IsBackground = true;
                T.Start();

                while (!box.IsHandleCreated)
                    Thread.Sleep(100);

                open(filepicker.FileName);

                box.Invoke(new Action(() =>
                {
                    box.Close();                    
                }));
                this.Focus();
            }
            catch (Exception)
            {
                box.Invoke(new Action(() =>
                {
                    box.Close();
                }));
                this.Focus();
                var errorbox = new Error();
                errorbox.maintext.Text = "无法打开此文件。";
                errorbox.linklabel.Text = "转到在线帮助";
                errorbox.link = "https://liuziangexit.com/HEIF-Utility/Help/No1.html";
                errorbox.ShowDialog();
                return;
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box2 = new Processing();
            try
            {
                var box = new SaveFileDialog();
                box.Title = "保存";
                box.Filter = "JPEG(.jpg)|*.jpg";
                box.ShowDialog();
                if (box.FileName == "") return;

                var sq = new setjpgquality();
                sq.ShowDialog();

                Thread T;
                T = new Thread(new ThreadStart(new Action(() =>
                {
                    box2.ShowDialog();
                })));
                T.IsBackground = true;
                T.Start();

                while (!box2.IsHandleCreated)
                    Thread.Sleep(100);

                try
                {
                    int copysize = 0;
                    byte[] write_this;
                    if (!sq.includes_exif)
                        write_this = invoke_dll.invoke_heif2jpg(heicfile, sq.value, "temp_bitstream.hevc", ref copysize, false);
                    else
                        write_this = invoke_dll.invoke_heif2jpg(heicfile, sq.value, "temp_bitstream.hevc", ref copysize, true);

                    FileStream fs = new FileStream(box.FileName, FileMode.Create);
                    BinaryWriter writer = new BinaryWriter(fs);
                    try
                    {
                        writer.Write(write_this, 0, copysize);
                        writer.Close();
                        fs.Close();
                    }
                    catch (Exception ex) {
                        try
                        {
                            writer.Close();
                            fs.Close();
                        }
                        catch (Exception) { }
                        throw ex;
                    }
                }
                catch (Exception)
                {
                    box2.Invoke(new Action(() =>
                    {
                        box2.Close();
                    }));
                    this.Focus();

                    var errorbox = new Error();
                    errorbox.maintext.Text = "无法保存到 " + box.FileName;
                    errorbox.linklabel.Text = "转到在线帮助";
                    errorbox.link = "https://liuziangexit.com/HEIF-Utility/Help/No2.html";
                    errorbox.ShowDialog();
                    return;
                }

                box2.Invoke(new Action(() =>
                {
                    box2.Close();
                }));
                this.Focus();

                MessageBox.Show("成功保存到 " + box.FileName);
                return;
            }
            catch (Exception)
            {
                box2.Invoke(new Action(() =>
                {
                    box2.Close();
                }));
                this.Focus();
                MessageBox.Show("发生未知错误。");
                return;
            }
        }

        void open(string openthis)
        {
            if (openthis == "") return;            
            var tempheicfile = invoke_dll.read_heif(openthis);
            int copysize = 0;
            MainPictureBox.Image = invoke_dll.ImageFromByte(invoke_dll.invoke_heif2jpg(tempheicfile, 50, "temp_bitstream.hevc", ref copysize, false));
            exifinfo = invoke_dll.invoke_getexif(tempheicfile, ref copysize);
            heicfile = tempheicfile;
            filename = openthis;
            MainPictureBox.Visible = true;
            DetailedButton.Visible = true;
            SoftwareName.Visible = false;
            拷贝ToolStripMenuItem.Enabled = true;
            另存为ToolStripMenuItem.Enabled = true;
        }

        private void 详细信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var box = new ImageInfo(filename, exifinfo);
                box.ShowDialog();
                DragPicture.Focus();
            }
            catch (Exception) {
                DragPicture.Focus();
            }
        }

        private void HU_DragDrop(object sender, DragEventArgs e)
        {
            var box = new Processing();
            try
            {
                Thread T;
                T = new Thread(new ThreadStart(new Action(() =>
                {
                    box.ShowDialog();
                })));
                T.IsBackground = true;
                T.Start();

                while (!box.IsHandleCreated)
                    Thread.Sleep(100);

                open(((string[])e.Data.GetData(DataFormats.FileDrop, false))[0]);

                box.Invoke(new Action(() =>
                {
                    box.Close();
                }));
                this.Focus();
            }
            catch (Exception)
            {
                box.Invoke(new Action(() =>
                {
                    box.Close();
                }));
                this.Focus();

                var errorbox = new Error();
                errorbox.maintext.Text = "无法打开此文件。";
                errorbox.linklabel.Text = "转到在线帮助";
                errorbox.link = "https://liuziangexit.com/HEIF-Utility/Help/No1.html";
                errorbox.ShowDialog();
            }
        }
        
        private void HU_DragEnter(object sender, DragEventArgs e)
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
            var box = new Batch_Conversion();
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

        private void 在线帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://liuziangexit.com/HEIF-Utility/Help");
            }
            catch (Exception) {
                var box = new ShowLinkCopyable();
                box.link.Text = "https://liuziangexit.com/HEIF-Utility/Help";
                box.ShowDialog();
            }
        }

        private void 拷贝ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetImage(MainPictureBox.Image);
            }
            catch (Exception) { }
        }
        
    }
}
