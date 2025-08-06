using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Domain.Models.Erp
{
    //widok [QA_MeldunkiSpieku]
    public class SinteringReport
    {
        public int ErpOrderId { get; set; }
        public string  KluczZp { get; set; }
        public int Ilosc {  get; set; }
        public decimal MeNrprpo { get; set; }
    }
}
