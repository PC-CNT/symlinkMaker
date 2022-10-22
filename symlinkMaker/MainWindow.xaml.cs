using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.IO;


namespace symlinkMaker
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private string path_src, path_dst;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Open_src(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();



            dialog.FileName = "File";

            dialog.CheckFileExists = false;

            if (dialog.ShowDialog() == true)
            {
                if (System.IO.File.Exists(dialog.FileName))
                {
                    path_src = dialog.FileName;
                }
                else
                {
                    path_src = System.IO.Path.GetDirectoryName(dialog.FileName);
                }


                this.textBox_src.Text = path_src;
            }
        }


        private void Button_Open_dst(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();

            dialog.FileName = "File";

            dialog.CheckFileExists = false;

            if (dialog.ShowDialog() == true)
            {
                if (System.IO.File.Exists(dialog.FileName))
                {
                    path_dst = dialog.FileName;
                }
                else
                {
                    path_dst = System.IO.Path.GetDirectoryName(dialog.FileName);
                }


                this.textBox_dst.Text = path_dst;
            }

        }


        private void textBox_src_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.All;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void textBox_dst_DragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }



        private void textBox_src_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                this.Content = string.Empty;

                string[] filenames = (string[])e.Data.GetData(DataFormats.FileDrop);

                this.Content = filenames[0];
            }
        }

        private void Button_mklink_Click(object sender, RoutedEventArgs e)
        {
            // https://learn.microsoft.com/ja-jp/dotnet/core/porting/upgrade-assistant-overview
            // https://qiita.com/furugen/items/cac11ccbca38c400a9ac
            // これの為に.NET Framework から .NET Core に変更したという
            Directory.CreateSymbolicLink(this.textBox_dst.Text, this.textBox_src.Text);
        }

    }
}
