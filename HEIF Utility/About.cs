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
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            var box = new People();
            box.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            var box = new sc("https://github.com/liuziangexit/HEIF-Utility");
            box.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            var box = new LICENSE();
            box.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://liuziangexit.com/HEIF-Utility");
            }
            catch (Exception) {
                var box = new ShowLinkCopyable();
                box.link.Text = "https://liuziangexit.com/HEIF-Utility";
                box.ShowDialog();
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            var box = new sc("邮箱: liuziang@liuziangexit.com");
            box.ShowDialog();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            var box = new pb();
            box.ShowDialog();
        }
    }
}
