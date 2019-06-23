using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualWPFProj.Model
{
    public interface IParser
    {
        ObservableCollection<DataEntity> ParseData(string thread);
    }
}