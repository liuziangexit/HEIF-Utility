using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HEIF_Utility
{
    public partial class ImageInfo : Form
    {        
        private class ExifInfo
        {
            public string Datetime { get; set; }
            public string Software { get; set; }
            public string Make { get; set; }
            public string Model { get; set; }
            public string Camera { get; set; }            
            public double FocalLength { get; set; }
            public int FocalLengthIn35mm { get; set; }
            public double ExposureTime { get; set; }
            public double FNumber { get; set; }
            public int ISOSpeedRatings { get; set; }
            public int ExposureProgram { get; set; }
            public bool Flash { get; set; }
            public int ImageHeight { get; set; }
            public int ImageWidth { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
            public double Altitude { get; set; }
            public double DOP { get; set; }
        }

        private string filename, exifinfo_raw;

        public ImageInfo(string inputfilename, string inputexifinfo_raw)
        {
            InitializeComponent();

            filename = inputfilename;
            exifinfo_raw = inputexifinfo_raw;
        }

        private void ImageInfo_Shown(object sender, EventArgs e)
        {
            loadimageinfo();
        }

        void loadimageinfo()
        {
            {
                var fi = new FileInfo(filename);
                fi.OpenRead();
                dataGridView1.Rows.Add("File Name", fi.Name);
                dataGridView1.Rows.Add("File Size", fi.Length.ToString() + " Byte(s)");                
            }

            ExifInfo exifinfo = null;

            try
            {
                exifinfo = JsonConvert.DeserializeObject<ExifInfo>(exifinfo_raw);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Shooting Date", exifinfo.Datetime);
            }
            catch (Exception) { }
            
            try
            {
                dataGridView1.Rows.Add("Image Size", exifinfo.ImageWidth.ToString() + "x" + exifinfo.ImageHeight.ToString());
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Manufacturer", exifinfo.Make);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Device", exifinfo.Model);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Lens", exifinfo.Camera);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Software", exifinfo.Software);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Aperture", "f/" + exifinfo.FNumber);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Focal Length", exifinfo.FocalLength + " mm");
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("35mm Focal Length", exifinfo.FocalLengthIn35mm + " mm");
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Exposure Time", "1/" + ((int)((double)1 / exifinfo.ExposureTime)).ToString()+ " Second(s)");
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("ISO", exifinfo.ISOSpeedRatings);
            }
            catch (Exception) { }

            try
            {
                switch (exifinfo.ExposureProgram)
                {
                    case 0:
                        dataGridView1.Rows.Add("Exposure Program", "Undefined");
                        break;
                    case 1:
                        dataGridView1.Rows.Add("Exposure Program", "Manual");
                        break;
                    case 2:
                        dataGridView1.Rows.Add("Exposure Program", "Auto");
                        break;
                    case 3:
                        dataGridView1.Rows.Add("Exposure Program", "Aperture Priority");
                        break;
                    case 4:
                        dataGridView1.Rows.Add("Exposure Program", "Shutter Priority");
                        break;
                    case 5:
                        dataGridView1.Rows.Add("Exposure Program", "Creative Program");
                        break;
                    case 6:
                        dataGridView1.Rows.Add("Exposure Program", "Action Program");
                        break;
                    case 7:
                        dataGridView1.Rows.Add("Exposure Program", "Portrait Mode");
                        break;
                    case 8:
                        dataGridView1.Rows.Add("Exposure Program", "Landscape Mode");
                        break;
                    default:
                        dataGridView1.Rows.Add("Exposure Program", "Unknown");
                        break;
                }
            }
            catch (Exception) { }

            try
            {
                if (exifinfo.Flash)
                    dataGridView1.Rows.Add("Flash", "On");
                else
                    dataGridView1.Rows.Add("Flash", "Off");
            }
            catch (Exception) { }
            
            try
            {
                dataGridView1.Rows.Add("Latitude", exifinfo.Latitude);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Longitude", exifinfo.Longitude);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("Altitude", exifinfo.Altitude + " Meter(s)");
            }
            catch (Exception) { }
        }
    }
}