using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            try
            {
                if (button3.Text == "保存脚本")
                {
                    var SFD = new SaveFileDialog();
                    SFD.Filter = "Windows 命令行脚本|*.bat";
                    SFD.ShowDialog();
                    if (string.IsNullOrEmpty(SFD.FileName))
                        return;
                    File.WriteAllText(SFD.FileName, textBox1.Text, Encoding.Default);
                    Close();
                    MessageBox.Show("脚本已保存到 " + SFD.FileName + "，双击该文件来开始批量转换。");
                    return;
                }
                var box = new Confirm();
                box.Text = "确认";
                box.set_text("源: " + from + "\r\n输出: " + to + "\r\n转换为: " + suffix);
                if (box.ShowDialog() != DialogResult.OK)
                {
                    box.Close();
                    return;
                }

                make_bat();
                box.Close();
                textBox1.Visible = true;
                button3.Text = "保存脚本";
            }
            catch (Exception) { }
        }

        private bool make_bat()
        {
            try
            {
                string bat = "@echo off\r\necho 此文件由 HEIF Utility 生成。\r\necho liuziangexit.com 版权所有。\r\necho.\r\necho.\r\necho 即将开始 HEIF 批量转换。\r\npause\r\n";
                var FileList = Directory.GetFiles(from, "*.*");
                var HUD = Application.StartupPath + "\\HUD.exe\" ";
                var ffmpeg = Application.StartupPath + "\\ffmpeg.exe\" ";
                foreach (var file in FileList)
                {
                    string aFirstName = file.Substring(file.LastIndexOf("\\") + 1, (file.LastIndexOf(".") - file.LastIndexOf("\\") - 1));
                    bat += "\"" + HUD + "\"" + file + "\"" + " " + "\"" + to + "\\" + aFirstName + ".265\"\r\n";
                    bat += "\"" + ffmpeg + "-y -i " + "\"" + to + "\\" + aFirstName + ".265" + "\" " + "\"" + to + "\\" + aFirstName + suffix + "\"\r\n";
                    bat += "del " + "\"" + to + "\\"+ aFirstName + ".265" + "\"\r\n";
                }
                textBox1.Text = bat;
            }
            catch (Exception) {
                return false;
            }
            return true;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            button3.Focus();
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
