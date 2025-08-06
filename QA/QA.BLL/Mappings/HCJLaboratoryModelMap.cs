using CsvHelper.Configuration;
using QA.BLL.Models;

namespace QA.BLL.Mappings
{
    public class HCJLaboratoryModelMap : ClassMap<HCJLaboratoryModel>
    {
        public HCJLaboratoryModelMap()
        {
            Map(m => m.Index).Index(0);
            Map(m => m.HCJ).Index(1);
            Map(m => m.TimeStamp).Index(4).TypeConverterOption.Format("yyyy-MM-dd HH:mm");
        }
    }
}
