using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LegacyWebFormsApp.Models
{
    [Serializable]
    public class DashboardMetrics
    {
        public int TotalProducts { get; set; }
        public int TotalUsers { get; set; }
        public int LowStockProducts { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}