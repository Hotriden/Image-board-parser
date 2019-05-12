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
using System.Threading;

namespace WPFTest
{

    public interface IMainWindow
    {
        string ContentPath { get; }
        void SetContentCount(int count);
    }

    public partial class MainWindow : Window, IMainWindow
    {
        MainParser pars = new MainParser();
        MessageService message = new MessageService();
        ParsResult ContentResult = new ParsResult();
        private DownloadContent DownloadWorker; 

        public string ContentPath { get; }
        public int SetSymbolCount { get; set; }
        private MyDataContext MyData;
        public string AbsoluteUrl { get; set; }

        public void SetContentCount(int count)
        {
            int counter = ContentResult.UrlResult.Count;
        }

        public MainWindow()
        {
            InitializeComponent();
            MyData = new MyDataContext();
            DataContext = MyData;
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

                    for (int i = 0; i < ContentResult.TitleResult.Count; i++)
                    {
                        SetSymbolCount++;
                        MyData.MyCollection.Add(new DataModel()
                        {
                            Id = SetSymbolCount,
                            Url = ContentResult.UrlResult[i],
                            Image = new Uri(ContentResult.TitleResult[i]),
                            Checked = false
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

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxFilePath.Text != "")
            {
                try
                {
                    foreach (var b in ContentResult.UrlResult)
                    {
                        DownloadWorker.SaveImage(b, TextBoxFilePath.Text);
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

        private void Hyperlink_Download(object sender, RequestNavigateEventArgs e)
        {
            string one = e.Uri.AbsoluteUri;
            string too = TextBoxFilePath.Text;
            DownloadWorker = new DownloadContent();
            DownloadWorker.ProgressChanged += DownloadWorker_ProgressChanged;
            DownloadWorker.WorkCompleted += DownloadWorker_WorkCompleted;

            if (TextBoxFilePath.Text != "")
            {
                Thread t = new Thread(() => DownloadWorker.SaveImage(one, too));
                t.Start();
            }
            else
            {
                message.ShowMessage("Введите путь для сохранения файлов");
            }
        }

        private void DownloadWorker_ProgressChanged(int progress)
        {
            Action action = () =>
            {
                pBar.Value = pBar.Value + 1;
                pBar.Value = progress;
            };
            Dispatcher.Invoke(action);
        }

        private void DownloadWorker_WorkCompleted(bool cancelled)
        {
            Action action = () =>
            {
                string message = cancelled ? "Загрузка отменена" : "Файл загружен";
                MessageBox.Show(message);
            };
            Dispatcher.Invoke(action);
        }
    }
}
