using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;



namespace CheqySystem.Reports
{
    public partial class chequ_report : Form
    {
        Model1 db = new Model1();

        public chequ_report()
        {
            InitializeComponent();

            var emplpy = (from c in db.DipsatchInfoes
                          join p in db.SectorsInfoes on c.sectorId equals p.sectorId
                          orderby c.dispId descending
                          select new
                          {
                              c.dispId,
                              p.sectorId,
                              p.sectorName
                          }).ToList();
            comboBox1.DataSource = emplpy.ToList();
            comboBox1.DisplayMember = "sectorName";
            comboBox1.ValueMember = "sectorId";

            comboBox2.DataSource = emplpy.ToList();
            comboBox2.DisplayMember = "dispId";
            comboBox2.ValueMember = "dispId";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReportDocument cryRpt = new ReportDocument();
            CrystalReport1 rpt = new CrystalReport1();
            rpt.SetDatabaseLogon("sa", "ali123", "localhost", "ChequeSys");
            rpt.SetParameterValue("dispId", comboBox2.SelectedValue);
            crystalReportViewer1.ReportSource = rpt;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedItem = comboBox1.SelectedItem;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = comboBox2.SelectedItem;
        }

        private void chequ_report_Load(object sender, EventArgs e)
        {

        }
    }
}
