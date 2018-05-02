using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace HEIF_Utility_HiDPI
{
    class invoke_dll
    {
        [DllImport("HUD.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private unsafe extern static void heif2jpg(byte* heif_bin, int input_buffer_size, int jpg_quality, byte* ouput_buffer, int output_buffer_size, byte* temp_filename, int* copysize, bool include_exif, bool color_profile, byte* icc_bin, int icc_size);

        [DllImport("HUD.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private unsafe extern static void getexif(byte* heif_bin, int input_buffer_size, byte* ouput_buffer, int output_buffer_size, int* copysize);

        static byte[] DisplayP3Profile = null;

        public static bool read_profile()
        {
            try
            {
                DisplayP3Profile = File.ReadAllBytes("icc-profile/Display P3.icc");
            }
            catch (Exception)
            {
                DisplayP3Profile = new byte[] { 0 };
                return false;
            }
            return true;
        }

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

        public static unsafe byte[] invoke_heif2jpg(byte[] heif_bin, int jpg_quality, string temp_filename, ref int copysize, bool include_exif, bool color_profile)
        {
            if (color_profile == true && DisplayP3Profile.Length == 1)
                throw new Exception();//没有ICC却指定要写入ICC

            var output_buffer = new byte[heif_bin.Length * 10];
            byte[] temp_filename_byte_array = System.Text.Encoding.Default.GetBytes(temp_filename);
            int[] copysize_array = new int[1] { 0 };
            fixed (byte* input = &heif_bin[0], output = &output_buffer[0], temp_filename_byte = &temp_filename_byte_array[0], icc_ptr = &DisplayP3Profile[0])
            fixed (int* copysize_p = &copysize_array[0])
            {
                heif2jpg(input, heif_bin.Length, jpg_quality, output, output_buffer.Length, temp_filename_byte, copysize_p, include_exif, color_profile, icc_ptr, DisplayP3Profile.Length);
            }
            copysize = copysize_array[0];
            return output_buffer;
        }

        public static unsafe string invoke_getexif(byte[] heif_bin, ref int copysize)
        {
            var output_buffer = new byte[65535];
            int[] copysize_array = new int[1] { 0 };
            fixed (byte* input = &heif_bin[0], output = &output_buffer[0])
            fixed (int* copysize_p = &copysize_array[0])
            {
                getexif(input, heif_bin.Length, output, output_buffer.Length, copysize_p);
            }
            copysize = copysize_array[0];
            return Encoding.Default.GetString(output_buffer, 0, copysize);
        }
    }
}