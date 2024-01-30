using HtmlAgilityPack;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using NovelScraper.Models;
using System.Diagnostics;
using System.Text;
using iText.Html2pdf;
using iText.Layout;
using iText.Kernel.Pdf;
using iText.Kernel.Events;
using iText.Layout.Element;
using iText.Layout.Renderer;
using System.Net;

namespace NovelScraper.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            CreateAllChapters();

            return View();
        }

        private static async Task<string> CallUrl(string fullUrl)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync(fullUrl);
            return response;
        }

        private List<string> ParseHtml(string html) 
        {
            HtmlDocument htmldoc = new();
            htmldoc.LoadHtml(html);



            var paragraphs = htmldoc.DocumentNode.Descendants("p")

                                            .Select(node => WebUtility.HtmlDecode(node.InnerHtml))
                                            .Distinct()
                                            .ToList();
            return paragraphs;
        }


        private void WriteToPdf(List<string> lines, string name)
        {
            var pdfPath = $"{name}.pdf";

            using (var pdfWriter = new PdfWriter(pdfPath))
            {
                using (var pdf = new PdfDocument(pdfWriter))
                {
                    var document = new Document(pdf);

                    foreach (var line in lines)
                    {
                        var paragraph = new Paragraph(string.Join(Environment.NewLine, line)).SetFontSize(12);
                        document.Add(paragraph);
                    }
                }
            }
        }
        //private void WriteToPdf(List<string> lines, string name)
        //{
        //    var pdfPath = $"{name}.pdf";

        //    using (var pdfWriter = new PdfWriter(pdfPath))
        //    {
        //        using (var pdf = new PdfDocument(pdfWriter))
        //        {
        //            var document = new Document(pdf);


        //            var paragraph = new Paragraph(string.Join(Environment.NewLine, lines)).SetFontSize(12);

        //            document.Add(paragraph);
        //        }
        //    }
        //}



        private void WriteToCsv(List<string> lines, string name)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var line in lines)
            {
                sb.AppendLine(line + Environment.NewLine);
            }

            System.IO.File.WriteAllText($"{name}.csv", sb.ToString()); 
        }



        private void CreateChapter(string volume, string chapter)
        {
            string url = $"https://www.readlightnovel.today/rakudai-kishi-no-eiyuutan/volume-{volume}/chapter-{chapter}";
            var result = CallUrl(url).Result;
            var list = ParseHtml(result);
            list.RemoveAt(list.Count - 1);
            //WriteToCsv(list, $"Chapters/result-v{volume}-c{chapter}");
            WriteToPdf(list, $"Chapters/Volume{volume}.0-Chapter{chapter}");
        }

        

        private void CreateAllChapters()
        {
            //volume 1
            CreateChapter("1", "prologue");
            CreateChapter("1", "1");
            CreateChapter("1", "2");
            CreateChapter("1", "3");
            CreateChapter("1", "4");
            CreateChapter("1", "epilogue");
            ////volume 2
            CreateChapter("2", "prologue");
            CreateChapter("2", "1");
            CreateChapter("2", "2");
            CreateChapter("2", "3");
            CreateChapter("2", "4");
            CreateChapter("2", "epilogue");
            //volume 3
            CreateChapter("3", "prologue");
            CreateChapter("3", "1");
            CreateChapter("3", "2");
            CreateChapter("3", "3");
            CreateChapter("3", "4");
            CreateChapter("3", "epilogue");
            //volume 4 
            CreateChapter("4", "prologue");
            CreateChapter("4", "1");
            CreateChapter("4", "2");
            CreateChapter("4", "3");
            CreateChapter("4", "4");
            CreateChapter("4", "epilogue");
            //volume 5
            CreateChapter("5", "prologue");
            CreateChapter("5", "1");
            CreateChapter("5", "2");
            CreateChapter("5", "3");
            CreateChapter("5", "4");
            CreateChapter("5", "epilogue");
            //volume 6
            CreateChapter("6", "prologue");
            CreateChapter("6", "5");
            CreateChapter("6", "6");
            CreateChapter("6", "7");
            //volume 7
            CreateChapter("7", "8");
            CreateChapter("7", "9");
            CreateChapter("7", "10");
            //volume 8
            CreateChapter("8", "11");
            CreateChapter("8", "12");
            CreateChapter("8", "13");
            //volume 9
            CreateChapter("9", "14");
            //volume 10
            CreateChapter("10", "prologue");
            CreateChapter("10", "1");
            CreateChapter("10", "2");
            CreateChapter("10", "3");
            CreateChapter("10", "4");
            //volume 11
            CreateChapter("11", "5");
            CreateChapter("11", "6");
            CreateChapter("11", "7");
            CreateChapter("11", "8");
            CreateChapter("11", "9");
            //volume 12
            CreateChapter("12", "intermission");
            CreateChapter("12", "10");
            CreateChapter("12", "11");
            CreateChapter("12", "12");
            CreateChapter("12", "13");
            CreateChapter("12", "14");
            //olume 13
            CreateChapter("13", "intermission-1");
            CreateChapter("13", "intermission-2");
            CreateChapter("13", "15");
            CreateChapter("13", "16");
            CreateChapter("13", "17");
            CreateChapter("13", "18");
            //volume 14
            CreateChapter("14", "intermission");
            CreateChapter("14", "19");
            CreateChapter("14", "20");
            CreateChapter("14", "21");
            //volume 15
            CreateChapter("15", "intermission");
            CreateChapter("15", "epilogue-2");
            CreateChapter("15", "22");
            CreateChapter("15", "23");
            CreateChapter("15", "24");
            CreateChapter("15", "25");
            CreateChapter("15", "epilogue");
            //volume 16
            CreateChapter("16", "prologue");
            CreateChapter("16", "1");
            CreateChapter("16", "2");
            CreateChapter("16", "3");
            //volume 17 
            CreateChapter("17", "intermission");
            CreateChapter("17", "4");
            CreateChapter("17", "5");
            CreateChapter("17", "6");
            CreateChapter("17", "7");
            CreateChapter("17", "epilogue");
            //volume 18
            CreateChapter("18", "prologue");
            CreateChapter("18", "1");
            CreateChapter("18", "2");
            CreateChapter("18", "3");
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }

    

   
}