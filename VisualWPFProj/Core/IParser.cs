using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualWPFProj.Model;

namespace VisualWPFProj.Core
{
    public interface IParser
    {
        string ThreadSection { get; set; }
        int? ThreadNumber { get; set; }
        void ParseData();
    }
}
