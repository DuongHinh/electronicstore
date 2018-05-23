using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicStore.Data.Repositories.Projections
{
    public class ProfitAndRevenueProjection
    {
        public DateTime Date { set; get; }

        public decimal Revenue { set; get; }

        public decimal Profit { set; get; }
    }
}
