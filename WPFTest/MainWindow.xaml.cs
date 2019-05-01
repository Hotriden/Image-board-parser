using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Net;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Navigation;
using System.Threading.Tasks;

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
        ParsResult ContentResult = new ParsResult();
        public string ContentPath { get; }
        public int SetSymbolCount { get; set; }
        private MyDataContext MyData;

        public void SetContentCount(int count)
        {
            int counter = ContentResult.UrlResult.Count;
        }

        public MainWindow()
        {
            InitializeComponent();
            MyData = new MyDataContext();
            DataContext = MyData;

            /*
            ScrollViewer sv = new ScrollViewer();
            sv.CanContentScroll = true;
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            myStackPanel.Children.Add(sv);
            */
            
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
                    MyData.MyCollection.Clear();
                    SetSymbolCount = 0;
                    ContentResult = pars.ParseByAngle(int.Parse(TextBoxFind.Text), TextBoxSection.Text);

                    for(int i=0; i<ContentResult.TitleResult.Count; i++)
                    {
                        SetSymbolCount++;
                        MyData.MyCollection.Add(new DataModel()
                        {
                            Id = SetSymbolCount,
                            Url = ContentResult.UrlResult[i],
                            Image= new Uri(ContentResult.TitleResult[i]),
                            Checked=false
                        });
                    }
                    listView.ItemsSource = MyData.MyCollection;
                    CountValue.Text = Convert.ToString(SetSymbolCount);
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

        private async void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxFilePath.Text != "")
            {
                try
                {
                    foreach (var b in ContentResult.UrlResult)
                    {
                        await pars.SaveImage(b, TextBoxFilePath.Text);
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

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private async void Hyperlink_Download(object sender, RequestNavigateEventArgs e)
        {
            if (TextBoxFilePath.Text != "")
            {
                try
                {
                    var progress = new Progress<int>(value => pBar.Value = value);
                    await Task.Run(() =>
                    {
                        for (int i = 0; i < 100; i++)
                        {
                            Dispatcher.Invoke(() => {
                                pars.SaveImage(e.Uri.AbsoluteUri, TextBoxFilePath.Text);
                            });
                            pBar.Dispatcher.Invoke(() => pBar.Value = i, System.Windows.Threading.DispatcherPriority.Background);
                        }
                    });
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

        //public event EventHandler CheckContent;
        //public event EventHandler LoadContent;
    }
}
