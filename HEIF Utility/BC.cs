using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HEIF_Utility
{
    public partial class BC : Form
    {
        private string from, to, suffix;

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                suffix = ".jpg";
                label3.ForeColor = Color.ForestGreen;
            }
            enable_next();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                suffix = ".png";
                label3.ForeColor = Color.ForestGreen;
            }
            enable_next();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var filepicker = new FolderBrowserDialog();
            filepicker.ShowDialog();
            if (string.IsNullOrEmpty(filepicker.SelectedPath))
                return;
            from = filepicker.SelectedPath;
            label1.ForeColor = Color.ForestGreen;
            enable_next();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var filepicker = new FolderBrowserDialog();
            filepicker.ShowDialog();
            if (string.IsNullOrEmpty(filepicker.SelectedPath))
                return;
            to = filepicker.SelectedPath;
            label2.ForeColor = Color.ForestGreen;
            enable_next();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var box = new Confirm();
            box.Text = "确认";
            box.set_text("源文件夹: " + from + "\r\n输出文件夹: " + to + "\r\n转换为: " + suffix);
            if (box.ShowDialog() != DialogResult.OK)
            {
                box.Close();
                return;
            }
            box.Close();
            MessageBox.Show("已确定。");
        }

        private void enable_next()
        {
            if (!string.IsNullOrEmpty(from) && !string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(suffix))
                button3.Enabled = true;
            else
                button3.Enabled = false;
        }

        public BC()
        {
            InitializeComponent();
        }
    }
}
