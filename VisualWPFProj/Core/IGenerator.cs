using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualWPFProj.Model;

namespace VisualWPFProj.Core
{
    public interface IGenerator
    {
        int CountData(DataContext context);
        List<DataEntity> GenerateData(DataContext dataContext);
    }
}
