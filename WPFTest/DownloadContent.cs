using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace WPFTest
{
    public class DownloadContent
    {
        private bool _cancelled = false;

        public void Cancel()
        {
            _cancelled = true;
        }

        public void SaveImage(string imageUrl, string pathFile)
        {
            string result = imageUrl.Substring(imageUrl.IndexOf("src/") + 14);
            string fileName = pathFile + @"\" + result;

            using (WebClient webclient = new WebClient())
            {
                for (int i = 0; i <= 100; i++)
                {
                    if (_cancelled)
                        break;
                    webclient.DownloadFileAsync(new Uri(imageUrl), fileName);
                    ProgressChanged(i);
                }
            }
            WorkCompleted(_cancelled);
        }

        public event Action<int> ProgressChanged;
        public event Action<bool> WorkCompleted;
    }
}
