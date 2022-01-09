using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Drawer
{
    public class UserToBuild
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int Points { get; set; }

        public int CurrentPosition { get; set; }
        public int PreviousPosition { get; set; }
        public int DeltaPosition { get; set; }

        public Dictionary<int, string> Predictions { get; set; }

        public UserToBuild(string Name)
        {
            this.Name = Name;
            this.Link = "vk.com/id0";

            this.Points = 0;
            this.CurrentPosition = 0;
            this.PreviousPosition = 0;
            this.DeltaPosition = 0;

            Predictions = InitPredictions();
        }

        public UserToBuild(string Name, string Link) : this(Name)
        {
            this.Link = Link;
        }

        public UserToBuild(string Name, Dictionary<int, string> Predictions) : this(Name)
        {
            this.Predictions = Predictions;
        }

        public UserToBuild(string Name, string Link, Dictionary<int, string> Predictions) : this(Name, Link)
        {
            this.Predictions = Predictions;
        }

        public UserToBuild(string Name, string Link, int Points, Dictionary<int, string> Predictions) : this(Name, Link)
        {
            this.Points = Points;
            this.Predictions = Predictions;
        }


        [JsonConstructor]
        public UserToBuild(int ID, string Name, string Link, int Points, Dictionary<int, string> Predictions) : this(Name, Link)
        {
            this.ID = ID;
            this.Points = Points;
            this.Predictions = Predictions;
        }

        private Dictionary<int, string> InitPredictions()
        {
            Predictions = new Dictionary<int, string>();

            for (int i = 20; i > 0; i--)
            {
                Predictions.Add(i, "Unknown");
            }

            return Predictions;
        }

        public string DisplayAllInfo()
        {
            string predicts = string.Empty;
            foreach (var item in Predictions)
            {
                predicts += item.Value + " ";
            }
            return $"{predicts}\t\t\t{CurrentPosition}. {Points}pt. {Name}";
        }

        public void DisplayInConsole(Dictionary<int, PlayerToBuild> Players)
        {
            foreach (var item in Predictions)
            {
                if (Players.Any(x => x.Value.Nickname == item.Value))
                {
                    int delta = Math.Abs(item.Key - Players.First(x => x.Value.Nickname == item.Value).Key);
                    Console.ForegroundColor = delta == 0 ? ConsoleColor.Green : delta == 1 ? ConsoleColor.Yellow : delta <= 3 ? ConsoleColor.Red : ConsoleColor.Blue;
                    Console.Write(item.Value);
                }
                else
                {
                    Console.Write(item.Value);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(" ");
            }
            Console.WriteLine($"                  \t\t{CurrentPosition}. [{DeltaPosition}] {Points}pt. {Name}");
        }
        public override string ToString()
        {
            string outcome = string.Empty;
            foreach (var item in Predictions)
            {
                outcome += $"{item.Key} - {item.Value}.\n";
            }

            //return $"Name - {Name}. [{CurrentPosition} current | {PreviousPosition} previous | {DeltaPosition} delta]\n" +
            //    $"Link - {Link}.\n" + outcome;

            return $"{CurrentPosition}. [{DeltaPosition}] {Name} - {Points}";
        }
    }
}