using System.Text.Json.Serialization;

namespace Drawer
{
    public class PlayerToBuild
    {
        public string Nickname { get; set; }
        public int Placement { get; set; }

        public int PickedCount { get; set; }
        public int RightPickedCount { get; set; }
        public int OneDeviationCount { get; set; }
        public int TwoDeviationCount { get; set; }
        public int AtleastInTopCount { get; set; }

        public PlayerToBuild()
        {
            this.Nickname = "Unknown";
            this.Placement = -1;
            this.PickedCount = 0;
            this.RightPickedCount = 0;
        }

        public PlayerToBuild(string Nickname, int Placement) : this()
        {
            this.Nickname = Nickname;
            this.Placement = Placement;
        }

        [JsonConstructor]
        public PlayerToBuild(string Nickname, int Placement, int PickedCount) : this(Nickname, Placement)
        {
            this.PickedCount = PickedCount;
            this.Nickname = Nickname;
            this.Placement = Placement;
        }

        public override string ToString()
        {
            return $"Player \"{Nickname}\" - {Placement} place.";
        }
    }
}