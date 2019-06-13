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
using System.ComponentModel;
using VisualWPFProj.Model;


namespace VisualWPFProj
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        public string ThreadNumber { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        public string FilePath
        {
            get { return fld_Path.Text; }
        }

        public string ThreadSection
        {
            get { return fld_Input.Text; }
            set { fld_Input.Text = value; }
        }

        public event EventHandler FileChosePathClick;
        public event EventHandler FileSaveContentClick;
        public event EventHandler FileLoadContentClick;

        public void SetThreadCount(int count)
        {
            throw new NotImplementedException();
        }

        #region Based Window Properties
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion


    }
}
