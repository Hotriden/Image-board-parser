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
        List<List<string>> ParseByAngle(int number, string section);
        void SaveImage(string imageUrl, string filePath);
    }

    public class MainParser:IMainParser
    {
        public List<List<string>> ParseByAngle(int number, string section)
        {
            string firstSymb = "https://2ch.hk/";
            string url = firstSymb + section + "/res/" + number + ".html";
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string Response = wc.DownloadString(url);
            var parser = new HtmlParser();
            var search = parser.ParseDocument(Response);
            var result = search.GetElementsByClassName("post__file-attr").ToArray();

            List<string> TitleResult = new List<string>();
            List<string> UrlResult = new List<string>();
            List<List<string>> FinalResult = new List<List<string>>();

            foreach (var b in result)
            {
                string patternUrl = "href='".Replace("'", "\"") + "/" + section;
                string[] splitted = b.InnerHtml.Split();
                string temp = "";
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (splitted[i].StartsWith(patternUrl))
                    {
                        temp += splitted[i];
                    }
                }

                string subString = @"href=""";
                int indexOfSubString = temp.IndexOf(subString);
                if (indexOfSubString >= 0)
                {
                    string finalUrl = temp.Remove(indexOfSubString, subString.Length);
                    string finalUrlResult = firstSymb + finalUrl.Remove(finalUrl.Length - 1, 1);
                    UrlResult.Add(finalUrlResult);
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

        /*
        public Uri UrlCheck(string url)
        {
            string currentPath = Environment.CurrentDirectory;
            string end = url.Remove(0, url.Length - 4);
            switch (end)
            {
                case ".mp4":
                    return new Uri(Environment.CurrentDirectory + currentPath + )
            }
            return new Uri(url);
        }
        */
    }
}
