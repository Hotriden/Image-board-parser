using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Prism.Mvvm;
using VisualWPFProj.Model;
using Prism.Commands;

namespace VisualWPFProj.ViewModels
{
    public class ViewModel:BindableBase
    {
        public DataContext _context = new DataContext();
        public ViewModel()
        {
            _context.PropertyChanged += (s, e) => { RaisePropertyChanged(e.PropertyName); };
            LoadContext = new DelegateCommand<string>(str =>
            {
                ObservableCollection<DataEntity> entity = new ObservableCollection<DataEntity>();

                if (str != null)
                    _context.LoadData(str);
            });
        }

        public DelegateCommand<string> LoadContext { get; }
        public ReadOnlyObservableCollection<DataEntity> MyValues => _context.MyValues;

    }
}
