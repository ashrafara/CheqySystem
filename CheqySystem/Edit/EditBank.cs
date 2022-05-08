using CheqySystem.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity.Migrations;
using System.Data.Entity;

namespace CheqySystem.Edit
{
    public partial class EditBank : Form
    {
        Model1 db = new Model1();
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();

        public EditBank()
        {
            InitializeComponent();

            var emplpy = (from c in db.SectorsInfoes
                          select new
                          {
                              c.sectorId,
                              c.sectorAddress,
                              c.sectorNote,
                              c.sectorName
                          }).ToList();
            cbSector.DataSource = emplpy.ToList();
            cbSector.DisplayMember = "sectorName";
            cbSector.ValueMember = "sectorId";

            var ser = (from p in db.DipsatchInfoes
                       select new
                       {
                           dispId = p.dispId,
                           sectorId = p.SectorsInfo.sectorName,
                           dispamount = p.dispamount,
                           dispcheque = p.dispcheque,
                           displetter = p.displetter,
                           dispnote = p.dispnote
                       }).ToList();
            dataGridView1.DataSource = ser.ToList();
            dataGridView1.Columns[0].HeaderText = "ر.م";
            dataGridView1.Columns[1].HeaderText = "اسم الجهة";
            dataGridView1.Columns[2].HeaderText = "القيمة";
            dataGridView1.Columns[3].HeaderText = "رقم الصك";
            dataGridView1.Columns[4].HeaderText = "بالحروف";
            dataGridView1.Columns[5].HeaderText = "ملاحظات";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
            cbSector.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
            txtNumber.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
            txtchequenum.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            txtArabicWord.Text = dataGridView1.Rows[i].Cells[4].Value.ToString();
            txtnote.Text = dataGridView1.Rows[i].Cells[5].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(textBox1.Text);
            var deletedemply = (from c in db.DipsatchInfoes
                                where c.dispId == id
                                select c).Single();
            db.DipsatchInfoes.Remove(deletedemply);
            db.SaveChanges();
            MessageBox.Show("تم الحذف");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bind = new DipsatchInfo
            {
                dispId= int.Parse(textBox1.Text),
                sectorId = Convert.ToInt32(cbSector.SelectedValue),
                dispamount = Math.Truncate(double.Parse(txtNumber.Text) * 1000) / 1000,
                dispcheque = string.IsNullOrEmpty(txtchequenum.Text) ? (double?)0 : double.Parse(txtchequenum.Text),
                displetter = string.IsNullOrEmpty(txtArabicWord.Text) ? "" : (txtArabicWord.Text),
                dispnote = string.IsNullOrEmpty(txtnote.Text) ? "" : (txtnote.Text)
            };
            db.DipsatchInfoes.AddOrUpdate(bind);
            db.SaveChanges();
            MessageBox.Show("تم الحفظ");
        }

        private void txtletter_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtamount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ToWord toWord = new ToWord(Convert.ToDecimal(txtNumber.Text), currencies[Convert.ToInt32(cboCurrency.SelectedValue)]);
                txtArabicWord.Text = toWord.ConvertToArabic();
                txtArabicWord.Text = txtArabicWord.Text.Substring(0, txtArabicWord.Text.IndexOf("دينار") + 5);
                decimal d = Convert.ToDecimal(txtNumber.Text) - Math.Truncate(Convert.ToDecimal(txtNumber.Text));
                if (d > 0)
                    txtArabicWord.Text += " و " + d.ToString().Substring(d.ToString().IndexOf('.') + 1, 3) + " درهما لاغير";

            }
            catch (Exception ex)
            {
                txtArabicWord.Text = String.Empty;
            }
        }

        private void EditBank_Load(object sender, EventArgs e)
        {
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Libya));
        }
    }
}
