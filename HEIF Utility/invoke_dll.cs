using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HEIF_Utility
{
    class invoke_dll
    {

        [DllImport("HUD.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private unsafe extern static void heif2jpg(byte* heif_bin, int input_buffer_size, int jpg_quality, byte* ouput_buffer, int output_buffer_size);

        public static byte[] read_heif(string filename)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(filename, FileMode.Open);
                System.IO.BinaryReader br = new BinaryReader(fs);
                byte[] byte_array = br.ReadBytes((int)fs.Length);
                fs.Close();
                return byte_array;
            }
            catch (Exception ex)
            {
                fs.Close();
                throw ex;
            }
        }

        public static unsafe Image invoke_heif_to_jpg(byte[] heif_bin, int jpg_quality)
        {
            var output_buffer = new byte[heif_bin.Length * 10];
            fixed (byte* input = &(heif_bin[0]), output = (&output_buffer[0]))
            {
                heif2jpg(input, heif_bin.Length, jpg_quality, output, heif_bin.Length * 10);
            }
            return Image.FromStream(new MemoryStream(output_buffer));
        }
    }
}
