using KartRacingManager.Interfaces.Exports;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using PdfSharp.Pdf;
using System.Collections.Generic;

namespace KartRacingManager.Exports
{
    public class PdfExporter : IExporter
    {
        private const string racersExportPath = "../../../Exports/Racers/";
        private const string racesExportPath = "../../../Exports/Races/";
        private const string extension = ".pdf";

        public void ExportRacerInfo(Dictionary<string, string> racerInfo)
        {
            string fileName = "Information about " + racerInfo["First name"] + " "  + racerInfo["Last name"];

            GeneratePdfDocument(fileName, racersExportPath, racerInfo); 
        }

        public void ExportRaceInfo(Dictionary<string, string> raceInfo)
        {
            string fileName = "Information about " + raceInfo["Race name"];

            GeneratePdfDocument(fileName, racesExportPath, raceInfo);
        }

        private void GeneratePdfDocument(string fileName, string path, Dictionary<string, string> information)
        {
            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont keyFont = new XFont("ComicSans", 18, XFontStyle.Bold);
            XFont valueFont = new XFont("Times", 20, XFontStyle.Bold);
            XFont headingFont = new XFont("Verdana", 30, XFontStyle.Italic);

            XRect titleRect/*m8*/ = new XRect(0, 0, page.Width, page.Height);
            XRect infoRect/*m8*/ = new XRect(0, 50, page.Width, page.Height);

            XTextFormatter textFormatter = new XTextFormatter(gfx);

            gfx.DrawRectangle(XBrushes.WhiteSmoke, infoRect);

            textFormatter.DrawString($"{fileName}:", headingFont, XBrushes.Bisque, titleRect, XStringFormats.TopLeft);

            foreach (KeyValuePair<string, string> entry in information)
            {
                infoRect.X += 5;
                textFormatter.DrawString(entry.Key + ":", keyFont, XBrushes.DarkCyan, infoRect, XStringFormats.TopLeft);
                infoRect.Y += 20;
                infoRect.X += 80;
                textFormatter.DrawString(entry.Value, valueFont, XBrushes.DarkSeaGreen, infoRect, XStringFormats.TopLeft);
                infoRect.Y += 30;
                infoRect.X -= 85;
            }

            document.Save(path + fileName + extension);
        }
    }
}
