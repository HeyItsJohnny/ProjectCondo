using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace ProjectCondo
{
    public partial class AddToFridge : Form
    {
        FireBaseHandler fbHandler = new FireBaseHandler();

        public AddToFridge()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            fbHandler.CreateFireBaseClient();
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            fbHandler.SendFridgeData(ItemTxt.Text, QuantityTxt.Text, ExpirationTxt.Text);
            this.Close();
        }
    }
}
