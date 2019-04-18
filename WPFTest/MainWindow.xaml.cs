using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Net;

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
        public List<string> ContentResult = new List<string>();
        public string ContentPath { get; }
        public int SetSymbolCount { get; set; }

        public void SetContentCount(int count)
        {
            int counter = ContentResult.Count;
        }

        public MainWindow()
        {
            InitializeComponent();
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
                    List<string> result = new List<string>();
                    result = pars.ParseUrlByAngle(int.Parse(TextBoxFind.Text), TextBoxSection.Text);

                    myStackPanelURL.Children.Clear();
                    myStackPanelNum.Children.Clear();
                    myStackPanelPick.Children.Clear();
                    foreach (var b in result)
                    {
                        SetSymbolCount++;
                        ContentResult.Add(b);

                        TextBox textboxURL = new TextBox();
                        TextBox textboxNum = new TextBox();
                        CheckBox checkbox = new CheckBox();
                        Button buttonFCheck = new Button();

                        textboxNum.Text = Convert.ToString(SetSymbolCount);
                        textboxURL.Text = b;
                        textboxURL.HorizontalAlignment = HorizontalAlignment.Left;
                        textboxURL.Width = 300;
                        textboxURL.IsReadOnly = true;

                        textboxURL.Margin = new Thickness(0, 1, 0, 0);
                        textboxNum.Margin = new Thickness(0, 1, 0, 0);
                        checkbox.Margin = new Thickness(0, 1, 0, 0);
                        buttonFCheck.Margin = new Thickness(0, 1, 0, 0);

                        textboxURL.Height = 18;
                        textboxNum.Height = 18;
                        checkbox.Height = 18;
                        buttonFCheck.Height = 18;

                        checkbox.HorizontalAlignment = HorizontalAlignment.Center;
                        checkbox.IsChecked = true;
                        buttonFCheck.Content = "Скачать";

                        myStackPanelURL.Children.Add(textboxURL);
                        myStackPanelNum.Children.Add(textboxNum);
                        myStackPanelCheck.Children.Add(checkbox);
                        myStackPanelButton.Children.Add(buttonFCheck);

                        CountValue.Text = Convert.ToString(SetSymbolCount);
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
