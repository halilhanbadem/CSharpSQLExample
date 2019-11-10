///No architecture was used. All transactions are coded in this class!
///Dev. Halil Han Badem

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CSharpSQLExample
{
    public class connectionData
    {
       public SqlConnection sqlConnector = new SqlConnection();
       public SqlCommand sqlQuery = new SqlCommand();


        string SQLConnectionString = @"Data Source=POLICE\HALILHAN; Initial Catalog=SQLExample; Integrated Security=SSPI";

        public void dbBaglan()
        {
            if (sqlConnector.State == ConnectionState.Open)
            {
                dbBaglantiKes();
            }
            sqlConnector.ConnectionString = SQLConnectionString;
            sqlConnector.Open();
        }

        public void dbBaglantiKes()
        {
            sqlConnector.Close();
        }



        public bool kullaniciKontrol(string kullaniciadi, string kullanicisifre)
        {
            dbBaglan();
            sqlQuery.Connection = sqlConnector;
            sqlQuery.CommandText = "SELECT count(*) as sayi FROM KULLANICILAR WHERE KULLANICI_ADI=@KULLANICIADI AND KULLANICI_SIFRE=@KULLANICISIFRE";
            sqlQuery.Parameters.Clear();
            sqlQuery.Parameters.AddWithValue("KULLANICIADI", kullaniciadi);
            sqlQuery.Parameters.AddWithValue("KULLANICISIFRE", kullanicisifre);
            SqlDataReader qryResult = sqlQuery.ExecuteReader();


            if (qryResult.Read())
            {
                if (Convert.ToInt32(qryResult["sayi"].ToString()) == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void comboDoldur(ComboBox comboName)
        {
            dbBaglan();
            sqlQuery.Connection = sqlConnector;
            sqlQuery.CommandText = "SELECT * FROM BIRIMLER";
            SqlDataReader qryResult = sqlQuery.ExecuteReader();

            while (qryResult.Read())
            {
                comboName.Items.Add(qryResult["BIRIM_ADI"].ToString());
            }

            comboName.SelectedItem = 0;
        }

        public void kayitEkle(string kullaniciAdi, string kullaniciSifre, DateTime kayitTarihi, string kullaniciBirim)
        {
            try
            {
                dbBaglan();
                sqlQuery.Connection = sqlConnector;
                sqlQuery.Parameters.Clear();
                sqlQuery.CommandText = "INSERT INTO KULLANICILAR(KULLANICI_ADI, " +
                    "KULLANICI_SIFRE, KULLANICI_KAYIT_TARIHI, KULLANICI_BIRIM_ID)" +
                    "VALUES(@KULLANICI_ADI, @KULLANICI_SIFRE, @KULLANICI_KAYIT_TARIHI," +
                    " @KULLANICI_BIRIM)";
                sqlQuery.Parameters.AddWithValue("KULLANICI_ADI", kullaniciAdi);
                sqlQuery.Parameters.AddWithValue("KULLANICI_SIFRE", kullaniciSifre);
                sqlQuery.Parameters.AddWithValue("KULLANICI_KAYIT_TARIHI", kayitTarihi);
                sqlQuery.Parameters.AddWithValue("KULLANICI_BIRIM", kullaniciBirim);
                sqlQuery.ExecuteNonQuery();
                dbBaglantiKes();

                MessageBox.Show("Kayıt başarılı bir şekilde eklendi!", "Başarılı!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception hataMesaji)
            {
                MessageBox.Show("Hata mevcut. Hata açıklaması: " + hataMesaji.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        public void kayitListele(DataGridView gridName)
        {
            dbBaglan();
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("SELECT KULLANICILAR.ID, " +
                " KULLANICILAR.KULLANICI_ADI, KULLANICILAR.KULLANICI_SIFRE," +
                " KULLANICILAR.KULLANICI_KAYIT_TARIHI, " +
                "BIRIMLER.BIRIM_ADI FROM KULLANICILAR INNER JOIN BIRIMLER ON " +
                "BIRIMLER.ID = KULLANICILAR.KULLANICI_BIRIM_ID", sqlConnector);

            DataTable Table = new DataTable();
            sqlAdapter.Fill(Table);
            sqlAdapter.Update(Table);
            gridName.DataSource = Table;
            sqlAdapter.Dispose();
            dbBaglantiKes();
        }

        public void kayitSil(int recordID)
        {
            try { 
            dbBaglan();
            sqlQuery.CommandText = "DELETE FROM KULLANICILAR WHERE ID=@ID";
                sqlQuery.Parameters.Clear();
            sqlQuery.Parameters.AddWithValue("ID", recordID);
            sqlQuery.ExecuteNonQuery();
                MessageBox.Show("Kayıt silme işlemi başarılı bir şekilde gerçekleşti!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dbBaglantiKes();
            } catch (Exception mesaj)
            {
                MessageBox.Show("Kayıt silme işlemi gerçekleşirken hata oluştu! Hata: " + mesaj.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbBaglantiKes();
            }
        }

        public void kayitGuncelle(string kullaniciAdi, string kullaniciSifre, int kullaniciBirim, int recordId)
        {
            try
            {
                dbBaglan();
                sqlQuery.CommandText = "UPDATE KULLANICILAR SET KULLANICI_ADI=@KULLANICI_ADI, KULLANICI_SIFRE=@KULLANICI_SIFRE, KULLANICI_BIRIM_ID=@KULLANICI_BIRIM_ID WHERE ID=@ID";
                sqlQuery.Parameters.Clear();
                sqlQuery.Parameters.AddWithValue("KULLANICI_ADI", kullaniciAdi);
                sqlQuery.Parameters.AddWithValue("KULLANICI_SIFRE", kullaniciSifre);
                sqlQuery.Parameters.AddWithValue("KULLANICI_BIRIM_ID", kullaniciBirim);
                sqlQuery.Parameters.AddWithValue("ID", recordId);
                sqlQuery.ExecuteNonQuery();
                dbBaglantiKes();
                MessageBox.Show("Kayıt güncelleme işlemi başarıyla gerçekleşti!", "Kayıt Güncelleme Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception mesaj)
            {
                MessageBox.Show("Kayıt güncellenirken hata oluştu!" + mesaj.Message, "Hata!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dbBaglantiKes(); 
            }
        }
    }
}
