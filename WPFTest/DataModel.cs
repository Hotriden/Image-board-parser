using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTest
{
    public class DataModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public Uri Image { get; set; }
        public bool Checked { get; set; }
    }

    public class SearchResult
    {
        public List<string> Titles { get; set; }
        public List<string> Results { get; set; }
    }
}
