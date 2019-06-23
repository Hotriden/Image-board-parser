using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VisualWPFProj.MessageService;
using System.Collections.ObjectModel;

namespace VisualWPFProj.Model
{
    public class Parser : IParser
    {
        private readonly IMessageService service;

        public ObservableCollection<DataEntity> ParseData(string thread)
        {
            string board = "https://2ch.hk/";
            string[] ThreadArray = thread.Split('/');
            string ThreadSection = ThreadArray[3];
            string ThreadNumber = ThreadArray[5];

            if (ThreadNumber == null || ThreadSection == null)
            {
                service.ShowMessage("Введите раздел или номер треда");
            }
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            var parser = new HtmlParser();
            int counter = 0;
            ObservableCollection<DataEntity> dataEntities = new ObservableCollection<DataEntity>();

            string url = board + ThreadSection + "/res/" + ThreadNumber;
            string Response = wc.DownloadString(url);
            var search = parser.ParseDocument(Response);
            var result = search.GetElementsByClassName("post__file-attr").ToArray();

            foreach (var b in result)
            {
                counter++;
                string patternUrl = "href='".Replace("'", "\"") + "/" + ThreadSection;
                string patternTitle = "href='".Replace("'", "\"") + "http://www.google.com/searchbyimage?image_url=https://2ch.hk/";
                string[] splitted = b.InnerHtml.Split();
                string tempUrl = "";
                string tempTitle = "";
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (splitted[i].StartsWith(patternUrl))
                    {
                        tempUrl += splitted[i];
                    }
                    else if (splitted[i].StartsWith(patternTitle))
                    {
                        tempTitle += splitted[i];
                    }
                }
                if (tempUrl != "")
                {
                    DataEntity entity = new DataEntity();
                    string subStringUrl = @"href=""";
                    string finalUrl = tempUrl.Remove(0, subStringUrl.Length);
                    string finalUrlResult = "https://2ch.hk/" + finalUrl.Remove(finalUrl.Length - 1, 1);
                    string finalTitle = tempTitle.Remove(0, 52);
                    string finalTitleResult = finalTitle.Remove(finalTitle.Length - 1, 1);
                    entity.Checked = false;
                    entity.Title = finalTitle;
                    entity.Url = finalUrlResult;
                    entity.Id = counter;
                    dataEntities.Add(entity);
                }
            }
            return dataEntities;
        }
    }
}
