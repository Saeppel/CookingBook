using CookingLib.Interfaces;
using CookingLib.Objects;
using CookingLib.ReportData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingBook.Helper
{
    public class ReportHelper
    {
        private static IReportCaller _reportCaller = new Reporter.Reporter();

        public static void CallReport(ReportData recipe)
        {
            _reportCaller.ShowReport(recipe);
        }
    }
}
