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
    public partial class frmLogin : Form
    {
        connectionData conn = new connectionData();
        public frmLogin()
        {
            InitializeComponent();

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                conn.dbBaglan();
            }
            catch (Exception hataMesaji)
            {
                MessageBox.Show("Hata meydana geldi! Hata açıklaması" + hataMesaji.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            if (conn.kullaniciKontrol(txtKullaniciAdi.Text, txtSifre.Text) == true)
            {
                conn.dbBaglantiKes();
                frmMain frmAna = new frmMain();
                frmAna.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ve şifreniz yanlış!", "Yanlış Bilgiler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                conn.dbBaglantiKes();
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
