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

namespace CheqySystem.Main
{
    public partial class AddBank : Form
    {
        Model1 db = new Model1();
        List<CurrencyInfo> currencies = new List<CurrencyInfo>();

        public AddBank()
        {
            InitializeComponent();

            var emplpy = (from c in db.SectorsInfoes
                          orderby c.sectorName
                          select new
                          {
                              c.sectorId,c.sectorAddress,c.sectorNote,c.sectorName
                          }).ToList();
            cbSector.DataSource = emplpy.ToList();
            cbSector.DisplayMember = "sectorName";
            cbSector.ValueMember = "sectorId";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bind = new DipsatchInfo
            {
                sectorId = Convert.ToInt32(cbSector.SelectedValue),
                dispamount = Math.Truncate(double.Parse(txtNumber.Text)*1000)/1000,
                dispcheque = string.IsNullOrEmpty(txtchequenum.Text) ? (double?)0 : double.Parse(txtchequenum.Text),
                displetter = string.IsNullOrEmpty(txtArabicWord.Text) ? "" : (txtArabicWord.Text),
                dispnote = string.IsNullOrEmpty(txtnote.Text) ? "" : (txtnote.Text)
            };
            db.DipsatchInfoes.Add(bind);
            db.SaveChanges();
            MessageBox.Show("تم الحفظ");
            ClearTextBoxes();
        }
        private void ClearTextBoxes()
        {
            Action<Control.ControlCollection> func = null;

            func = (controls) =>
            {
                foreach (Control control in controls)
                    if (control is TextBox)
                        (control as TextBox).Clear();
                    else
                        func(control.Controls);
            };

            func(Controls);
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

        private void AddBank_Load(object sender, EventArgs e)
        {
            currencies.Add(new CurrencyInfo(CurrencyInfo.Currencies.Libya));
        }

        private void txtArabicWord_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtchequenum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)) 
    {
        e.Handled = true;
    }
        }
    }
}
