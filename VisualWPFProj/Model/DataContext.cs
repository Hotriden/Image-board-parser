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
        public readonly ReadOnlyObservableCollection<DataEntity> MyValues;

        public DataContext()
        {
            MyValues = new ReadOnlyObservableCollection<DataEntity>(_myValues);
        }

        public void LoadData(string value)
        {
            ObservableCollection<DataEntity> entity = new ObservableCollection<DataEntity>();
            entity = parser.ParseData(value);
            _myValues = entity;
            RaisePropertyChanged("Result");
        }

        public DataEntity Result => MyValues.FirstOrDefault();
    }
}
