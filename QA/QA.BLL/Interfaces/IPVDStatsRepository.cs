

namespace QA.BLL.Interfaces
{
    public interface IPVDStatsRepository
    {
        //void ReadPVDStatsFile();
        //void ReadPVDStatsLastReport();


        string CopyCoatingUnitFiles(string runNo);
        void LoadXmlData(string runNo);
    }
}
