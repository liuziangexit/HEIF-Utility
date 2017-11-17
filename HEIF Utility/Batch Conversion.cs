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
        private volatile int index = 0;
        static readonly object index_locker = new object();

        public Batch_Conversion()
        {
            filelist = new HEIF_Utility.ListViewWithoutScrollBar();
            
            InitializeComponent();

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

            try
            {
                filelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception) { }

            //set line spacing
            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(1, 20);//1-256
            filelist.SmallImageList = imageList;
        }

        private void filelist_add(string str)
        {
            try
            {
                //if (filelist_find(str))
                //    return;
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
        
        private void Batch_Conversion_Resize(object sender, EventArgs e)
        {
            try
            {
                filelist.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            catch (Exception) { }
        }

        private void add_file_Click(object sender, EventArgs e)
        {
            var filepicker = new OpenFileDialog();
            filepicker.Title = "打开";
            filepicker.Multiselect = true;
            filepicker.Filter = "HEIF(.heic)|*.heic|任意文件|*.*";
            filepicker.ShowDialog();
            if (filepicker.FileNames.Length == 0) return;

            var original = this.Text;
            this.Text += " - 正在添加文件";

            filelist.BeginUpdate();
            for (int i = 0; i < filepicker.FileNames.Length; i++)
                filelist_add(filepicker.FileNames[i]);
            filelist.EndUpdate();

            this.Text = original;
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
            var box = new setjpgquality(output_quality);
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
                box.set_text("将 " + filelist.Items.Count + " 张 Apple HEIF 图片转换为 JPEG 格式并保存到文件夹 " + output_folder + " 中。");
                if (box.ShowDialog() != DialogResult.OK)
                    return;

                int ThreadCount = 0;
                try
                {
                    ThreadCount = Environment.ProcessorCount / 2;
                    if (ThreadCount < 1)
                        ThreadCount = 1;
                }
                catch (Exception)
                {
                    ThreadCount = 1;
                }

                set_processing_ui(ThreadCount);
                for (int i = 0; i < ThreadCount; i++)
                {
                    Thread T;
                    T = new Thread(new ParameterizedThreadStart(process));
                    T.IsBackground = true;
                    T.Start(make_temp_filename("batch_temp", i));
                }
                this.isStart = true;
            }
            catch (Exception)
            {
                this.isStart = false;
                MessageBox.Show("发生未知错误。", "无法开始批量转换");
                return;
            }
        }

        private int get_index()
        {
            lock (index_locker)
            {
                int returnthis = index;
                index++;
                return returnthis;
            }
        }

        private string make_temp_filename(string filename, int number)
        {
            return filename + number.ToString();
        }

        private void set_processing_ui(int coreCount)
        {
            Title.Text = "转换中";
            set_output_quality.Visible = start.Visible = set_output_folder.Visible = ClearFileList.Visible = add_file.Visible = pop_file.Visible = false;

            if (coreCount > 1)
                this.Text += " - 已启用" + coreCount.ToString() + "线程";
            else
                this.Text += " - 未启用多线程";

            for (int i = 0; i < filelist.Items.Count; i++)
                filelist.Items[i].BackColor = Color.FromArgb(255, 255, 189, 53);

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

        private void process(object input_temp_filename)
        {
            //获取临时文件名
            string temp_filename;
            try
            {
                temp_filename = input_temp_filename as string;
                if (string.IsNullOrWhiteSpace(temp_filename))
                    return;
            }
            catch (Exception)
            {
                return;
            }
            //拷贝文件列表
            string[] list_copy = new string[filelist.Items.Count];
            this.Invoke(new Action(() =>
            {
                for (int i = 0; i < filelist.Items.Count; i++)
                {
                    list_copy[i] = filelist.Items[i].Text;
                }
            }));

            //开始
            try
            {
                try
                {
                    int index_while = get_index();
                    while (index_while < filelist.Items.Count)
                    {
                        try
                        {
                            var heif_data = invoke_dll.read_heif(list_copy[index_while]);
                            int copysize = 0;
                            invoke_dll.invoke_heif2jpg(heif_data, this.output_quality, temp_filename, ref copysize).Save(this.output_folder + "\\" + make_output_filename(list_copy[index_while]));

                            this.Invoke(new Action(() =>
                            {
                                filelist.Items[index_while].BackColor = Color.FromArgb(255, 0, 204, 75);
                                if (MainPrograssBar.Value < ((float)(index_while + 1) / (float)list_copy.Length) * 100)
                                    MainPrograssBar.Value = (int)(((float)(index_while + 1) / (float)list_copy.Length) * 100);
                            }));
                        }
                        catch (Exception ex)
                        {
                            this.Invoke(new Action(() =>
                            {
                                filelist.Items[index_while].BackColor = Color.FromArgb(255, 229, 100, 90);
                                if (MainPrograssBar.Value < ((float)(index_while + 1) / (float)list_copy.Length) * 100)
                                    MainPrograssBar.Value = (int)(((float)(index_while + 1) / (float)list_copy.Length) * 100);
                            }));
                        }
                        index_while = get_index();
                        if (isStart != true)
                            return;
                    }
                    this.isStart = false;
                    this.Invoke(new Action(() =>
                    {
                        this.Title.Text = "已完成批量转换";
                        MainPrograssBar.Value = MainPrograssBar.Maximum;
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
                                MainPrograssBar.Value = MainPrograssBar.Maximum;
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
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
