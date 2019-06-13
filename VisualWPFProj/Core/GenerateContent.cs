using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisualWPFProj.Core;
using VisualWPFProj.Model;

namespace VisualWPFProj.Core
{
    class GenerateContent:IGenerator
    {
        private int Counter { get; set; }
        private readonly IParser parser;
        private readonly IDataContext context;

        public GenerateContent(IParser pars, DataContext cont)
        {
            context = cont;
            parser = pars;
        }

        public int CountData(IDataContext cont)
        {
            Counter = cont.UrlResult.Count();
            return Counter;
        }

        public List<DataEntity> GenerateData(DataContext dataContext)
        {
            List<DataEntity> result = new List<DataEntity>();
            for (int i = 0; i < Counter; i++)
            {
                DataEntity entity = new DataEntity();
                entity.Checked = false;
                entity.Id = i;
                entity.Url = dataContext.UrlResult.ElementAt(i);
                entity.Title = dataContext.TitleResult.ElementAt(i);
                result.Add(entity);
            }
            return result;
        }
    }
}
