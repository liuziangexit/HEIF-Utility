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
    public partial class Confirm : Form
    {
        public Confirm()
        {
            InitializeComponent();
        }

        public void set_text(string str)
        {
            label1.Text = str;
        }
    }
}
