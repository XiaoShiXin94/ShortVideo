using AxWMPLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace ShortVideo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WindowsMediaPlayer初始化();
            Strat();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public void Strat()
        {
            string api = HttpGet("https://tucdn.wpon.cn/api-girl/index.php?wpon=json");
            var obj = JsonConvert.DeserializeObject<JObject>(api);
            string vdName = obj["mp4"].ToString();
            axWindowsMediaPlayer1.URL = "https:" + vdName;
            button2.Enabled = false;
        }

        public void WindowsMediaPlayer初始化()
        {
            axWindowsMediaPlayer1.enableContextMenu=false;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.uiMode = "None";
            axWindowsMediaPlayer1.settings.volume = 100;
        }
        public static string HttpGet(string url)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/json";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            if ((int)axWindowsMediaPlayer1.playState == 1)
            {
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Strat();
        }

        private void axWindowsMediaPlayer1_ClickEvent(object sender, _WMPOCXEvents_ClickEvent e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.pause();
            button2.Text = "继续";
            button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.play();
            button2.Text = "播放";
            button2.Enabled = false;




        }
    }
}
