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
        private string filename;
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                box.Invoke(new Action(() =>
                {
                    box.Close();
                }));
                this.Focus();
                MessageBox.Show("Unable to open this file.");
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
                if (!System.IO.File.Exists(Application.StartupPath + "\\HUD.dll") || !System.IO.File.Exists(Application.StartupPath + "\\opencv_ffmpeg330_64.dll") || !System.IO.File.Exists(Application.StartupPath + "\\opencv_world330.dll"))
                {
                    MessageBox.Show("The core component is missing, HEIF Utility can not start.");
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
                filepicker.Title = "Open";
                filepicker.Multiselect = false;
                filepicker.Filter = "HEIF(.heic)|*.heic|Any File|*.*";
                filepicker.ShowDialog();
                if (filepicker.FileName == "") return;
                filename = filepicker.FileName;

                
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
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                box.Invoke(new Action(() =>
                {
                    box.Close();
                }));
                this.Focus();
                MessageBox.Show("Unable to open this file.");
                return;
            }
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var box2 = new Processing();
            try
            {
                var box = new SaveFileDialog();
                box.Title = "Save";
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

                try
                {
                    invoke_dll.invoke_heif_to_jpg(heicfile, sq.value, "temp_bitstream.hevc").Save(box.FileName, ImageFormat.Jpeg);
                }
                catch (Exception)
                {
                    MessageBox.Show("Can not save to " + box.FileName);
                    return;
                }

                box2.Invoke(new Action(() =>
                {
                    box2.Close();
                }));
                this.Focus();

                MessageBox.Show("Successfully saved to " + box.FileName);
                return;
            }
            catch (Exception ex)
            {
                box2.Invoke(new Action(() =>
                {
                    box2.Close();
                }));
                this.Focus();
                MessageBox.Show(ex.Message);
                return;
            }
        }

        void open(string openthis)
        {
            if (openthis == "") return;
            filename = openthis;
            heicfile = invoke_dll.read_heif(openthis);
            MainPictureBox.Image = invoke_dll.invoke_heif_to_jpg(heicfile, 50, "temp_bitstream.hevc");
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
                var fi = new FileInfo(filename);
                fi.OpenRead();

                MessageBox.Show("filename: " + fi.Name + "\r\ndate: " + fi.CreationTime.ToLongDateString() + " " + fi.CreationTime.ToLongTimeString() +
                    "\r\nsize: " + fi.Length.ToString() + " byte\r\nresolution: " + MainPictureBox.Image.Width + "x" + MainPictureBox.Image.Height);
                

                DragPicture.Focus();
            }
            catch (Exception) {
                DragPicture.Focus();
            }
        }

        private void HU_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var box = new Processing();
                Thread T;
                T = new Thread(new ThreadStart(new Action(() =>
                {
                    box.ShowDialog();
                })));
                T.IsBackground = true;
                T.Start();

                open(((string[])e.Data.GetData(DataFormats.FileDrop, false))[0]);

                box.Invoke(new Action(() =>
                {
                    box.Close();
                }));
                this.Focus();
            }
            catch (Exception) { }
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
