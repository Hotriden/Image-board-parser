using AngleSharp.Html.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VisualWPFProj.MessageService;

namespace VisualWPFProj.Model
{
    public class Parser : IParser
    {
        public int? ThreadNumber { get; set; }
        public string ThreadSection { get; set; }
        private readonly IMessageService service;

        public List<DataEntity> ParseData()
        {
            if (ThreadNumber == null || ThreadSection == null)
            {
                service.ShowMessage("Введите раздел или номер треда");
            }
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            var parser = new HtmlParser();
            int counter = 0;
            List<DataEntity> dataEntities = new List<DataEntity>();

            string url = "https://2ch.hk/" + this.ThreadSection + "/res/" + this.ThreadNumber + ".html";
            string Response = wc.DownloadString(url);
            var search = parser.ParseDocument(Response);
            var result = search.GetElementsByClassName("post__file-attr").ToArray();

            foreach (var b in result)
            {
                counter++;
                string patternUrl = "href='".Replace("'", "\"") + "/" + this.ThreadSection;
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
