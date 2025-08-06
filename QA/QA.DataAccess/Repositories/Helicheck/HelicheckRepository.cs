using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models.Helicheck.Models;
using System.Text.RegularExpressions;

namespace QA.DataAccess.Repositories.Helicheck
{
    public class HelicheckRepository : IHelicheckRepository
    {
        private readonly HelicheckContext _context;

        public HelicheckRepository(HelicheckContext context)
        {
            _context = context;
        }

        public List<HelicheckParameter> GetParametersBy(string orderNo)
        {
            var measurements = _context.HelicheckMeasurements
                .Include(m => m.InfoDat2)
                .Include(m => m.Parameters)
                .Where(m => m.InfoDat2.Str == orderNo)
                .ToList();

            //var parameters = measurements.SelectMany(m => m.Parameters).Distinct().OrderBy(p => p.LfdNr).ToList();
            //var parameters = measurements.SelectMany(m => m.Parameters).Distinct().OrderBy(p => p.Name).ToList();
            //var parameters = measurements.SelectMany(m => m.Parameters).Distinct().OrderBy(p => p.Ids).ToList();
            //var parameters = measurements.SelectMany(m => m.Parameters).Distinct().OrderBy(p => p.Ix).ToList();

            var parameters = measurements
                .SelectMany(m => m.Parameters)
                .Distinct()
                .OrderBy(p =>
                        {
                            var match = Regex.Match(p.Ids ?? "", @"RES_(\d+)\.(\d+)");
                            int part1 = match.Success ? int.Parse(match.Groups[1].Value) : 0;
                            int part2 = match.Success ? int.Parse(match.Groups[2].Value) : 0;
                            return (part1, part2);
                        })
                .ToList();

            return parameters;
        }

        public List<HelicheckResult> GetResultsBy(int parameterId, string orderNo)
        {
            return _context.HelicheckResults.Include(r => r.HelicheckMeasurements).ThenInclude(m => m.InfoDat2).Where(r => r.KritIx == parameterId && r.HelicheckMeasurements.InfoDat2.Str.StartsWith(orderNo)).ToList();
        }

        public bool OrderHasHelicheckMeasurements(string orderNo)
        {
            return _context.HelicheckInfos.Any(i => i.Str.StartsWith(orderNo));
        }
    }
}


///****** Script for SelectTopNRows command from SSMS  ******/
//SELECT m.Ix, r.MessIx, r.KritIx
//  FROM [Qcm8Order].[dbo].[Messung] m
//join InfoDat2 info ON info.IdIx = m.ToolStrIx
//join Results r ON r.MessIx = m.ix
//join Kriterien k On k.ix = r.KritIx
//where info.Str like '%25000408%'
///****** Script for SelectTopNRows command from SSMS  ******/
//SELECT m.Ix, r.MessIx, r.KritIx
//  FROM [Qcm8Order].[dbo].[Messung] m
//join InfoDat2 info ON info.IdIx = m.ToolStrIx
//join Results r ON r.MessIx = m.ix
//join Kriterien k On k.ix = r.KritIx
//where info.Str like '%25000408%'


//SELECT m.Ix, r.MessIx, r.KritIx, k.dim, k.LfdNr, k.OTol, k.UTol,k.Name, r.ValueF, k.PrgIx, m.PrgIx
//  FROM [Qcm8Order].[dbo].[Messung] m
//join InfoDat2 info ON info.IdIx = m.ToolStrIx
//join Results r ON r.MessIx = m.ix
//join Kriterien k On k.ix = r.KritIx
//where info.Str like '%25000408%'SELECT m.Ix, r.MessIx, r.KritIx, k.dim, k.LfdNr, k.OTol, k.UTol,k.Name, r.ValueF, k.PrgIx, m.PrgIx
//  FROM [Qcm8Order].[dbo].[Messung] m
//join InfoDat2 info ON info.IdIx = m.ToolStrIx
//join Results r ON r.MessIx = m.ix
//join Kriterien k On k.ix = r.KritIx
//where info.Str like '%25000408%'

