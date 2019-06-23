using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualWPFProj.Model
{
    public class DataContext:IDataContext
    {
        public List<DataEntity> DataEntities { get; private set; }

        public DataContext(List<DataEntity> context)
        {
            DataEntities = context;
        }
    }
}
