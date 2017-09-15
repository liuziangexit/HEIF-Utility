using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HEIF_Utility
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                System.IO.File.Delete(Application.StartupPath + "//peek.jpg");
            }
            catch (Exception)
            { }
            try
            {
                System.IO.File.Delete(Application.StartupPath + "//out.265");
            }
            catch (Exception)
            { }

            var s = new System.Drawing.Size();

            try
            {
                var fs = new FileStream(Application.StartupPath + "//MainWindowSize", FileMode.Open);
                var sr = new StreamReader(fs);
                s.Width = int.Parse(sr.ReadLine());
                s.Height = int.Parse(sr.ReadLine());
                sr.Close();
                if (s.Width <= 0 || s.Height <= 0)
                    throw new Exception();
            }
            catch (Exception)
            {
                s.Width = s.Height = 0;
            }

            //if (args.Length != 0)
            //   Application.Run(new MainWindow(args[0], s));
            //else
                Application.Run(new MainWindow(s));
            try
            {
                System.IO.File.Delete(Application.StartupPath + "//peek.jpg");
            }
            catch (Exception)
            { }
            try
            {
                System.IO.File.Delete(Application.StartupPath + "//out.265");
            }
            catch (Exception)
            { }
        }
    }
}
