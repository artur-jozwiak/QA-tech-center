using CsvHelper.Configuration;
using QA.BLL.Models;

namespace QA.BLL.Mappings
{
    public class MSLaboratoryModelMap : ClassMap<MSLaboratoryModel>
    {
        public MSLaboratoryModelMap()
        {
            Map(m => m.Index).Index(0);
            Map(m => m.Sigma).Index(2);
            Map(m => m.MS).Index(3);
            Map(m => m.Weight).Index(4);
            Map(m => m.TimeStamp).Index(9).TypeConverterOption.Format("yyyy-MM-dd HH:mm");
        }
    }
}
