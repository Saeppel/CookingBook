using CookingLib.Interfaces;
using CookingLib.Objects;
using CookingLib.ReportData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporter
{
    public class Reporter : IReportCaller
    {
        public void ShowReport(ReportData recipe)
        {
            ReportForm form = new ReportForm();
            form.ShowDialog();
        }
    }
}
