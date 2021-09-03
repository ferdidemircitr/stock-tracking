﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stoktakip
{
    public partial class frmMüşteriListele : Form
    {
        public frmMüşteriListele()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=MSI;Initial Catalog=Stok_Takip;Integrated Security=True");
        DataSet dasett = new DataSet();
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void frmMüşteriListele_Load(object sender, EventArgs e)
        {
            Kayıt_Göster();
        }

        private void Kayıt_Göster()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from müşteri", baglanti);
            adtr.Fill(dasett, "müşteri");
            dataGridView1.DataSource = dasett.Tables["müşteri"];
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtTc.Text = dataGridView1.CurrentRow.Cells["tc"].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells["adsoyad"].Value.ToString();
            txtTelefon.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells["email"].Value.ToString();
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update müşteri set adsoyad=@adsoyad,telefon=@telefon,adres=@adres,email=@email where tc=@tc",baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@email", txtEmail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            dasett.Tables["müşteri"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Müşteri kaydı güncellendi");
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }

            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from müşteri where tc='"+dataGridView1.CurrentRow.Cells["tc"].Value.ToString()+"'",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            dasett.Tables["müşteri"].Clear();
            Kayıt_Göster();
            MessageBox.Show("Kayıt silindi");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select *from müşteri where tc like '%"+ txtTcAra.Text+"%'",baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
    }
}
