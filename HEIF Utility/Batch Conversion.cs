using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HEIF_Utility
{
    public partial class Batch_Conversion : Form
    {
        private ListViewWithoutScrollBar filelist;
        private string output_folder;
        private int output_quality = 50;
        private ProgressBar MainPrograssBar;
        private volatile bool isStart = false;
        private Thread process_thread;

        public Batch_Conversion()
        {
            InitializeComponent();

            filelist = new HEIF_Utility.Batch_Conversion.ListViewWithoutScrollBar();
            FilelistPanel.Controls.Add(filelist);
            
            //set filelist property
            filelist.Dock = DockStyle.Fill;
            filelist.AllowColumnReorder = false;
            filelist.Columns.Add("filename", 60);
            filelist.HeaderStyle = ColumnHeaderStyle.None;
            filelist.MultiSelect = true;
            filelist.TabStop = false;
            filelist.View = View.Details;
            filelist.GridLines = true;
            filelist.FullRowSelect = true;

            //set line spacing
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(1, 20);//1-256
            filelist.SmallImageList = imageList;
        }

        private void filelist_add(string str)
        {
            try
            {
                if (filelist_find(str))
                    return;
                filelist.Items.Add(str);
            }
            catch (Exception) { }
        }

        private bool filelist_find(string text)
        {
            try
            {
                for (int i = 0; i < filelist.Items.Count; i++)
                    if (filelist.Items[i].Text == text)
                        return true;
            }
            catch (Exception) { }
            return false;
        }

        private void filelist_erase(ListViewItem item)
        {
            try
            {
                filelist.Items.Remove(item);
            }
            catch (Exception) { }
        }

        private void filelist_clear()
        {
            try
            {
                filelist.Items.Clear();
            }
            catch (Exception) { }
        }

        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            start.Focus();
        }

        private void Batch_Conversion_Load(object sender, EventArgs e)
        {
            filelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void Batch_Conversion_Resize(object sender, EventArgs e)
        {
            filelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private class ListViewWithoutScrollBar : ListView
        {
            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case 0x83: // WM_NCCALCSIZE
                        int style = (int)GetWindowLong(this.Handle, GWL_STYLE);
                        if ((style & WS_HSCROLL) == WS_HSCROLL)
                            SetWindowLong(this.Handle, GWL_STYLE, style & ~WS_HSCROLL);
                        base.WndProc(ref m);
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            const int GWL_STYLE = -16;
            const int WS_HSCROLL = 0x00100000;

            public static int GetWindowLong(IntPtr hWnd, int nIndex)
            {
                if (IntPtr.Size == 4)
                    return (int)GetWindowLong32(hWnd, nIndex);
                else
                    return (int)(long)GetWindowLongPtr64(hWnd, nIndex);
            }

            public static int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong)
            {
                if (IntPtr.Size == 4)
                    return (int)SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
                else
                    return (int)(long)SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            }

            [DllImport("user32.dll", EntryPoint = "GetWindowLong", CharSet = CharSet.Auto)]
            public static extern IntPtr GetWindowLong32(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", CharSet = CharSet.Auto)]
            public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll", EntryPoint = "SetWindowLong", CharSet = CharSet.Auto)]
            public static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, int nIndex, int dwNewLong);

            [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", CharSet = CharSet.Auto)]
            public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, int dwNewLong);
        }

        private void add_file_Click(object sender, EventArgs e)
        {
            var filepicker = new OpenFileDialog();
            filepicker.Title = "打开";
            filepicker.Multiselect = true;
            filepicker.Filter = "HEIF(.heic)|*.heic|任意文件|*.*";
            filepicker.ShowDialog();
            if (filepicker.FileNames.Length == 0) return;

            for (int i = 0; i < filepicker.FileNames.Length; i++)
                filelist_add(filepicker.FileNames[i]);
        }

        private void pop_file_Click(object sender, EventArgs e)
        {
            while (true)
            {
                if (filelist.SelectedItems.Count == 0) break;
                filelist_erase(filelist.SelectedItems[0]);
            }
        }

        private void ClearFileList_Click(object sender, EventArgs e)
        {
            filelist_clear();
        }

        private void set_output_folder_Click(object sender, EventArgs e)
        {
            var filepicker = new FolderBrowserDialog();
            filepicker.ShowDialog();
            if (string.IsNullOrEmpty(filepicker.SelectedPath))
                return;
            output_folder = filepicker.SelectedPath;
            set_output_folder.ForeColor = Color.ForestGreen;
        }

        private void set_output_quality_Click(object sender, EventArgs e)
        {
            var box = new setjpgquality();
            box.ShowDialog();
            output_quality = box.value;
        }

        private void start_Click(object sender, EventArgs e)
        {
            try
            {

                if (output_folder == null || output_folder == "")
                {
                    MessageBox.Show("未设置输出目录。", "无法开始批量转换");
                    return;
                }
                if (filelist.Items.Count == 0)
                {
                    MessageBox.Show("未选择要转换的文件。", "无法开始批量转换");
                    return;
                }

                var box = new Confirm();
                box.Text = "确认开始批量转换";
                box.set_text("将 " + filelist.Items.Count + " 张 Apple HEIF 图片转换为 JPG 格式并保存到文件夹 " + output_folder + " 中。");
                if (box.ShowDialog() != DialogResult.OK)
                    return;


                set_processing_ui();
                Thread T;
                T = new Thread(new ThreadStart(new Action(() =>
                {
                    process();
                })));
                T.IsBackground = true;
                process_thread = T;
                this.isStart = true;
                T.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("发生未知错误。", "无法开始批量转换");
                return;
            }
        }

        private void set_processing_ui()
        {
            Title.Text = "转换中";
            set_output_quality.Visible = start.Visible = set_output_folder.Visible = ClearFileList.Visible = add_file.Visible = pop_file.Visible = false;

            for (int i = 0; i < filelist.Items.Count; i++)
                filelist.Items[i].BackColor = Color.Orange;

            var prograssbar = new ProgressBar();
            prograssbar.Maximum = 100;
            prograssbar.Minimum = 0;
            prograssbar.Step = 10;
            prograssbar.Style = ProgressBarStyle.Continuous;
            prograssbar.Width = FilelistPanel.Width;
            prograssbar.Height = 25;
            prograssbar.Location = new Point(FilelistPanel.Location.X, FilelistPanel.Location.Y + FilelistPanel.Size.Height + 11);
            prograssbar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MainPrograssBar = prograssbar;
            this.Controls.Add(prograssbar);
        }

        private string remove_path(string filename)
        {
            try
            {
                if (filename.LastIndexOf('\\') == -1)
                    return filename;

                return filename.Substring(filename.LastIndexOf('\\') + 1);
            }
            catch (Exception)
            {
                return filename;
            }
        }

        private string make_output_filename(string filename_heic)
        {
            try
            {
                var removed_path_filename = remove_path(filename_heic);

                if (-1 == removed_path_filename.IndexOf('.'))
                    return removed_path_filename + ".jpg";

                if (removed_path_filename.Length - 1 == removed_path_filename.LastIndexOf('.'))
                    return removed_path_filename + ".jpg";

                return removed_path_filename.Substring(0, removed_path_filename.Length - (removed_path_filename.Length - removed_path_filename.LastIndexOf('.'))) + ".jpg";
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void HandleFormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            if (!isStart)
                return;
            var box = new Confirm();
            box.Text = "确认终止批量转换";
            box.set_text("关闭此窗口将会终止批量转换。");
            if (box.ShowDialog() != DialogResult.OK)
            {
                e.Cancel = true;
                return;
            }
        }

        private void process()
        {
            try
            {
                string[] list_copy = new string[filelist.Items.Count];
                this.Invoke(new Action(() =>
                {
                    for (int i = 0; i < filelist.Items.Count; i++)
                    {
                        list_copy[i] = filelist.Items[i].Text;
                    }
                }));

                for (int i = 0; i < list_copy.Length; i++)
                {
                    try
                    {
                        var heif_data = invoke_dll.read_heif(list_copy[i]);
                        invoke_dll.invoke_heif_to_jpg(heif_data, this.output_quality).Save(this.output_folder + "\\" + make_output_filename(list_copy[i]));

                        this.Invoke(new Action(() =>
                        {
                            filelist.Items[i].BackColor = Color.ForestGreen;
                            if (MainPrograssBar.Value < ((float)(i + 1) / (float)list_copy.Length) * 100)
                                MainPrograssBar.Value = (int)(((float)(i + 1) / (float)list_copy.Length) * 100);
                        }));
                    }
                    catch (Exception ex)
                    {
                        this.Invoke(new Action(() =>
                        {
                            filelist.Items[i].BackColor = Color.DarkRed;
                            if (MainPrograssBar.Value < ((float)(i + 1) / (float)list_copy.Length) * 100)
                                MainPrograssBar.Value = (int)(((float)(i + 1) / (float)list_copy.Length) * 100);
                        }));
                    }
                }
                this.isStart = false;
                this.Invoke(new Action(() =>
                {
                    this.Title.Text = "已完成批量转换";
                }));
            }
            catch (Exception)
            {
                try
                {
                    this.isStart = false;
                    this.Invoke(new Action(() =>
                    {                        
                        try
                        {
                            Title.Text = "无法完成批量转换";
                            for (int i = 0; i < filelist.Items.Count; i++)
                            {
                                if (filelist.Items[i].BackColor != Color.ForestGreen)
                                    filelist.Items[i].BackColor = Color.DarkRed;
                            }
                        }
                        catch (Exception) { }
                    }));
                }
                catch (Exception) { }
                return;
            }
        }
    }
}
