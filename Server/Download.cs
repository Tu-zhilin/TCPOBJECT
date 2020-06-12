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
        public int Now = 0;

        public Download(int BlockData)
        {
            this.BlockData = BlockData;
            InitializeComponent();
            Process.Maximum = BlockData;
        }

        public void Change(int data)
        {
            Process.Value = BlockData - data;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Change(Now);
        }
    }
}
