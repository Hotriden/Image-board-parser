using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using VisualWPFProj.Model;
using AngleSharp.Html.Parser;
using VisualWPFProj.MessageService;

namespace VisualWPFProj.Core
{
    public class Parser : IParser
    {
        public int? ThreadNumber { get; set; }
        public string ThreadSection { get; set; }
        private readonly IMessageService service;
        private readonly IDataContext context;

        public Parser(IDataContext cont, IMessageService message)
        {
            service = message;
            context = cont;
        }

        public void ParseData()
        {
            if (ThreadNumber == null || ThreadSection == null)
            {
                service.ShowMessage("Введите раздел или номер треда");
            }
                WebClient wc = new WebClient();
                wc.Encoding = Encoding.UTF8;
                var parser = new HtmlParser();
                // DataContext dataContext = new DataContext();

                string url = "https://2ch.hk/" + this.ThreadSection + "/res/" + this.ThreadNumber + ".html";
                string Response = wc.DownloadString(url);
                var search = parser.ParseDocument(Response);
                var result = search.GetElementsByClassName("post__file-attr").ToArray();

                foreach (var b in result)
                {
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
                        string subStringUrl = @"href=""";
                        string finalUrl = tempUrl.Remove(0, subStringUrl.Length);
                        string finalUrlResult = "https://2ch.hk/" + finalUrl.Remove(finalUrl.Length - 1, 1);
                        string finalTitle = tempTitle.Remove(0, 52);
                        string finalTitleResult = finalTitle.Remove(finalTitle.Length - 1, 1);
                        this.context.UrlResult.Add(finalTitleResult);
                        this.context.TitleResult.Add(finalUrlResult);
                    }
                }
            }
        }
    }
