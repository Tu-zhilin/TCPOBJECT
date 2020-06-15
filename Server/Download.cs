using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Download : Form
    {
        private int BlockData;
        public int Now;

        public Download(int BlockData)
        {
            InitializeComponent();
            Process.Maximum = BlockData;
            this.BlockData = BlockData;
            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Process.Value = BlockData - Now;
        }
    }
}
