using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ti_2tr
{
    public partial class Form1 : Form
    {
        public const int maxViewLengthD2 = 50;
        Cipher cip = new Cipher();
        Coder cod = new Coder();
        byte[] data;
        byte[] key;
        public Form1()
        { 
            InitializeComponent();    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lblCount.Text != "34")
                MessageBox.Show("Ключ должен состоять из 34 единиц и нулей");
            else
            {
                Int64 k = 0;
                for (int i = 0; i<tbInsKey.Text.Length; i++)
                    if (tbInsKey.Text[i] == '0' || tbInsKey.Text[i] == '1')
                        k = (k << 1) + (tbInsKey.Text[i] - '0');
                if (tbText.Text.Length < maxViewLengthD2*2)
                    data = cod.BinaryToBytes(tbText.Text);
                key = cip.EnCrypt(data, k);
                UpdateTextBox();
            }
        }
        public void UpdateTextBox()
        {
            if (key != null)
            {
                if (key.Length <= maxViewLengthD2 * 2)
                {
                    tbKey.Text = cod.BytesToBinary(key);
                    tbKey.Enabled = false;
                }
                else
                {
                    tbKey.Enabled = false;
                    byte[] tempArr1 = new byte[maxViewLengthD2];
                    byte[] tempArr2 = new byte[maxViewLengthD2];
                    Array.Copy(key, tempArr1, maxViewLengthD2);
                    Array.Copy(key, key.Length - maxViewLengthD2, tempArr2, 0, maxViewLengthD2);
                    tbKey.Text = cod.BytesToBinary(tempArr1) + "...\n" + cod.BytesToBinary(tempArr2);
                }

                if (data.Length <= maxViewLengthD2 * 2)
                {
                    textBox1.Text = cod.BytesToBinary(data);
                    textBox1.Enabled = true;
                }
                else
                {
                    textBox1.Enabled = false;
                    byte[] tempArr1 = new byte[maxViewLengthD2];
                    byte[] tempArr2 = new byte[maxViewLengthD2];
                    Array.Copy(data, tempArr1, maxViewLengthD2);
                    Array.Copy(data, data.Length - maxViewLengthD2, tempArr2, 0, maxViewLengthD2);
                    textBox1.Text = cod.BytesToBinary(tempArr1) + "...\n" + cod.BytesToBinary(tempArr2);
                }
            }
            else if (data.Length <= maxViewLengthD2 * 2)
            {
                tbText.Text = cod.BytesToBinary(data);
                tbText.Enabled = true;
            }
            else
            {
                tbText.Enabled = false;
                byte[] tempArr1 = new byte[maxViewLengthD2];
                byte[] tempArr2 = new byte[maxViewLengthD2];
                Array.Copy(data, tempArr1, maxViewLengthD2);
                Array.Copy(data, data.Length - maxViewLengthD2, tempArr2, 0, maxViewLengthD2);
                tbText.Text = cod.BytesToBinary(tempArr1) + "...\n" + cod.BytesToBinary(tempArr2);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                data = (File.ReadAllBytes(openFileDialog1.FileName));
                UpdateTextBox();
            }
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar != '0') && (e.KeyChar != '1'))
                e.Handled = true;
        }

        private void tbInsKey_TextChanged(object sender, EventArgs e)
        {
            int count = 0;
            for (int i = 0; i < tbInsKey.Text.Length; i++)
                if (tbInsKey.Text[i] == '0' || tbInsKey.Text[i] == '1')
                    count++;
            lblCount.Text = count.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (data != null)
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveFileDialog1.FileName, data);
                }
        }
    }
}
