using KartRacingManager.Interfaces.Exports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing.Layout;

namespace KartRacingManager.Exports
{
    public class PdfExporter : IExporter
    {
        public void ExportRacer(Dictionary<string, string> racerInfo)
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont keyFont = new XFont("ComicSans", 18, XFontStyle.Italic);
            XFont valueFont = new XFont("Times", 20, XFontStyle.Bold);
            XFont headingFont = new XFont("Verdana", 30, XFontStyle.Bold);

            XRect titleRect/*m8*/ = new XRect(0, 0, page.Width, page.Height);
            XRect infoRect/*m8*/ = new XRect(0, 50, page.Width, page.Height);

            XTextFormatter textFormatter = new XTextFormatter(gfx);

            gfx.DrawRectangle(XBrushes.SeaShell, infoRect);
            textFormatter.DrawString("Racer Information", headingFont, XBrushes.Bisque, titleRect, XStringFormats.TopLeft);

            foreach (KeyValuePair<string, string> entry in racerInfo)
            {
                infoRect.X += 5;
                textFormatter.DrawString(entry.Key + ":", keyFont, XBrushes.DarkCyan, infoRect, XStringFormats.TopLeft);
                infoRect.X += 150;
                textFormatter.DrawString(entry.Value, valueFont, XBrushes.DarkSeaGreen, infoRect, XStringFormats.TopLeft);
                infoRect.Y += 30;
                infoRect.X -= 155;
            }

            string title = String.Format($@"Information about {racerInfo["First name"]} {racerInfo["Last name"]}");
            document.Save($"../../../Exports/Racers/{title}.pdf");
        }

        //private void CreatePdfFile(string title, string path, )

        public void ExportRace(Dictionary<string, string> raceInfo)
        {

        }
    }
}
