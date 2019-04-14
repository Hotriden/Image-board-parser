using System;
using System.Collections.Generic;
using System.Linq;
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
using AngleSharp.Html.Parser;
using Microsoft.Win32;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Threading;

namespace WPFTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainParser pars = new MainParser();
        WebClient client = new WebClient();
        MessageService message = new MessageService();
        List<string> links = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }


        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxFind.Text != "")
            {
                try
                {
                    List<string> result = pars.ParseUrlByAngle(int.Parse(TextBoxFind.Text), TextBoxSection.Text);

                    foreach (var b in result)
                    {
                        links.Add(b);
                        TextBox textbox = new TextBox();
                        textbox.Text = b;
                        textbox.HorizontalAlignment = HorizontalAlignment.Left;
                        textbox.Width = 300;
                        textbox.IsReadOnly = true;
                        textbox.Margin = new Thickness(0, 0, 10, 0);

                        myStackPanel.Children.Add(textbox);
                    }
                }
                catch(Exception ex)
                {
                    message.ShowError(ex.Message);
                }
            }
            else
            {
                message.ShowExclamation("Не корректный ввод данных. Проверьте правильность заполнения.");
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
                if (TextBoxFilePath.Text != "")
                {
                    try
                    {
                        foreach (var b in links)
                        {
                            pars.SaveImage(b, TextBoxFilePath.Text);
                        }
                    }
                    catch (Exception ex)
                    {
                        message.ShowError(ex.Message);
                    }
                }
                else
                {
                    message.ShowMessage("Введите путь для сохранения файлов");
                }
        }

        private void BtnLoad_Select(object sender, RoutedEventArgs e)
        {
            if (TextBoxFilePath.Text != null)
            {
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();

                System.Windows.Forms.DialogResult dlg = fbd.ShowDialog();

                if (!string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    TextBoxFilePath.Text = fbd.SelectedPath;
                }
            }
            else
            {
                message.ShowMessage("Загрузите страницу для скачивания");
            }
        }
    }
}
