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
    public partial class setjpgquality : Form
    {
        public int value = 50;
        public bool includes_exif = true;

        public setjpgquality()
        {
            InitializeComponent();
        }

        public setjpgquality(int setvalue)
        {
            InitializeComponent();

            value = setvalue;
            label1.Text = value.ToString();
            trackBar1.Value = setvalue;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            label1.Text = trackBar1.Value.ToString();
            value = trackBar1.Value;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            includes_exif = checkBox1.Checked;
        }
    }
}
