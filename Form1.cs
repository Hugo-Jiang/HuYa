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
using Newtonsoft.Json;

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
            
            //System.Diagnostics.Debug.WriteLine(Directory.GetCurrentDirectory());
            //System.Diagnostics.Debug.WriteLine($@"DataSource={Directory.GetCurrentDirectory()}\type.db");
        }

        //分类解析
        private void GetSqliteType()
        {
            SQLiteConnection ThisConnectSqlite = new SQLiteConnection();
            try
            {
                ThisConnectSqlite = new SQLiteConnection($@"DataSource={Directory.GetCurrentDirectory()}\type.db");
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
            }
            ThisConnectSqlite.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxItem TypeList = (ComboBoxItem)comboBox1.Items[comboBox1.SelectedIndex];
            GetAnchorList(TypeList.Value.ToString(),1);
            //MessageBox.Show(TypeList.Value.ToString());
        }

        private void GetAnchorList(string gameId, int page)
        {
            string html = string.Empty;
            string url = $"https://www.huya.com/cache.php?m=LiveList&do=getLiveListByPage&gameId={gameId}&tagAll=0&page={page}";
            html = this.Get(url, "www.huya.com");
            Rootobject rootobject = JsonConvert.DeserializeObject<Rootobject>(html);
            Data1[] datas = rootobject.data.datas;
            foreach(var item in datas)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Text = item.nick.ToString();
                listViewItem.SubItems.Add(item.roomName);
                listViewItem.SubItems.Add(item.liveChannel);
                listViewItem.SubItems.Add(item.profileRoom);
                listView1.Items.Add(listViewItem);
                //System.Diagnostics.Debug.WriteLine(item.nick);
            }
            //MessageBox.Show(data1.nick.ToString());
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.GetSqliteType();
        }
    }
}
