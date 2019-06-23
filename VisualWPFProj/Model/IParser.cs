using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualWPFProj.Model
{
    public interface IParser
    {
        string ThreadSection { get; set; }
        int? ThreadNumber { get; set; }
        List<DataEntity> ParseData();
    }
}