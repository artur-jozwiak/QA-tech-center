using QA.Domain.Models.Enums;
using QA.Domain.Models.ToolTests;

namespace QA.BLL.Interfaces
{
    public interface IToolTestingRepository
    {
        ToolTestComparison GetComparisonById(int comparisonId);
        List<ToolTestComparison> GetAllComparisons();
        List<ToolTest> GetAll();
        void AddToolTest(ToolTest toolTest);
        void AddTestComparison(ToolTestComparison testComparison);
        void AddComparisonPoint(ComparisonPoint comparisonPoint);
        void RemoveComparison(ToolTestComparison toolTestComparison);
        void RemoveToolTest(ToolTest tooltest);
        void RemoveComparePoint(ComparisonPoint comparisonPoint);


        bool ComparisonExist(int toolId, TestType type);

        ToolTestComparison GetByToolIdAndType(int toolId, TestType type);

        List<Tool> GetAllTools();
    }
}
