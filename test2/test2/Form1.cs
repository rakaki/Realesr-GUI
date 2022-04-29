using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;//加入，使用进程类，创建
using System.IO;

namespace test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void ComboBoxForm_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("realesrgan-x4plus");
            comboBox1.Items.Add("realesrgan-x4plus-anime");
            comboBox1.Items.Add("realesrnet-x4plus");
            comboBox1.SelectedIndex = 1;
        }

        static bool RunCmd(string cmdExe, string cmdStr)
        {
            bool result = false;
            try
            {
                using (Process myPro = new Process())
                {
                    //指定启动进程是调用的应用程序和命令行参数
                    ProcessStartInfo psi = new ProcessStartInfo(cmdExe, cmdStr);
                    myPro.StartInfo = psi;
                    myPro.Start();
                    myPro.WaitForExit();
                    result = true;
                }
            }
            catch
            {

            }
            return result;
        }

        static bool RunCmd2(string cmdExe, string cmdStr)
        {
            bool result = false;
            try
            {
                using (Process myPro = new Process())
                {
                    myPro.StartInfo.FileName = "cmd.exe";
                    myPro.StartInfo.UseShellExecute = false;
                    myPro.StartInfo.RedirectStandardInput = true;
                    myPro.StartInfo.RedirectStandardOutput = true;
                    myPro.StartInfo.RedirectStandardError = true;
                    myPro.StartInfo.CreateNoWindow = true;
                    myPro.Start();
                    //如果调用程序路径中有空格时，cmd命令执行失败，可以用双引号括起来 ，在这里两个引号表示一个引号（转义）
                    string str = string.Format(@"""{0}"" {1} {2}", cmdExe, cmdStr, "&exit");

                    myPro.StandardInput.WriteLine(str);
                    myPro.StandardInput.AutoFlush = true;
                    myPro.WaitForExit();

                    result = true;
                }
            }
            catch
            {

            }
            return result;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //label1.Text = string.Format(@"""{0}""", label1.Text);
            string strInput = " -i " + label1.Text + " " + "-o " + label2.Text+ " " + "-n " + comboBox1.SelectedItem;
            Console.WriteLine(strInput);
            string exe = @".\realesr\realesrgan-ncnn-vulkan.exe";
            RunCmd(exe, strInput);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string filename = "";
            string outdir = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = dialog.FileName;
            }

            if(filename != "")
            {
                label1.Text = filename;
                label2.Text = Path.GetExtension(filename);
                outdir = filename.Substring(0, label1.Text.Length - label2.Text.Length) + "_1" + ".png";
                label2.Text = string.Format(@"""{0}""", outdir);
                pictureBox1.Image = Image.FromFile(@filename);
                label1.Text = string.Format(@"""{0}""", label1.Text);
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("realesrgan-x4plus");
            comboBox1.Items.Add("realesrgan-x4plus-anime");
            comboBox1.Items.Add("realesrnet-x4plus"); 
            comboBox1.Items.Add("realesr-animevideov3");
            comboBox1.SelectedIndex = 1;
        }
    }
}
