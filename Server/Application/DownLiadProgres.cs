using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class DownLoadProgres
    {
        public EventHandler SetUI;
        Download downloadform = new Download(100);

        public DownLoadProgres()
        {
            //SetUI += Set;
        }

        public void Set(object sender, EventArgs e)
        {
            
            downloadform.Show();
        }


    }
}
