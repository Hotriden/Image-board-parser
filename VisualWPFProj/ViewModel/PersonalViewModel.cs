using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VisualWPFProj.Model;
using VisualWPFProj.Core;

namespace VisualWPFProj.ViewModel
{
    class PersonalViewModel: DependencyObject
    {
        public string GetData
        {
            get { return (string)GetValue(GetDataProperty); }
            set { SetValue(GetDataProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetData.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetDataProperty =
            DependencyProperty.Register("GetData", typeof(string), typeof(PersonalViewModel), new PropertyMetadata(""));



        public DataEntity GetContent
        {
            get { return (DataEntity)GetValue(GetContentProperty); }
            set { SetValue(GetContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GetContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GetContentProperty =
            DependencyProperty.Register("GetContent", typeof(DataEntity), typeof(PersonalViewModel), new PropertyMetadata(null));

        Parser parser = new Parser();
        DataContext context = new DataContext();

        public PersonalViewModel()
        {
        }
    }
}
