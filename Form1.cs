using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Collections;
using ComboBoxNew;

namespace HuYa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string Get(string url, string host)
        {
            string html = string.Empty;

            HttpWebRequest http = WebRequest.CreateHttp(url);
            http.Method = "GET";
            http.Host = host;
            http.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1";
            http.ProtocolVersion = HttpVersion.Version10;

            HttpWebResponse response = http.GetResponse() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
            {
                html = reader.ReadToEnd();
            }
            responseStream.Close();
            response.Close();

            return html;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string Html = this.Get("https://m.huya.com/lpl", "m.huya.com");
            //System.Diagnostics.Debug.WriteLine(Html);
            this.GetSqliteType();
        }

        //分类解析
        private void GetSqliteType()
        {
            SQLiteConnection ThisConnectSqlite = new SQLiteConnection();
            try
            {
                ThisConnectSqlite = new SQLiteConnection(@"DataSource=\type.db");
                ThisConnectSqlite.Open();
            }catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            DataTable DataTable = new DataTable();
            SQLiteDataAdapter res = new SQLiteDataAdapter("select * from type", ThisConnectSqlite);
            res.Fill(DataTable);
            if(DataTable.Rows.Count > 0)
            {
                foreach (DataRow item in DataTable.Rows)
                {
                    ComboBoxItem TypeList = new ComboBoxItem();
                    TypeList.Text = item["gameName"].ToString();
                    TypeList.Value = item["gameId"].ToString();
                    comboBox1.Items.Add(TypeList);
                }
                comboBox1.DisplayMember = "Text";
                comboBox1.ValueMember = "Value";
                comboBox1.SelectedIndex = 0;
            }
            ThisConnectSqlite.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem TypeList = (ComboBoxItem)comboBox1.Items[comboBox1.SelectedIndex];
            MessageBox.Show(TypeList.Value.ToString());
        }
    }
}
