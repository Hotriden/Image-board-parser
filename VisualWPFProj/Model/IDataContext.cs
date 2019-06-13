using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualWPFProj.Model
{
    public interface IDataContext
    {
        List<string> UrlResult { get; set; }
        List<string> TitleResult { get; set; }
    }
}
