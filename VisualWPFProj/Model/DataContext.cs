using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualWPFProj.Core;

namespace VisualWPFProj.Model
{
    public class DataContext:IDataContext
    {
        public List<string> UrlResult { get; set; }

        public List<string> TitleResult { get; set; }
    }
}
