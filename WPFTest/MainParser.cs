using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Html.Parser;
using System.Net;
using System.Threading;

namespace WPFTest
{
    public interface IMainParser
    {
        List<string> ParseUrlByAngle(int number, string section);
        void SaveImage(string imageUrl, string filePath);
    }

    public class MainParser:IMainParser
    {
        public List<string> ParseUrlByAngle(int number, string section)
        {
            string firstSymb = "https://2ch.hk/";
            string url = firstSymb + section + "/res/" + number + ".html";
            WebClient wc = new WebClient();
            wc.Encoding = Encoding.UTF8;
            string Response = wc.DownloadString(url);
            var parser = new HtmlParser();
            var search = parser.ParseDocument(Response);
            var result = search.GetElementsByClassName("post__file-attr").ToArray();

            List<string> FinalResult = new List<string>();

                foreach (var b in result)
                {
                    string pattern = "href='".Replace("'", "\"") + "/" + section;
                    string[] splitted = b.InnerHtml.Split();
                    string temp = null;
                    for (int i = 0; i < splitted.Length; i++)
                    {
                        if (splitted[i].StartsWith(pattern))
                        {
                            temp += splitted[i];
                        }
                    }

                    string subString = @"href=""";
                    int indexOfSubString = temp.IndexOf(subString);
                    string final = temp.Remove(indexOfSubString, subString.Length);
                    string finalResult = firstSymb + final.Remove(final.Length - 1, 1);
                    FinalResult.Add(finalResult);
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
    }
}
