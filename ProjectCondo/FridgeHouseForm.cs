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
    public partial class FridgeHouseForm : Form
    {
        FireBaseHandler fbHandler = new FireBaseHandler();

        public FridgeHouseForm()
        {
            InitializeComponent();
            fbHandler.CreateFireBaseClient();
        }
    
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddToFridge form = new AddToFridge();
            form.Show();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddToHouseHoldForm form = new AddToHouseHoldForm();
            form.Show();
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {            
            GetTableData();
        }

        private async void GetTableData()
        {
            FridgeList.Items.Clear();
            HouseholdList.Items.Clear();

            fbHandler.CreateFireBaseClient();

            List<FridgeData> fridge = await fbHandler.GetFridgeData();
            List<HouseData> house = await fbHandler.GetHouseHoldData();

            foreach (FridgeData tmpFD in fridge)
            {
                ListViewItem item = new ListViewItem();
                item.Text = tmpFD.ID;
                item.SubItems.Add(tmpFD.Item);
                item.SubItems.Add(tmpFD.Quantity);
                item.SubItems.Add(tmpFD.Expiration);
                FridgeList.Items.Add(item);
            }

            foreach (HouseData tmpFD in house)
            {
                ListViewItem item = new ListViewItem();
                item.Text = tmpFD.ID;
                item.SubItems.Add(tmpFD.Item);
                item.SubItems.Add(tmpFD.Quantity);
                HouseholdList.Items.Add(item);
            }
        }
    }
}
