


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictsManager
{
    class User
    {
        public int ID { get; set; }
        public string VKLINK { get; set; }
        public string FIRSTNAME { get; set; }
        public string SECONDNAME { get; set; }
        public int POINTS { get; set; }
        public int DELTAPOINTS  { get; set; }
        public int PLACE1 { get; set; }
        public int PLACE2 { get; set; }
        public int PLACE3 { get; set; }
        public int PLACE4 { get; set; }
        public int PLACE5 { get; set; }
        public int PLACE6 { get; set; }
        public int PLACE7 { get; set; }
        public int PLACE8 { get; set; }
        public int PLACE9 { get; set; }
        public int PLACE10 { get; set; }
        public int PLACE11 { get; set; }
        public int PLACE12 { get; set; }
        public int PLACE13 { get; set; }
        public int PLACE14 { get; set; }
        public int PLACE15 { get; set; }
        public int PLACE16 { get; set; }
        public int PLACE17 { get; set; }
        public int PLACE18 { get; set; }
        public int PLACE19 { get; set; }
        public int PLACE20 { get; set; }

        public List<int> Places = new List<int>() { };
        public Dictionary<int, string> GetPredictions(List<Player> PlayerList)
        {
            Places.AddRange(new List<int>() { PLACE20, PLACE19, PLACE18, PLACE17, PLACE16, PLACE15, PLACE14, PLACE13, PLACE12, PLACE11, PLACE10, PLACE9, PLACE8, PLACE7, PLACE6, PLACE5, PLACE4, PLACE3, PLACE2, PLACE1 });

            int num = 0;
            Dictionary<int, string> Listening = new Dictionary<int, string>() { };
            for(int i = 20; i > 0; i--)
            {
                Listening.Add(i, PlayerList.Find(x => x.ID == Places[num]).NICKNAME);
                num++;
            }

            return Listening;
        }
        public string Display(List<Player> PlayerList)
        {
            string players = string.Empty;
           
            return $"{ID} {VKLINK} {FIRSTNAME} {SECONDNAME} {players}";
        }
    }
}
