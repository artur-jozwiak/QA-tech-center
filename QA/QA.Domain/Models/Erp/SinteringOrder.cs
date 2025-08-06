using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QA.Domain.Models.Erp
{
    // widok Q_nrSpieku QA_ZleceniaSpieku
    public class SinteringOrder
    {
        public string KluczZp { get; set; } = null!;
        public string KluczZpSkr { get; set; } = null!;

        public decimal MeNrprpo { get; set; }

        public DateTime Data { get; set; }
    }
}
