using Microsoft.EntityFrameworkCore;
using QA.BLL.Interfaces;
using QA.Domain.Models.Enums;
using QA.Domain.Models.ToolTests;

namespace QA.DataAccess.Repositories.ToolTesting
{
    public class ToolTestingRepository : IToolTestingRepository
    {
        private readonly QAContext _context;

        public ToolTestingRepository(QAContext context)
        {
            _context = context;
        }

        public void AddToolTest(ToolTest toolTest)
        {
            _context.ToolTests.Add(toolTest);
        }

        public void AddTestComparison(ToolTestComparison testComparison)
        {
            _context.ToolTestComparisons.Add(testComparison);
        }

        public void AddComparisonPoint(ComparisonPoint comparisonPoint)
        {
            _context.ComparePoints.Add(comparisonPoint);
        }

        public ToolTestComparison GetComparisonById(int comparisonId)
        {
           //return  _context.ToolTestComparisons.Include(ttc => ttc.ToolTests).ThenInclude(ttc => ttc.ComparisonPoints).ThenInclude(cp => cp.Images).FirstOrDefault(ttc => ttc.Id == comparisonId);
           return  _context.ToolTestComparisons.Include(ttc => ttc.Tool).Include(ttc => ttc.ToolTests).ThenInclude(ttc => ttc.ComparisonPoints).ThenInclude(cp => cp.Images).FirstOrDefault(ttc => ttc.Id == comparisonId);
        }

        public ToolTestComparison GetByToolIdAndType(int toolId, TestType type)
        {
            //return  _context.ToolTestComparisons.Include(ttc => ttc.ToolTests).ThenInclude(ttc => ttc.ComparisonPoints).ThenInclude(cp => cp.Images).FirstOrDefault(ttc => ttc.Id == comparisonId);
            return _context.ToolTestComparisons.Include(ttc => ttc.Tool).Include(ttc => ttc.ToolTests).ThenInclude(ttc => ttc.ComparisonPoints).ThenInclude(cp => cp.Images).FirstOrDefault(ttc => ttc.ToolId == toolId && ttc.TestType == type);
        }

        public List<ToolTestComparison> GetAllComparisons()
        {
            var comparisons = _context.ToolTestComparisons.Include(ttc => ttc.Tool).ThenInclude(t => t.ToolTestComparisons).Include(ttc => ttc.ToolTests).Include(ttc => ttc.ComparisonPoints).ThenInclude(cp => cp.Images).OrderBy(ttc => ttc.ToolId).ToList();

            if (comparisons != null)
            {
                return comparisons;
            }
            else
            {
                return Enumerable.Empty<ToolTestComparison>().ToList();
            }
        }

        public List<Tool> GetAllTools()
        {

            var tools = _context.Tool
                .Include(t => t.ToolTestComparisons)
                .ThenInclude(ttc => ttc.Tool)
                .ThenInclude(t => t.ToolTestComparisons)
                .Include(t => t.ToolTestComparisons)
                .ThenInclude(ttc => ttc.ToolTests)
                .Include(t => t.ToolTestComparisons)
                .ThenInclude(ttc => ttc.ComparisonPoints)
                .ThenInclude(cp => cp.Images)
                .ToList();

            if (tools != null)
            {
                return tools;
            }
            else
            {
                return Enumerable.Empty<Tool>().ToList();
            }
        }

        public List<ToolTest> GetAll()
        {
            var tests = _context.ToolTests.Include(tt => tt.Comparison).ThenInclude(c => c.ComparisonPoints).ThenInclude(cp => cp.Images).ToList();
            
            if(tests != null)
            {
                return tests;
            }
            else
            {
                return Enumerable.Empty<ToolTest>().ToList();
            }
        }

        public void RemoveComparison(ToolTestComparison toolTestComparison)
        {
            _context.ToolTestComparisons.Remove(toolTestComparison);
        }

        public void RemoveToolTest(ToolTest tooltest)
        {
            _context.ToolTests.Remove(tooltest);
        }

        public void RemoveComparePoint(ComparisonPoint comparisonPoint)
        {
            _context.ComparePoints.Remove(comparisonPoint);
        }

        public void AddTool(Tool tool)
        {
            _context.Tool.Add(tool);
        }


        public bool ComparisonExist(int toolId, TestType type)
        {
           return _context.ToolTestComparisons.Any(ttc => ttc.ToolId == toolId && ttc.TestType == type);
        }
    }
}
