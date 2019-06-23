using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace VisualWPFProj.Model
{
    public class DataContext: BindableBase
    {
        IParser parser = new Parser();

        private readonly ObservableCollection<DataEntity> _myValues = new ObservableCollection<DataEntity>();
        public readonly ReadOnlyObservableCollection<DataEntity> MyDataCollection;

        public DataContext()
        {
            MyDataCollection = new ReadOnlyObservableCollection<DataEntity>(_myValues);
        }

        public void LoadData(string value)
        {
            _myValues.Union(parser.ParseData(value));
            RaisePropertyChanged("LoadData");
        }
    }
}
