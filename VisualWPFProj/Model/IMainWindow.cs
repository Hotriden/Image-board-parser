using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualWPFProj.Model
{
    interface IMainWindow
    {
        string FilePath { get; }
        string ThreadSection { get; set; }
        string ThreadNumber { get; set; }
        void SetThreadCount(int count);
        event EventHandler FileChosePathClick;
        event EventHandler FileSaveContentClick;
        event EventHandler FileLoadContentClick;
    }
}
