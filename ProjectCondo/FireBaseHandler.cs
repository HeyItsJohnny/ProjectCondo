using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace ProjectCondo
{

    class FireBaseHandler
    {

        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "QhSQnrJgML1rCglFrLhI4meR9gJybFRXXvGC63sj",
            BasePath = "https://home-automation-49729.firebaseio.com/"
        };
        IFirebaseClient client;        

        public void CreateFireBaseClient()
        {
            client = new FireSharp.FirebaseClient(config);
        }

        public async void SendFridgeData(String ItmTxt, String QtyTxt, String ExpirTxt)
        {
            var data = new FridgeData
            {
                ID = await GetCounter("Fridge"),
                Item = ItmTxt,
                Quantity = QtyTxt,
                Expiration = ExpirTxt
            };

            SetResponse response = await client.SetTaskAsync("Fridge/" + data.ID, data);
            SetNewCounter(data.ID, "Fridge");
        }

        public async void SendHouseData(String ItmTxt, String QtyTxt)
        { 
            var data = new HouseData
            {
                ID = await GetCounter("House"),
                Item = ItmTxt,
                Quantity = QtyTxt
            };

            SetResponse response = await client.SetTaskAsync("House/" + data.ID, data);
            SetNewCounter(data.ID, "House");
        }

        public async Task<List<FridgeData>> GetFridgeData()
        {
            List<FridgeData> fd = new List<FridgeData>();

            int i = 0;
            FirebaseResponse resp1 = await client.GetTaskAsync("Counter/Fridge");
            Counter_Class obj1 = resp1.ResultAs<Counter_Class>();

            int cnt = Convert.ToInt32(obj1.cnt);

            while(true)
            {
                if (i == cnt)
                {
                    break;
                }

                i++;
                try
                {
                    FirebaseResponse resp2 = await client.GetTaskAsync("Fridge/" + i);
                    FridgeData obj2 = resp2.ResultAs<FridgeData>();
                    fd.Add(obj2);
                }
                catch
                {

                }
            }
            return fd;
        }

        public async Task<List<HouseData>> GetHouseHoldData()
        {
            List<HouseData> hd = new List<HouseData>();

            int i = 0;
            FirebaseResponse resp1 = await client.GetTaskAsync("Counter/House");
            Counter_Class obj1 = resp1.ResultAs<Counter_Class>();

            int cnt = Convert.ToInt32(obj1.cnt);

            while (true)
            {
                if (i == cnt)
                {
                    break;
                }

                i++;
                try
                {
                    FirebaseResponse resp2 = await client.GetTaskAsync("House/" + i);
                    HouseData obj2 = resp2.ResultAs<HouseData>();
                    hd.Add(obj2);
                }
                catch
                {

                }
            }
            return hd;
        }

        private async Task<String> GetCounter(String Function)
        {
            String ID = "0";

            FirebaseResponse resp = await client.GetTaskAsync("Counter/" + Function);
            Counter_Class get = resp.ResultAs<Counter_Class>();

            ID = (Convert.ToInt32(get.cnt) + 1).ToString();

            return ID;
        }

        private async void SetNewCounter(String ID, String function)
        {
            var obj = new Counter_Class
            {
                cnt = ID
            };

            SetResponse resp1 = await client.SetTaskAsync("Counter/" + function, obj);
        }
    }
}
