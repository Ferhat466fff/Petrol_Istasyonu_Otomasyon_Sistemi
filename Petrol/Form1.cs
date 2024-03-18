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

namespace Petrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=monster;Initial Catalog=Benzin;Integrated Security=True");

        void listele()
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Tbl_Benzin where PERTOLTUR='Kurşunsuz95'", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbl_Kurşunsuz95.Text = dr[3].ToString();//dr[3]-->fiyatı tutuyor o yuzden oyle cektik
                progressBar1.Value = int.Parse(dr[4].ToString());
                lbl_Depo_Kursunsuz95.Text = progressBar1.Value.ToString();

            }
            baglanti.Close();


            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select * from Tbl_Benzin where PERTOLTUR='Kurşunsuz97'", baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while (dr2.Read())
            {
                lbl_Kurşunsuz97.Text = dr2[3].ToString();
                progressBar2.Value = int.Parse(dr2[4].ToString());
                lbl_Depo_Kursunsuz97.Text = progressBar2.Value.ToString();

            }
            baglanti.Close();


            baglanti.Open();
            SqlCommand komut3 = new SqlCommand("select * from Tbl_Benzin where PERTOLTUR='V/Pro\r\nDiesel'", baglanti);
            SqlDataReader dr3 = komut3.ExecuteReader();
            while (dr3.Read())
            {
                Lbl_Dizel.Text = dr3[3].ToString();
                progressBar3.Value = int.Parse(dr3[4].ToString());
                lbl_Depo_Dizel.Text = progressBar3.Value.ToString();
            }
            baglanti.Close();



            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("select * from Tbl_Benzin where PERTOLTUR='PO/gaz\r\nOtogaz'", baglanti);
            SqlDataReader dr4 = komut4.ExecuteReader();
            while (dr4.Read())
            {
                lbl_Gaz_Otogaz.Text = dr4[3].ToString();
                progressBar4.Value = int.Parse(dr4[4].ToString());
                lbl_Depo_Gaz.Text = progressBar4.Value.ToString();

            }
            baglanti.Close();

            baglanti.Open();
            SqlCommand komut10 = new SqlCommand("select * from Tbl_Kasa", baglanti);
            SqlDataReader dr10 = komut10.ExecuteReader();
            while(dr10.Read())
            {
                lbl_Kasa.Text = dr10[0].ToString();
            }
            baglanti.Close();



        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            double tutar, Kursunsuz95,litre;
            Kursunsuz95 =Convert.ToDouble(lbl_Kurşunsuz95.Text);
            litre = Convert.ToDouble(numericUpDown1.Value);
            tutar = Kursunsuz95 * litre;
            txt_Kursunusz95_fiyat.Text = tutar.ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

            double tutar, Kursunsuz97, litre;
            Kursunsuz97 = Convert.ToDouble(lbl_Kurşunsuz97.Text);
            litre = Convert.ToDouble(numericUpDown2.Value);
            tutar = Kursunsuz97 * litre;
            txt_Kursunsuz97_Fiyat.Text = tutar.ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

            double tutar, dizel, litre;
            dizel = Convert.ToDouble(Lbl_Dizel.Text);
            litre = Convert.ToDouble(numericUpDown3.Value);
            tutar = dizel * litre;
            txt_Dizel_fiyat.Text = tutar.ToString();
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            double tutar, gaz, litre;
            gaz= Convert.ToDouble(lbl_Gaz_Otogaz.Text);
            litre = Convert.ToDouble(numericUpDown4.Value);
            tutar = gaz * litre;
            txt_Gaz_fiyat.Text = tutar.ToString();
        }

        private void btn_Doldur_Click(object sender, EventArgs e)
        {
            if(numericUpDown1.Value!=0)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("INSERT INTO [dbo].[Tbl_Hareket] (PLAKA,BENZINTURU,LITRE,FIYAT)VALUES(@p1,@p2,@p3,@p4)", baglanti);
                komut.Parameters.AddWithValue("@p1", txt_Plaka.Text);
                komut.Parameters.AddWithValue("@p2", "Kurşunsuz 95");
                komut.Parameters.AddWithValue("@p3", numericUpDown1.Value);
                komut.Parameters.AddWithValue("@p4",decimal.Parse( txt_Kursunusz95_fiyat.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut2 = new SqlCommand("update Tbl_Kasa set MIKTAR=MIKTAR+@p1", baglanti);
                komut2.Parameters.AddWithValue("@p1", decimal.Parse(txt_Kursunusz95_fiyat.Text));
                komut2.ExecuteNonQuery();
          
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("update Tbl_Benzin set STOK=STOK-@p1 WHERE PERTOLTUR='Kurşunsuz95'", baglanti);
                komut3.Parameters.AddWithValue("@p1", numericUpDown1.Value);
                komut3.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış yapıldı");
                baglanti.Close();
                listele();



            }
            if (numericUpDown2.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut3 = new SqlCommand("INSERT INTO [dbo].[Tbl_Hareket] (PLAKA,BENZINTURU,LITRE,FIYAT)VALUES(@p1,@p2,@p3,@p4)", baglanti);
                komut3.Parameters.AddWithValue("@p1", txt_Plaka.Text);
                komut3.Parameters.AddWithValue("@p2", "Kurşunsuz 97");
                komut3.Parameters.AddWithValue("@p3", numericUpDown2.Value);
                komut3.Parameters.AddWithValue("@p4", decimal.Parse(txt_Kursunsuz97_Fiyat.Text));
                komut3.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut4 = new SqlCommand("update Tbl_Kasa set MIKTAR=MIKTAR+@p1", baglanti);
                komut4.Parameters.AddWithValue("@p1", decimal.Parse(txt_Kursunsuz97_Fiyat.Text));
                komut4.ExecuteNonQuery();

                baglanti.Close();

                baglanti.Open();
                SqlCommand komut5 = new SqlCommand("update Tbl_Benzin set STOK=STOK-@p1 WHERE PERTOLTUR='Kurşunsuz97'", baglanti);
                komut5.Parameters.AddWithValue("@p1", numericUpDown2.Value);
                komut5.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış yapıldı");
                baglanti.Close();
                listele();



            }

            if (numericUpDown3.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut6 = new SqlCommand("INSERT INTO [dbo].[Tbl_Hareket] (PLAKA,BENZINTURU,LITRE,FIYAT)VALUES(@p1,@p2,@p3,@p4)", baglanti);
                komut6.Parameters.AddWithValue("@p1", txt_Plaka.Text);
                komut6.Parameters.AddWithValue("@p2", "V/Pro\r\nDiesel");
                komut6.Parameters.AddWithValue("@p3", numericUpDown3.Value);
                komut6.Parameters.AddWithValue("@p4", decimal.Parse(txt_Dizel_fiyat.Text));
                komut6.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut7 = new SqlCommand("update Tbl_Kasa set MIKTAR=MIKTAR+@p1", baglanti);
                komut7.Parameters.AddWithValue("@p1", decimal.Parse(txt_Dizel_fiyat.Text));
                komut7.ExecuteNonQuery();

                baglanti.Close();

                baglanti.Open();
                SqlCommand komut8 = new SqlCommand("update Tbl_Benzin set STOK=STOK-@p1 WHERE PERTOLTUR='V/Pro\r\nDiesel'", baglanti);
                komut8.Parameters.AddWithValue("@p1", numericUpDown3.Value);
                komut8.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış yapıldı");
                baglanti.Close();
                listele();



            }

            if (numericUpDown4.Value != 0)
            {
                baglanti.Open();
                SqlCommand komut9 = new SqlCommand("INSERT INTO [dbo].[Tbl_Hareket] (PLAKA,BENZINTURU,LITRE,FIYAT)VALUES(@p1,@p2,@p3,@p4)", baglanti);
                komut9.Parameters.AddWithValue("@p1", txt_Plaka.Text);
                komut9.Parameters.AddWithValue("@p2", "PO/gaz\r\nOtogaz");
                komut9.Parameters.AddWithValue("@p3", numericUpDown4.Value);
                komut9.Parameters.AddWithValue("@p4", decimal.Parse(txt_Gaz_fiyat.Text));
                komut9.ExecuteNonQuery();
                baglanti.Close();

                baglanti.Open();
                SqlCommand komut10 = new SqlCommand("update Tbl_Kasa set MIKTAR=MIKTAR+@p1", baglanti);
                komut10.Parameters.AddWithValue("@p1", decimal.Parse(txt_Gaz_fiyat.Text));
                komut10.ExecuteNonQuery();

                baglanti.Close();

                baglanti.Open();
                SqlCommand komut11= new SqlCommand("update Tbl_Benzin set STOK=STOK-@p1 WHERE PERTOLTUR='PO/gaz\r\nOtogaz'", baglanti);
                komut11.Parameters.AddWithValue("@p1", numericUpDown4.Value);
                komut11.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Satış yapıldı");
                baglanti.Close();
                listele();



            }



        }

        private void txt_Benzin_Ekle_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void btn_97_ekleme_Click(object sender, EventArgs e)
        {
            try
            {
                int ekle = Convert.ToInt32(txt_Kursunsuz95_Ekle.Text);
                int kasadus = ekle * 20;

                // Progress bar'ı güncelle
                progressBar1.Value = Math.Min(progressBar1.Maximum, progressBar1.Value + ekle);

                // Benzini stoğa ekle
                baglanti.Open();
                SqlCommand updateCommand = new SqlCommand("UPDATE Tbl_Benzin SET STOK = STOK + @p1 WHERE PERTOLTUR = 'Kurşunsuz95'", baglanti);
                updateCommand.Parameters.AddWithValue("@p1", ekle);
                updateCommand.ExecuteNonQuery();
                baglanti.Close();

                // Alış fiyatını güncelle
                baglanti.Open();
                SqlCommand updateAlisFiyatCommand = new SqlCommand("UPDATE Tbl_Benzin SET ALISFIYAT = @p5 WHERE PERTOLTUR = 'Kurşunsuz95'", baglanti);
                updateAlisFiyatCommand.Parameters.AddWithValue("@p5", txt_Kursunsuz95_Ekle.Text);
                updateAlisFiyatCommand.ExecuteNonQuery();
                baglanti.Close();

                // Kasadan ekle değerini çıkar
                baglanti.Open();
                SqlCommand komut10 = new SqlCommand("UPDATE Tbl_Kasa SET MIKTAR = MIKTAR - @p1", baglanti);
                komut10.Parameters.AddWithValue("@p1", kasadus);
                komut10.ExecuteNonQuery();
                baglanti.Close();

                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btn_Ekle97_Click(object sender, EventArgs e)
        {
            try
            {
                int ekle = Convert.ToInt32(txt_Kursunsuz97_Ekle.Text);
                int kasadus = ekle * 21;

                // Progress bar'ı güncelle
                progressBar2.Value = Math.Min(progressBar2.Maximum, progressBar2.Value + ekle);

                // Benzini stoğa ekle
                baglanti.Open();
                SqlCommand updateCommand = new SqlCommand("UPDATE Tbl_Benzin SET STOK = STOK + @p1 WHERE PERTOLTUR = 'Kurşunsuz97'", baglanti);
                updateCommand.Parameters.AddWithValue("@p1", ekle);
                updateCommand.ExecuteNonQuery();
                baglanti.Close();

                // Alış fiyatını güncelle
                baglanti.Open();
                SqlCommand updateAlisFiyatCommand = new SqlCommand("UPDATE Tbl_Benzin SET ALISFIYAT = @p5 WHERE PERTOLTUR = 'Kurşunsuz97'", baglanti);
                updateAlisFiyatCommand.Parameters.AddWithValue("@p5", txt_Kursunsuz97_Ekle.Text);
                updateAlisFiyatCommand.ExecuteNonQuery();
                baglanti.Close();

                // Kasadan ekle değerini çıkar
                baglanti.Open();
                SqlCommand komut10 = new SqlCommand("UPDATE Tbl_Kasa SET MIKTAR = MIKTAR - @p1", baglanti);
                komut10.Parameters.AddWithValue("@p1", kasadus);
                komut10.ExecuteNonQuery();
                baglanti.Close();

                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btn_ekle_dızel_Click(object sender, EventArgs e)
        {
            try
            {
                int ekle = Convert.ToInt32(txt_Dızel_ekle.Text);
                int kasadus = ekle * 21;

                // Progress bar'ı güncelle
                progressBar3.Value = Math.Min(progressBar3.Maximum, progressBar3.Value + ekle);

                // Benzini stoğa ekle
                baglanti.Open();
                SqlCommand updateCommand = new SqlCommand("UPDATE Tbl_Benzin SET STOK = STOK + @p1 WHERE PERTOLTUR = 'V/Pro\r\nDiesel'", baglanti);
                updateCommand.Parameters.AddWithValue("@p1", ekle);
                updateCommand.ExecuteNonQuery();
                baglanti.Close();

                // Alış fiyatını güncelle
                baglanti.Open();
                SqlCommand updateAlisFiyatCommand = new SqlCommand("UPDATE Tbl_Benzin SET ALISFIYAT = @p5 WHERE PERTOLTUR = 'V/Pro\r\nDiesel'", baglanti);
                updateAlisFiyatCommand.Parameters.AddWithValue("@p5", txt_Dızel_ekle.Text);
                updateAlisFiyatCommand.ExecuteNonQuery();
                baglanti.Close();

                // Kasadan ekle değerini çıkar
                baglanti.Open();
                SqlCommand komut10 = new SqlCommand("UPDATE Tbl_Kasa SET MIKTAR = MIKTAR - @p1", baglanti);
                komut10.Parameters.AddWithValue("@p1", kasadus);
                komut10.ExecuteNonQuery();
                baglanti.Close();

                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btn_Ekle_Gaz_Click(object sender, EventArgs e)
        {
            try
            {
                int ekle = Convert.ToInt32(txt_Gaz_Ekle.Text);
                int kasadus = ekle * 10;

                // Progress bar'ı güncelle
                progressBar4.Value = Math.Min(progressBar4.Maximum, progressBar4.Value + ekle);

                // Benzini stoğa ekle
                baglanti.Open();
                SqlCommand updateCommand = new SqlCommand("UPDATE Tbl_Benzin SET STOK = STOK + @p1 WHERE PERTOLTUR = 'PO/gaz\r\nOtogaz'", baglanti);
                updateCommand.Parameters.AddWithValue("@p1", ekle);
                updateCommand.ExecuteNonQuery();
                baglanti.Close();

                // Alış fiyatını güncelle
                baglanti.Open();
                SqlCommand updateAlisFiyatCommand = new SqlCommand("UPDATE Tbl_Benzin SET ALISFIYAT = @p5 WHERE PERTOLTUR = 'PO/gaz\r\nOtogaz'", baglanti);
                updateAlisFiyatCommand.Parameters.AddWithValue("@p5", txt_Gaz_Ekle.Text);
                updateAlisFiyatCommand.ExecuteNonQuery();
                baglanti.Close();

                // Kasadan ekle değerini çıkar
                baglanti.Open();
                SqlCommand komut10 = new SqlCommand("UPDATE Tbl_Kasa SET MIKTAR = MIKTAR - @p1", baglanti);
                komut10.Parameters.AddWithValue("@p1", kasadus);
                komut10.ExecuteNonQuery();
                baglanti.Close();

                listele();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}

