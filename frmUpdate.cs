using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CSharpSQLExample
{
    public partial class frmUpdate : Form
    {
        public frmUpdate()
        {
            InitializeComponent();
        }

        public int recordID { get; set; }
        connectionData conn = new connectionData();
        SqlCommand qrySorgu = new SqlCommand();
        

        private void frmUpdate_Load(object sender, EventArgs e)
        {
            conn.dbBaglan();
            qrySorgu.Connection = conn.sqlConnector;
            qrySorgu.CommandText = "SELECT * FROM KULLANICILAR WHERE ID=@ID";
            qrySorgu.Parameters.Clear();
            qrySorgu.Parameters.AddWithValue("ID", recordID);
            SqlDataReader objDataReader = qrySorgu.ExecuteReader();
            objDataReader.Read();

            txtKullaniciAdi.Text = objDataReader["KULLANICI_ADI"].ToString();
            txtKullaniciSifre.Text = objDataReader["KULLANICI_SIFRE"].ToString();
            int BirimSira = Convert.ToInt32(objDataReader["KULLANICI_BIRIM_ID"].ToString());
            conn.comboDoldur(cmbBirim);
            cmbBirim.SelectedIndex = BirimSira - 1;
        }

        private void yok_Click(object sender, EventArgs e)
        {
            conn.kayitGuncelle(txtKullaniciAdi.Text, txtKullaniciSifre.Text, cmbBirim.SelectedIndex + 1, recordID);
            frmMain formMain = (frmMain)Application.OpenForms["frmMain"];
            formMain.externalGridLoad();
            this.Close();
        
        }
    }
}
