using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace WebApi
{
    internal class Xm8WebApi
    {
        public static string filepath { get; } = "C:\\Users\\i59098\\Google Drive\\webApi.txt";
        public static string webUrl { get; } = "https://test.xactimate.com/api/help";

        public static string ParseHtmlCode(string url = null)
        {
            string htmlCode;
            string returnString = "";
            using (WebClient client = new WebClient())
            {
                htmlCode = client.DownloadString(url ?? webUrl);
            }

            while (htmlCode.IndexOf("<h2") > 0)
            {
                var thing = Regex.Match(htmlCode, "(?<irrelevant><h2 id=\")(?<controller>.*?)(?<irrelevant>\")").Groups;
                returnString += $"\n-{thing["controller"].Value}-\n\n";

                //make htmlCode smaller
                htmlCode = htmlCode.Substring(htmlCode.IndexOf("api-name"));

                while (htmlCode.IndexOf("<h2") > htmlCode.IndexOf("api-name") || (htmlCode.IndexOf("<h2") < 0 && htmlCode.IndexOf("api-name") >= 0))
                {
                    thing =
                        Regex.Match(htmlCode, "(?<irrelevent><a href=\".*?\">)(?<endpoint>.*?)(?<irrelevent></a>)",
                            RegexOptions.Singleline).Groups;

                    returnString += thing["endpoint"].Value + "\n";

                    //make htmlCode smaller
                    htmlCode = htmlCode.Substring(htmlCode.IndexOf("</a>", StringComparison.Ordinal) + 4);
                }
            }

            return returnString;
        }

        public static string CompareHtmlToFile(string parsedHtml, string filePath = null)
        {
            var fileContents = File.ReadAllText(filePath ?? filepath);
            if (fileContents.Length == parsedHtml.Length)
                return "They're probably no new endpoints or controllers =)";

            fileContents = fileContents.TrimStart();
            parsedHtml = parsedHtml.TrimStart();
            string differences = "";
            string fLine = null;
            string pLine = null;
            do
            {
                if (pLine != fLine)
                {
                    //Console.WriteLine($"NOT EQUAL! pLine: {pLine} and fLine: {fLine}");
                    if (parsedHtml.IndexOf(fLine) < 0)
                    {
                        differences += $"-REMOVED: {fLine}\n";
                        fLine = fileContents.Substring(0, fileContents.IndexOf("\n") + 1).Trim();
                        fileContents = fileContents.Substring(fileContents.IndexOf("\n") + 1).TrimStart();
                    }
                    else
                    {
                        differences += $"+ADDED: {pLine}\n";
                        pLine = parsedHtml.Substring(0, parsedHtml.IndexOf("\n") + 1).Trim();
                        parsedHtml = parsedHtml.Substring(parsedHtml.IndexOf("\n") + 1).TrimStart();
                    }
                }
                else
                {
                    fLine = fileContents.Substring(0, fileContents.IndexOf("\n") + 1).Trim();
                    pLine = parsedHtml.Substring(0, parsedHtml.IndexOf("\n") + 1).Trim();
                    fileContents = fileContents.Substring(fileContents.IndexOf("\n") + 1).TrimStart();
                    parsedHtml = parsedHtml.Substring(parsedHtml.IndexOf("\n") + 1).TrimStart();
                }
            } while (pLine.Length > 0 || fLine.Length > 0);

            return differences;
        }

        public static void WriteStringToFile(string toWrite, string filePath = null)
        {
            File.WriteAllText(filePath ?? filepath, toWrite);
        }

        //private static void Main(string[] args)
        //{
        //    var parsedHtml = ParseHtmlCode("https://beta.xactimate.com/api/help");
        //    //var parsedHtml = ParseHtmlCode();
        //    var result = CompareHtmlToFile(parsedHtml);
        //    Console.WriteLine(result);
        //    //WriteStringToFile(parsedHtml);
        //}
    }
}