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
                dataGridView1.Rows.Add("文件名", fi.Name);
                dataGridView1.Rows.Add("文件大小", fi.Length.ToString() + "字节");                
            }

            ExifInfo exifinfo = null;

            try
            {
                exifinfo = JsonConvert.DeserializeObject<ExifInfo>(exifinfo_raw);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("拍摄日期", exifinfo.Datetime);
            }
            catch (Exception) { }
            
            try
            {
                dataGridView1.Rows.Add("图像大小", exifinfo.ImageWidth.ToString() + "x" + exifinfo.ImageHeight.ToString());
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("制造商", exifinfo.Make);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("设备", exifinfo.Model);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("镜头", exifinfo.Camera);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("软件", exifinfo.Software);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("光圈", "f/" + exifinfo.FNumber);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("焦距", exifinfo.FocalLength + "毫米");
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("35毫米焦距", exifinfo.FocalLengthIn35mm + "毫米");
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("曝光时间", "1/" + ((int)((double)1 / exifinfo.ExposureTime)).ToString()+"秒");
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("感光度", exifinfo.ISOSpeedRatings);
            }
            catch (Exception) { }

            try
            {
                switch (exifinfo.ExposureProgram)
                {
                    case 0:
                        dataGridView1.Rows.Add("曝光程序", "未定义");
                        break;
                    case 1:
                        dataGridView1.Rows.Add("曝光程序", "手动");
                        break;
                    case 2:
                        dataGridView1.Rows.Add("曝光程序", "自动");
                        break;
                    case 3:
                        dataGridView1.Rows.Add("曝光程序", "光圈优先");
                        break;
                    case 4:
                        dataGridView1.Rows.Add("曝光程序", "快门优先");
                        break;
                    case 5:
                        dataGridView1.Rows.Add("曝光程序", "Creative Program");
                        break;
                    case 6:
                        dataGridView1.Rows.Add("曝光程序", "Action Program");
                        break;
                    case 7:
                        dataGridView1.Rows.Add("曝光程序", "人像模式");
                        break;
                    case 8:
                        dataGridView1.Rows.Add("曝光程序", "Landscape Mode");
                        break;
                    default:
                        dataGridView1.Rows.Add("曝光程序", "未知");
                        break;
                }
            }
            catch (Exception) { }

            try
            {
                if (exifinfo.Flash)
                    dataGridView1.Rows.Add("闪光灯", "开启");
                else
                    dataGridView1.Rows.Add("闪光灯", "关闭");
            }
            catch (Exception) { }
            
            try
            {
                dataGridView1.Rows.Add("纬度", exifinfo.Latitude);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("经度", exifinfo.Longitude);
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("海拔高度", exifinfo.Latitude+"米");
            }
            catch (Exception) { }

            try
            {
                dataGridView1.Rows.Add("定位误差", exifinfo.DOP.ToString() + " DOP");
            }
            catch (Exception) { }
        }
    }
}