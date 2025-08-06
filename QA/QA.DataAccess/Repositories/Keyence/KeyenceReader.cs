using Microsoft.Extensions.Configuration;
using QA.BLL.Interfaces;
using QA.Domain.Models.Keyence;
using System.Globalization;

namespace QA.DataAccess.Repositories.Keyence
{
    public class KeyenceReader : IKeyenceReader
    {
        private IConfiguration _configuration;
        private QAContext _context;

        public KeyenceReader(IConfiguration configuration, QAContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task GetKeyenceData()
        {
            await GetAllDfdFiles();
            await GetAllDfxFiles();
        }

        private async Task GetAllDfdFiles()
        {
            string[] dfdfiles = GetFilesByExtension(".dfd");

            foreach (string file in dfdfiles)
            {
                string fileName = Path.GetFileName(file);
                DateTime modificationDate = File.GetLastWriteTime(file);
                var dbParameters = GetParametersByFileName(fileName);

                if (_context.KeyenceParameters.Any(km => km.FileName == fileName && km.ModificationDate != modificationDate))
                {
                    if (dbParameters.Any(p => p.ModificationDate != modificationDate))
                    {
                        _context.KeyenceParameters.RemoveRange(dbParameters);
                        await _context.SaveChangesAsync();

                        var content = ReadFile(file);
                        var fileParameters = MapContentToParameters(content);
                        List<KeyenceParameter> parametersToAdd = new List<KeyenceParameter>();

                        foreach (var parameter in fileParameters)
                        {
                            parameter.ModificationDate = modificationDate;
                            parameter.FileName = fileName;
                            parametersToAdd.Add(parameter);
                        }

                        try
                        {
                            _context.KeyenceParameters.AddRange(parametersToAdd);
                            await _context.SaveChangesAsync();
                        }
                        catch (Exception ex)
                        {
                            throw;
                        }
                    }
                }
                else if (_context.KeyenceParameters.All(kp => kp.FileName != fileName))
                {
                    var content = ReadFile(file);
                    var fileParameters = MapContentToParameters(content);
                    List<KeyenceParameter> parametersToAdd = new List<KeyenceParameter>();

                    foreach (var parameter in fileParameters)
                    {
                        parameter.ModificationDate = modificationDate;
                        parameter.FileName = fileName;

                        parametersToAdd.Add(parameter);
                    }

                    try
                    {
                        _context.KeyenceParameters.AddRange(parametersToAdd);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        private async Task GetAllDfxFiles()
        {
            string[] dfxfiles = GetFilesByExtension(".dfx");

            foreach (string file in dfxfiles)
            {
                string fileName = Path.GetFileName(file);
                DateTime modificationDate = File.GetLastWriteTime(file);
                var parameters = new List<KeyenceParameter>();

                if (_context.KeyenceMeasurements.Any(km => km.FileName == fileName && km.FileModificationDate != modificationDate))
                {
                    var measurementsForRemove = _context.KeyenceMeasurements.Where(km => km.FileName == fileName);

                    if (measurementsForRemove != null)
                    {
                        _context.KeyenceMeasurements.RemoveRange(measurementsForRemove);
                        parameters = GetParametersByFileName(Path.GetFileNameWithoutExtension(file) + ".dfd");

                        if (parameters != null)
                        {
                            var content = ReadFile(file);
                            var fileMeasurements = MapContentToMeasurements(content);
                            await SaveMeasurements(fileMeasurements, parameters, modificationDate, fileName);
                        }
                    }
                }
                else if (_context.KeyenceMeasurements.All(km => km.FileName != fileName))
                {
                    parameters = GetParametersByFileName(Path.GetFileNameWithoutExtension(file) + ".dfd");

                    if (parameters != null)
                    {
                        var content = ReadFile(file);
                        var fileMeasurements = MapContentToMeasurements(content);
                        await SaveMeasurements(fileMeasurements, parameters, modificationDate, fileName);
                    }
                }
            }
        }

        private List<string> ReadFile(string fileName)
        {
            string path = _configuration["AppSettings:KeyencePath"];
            string fullPath = Path.Combine(path, fileName);
            string extension = fileName.Split('.').Last();
            List<string> lines = new List<string>();

            using (StreamReader streamReader = new StreamReader(fullPath))
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();
                    if (extension == "dfd")
                    {
                        if (line.StartsWith("K2001") || line.StartsWith("K2002") || line.StartsWith("K2101") || line.StartsWith("K2110") || line.StartsWith("K2111") || line.StartsWith("K2112") || line.StartsWith("K2113") || line.StartsWith("K2142"))
                        {
                            lines.Add(line.Trim());
                        }
                    }
                    else if (extension == "dfx")
                    {

                        if (!line.StartsWith("K0008"))
                        {
                            lines.Add(line);
                        }
                    }
                }
            }
            return lines;
        }

        private List<KeyenceParameter>? GetParametersByFileName(string fileName)
        {
            if (_context.KeyenceParameters.Any(kp => kp.FileName == fileName))
            {
                return _context.KeyenceParameters.Where(kp => kp.FileName == fileName).ToList();
            }
            else
            {
                return null;
            }
        }

        private List<KeyenceMeasurement>? GetMeasurementsByFileName(string fileName)
        {
            if (_context.KeyenceMeasurements.Any(km => km.FileName == fileName))
            {
                return _context.KeyenceMeasurements.Where(km => km.FileName == fileName).ToList();
            }
            else
            {
                return null;
            }
        }

        private List<KeyenceParameter> MapContentToParameters(List<string> lines)
        {
            List<KeyenceParameter> parameters = new List<KeyenceParameter>();
            if (lines != null)
            {
                var txtParameters = GetParametersSubCollections(lines, "K2001/", "K2142");
                int paramNumber = 0;
                foreach (var txtParameter in txtParameters)
                {
                    paramNumber++;
                    var parameter = MapParameter(txtParameter, paramNumber);
                    parameters.Add(parameter);
                }
            }
            return parameters;
        }

        private KeyenceParameter MapParameter(string[] txtParameter, int paramNumber)
        {
            KeyenceParameter parameter = new();

            if (txtParameter != null)
            {
                for (int i = 0; i < txtParameter.Length; i++)
                {
                    txtParameter[i] = txtParameter[i].Split(' ').LastOrDefault();
                }

                parameter.Number = paramNumber;
                parameter.Name = txtParameter[1];
                parameter.Nominal = Decimal.Parse(txtParameter[2], CultureInfo.InvariantCulture);
                parameter.LSL = Decimal.Parse(txtParameter[3], CultureInfo.InvariantCulture);
                parameter.USL = Decimal.Parse(txtParameter[4], CultureInfo.InvariantCulture);
                parameter.LowerTollerance = Decimal.Parse(txtParameter[5], CultureInfo.InvariantCulture);
                parameter.UpperTollerance = Decimal.Parse(txtParameter[6], CultureInfo.InvariantCulture);
                parameter.Unit = txtParameter[7];
            }
            return parameter;
        }

        private List<KeyenceMeasurement> MapContentToMeasurements(List<string> lines)
        {
            List<KeyenceMeasurement> measurements = new List<KeyenceMeasurement>();
            lines.Insert(0, "K0009/0 0");

            for (int i = 0; i <= lines.Count - 2; i += 2)
            {
                measurements.AddRange(MapMeasurements(new string[] { lines[i], lines[i + 1] }));
            }

            return measurements;
        }

        private List<KeyenceMeasurement> MapMeasurements(string[] txtMeasurements)
        {
            //////////////////////
            try
            {

                List<KeyenceMeasurement> measurements = new();

                var elements = txtMeasurements[1].Split((char)20, (char)15).ToList();
                string orderNo = elements[4].Replace("#", "");
                int series = 0;

                if (orderNo.ToLower() != "test")
                {
                    DateTime date = DateTime.ParseExact(elements[2], "M/d/yyyy/H:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

                    string stringSeries = txtMeasurements[0].Split(" ").LastOrDefault();

                    if (int.TryParse(stringSeries, out series))
                    {
                        series = int.Parse(stringSeries);
                    }
                    else { series = 999; }

                    elements.RemoveAt(2);
                    elements.RemoveAt(3);
                    int j = 0;

                    for (int i = 0; i < elements.Count; i++)
                    {
                        if (elements[i] != "0" && elements[i] != "")
                        {
                            j++;
                            KeyenceMeasurement measurement = new KeyenceMeasurement();
                            measurement.Series = series;
                            measurement.Number = j;
                            measurement.Date = date;
                            measurement.OrderNo = orderNo.ToUpper();
                            measurement.Value = Decimal.Parse(elements[i], CultureInfo.InvariantCulture);
                            measurements.Add(measurement);
                        }
                    }
                }
                return measurements;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        static List<string[]> GetParametersSubCollections(List<string> lines, string startMarker, string endMarker)
        {
            List<string[]> subcollections = new List<string[]>();
            List<string> currentSubcollection = null;

            foreach (var line in lines)
            {
                if (line.StartsWith(startMarker))
                {
                    currentSubcollection = new List<string> { line };
                }
                else if (currentSubcollection != null && line.StartsWith(endMarker))
                {
                    currentSubcollection.Add(line);
                    subcollections.Add(currentSubcollection.ToArray());
                    currentSubcollection = null;
                }
                else if (currentSubcollection != null)
                {
                    currentSubcollection.Add(line);
                }
            }

            return subcollections;
        }

        private string[] GetFilesByExtension(string extension)
        {
            string path = _configuration["AppSettings:KeyencePath"];

            if (Directory.Exists(path))
            {
                string[] filesPaths = Directory.GetFiles(path)
                                          .Where(plik => Path.GetExtension(plik).Equals(extension, StringComparison.OrdinalIgnoreCase))
                                          .ToArray();
                return filesPaths;
            }
            return new string[0];
        }

        private async Task SaveMeasurements(List<KeyenceMeasurement> measurements, List<KeyenceParameter> parameters, DateTime modificationDate, string fileName)
        {
            foreach (var measurement in measurements)
            {
                if (measurement.Number <= parameters.Count)
                {
                    var parameter = parameters.FirstOrDefault(p => p.Number == measurement.Number);

                    if (parameter != null)
                    {
                        measurement.FileModificationDate = modificationDate;
                        measurement.FileName = fileName;
                        measurement.Parameter = parameter;
                    }
                }
            }

            try
            {
                _context.KeyenceMeasurements.AddRange(measurements.Where(m => m.Number <= parameters.Count));
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
