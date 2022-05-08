using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheqySystem.ShowAll
{
    public partial class SecShow : Form
    {
        Model1 db = new Model1();
        DataTable ser;

        public SecShow()
        {
            InitializeComponent();

            var ser = (from p in db.SectorsInfoes
                       select new
                       {
                           sectorId = p.sectorId,
                           sectorName = p.sectorName == null ? "" : p.sectorName,
                           sectorAddress = p.sectorAddress == null ? "" : p.sectorAddress,
                           sectorNote = p.sectorNote == null ? "" : p.sectorNote
                       }).ToList();
            dataGridView1.DataSource = ser.ToList();
            dataGridView1.Columns[0].HeaderText = "ر.م";
            dataGridView1.Columns[1].HeaderText = "الاسم";
            dataGridView1.Columns[2].HeaderText = "العنوان";
            dataGridView1.Columns[3].HeaderText = "ملاحظات";
        }

        private void SecShow_Load(object sender, EventArgs e)
        {

        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (txt_search.Text.ToString() == "")
            {
                var bind = (from p in db.SectorsInfoes
                            orderby p.sectorId ascending
                            group p by new
                            {
                                sectorId = p.sectorId,
                                sectorName = p.sectorName == null ? "" : p.sectorName,
                                sectorAddress = p.sectorAddress == null ? "" : p.sectorAddress,
                                sectorNote = p.sectorNote == null ? "" : p.sectorNote
                            } into res
                            select res.Key).ToList();
                dataGridView1.DataSource = bind.ToList();
            }
            else
            {
                var bind = (from p in db.SectorsInfoes
                            where p.sectorName.ToLower().Contains(txt_search.Text.ToString().ToLower()) ||
                            p.sectorAddress.ToLower().Contains(txt_search.Text.ToString().ToLower())
                            || p.sectorNote.ToLower().Contains(txt_search.Text.ToString().ToLower())
                            orderby p.sectorId ascending
                            group p by new
                            {
                                sectorId = p.sectorId,
                                sectorName = p.sectorName == null ? "" : p.sectorName,
                                sectorAddress = p.sectorAddress == null ? "" : p.sectorAddress,
                                sectorNote = p.sectorNote == null ? "" : p.sectorNote
                            } into res
                            select res.Key).ToList();
                dataGridView1.DataSource = bind.ToList();
            }
        }
    }
}
