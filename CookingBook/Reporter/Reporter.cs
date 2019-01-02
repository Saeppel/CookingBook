using CookingLib.Interfaces;
using CookingLib.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reporter
{
    public class Reporter : IReportCaller
    {
        public void ShowReport(Recipe recipe)
        {
            ReportForm form = new ReportForm();
            form.ShowDialog();
        }
    }
}
