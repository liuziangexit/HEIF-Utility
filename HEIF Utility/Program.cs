using System;
using System.Collections.Generic;
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
                System.IO.File.Delete("src.heic");
            }
            catch (Exception)
            { }
            try
            {
                System.IO.File.Delete("peek.jpg");
            }
            catch (Exception)
            { }
            try
            {
                System.IO.File.Delete("out.265");
            }
            catch (Exception)
            { }
            if (args.Length != 0)
                Application.Run(new MainWindow(args[0]));
            else
                Application.Run(new MainWindow());
            try
            {
                System.IO.File.Delete("src.heic");
            }
            catch (Exception)
            { }
            try
            {
                System.IO.File.Delete("peek.jpg");
            }
            catch (Exception)
            { }
            try
            {
                System.IO.File.Delete("out.265");
            }
            catch (Exception)
            { }
        }
    }
}
