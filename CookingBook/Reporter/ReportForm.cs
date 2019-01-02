using CookingLib.ReportData;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reporter
{
    public partial class ReportForm : Form
    {
        private ReportData _reportData;

        public ReportForm()
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            InitializeComponent();

            _reportData = new ReportData(new CookingLib.Objects.Recipe()
            {
                Name = "TEST"
            });
        }

        public ReportForm(ReportData reportData)
        {
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            InitializeComponent();

            _reportData = reportData;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.reportViewer.LocalReport.SetParameters(GetParameters());
            this.reportViewer.RefreshReport();
        }

        private List<ReportParameter> GetParameters()
        {
            var paras = new List<ReportParameter>()
            {
                new Microsoft.Reporting.WinForms.ReportParameter("RecipeName", "Super geiles Rezept")
            };

            return paras;
        }
    }
}
