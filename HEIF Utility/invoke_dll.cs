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
        private unsafe extern static void heif2jpg(byte* heif_bin, int input_buffer_size, int jpg_quality, byte* ouput_buffer, int output_buffer_size, byte* temp_filename, int* copysize);

        [DllImport("HUD.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private unsafe extern static void getexif(byte* heif_bin, int input_buffer_size, byte* ouput_buffer, int output_buffer_size, int* copysize);

        [DllImport("HUWED.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Cdecl)]
        private unsafe extern static void write_exif(byte* image_data, int image_data_size, byte* exif_info, int exif_info_size, byte* output_filename, int output_filename_size, bool* result);

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

        public static unsafe byte[] invoke_heif2jpg_to_byte(byte[] heif_bin, int jpg_quality, string temp_filename, ref int copysize)
        {
            var output_buffer = new byte[heif_bin.Length * 10];
            byte[] temp_filename_byte_array = System.Text.Encoding.Default.GetBytes(temp_filename);
            int[] copysize_array = new int[1] { 0 };
            fixed (byte* input = &heif_bin[0], output = &output_buffer[0], temp_filename_byte = &temp_filename_byte_array[0])
            fixed (int* copysize_p = &copysize_array[0])
            {
                heif2jpg(input, heif_bin.Length, jpg_quality, output, output_buffer.Length, temp_filename_byte, copysize_p);
            }
            copysize = copysize_array[0];
            return output_buffer;
        }

        public static unsafe Image invoke_heif2jpg(byte[] heif_bin, int jpg_quality, string temp_filename, ref int copysize)
        {
            var output_buffer = new byte[heif_bin.Length * 10];
            byte[] temp_filename_byte_array = System.Text.Encoding.Default.GetBytes(temp_filename);
            int[] copysize_array = new int[1] { 0 };
            fixed (byte* input = &heif_bin[0], output = &output_buffer[0], temp_filename_byte = &temp_filename_byte_array[0])
            fixed (int* copysize_p = &copysize_array[0])
            {
                heif2jpg(input, heif_bin.Length, jpg_quality, output, output_buffer.Length, temp_filename_byte, copysize_p);
            }
            copysize = copysize_array[0];
            return Image.FromStream(new MemoryStream(output_buffer));
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

        public static unsafe bool invoke_write_exif(byte[] image_data, byte[] exif_info, byte[] output_filename)
        {
            bool[] result = new bool[1] { false };
            fixed (byte* input_image_data = &image_data[0], input_exif_info = &exif_info[0], input_output_filename = &output_filename[0])
            fixed (bool* result_p = &result[0])
            {
                write_exif(input_image_data, image_data.Length, input_exif_info, exif_info.Length, input_output_filename, output_filename.Length, result_p);
            }
            return result[0];
        }
    }
}
