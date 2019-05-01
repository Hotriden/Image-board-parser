using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Html.Parser;
using System.Net;

namespace WPFTest
{
    public interface IMainParser
    {
        ParsResult ParseByAngle(int number, string section);
        void SaveImage(string imageUrl, string filePath);
    }

    public class MainParser:IMainParser
    {
        public ParsResult ParseByAngle(int number, string section)
        {
            string firstSymb = "https://2ch.hk/";
            string url = firstSymb + section + "/res/" + number + ".html";
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string Response = wc.DownloadString(url);
            var parser = new HtmlParser();
            var search = parser.ParseDocument(Response);
            var result = search.GetElementsByClassName("post__file-attr").ToArray();

            ParsResult FinalResult = new ParsResult();

            foreach (var b in result)
            {
                string patternUrl = "href='".Replace("'", "\"") + "/" + section;
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
                    string finalUrlResult = firstSymb + finalUrl.Remove(finalUrl.Length - 1, 1);
                    string finalTitle = tempTitle.Remove(0, 52);
                    string finalTitleResult = finalTitle.Remove(finalTitle.Length - 1, 1);
                    FinalResult.TitleResult.Add(finalTitleResult);
                    FinalResult.UrlResult.Add(finalUrlResult);
                }
            }
            
            return FinalResult;
        }

        public void SaveImage(string imageUrl, string pathFile)
        {
            string reversedUrl;
            string backforward = null;
            char[] revereUrl = imageUrl.ToCharArray();
            Array.Reverse(revereUrl);
            reversedUrl = new string(revereUrl);

            for (int i = 0; i<reversedUrl.Length; i++)
            {
                if (reversedUrl[i] == '/')
                {
                    break;
                }
                else
                {
                    backforward += reversedUrl[i];
                }
            }
            char[] secondRevers = backforward.ToCharArray();
            Array.Reverse(secondRevers);
            reversedUrl = new string(secondRevers);
            string fileName = pathFile + @"\" + reversedUrl;
            
            using (WebClient webclient = new WebClient())
            {
                webclient.DownloadFileAsync(new Uri(imageUrl), fileName);
            }
        }

        public Uri UriConverter(string url)
        {
            string end = url.Remove(0, url.Length - 4);
            if (end == ".mp4")
            {
                return new Uri(url.Remove(url.Length - 4, 4) + ".jpg");
            }
            else if(end == "webm")
            {
                return new Uri(url.Remove(url.Length-4, 4) + "jpg");
            }
            return new Uri(url);
        }
       }
}
