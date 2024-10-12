using System;
using System.Threading;
using System.Windows.Forms;

namespace MultithreadUINoRespond
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void noRespondAction_Click(object sender, EventArgs e)
        {
            LongOperation();
        }

        private void respondAction_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(LongOperation);
            thread.Start();
        }

        private void LongOperation()
        {
            Thread.Sleep(5000);
        }
    }
}
