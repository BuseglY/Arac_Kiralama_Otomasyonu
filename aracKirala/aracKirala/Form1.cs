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

namespace aracKirala
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'arabaKiralaDataSet.uye' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.uyeTableAdapter.Fill(this.arabaKiralaDataSet.uye);

        }

        SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-FBHESMKC\\SQLEXPRESS;Initial Catalog=arabaKirala;Integrated Security=True");

        private void btnListele_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("goster",baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand ekle = new SqlCommand("ekle",baglanti);
            ekle.CommandType = CommandType.StoredProcedure;
            ekle.Parameters.AddWithValue("@tc", tcTextBox.Text);
            ekle.Parameters.AddWithValue("@ad",adTextBox.Text);
            ekle.Parameters.AddWithValue("@soyad", soyadTextBox.Text);
            ekle.Parameters.AddWithValue("@tel",telTextBox.Text);
            ekle.Parameters.AddWithValue("@marka", markaComboBox.Text);
            ekle.Parameters.AddWithValue("@seri",seriTextBox.Text);
            ekle.Parameters.AddWithValue("@tarih",tarihTextBox.Text);
            ekle.Parameters.AddWithValue("@gun",gunTextBox.Text);
            ekle.ExecuteNonQuery();
            MessageBox.Show("Ekleme başarılı.");
            baglanti.Close();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("sil",baglanti);
            sil.CommandType = CommandType.StoredProcedure;
            sil.Parameters.AddWithValue("@tc",tcTextBox.Text);
            sil.ExecuteNonQuery();
            baglanti.Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            string tc = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            string ad = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            string soyad = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            string tel= dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            string marka = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            string seri = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            string tarih = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
            string gun = dataGridView1.Rows[secilen].Cells[7].Value.ToString();

            tcTextBox.Text = tc;
            adTextBox.Text = ad;
            soyadTextBox.Text = soyad;
            telTextBox.Text = tel;
            markaComboBox.Text = marka;
            seriTextBox.Text = seri;
            tarihTextBox.Text = tarih;
            gunTextBox.Text = gun;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("guncelle",baglanti);
            guncelle.CommandType = CommandType.StoredProcedure;
            guncelle.Parameters.AddWithValue("@p1",tcTextBox.Text);
            guncelle.Parameters.AddWithValue("@p2",adTextBox.Text);
            guncelle.Parameters.AddWithValue("@p3", soyadTextBox.Text);
            guncelle.Parameters.AddWithValue("@p4", telTextBox.Text);
            guncelle.Parameters.AddWithValue("@p5", markaComboBox.Text);
            guncelle.Parameters.AddWithValue("@p6", seriTextBox.Text);
            guncelle.Parameters.AddWithValue("@p7", tarihTextBox.Text);
            guncelle.Parameters.AddWithValue("@p8", gunTextBox.Text);
            guncelle.ExecuteNonQuery();
            baglanti.Close();
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand hesapla = new SqlCommand("select gun*gunlukFiyat from uye,seri where seri= (select seri where seriAd='"+seriTextBox.Text+"' and tc='"+tcTextBox.Text+"')",baglanti);
            SqlDataReader oku = hesapla.ExecuteReader();
            while (oku.Read())
            {
                MessageBox.Show(oku[0].ToString());
            }

            baglanti.Close();

        }
    }
}
