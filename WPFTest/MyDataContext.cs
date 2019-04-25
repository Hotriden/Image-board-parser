using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest
{
    public class MyDataContext
    {
        public ObservableCollection<DataModel> MyCollection = new ObservableCollection<DataModel>(); 
    }
}
