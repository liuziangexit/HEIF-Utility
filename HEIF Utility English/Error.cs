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
    public partial class Error : Form
    {
        public string link;

        public Error()
        {
            InitializeComponent();
        }

        private void link_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(link);
            }
            catch (Exception) {
                var box = new ShowLinkCopyable();
                box.link.Text = link;
                box.ShowDialog();
            }
        }
    }
}
