using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictsManager
{
    public class Player
    {
        public int ID { get; set; }
        public string NICKNAME { get; set; }
        public int PLACE { get; set; }
        public string FIRSTNAME { get; set; }
        public string SECONDNAME { get; set; }

        public Player()
        {
            this.NICKNAME = "Unknown";
            this.PLACE = -1;
            this.FIRSTNAME = "Unknown";
            this.SECONDNAME = "Unknown";
        }

        public Player(string NICKNAME) : this()
        {
            this.NICKNAME = NICKNAME;
        }

        public Player(string NICKNAME, string FIRSTNAME, string SECONDNAME) : this(NICKNAME)
        {
            this.FIRSTNAME = FIRSTNAME;
            this.SECONDNAME = SECONDNAME;
        }

        public Player(string NICKNAME, string FIRSTNAME, string SECONDNAME, int PLACE) : this(NICKNAME, FIRSTNAME, SECONDNAME)
        {
            this.PLACE = PLACE;
        }

        public Player(int ID, string NICKNAME, string FIRSTNAME, string SECONDNAME, int PLACE) : this(NICKNAME, FIRSTNAME, SECONDNAME)
        {
            this.ID = ID;
            this.PLACE = PLACE;
        }

        public override string ToString()
        {
            return $"{NICKNAME} - {PLACE} place";
        }
    }
}
