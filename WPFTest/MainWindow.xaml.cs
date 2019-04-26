using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Net;
using System.Collections.ObjectModel;

namespace WPFTest
{

    public interface IMainWindow
    {
        string ContentPath { get; }
        void SetContentCount(int count);
        //List<string> ContentResult { get; set; }
        //event EventHandler CheckContent;
        //event EventHandler LoadContent;
    }

    public partial class MainWindow : Window, IMainWindow
    {
        MainParser pars = new MainParser();
        WebClient client = new WebClient();
        MessageService message = new MessageService();
        public List<List<string>> ContentResult = new List<List<string>>();
        public string ContentPath { get; }
        public int SetSymbolCount { get; set; }
        private MyDataContext MyData;

        public void SetContentCount(int count)
        {
            int counter = ContentResult.Count;
        }

        public MainWindow()
        {
            InitializeComponent();
            MyData = new MyDataContext();
            DataContext = MyData;
            /*
            BtnCheck.Click += BtnCheck_Click;
            BtnSelect.Click += BtnSelect_Click;
            BtnLoad.Click += BtnLoad_Click;
            */
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxFind.Text != "")
            {
                try
                {
                    ContentResult = pars.ParseByAngle(int.Parse(TextBoxFind.Text), TextBoxSection.Text);


                    foreach(List<string> list in ContentResult)
                    {
                        SetSymbolCount++;
                        MyData.MyCollection.Add(new DataModel()
                        {
                            Image = pars.UriConverter(b),
                            Id = SetSymbolCount,
                            Url = b,
                            Checked = false
                        });
                        CountValue.Text = Convert.ToString(SetSymbolCount);
                    }

                    listView.ItemsSource = MyData.MyCollection;
                }
                catch (Exception ex)
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
                        foreach (var b in ContentResult)
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

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
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
        
        //public event EventHandler CheckContent;
        //public event EventHandler LoadContent;
    }
}
