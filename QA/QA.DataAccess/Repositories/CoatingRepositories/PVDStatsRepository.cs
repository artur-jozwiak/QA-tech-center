using Microsoft.Extensions.Configuration;
using QA.BLL.Interfaces;
using QA.Domain.Models.CoatingModels;
using System.IO.Compression;
using System.Xml.Linq;

namespace QA.DataAccess.Repositories.CoatingRepositories
{
    public class PVDStatsRepository : IPVDStatsRepository
    {
        private IConfiguration _configuration;
        private QAContext _context;

        public PVDStatsRepository(IConfiguration configuration, QAContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public void LoadXmlData(string runNo)
        {
            //27.05.25
            //string path = Path.Combine(_configuration["AppSettings:QAPath"], "PVD", $"Polcomm-01_{runNo}_Stats.txt");
            string path = Path.Combine(_configuration["AppSettings:CoatingUnitPath"], $"Polcomm-01_{runNo}_Stats.txt");

            if (File.Exists(Path.Combine(path)))
            {
                string xml = File.ReadAllText(path);
                var cp = ParseFromXml(xml);

                if (_context.CoatingProcess.Any(cp => cp.RunNo.ToString() == runNo))
                {
                    var coatingProcess = _context.CoatingProcess.FirstOrDefault(cp => cp.RunNo.ToString() == runNo);
                    coatingProcess.ProcessId = cp.ProcessId;
                    coatingProcess.Coating = cp.Coating;
                    _context.SaveChanges();
                }
            }
        }

        public CoatingProcess ParseFromXml(string xml)
        {
            var doc = XDocument.Parse(xml);
            var charge = doc.Root.Element("Processinfo")?.Element("Charge");
            var process = doc.Root.Element("Processinfo")?.Element("Process");

            return new CoatingProcess
            {
                RunNo = int.Parse(charge?.Attribute("runnumber")?.Value ?? "0"),
                ProcessId = process?.Attribute("id")?.Value,
                Coating = process?.Attribute("coating")?.Value
            };
        }

        //wykonywac co godzine
        public string CopyCoatingUnitFiles(string runNo)
        {
            string message = string.Empty;
            List<string> filesPrefixes = new List<string>()
            {
                "_report.html",
                "_Stats.txt"
            };

            foreach (var item in filesPrefixes)
            {
                string sourceDirectory = String.Concat("Polcomm-01_0000", runNo, ".zip");
                string sorceFile = String.Concat("Polcomm-01_", runNo, item);
                string sourcePath = _configuration["AppSettings:PVDStatsPath"];

                //27.05.25
                //string destinationPath = Path.Combine(_configuration["AppSettings:QAPath"], "PVD");
                string destinationPath = _configuration["AppSettings:CoatingUnitPath"];

                if (!File.Exists(Path.Combine(sourcePath, sourceDirectory)))
                {
                    message = $"The coating process[{runNo}] files do not exist.";
                }
                else
                {
                    using (ZipArchive archive = ZipFile.OpenRead(Path.Combine(sourcePath, sourceDirectory)))
                    {
                        var entry = archive.GetEntry(sorceFile);

                        string destinationFilePath = Path.Combine(destinationPath, sorceFile);
                        using (var entryStream = entry.Open())
                        using (var fileStream = File.Create(destinationFilePath))
                        {
                            entryStream.CopyTo(fileStream);
                        }
                    }
                }
            }

            return message;
        }
    }
}

