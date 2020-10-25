using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace QScalpDataDownloader
{
	class Program
	{
      static void Main(string[] args)
		{
         List<string> instruments = new List<string>();
         List<DateTime> dateList = new List<DateTime>();
         string fileRead;
         using (StreamReader sr = new StreamReader("./input.txt"))
         {
            fileRead = sr.ReadLine();
         }
         using (StreamReader sr = new StreamReader(fileRead))
			{
            instruments = sr.ReadLine().Split(';').ToList<string>();
            dateList = sr.ReadLine().Split(';').Select(el => Convert.ToDateTime(el)).ToList();
         }
         if (!Directory.Exists("./Storage"))
         {
            Directory.CreateDirectory("./Storage");
         }
         using (WebClient client = new WebClient())
         {
            for (DateTime dateI = dateList[0]; dateI < dateList[1]; dateI = dateI.AddDays(1))
            {
               string formattedDateFolderName = dateI.ToString("yyyy-MM-dd");
               string url = "http://erinrv.qscalp.ru/";
               var web = new HtmlWeb();
               HtmlDocument doc = web.Load(url);
               StringBuilder sb = new StringBuilder();
               sb.AppendFormat("//a[@href='/{0}/']", formattedDateFolderName);
               string folderSelector = sb.ToString();
               if (doc.DocumentNode.SelectNodes(folderSelector)?.Count > 0)
               {
                  if (!Directory.Exists($"./Storage/{formattedDateFolderName}"))
                  {
                     Directory.CreateDirectory($"./Storage/{formattedDateFolderName}");
                  }
                  StringBuilder sb2 = new StringBuilder();
                  sb2.AppendFormat("http://erinrv.qscalp.ru/{0}/", formattedDateFolderName);
                  doc = web.Load(sb2.ToString());
                  sb2.Clear();
                  foreach (var el in instruments)
                  {
                     sb2.AppendFormat("//a[contains(.,'{0}')]", el);
                     foreach (var item in doc.DocumentNode.SelectNodes(sb2.ToString()))
                     {
                        client.DownloadFile($"http://erinrv.qscalp.ru/{ formattedDateFolderName }/{ item.InnerText }",
                                      $"./Storage/{ formattedDateFolderName }/{ item.InnerText }");
                     }
                     sb2.Clear();
                  }
                  Console.WriteLine($"{formattedDateFolderName} download finished");
               }
            }
         }
		}
	}
}
