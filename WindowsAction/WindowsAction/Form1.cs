using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsAction
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon1.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            this.notifyIcon1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int hour = (int)numericUpDown_hour.Value;
            int min = (int)numericUpDown_min.Value;
            int sec = (int)numericUpDown_sec.Value;
            double interval = hour * 3600 + min * 60 + sec;

            btnShutDown_Click(interval);
        }

        private void numericUpDown_hour_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_hour.Value == 24)
                numericUpDown_hour.Value = 0;
            if (numericUpDown_hour.Value == -1)
                numericUpDown_hour.Value = 23;
        }

        private void numericUpDown_min_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_min.Value == 60)
                numericUpDown_min.Value = 0;
            if (numericUpDown_min.Value == -1)
                numericUpDown_min.Value = 59;
        }

        private void numericUpDown_sec_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown_sec.Value == 60)
                numericUpDown_sec.Value = 0;
            if (numericUpDown_sec.Value == -1)
                numericUpDown_sec.Value = 59;
        }

        //关机　和　计时关机
        private void btnShutDown_Click(double interval)
        {
            if (MessageBox.Show("将要设定计划关机，是否确认操作？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                string str = interval.ToString();
                Process.Start("shutdown.exe", "-s -t " + str);//计时关机
            }
            //if (MessageBox.Show("是否确认关机？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //{
            //    Process.Start("shutdown.exe", "-s");     //立即关机
            //}
        }

        //重启
        private void butRestar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认重启？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Process.Start("shutdown.exe", "-r");        //关机并重启
                Process.Start("shutdown.exe", "-r -t 10");  //定时重启
            }
        }

        //注销
        private void butLogOff_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认注销？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                Process.Start("shutdown.exe", "-l");    //注销
        }
    }
}
