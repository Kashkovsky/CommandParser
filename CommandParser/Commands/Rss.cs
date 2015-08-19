using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace CommandParser.Commands
{
    class Rss : Command
    {
        public Rss ()
        {
            base.receiveArgs = true;
        }
        string path = "";
        
        string[,] rssData = null;
        public override void Do(IEnumerable<string> flagArguments)
        {
            if (flagArguments.Count() > 0)
            {
                path = flagArguments.First();
                Refresh();
            }
            else Console.WriteLine("You should give an URL for -rss command.");
        }

        private string[,] getRssData (string channel)
        {
            XmlNodeList items;
            try
            {
                WebRequest request = WebRequest.Create(channel);
                WebResponse responce = request.GetResponse();
                Stream rssStream = responce.GetResponseStream();
                XmlDocument rssDoc = new XmlDocument();
                rssDoc.Load(rssStream);
                items = rssDoc.SelectNodes("rss/channel/item");
            }
            catch (UriFormatException)
            {
                Flag flag = new Flag("-help");
                flag.Do();
                Console.WriteLine($"Error: {channel} doesn't seem to be a valid feed URL.");
                return null;
            }
            catch (Exception ex)
            {
                Flag flag = new Flag("-help");
                flag.Do();
                Console.WriteLine($"Cannot load feed. Why? {ex.Message}");
                return null;
            }
            
            string[,] tempRssData = new string[items.Count, 3];
            for (int i = 0; i < items.Count; i++)
            {
                XmlNode rssNode;

                rssNode = items.Item(i).SelectSingleNode("title");
                if (rssNode != null) tempRssData[i, 0] = rssNode.InnerText;
                else tempRssData[i, 0] = "";

                rssNode = items.Item(i).SelectSingleNode("description");
                if (rssNode != null) tempRssData[i, 1] = rssNode.InnerText;
                else tempRssData[i, 1] = "";

                rssNode = items.Item(i).SelectSingleNode("link");
                if (rssNode != null) tempRssData[i, 2] = rssNode.InnerText;
                else tempRssData[i, 2] = "";
            }
            return tempRssData;
        }
        private void Refresh()
        {
            Console.Clear();
            rssData = getRssData(path);
            if (rssData != null)
            {
                for (int i = 0; i < rssData.GetLength(0); i++)
                {
                    Console.WriteLine($"#{i} {rssData[i, 0]}");
                }
                Console.WriteLine();
                Menu();
            }
        }
        private void Menu()
        {
            Console.WriteLine("[R: refresh] [<number>: load issue] [X: quit]");
            string responce = Console.ReadLine();
            if (!ShowIssue(responce))
            {
                switch (responce)
                {
                    case "R":
                        Refresh();
                        break;
                    case "X":
                        Console.Clear();
                        Flag flag = new Flag("-help");
                        flag.Do();
                        return;
                    default:
                        Console.WriteLine($"Unknown command <{responce}> for RSS Reader.\n" + 
                                            "If you need any base command, exit RSS Reader first.");
                        break;
                }
            }
        }
        private bool ShowIssue(string responce)
        {
            int issueNumber;
            if (int.TryParse(responce, out issueNumber))
            {
                if (rssData[issueNumber, 1] != null)
                {
                    string description = ToPlainText(rssData[issueNumber, 1]);
                    Console.WriteLine(rssData[issueNumber, 0]);
                    Console.WriteLine(description);
                    Menu();
                    return true;
                }
                else
                {
                    Console.WriteLine("Cannot load an issue...");
                    return false;
                }
            }
            else return false;
        }
        private string ToPlainText(string input)
        {
            string result = "";
            var delimiters = new List<string> { "<", ">", "&quot;" };
            string pattern = "("
                     + String.Join("|", delimiters.Select(d => Regex.Escape(d)).ToArray())
                     + ")";
            string[] inputToArray = Regex.Split(input, pattern);
            List<string> inputList = inputToArray.ToList();
            
            for (int i = 0; i < inputList.Count; i++)
            {
                switch (inputList[i])
                {
                    case " ":
                        inputList.Remove(inputList[i]);
                        break;

                    case "<":
                        if (inputList[i + 1].Contains("p") || inputList[i + 1].Contains("br"))
                        {
                            inputList[i] = "\r\n";
                            for (int times = 0; times < 2; times++) inputList.Remove(inputList[i + 1]);
                        } 
                        //__________________________
                        
                        else if (inputList[i + 1].Contains("li") && inputList[i + 1] != "/li")
                        {
                            if (inputList[i + 2] == ">" && inputList[i + 3] != "<")
                            {
                                 inputList[i] = "\r\n    * ";
                           
                            for (int times = 0; times < 2; times++) inputList.Remove(inputList[i + 1]);
                            }
                           
                        }
                        //_________________________
                        else for (int times = 0; times < 3; times++) inputList.Remove(inputList[i]);
                        break;

                    case "&quot;":
                        inputList[i] = "\"";
                        break;

                    default:
                        break;
                }
                result += inputList[i];
            }
            return result;
        }

        public override string ToString()
        {
            return "opens RSS feed from given URL. Use -rss <feed url>";
        }
    }
}
