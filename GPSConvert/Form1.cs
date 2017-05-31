using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GPSConvert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //經緯度轉度分秒
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.textBox1.Text.Trim()))
                {
                    this.textBox2.Text = LatLngToGPS(this.textBox1.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //度分秒轉經緯度
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.textBox3.Text.Trim()))
                {
                    this.textBox4.Text = GPSToLatLng(this.textBox3.Text.Trim());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //經緯度轉度分秒
        private string LatLngToGPS(string text)
        {
            string result = "";

            string[] ary = text.Trim().Split('.');

            if (ary.Length == 2)
            {
                double degree, minute, second, temp;

                //ex. 21.12345
                if (double.TryParse(ary[0], out degree) && double.TryParse(ary[1], out temp))
                {
                    //取小數位 0.12345
                    temp = temp / System.Math.Pow(10, ary[1].Length);

                    //分只留下整數位
                    minute = Math.Floor(temp * 60);

                    //取分剩下的小數位
                    double temp1 = (temp * 60) - Math.Floor(temp * 60);

                    second = temp1 * 60;

                    result = degree.ToString() + "°" + minute.ToString("00") + "\'" + second.ToString() + "\"";
                }
            }

            return result;
        }

        //度分秒轉經緯度
        private string GPSToLatLng(string text)
        {
            string result = "";

            text = text.Trim();

            //必須有度分秒才可進行轉換
            if (text.IndexOf('°') != -1 && text.IndexOf('\'') != -1 && text.IndexOf('\"') != -1)
            {
                double degree, minute, second;

                //取得度分秒
                if (double.TryParse(text.Split('°')[0], out degree) &&
                    double.TryParse(text.Split('°')[1].Split('\'')[0], out minute) &&
                    double.TryParse(text.Split('°')[1].Split('\'')[1].Split('\"')[0], out second))
                {
                    //x度 y分 z秒 = x + y/60 + z/3600 度

                    result = (degree + (minute / 60) + (second / 3600)).ToString();
                }
            }

            return result;
        }
    }
}
