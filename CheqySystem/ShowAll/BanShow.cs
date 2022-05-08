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
    public partial class BanShow : Form
    {
        Model1 db = new Model1();
        public BanShow()
        {
            InitializeComponent();

            var ser = (from p in db.DipsatchInfoes
                       select new
                       {
                           dispId = p.dispId,
                           sectorId = p.SectorsInfo.sectorName,
                           dispamount = p.dispamount == null ? 0 : p.dispamount,
                           dispcheque = p.dispcheque == null ? 0 : p.dispcheque,
                           displetter = p.displetter == null ? "" : p.displetter,
                           dispnote = p.dispnote == null ? "" : p.dispnote,
                       }).ToList();
            dataGridView1.DataSource = ser.ToList();
            dataGridView1.Columns[0].HeaderText = "ر.م";
            dataGridView1.Columns[1].HeaderText = "اسم الجهة";
            dataGridView1.Columns[2].HeaderText = "القيمة";
            dataGridView1.Columns[3].HeaderText = "رقم الصك";
            dataGridView1.Columns[4].HeaderText = "بالحروف";
            dataGridView1.Columns[5].HeaderText = "ملاحظات";
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            if (txt_search.Text.ToString() == "")
            {
                var bind = (from p in db.DipsatchInfoes
                            orderby p.sectorId ascending
                            group p by new
                            {
                                dispId = p.dispId,
                                sectorId = p.SectorsInfo.sectorName,
                                dispamount = p.dispamount == null ? 0 : p.dispamount,
                                dispcheque = p.dispcheque == null ? 0 : p.dispcheque,
                                displetter = p.displetter == null ? "" : p.displetter,
                                dispnote = p.dispnote == null ? "" : p.dispnote,
                            } into res
                            select res.Key).ToList();
                dataGridView1.DataSource = bind.ToList();
            }
            else
            {
                var bind = (from p in db.DipsatchInfoes
                            where p.dispId.ToString().ToLower().Contains(txt_search.Text.ToString().ToLower()) ||
                            p.sectorId.ToString().ToLower().Contains(txt_search.Text.ToString().ToLower())
                            || p.dispcheque.ToString().ToLower().Contains(txt_search.Text.ToString().ToLower())
                            || p.dispamount.ToString().ToLower().Contains(txt_search.Text.ToString().ToLower())
                             || p.dispnote.ToLower().Contains(txt_search.Text.ToString().ToLower())
                            orderby p.sectorId ascending
                            group p by new
                            {
                                dispId = p.dispId,
                                sectorId = p.SectorsInfo.sectorName,
                                dispamount = p.dispamount == null ? 0 : p.dispamount,
                                dispcheque = p.dispcheque == null ? 0 : p.dispcheque,
                                displetter = p.displetter == null ? "" : p.displetter,
                                dispnote = p.dispnote == null ? "" : p.dispnote,
                            } into res
                            select res.Key).ToList();
                dataGridView1.DataSource = bind.ToList();
            }
        }

        private void BanShow_Load(object sender, EventArgs e)
        {

        }
    }

}
