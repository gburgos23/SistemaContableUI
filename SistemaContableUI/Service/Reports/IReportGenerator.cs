using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaContableUI.Service.Reports
{
    public interface IReportGenerator
    {
        string GenerateReport(string title);
    }
}
