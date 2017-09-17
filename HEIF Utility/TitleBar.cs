using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HEIF_Utility
{
    public partial class TitleBar : UserControl
    {
        public delegate void ClickHandler(object sender, TitleClickArgs e);
        public event ClickHandler OnClickExitButton;

        public TitleBar()
        {
            InitializeComponent();
        }

        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            button1.BackgroundImage = global::HEIF_Utility.Properties.Resources.CloseWhite;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackgroundImage = global::HEIF_Utility.Properties.Resources.Close;
            this.FocusLabel.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FocusLabel.Focus();
            OnClickExitButton(this, new HEIF_Utility.TitleClickArgs() { which = 1 });
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.BackgroundImage = global::HEIF_Utility.Properties.Resources.MaximizeWhite;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.BackgroundImage = global::HEIF_Utility.Properties.Resources.Maximize;
            this.FocusLabel.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FocusLabel.Focus();
            OnClickExitButton(this, new HEIF_Utility.TitleClickArgs() { which = 2 });
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {
            button3.BackgroundImage = global::HEIF_Utility.Properties.Resources.MinimizeWhite;
        }

        private void button3_MouseLeave(object sender, EventArgs e)
        {
            button3.BackgroundImage = global::HEIF_Utility.Properties.Resources.Minimize;
            this.FocusLabel.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FocusLabel.Focus();
            OnClickExitButton(this, new HEIF_Utility.TitleClickArgs() { which = 3 });
        }
    }

    public class TitleClickArgs : EventArgs
    {
        public int which = 0;//1close,2maximize,3minimize
    }
}
