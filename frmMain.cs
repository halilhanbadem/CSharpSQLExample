using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpSQLExample
{
    public partial class frmMain : Form
    {
        connectionData conn = new  connectionData();

        public frmMain()
        {
            InitializeComponent();
        }

        public void externalGridLoad()
        {
            conn.kayitListele(dataGrid);
        }

        public void ekranTemizle()
        {
            txtKullaniciAdi.Clear();
            txtKullaniciSifre.Clear();
            cmbBirim.SelectedItem = 0;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            conn.comboDoldur(cmbBirim);
            cmbBirim.SelectedIndex = 0;
            conn.kayitListele(dataGrid);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.kayitEkle(txtKullaniciAdi.Text, txtKullaniciSifre.Text, DateTime.Now, (cmbBirim.SelectedIndex + 1).ToString());
            conn.kayitListele(dataGrid);
            ekranTemizle();
                 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmLogin frmloginEkran = new frmLogin();
            frmloginEkran.Show();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedCells.Count > 0)
            {
               int recordId = Convert.ToInt32(dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["ID"].Value.ToString());
                DialogResult questionMessage = MessageBox.Show("Seçili kaydı silmek istediğinize emin misiniz?", "Emin misiniz?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (questionMessage == DialogResult.Yes)
                {
                    conn.kayitSil(recordId);
                    conn.kayitListele(dataGrid);
                }    
            } else
            {
                MessageBox.Show("Lütfen kayıt silmek bir adet kayıt seçiniz!", "Seçili Kayıt Yok!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            frmUpdate formUpdate = new frmUpdate();
            formUpdate.recordID = Convert.ToInt32(dataGrid.Rows[dataGrid.CurrentRow.Index].Cells["ID"].Value.ToString());
            formUpdate.ShowDialog();
        }


    }
}
