using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectCondo
{
    public partial class AddToHouseHoldForm : Form
    {
        FireBaseHandler fbHandler = new FireBaseHandler();

        public AddToHouseHoldForm()
        {
            InitializeComponent();
            fbHandler.CreateFireBaseClient();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            fbHandler.SendHouseData(ItemTxt.Text, QuantityTxt.Text);
            this.Close();
        }
    }
}
