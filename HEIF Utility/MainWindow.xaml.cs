using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HEIF_Utility_HiDPI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string RunningLanguage;
        public static string language_conf = "conf/Language";

        public MainWindow()
        {
            ChangeLanguage(GetSystemLanguage());
            InitializeComponent();
            ChangeLanguageStatus();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            IconRemover.RemoveIcon(this);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;
            IconRemover.SetWindowLong(hwnd, IconRemover.GWL_STYLE, IconRemover.GetWindowLong(hwnd, IconRemover.GWL_STYLE) & ~IconRemover.WS_SYSMENU);
        }

        private static ResourceDictionary GetLanguage(string Language)
        {
            if (Language == "zh-CN")
                return new ResourceDictionary() { Source = new Uri("lang_zh-CHS.xaml", UriKind.RelativeOrAbsolute) };
            else
               if (Language.IndexOf("zh") != -1)
                return new ResourceDictionary() { Source = new Uri("lang_zh-CHT.xaml", UriKind.RelativeOrAbsolute) };
            else
                return new ResourceDictionary() { Source = new Uri("lang_en-US.xaml", UriKind.RelativeOrAbsolute) };
        }

        private static ResourceDictionary GetSystemLanguage()
        {
            string SystemLanguage = null;
            try
            {
                SystemLanguage = File.ReadAllText(language_conf);
            }
            catch (Exception)
            {
                SystemLanguage = System.Globalization.CultureInfo.InstalledUICulture.Name;
            }
            return GetLanguage(SystemLanguage);
        }

        private void ChangeLanguage(ResourceDictionary new_lang)
        {
            App.Current.Resources.MergedDictionaries.Remove(App.Current.Resources.MergedDictionaries.ToList().Find((ResourceDictionary rd) =>
              {
                  if (rd.Source.ToString().IndexOf("lang_") != -1)
                      return true;
                  return false;
              }));
            App.Current.Resources.MergedDictionaries.Add(new_lang);
            RunningLanguage = new_lang.Source.ToString();
        }

        private void ChangeLanguageStatus()
        {
            enUS.IsChecked = zhCHS.IsChecked = zhCHT.IsChecked = false;
            switch (RunningLanguage)
            {
                case "lang_en-US.xaml": enUS.IsChecked = true; break;
                case "lang_zh-CHS.xaml": zhCHS.IsChecked = true; break;
                case "lang_zh-CHT.xaml": zhCHT.IsChecked = true; ; break;
            }
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("打开");
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("保存");
        }

        private void Copy_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("复制");
        }
        
        private void StartOnlineSupport(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("在线支持");
        }

        private void StartAbout(object sender, RoutedEventArgs e)
        {
            var box = new About();
            box.ShowDialog();
        }

        private void lang_Click(object sender, RoutedEventArgs e)
        {
            switch (((MenuItem)sender).Name)
            {
                case "enUS":
                    {
                        ChangeLanguage(GetLanguage("en-US"));
                        try
                        {
                            File.WriteAllText(language_conf, "en-US");
                        }
                        catch (Exception) { }
                    }
                    break;
                case "zhCHS":
                    {
                        ChangeLanguage(GetLanguage("zh-CN"));
                        try
                        {
                            File.WriteAllText(language_conf, "zh-CN");
                        }
                        catch (Exception) { }
                    }
                    break;
                case "zhCHT":
                    {
                        ChangeLanguage(GetLanguage("zh-Tr"));
                        try
                        {
                            File.WriteAllText(language_conf, "zh-Tr");
                        }
                        catch (Exception) { }
                    }
                    break;
            }
            ChangeLanguageStatus();
        }

        private void use_sys_lang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                File.Delete(language_conf);
            }
            catch (Exception) { }
            ChangeLanguage(GetSystemLanguage());
            ChangeLanguageStatus();
        }
        
    }
}
