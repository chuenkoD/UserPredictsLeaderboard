using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Drawer
{
    public class Leaderboard
    {
        public List<UserToBuild> Users { get; set; }
        public Dictionary<int, PlayerToBuild> Players { get; set; }

        public Leaderboard()
        {
            Users = new List<UserToBuild>();
            Players = new Dictionary<int, PlayerToBuild>();
        }
        public void SaveAll()
        {
            FileManager.SaveAll(Users, Players);
        }
        public void ReadAllSaves()
        {
            this.Users = FileManager.ReadExistUsers();
            this.Players = FileManager.ReadExistPlayers();
        }

        public void CountPicks(string Player)
        {
            string output = string.Empty;

            output += $"Первое место в топе прогнозистов занимает [{Users[0].Link.Remove(0, Users[0].Link.LastIndexOf("/") + 1)}|{Users[0].Name}], у него {Users[0].Points} очков. По сравнению с прошлым обновлением топа он {(Users[0].DeltaPosition > 0 ? "поднялся" : "упал")} на {Math.Abs(Users[0].DeltaPosition)} мест. Перед этим он был на {Users[0].PreviousPosition} месте.\n\n";

            Users.ForEach(x =>
            {
                foreach (var item in Players.Where(x => !x.Value.Nickname.Contains("Jan")).ToList())
                {
                    if (x.Predictions.ContainsValue(item.Value.Nickname))
                    {
                        item.Value.PickedCount++;
                        int delta = Math.Abs(item.Value.Placement - x.Predictions.First(x => x.Value == item.Value.Nickname).Key);

                        switch (delta)
                        {
                            case 0:
                                item.Value.RightPickedCount++;
                                break;
                            case 1:
                                item.Value.OneDeviationCount++;
                                break;
                            case 2:
                                item.Value.TwoDeviationCount++;
                                break;
                            default:
                                item.Value.AtleastInTopCount++;
                                break;
                        }
                    }
                }
            });

            foreach (var item in Players.Where(x => !x.Value.Nickname.Contains("Jan")).ToList())
            {
                output += $"Из {Users.Count} прогнозистов {item.Value.Nickname}'a выбрали {item.Value.PickedCount} раз.\nЭто {Math.Round((float)item.Value.PickedCount / (float)Users.Count * 100, 2)}% от общего количества прогнозов.\n" +
                    $"Точно угадали позицию — {item.Value.RightPickedCount} человек. ({Math.Round((float)item.Value.RightPickedCount / (float)Users.Count * 100, 2)}%).\n" +
                    $"Ошиблись на 1 позицию — {item.Value.OneDeviationCount} человек. ({Math.Round((float)item.Value.OneDeviationCount / (float)Users.Count * 100, 2)}%).\n" +
                    $"Ошиблись на 2 позиции — {item.Value.TwoDeviationCount} человек. ({Math.Round((float)item.Value.TwoDeviationCount / (float)Users.Count * 100, 2)}%).\n" +
                    $"Хотя бы добавили в топ — {item.Value.AtleastInTopCount} человек. ({Math.Round((float)item.Value.AtleastInTopCount / (float)Users.Count * 100, 2)}%).\n\n";
            }

            UserToBuild MainGroup = Users.Find(x => x.Name == "NEWSGO Subs");
            UserToBuild thet1x = Users.Find(x => x.Predictions.Values.Contains("thet1x"));

            output += $"Предикты подписчиков группы [newsgo|NEWS:GO] занимают {MainGroup.CurrentPosition} место с {MainGroup.Points} очками. По сравнению с прошлым обновлением топа они {(MainGroup.DeltaPosition > 0 ? "поднялись" : "упали")} на {Math.Abs(MainGroup.DeltaPosition)} мест. Перед этим они были на {MainGroup.PreviousPosition} месте.\n\n";
            output += $"Предикты [thet1x|папы зетикса] занимают {thet1x.CurrentPosition} место с {thet1x.Points} очками. По сравнению с прошлым обновлением топа они {(thet1x.DeltaPosition > 0 ? "поднялись" : "упали")} на {Math.Abs(thet1x.DeltaPosition)} мест. Перед этим они были на {thet1x.PreviousPosition} месте.\n\n";

            File.WriteAllText("PlayerPick.txt", output);
        }
        public void Start()
        {
            if (!IsAllPlayersUnknown())
            {
                CalculatePoints();
                SortUsersByRatingAdvanced();
                CalculateFinishPoints();
                SortUsersByRatingAdvanced();
                CalculateDelta();
            }
        }

        public void CalculatePoints()
        {
            this.Users.ForEach(x => Compute(x, Players));
        }

        private void Compute(UserToBuild user, Dictionary<int, PlayerToBuild> Players)
        {
            int Points = 0;
            int CountOfUknownPlayers = Players.Count(x => x.Value.Nickname.Contains("Jan"));
            //Console.WriteLine(Players.Count(x => x.Value.Nickname.Contains("Jan")));

            foreach (var item in Players)
            {
                if (item.Key == CountOfUknownPlayers+1)
                    break;

                if (user.Predictions.ContainsValue(item.Value.Nickname))
                {
                    int delta = Math.Abs(item.Value.Placement - user.Predictions.First(x => x.Value == item.Value.Nickname).Key);
                    Points += delta == 0 ? 5 : delta == 1 ? 3 : delta == 2 ? 2 : 1;
                }
            }
            user.Points = Points;
        }

        public void CalculateFinishPoints()
        {
            this.Users.ForEach(x => ComputeFinish(x, Players));
        }
        private void ComputeFinish(UserToBuild user, Dictionary<int, PlayerToBuild> Players)
        {
            int Points = user.Points;
            int CountOfUknownPlayers = Players.Count(x => x.Value.Nickname.Contains("Jan"));

            user.PreviousPosition = user.CurrentPosition;

            if (user.Predictions.ContainsValue(Players[CountOfUknownPlayers + 1].Nickname))
            {
                int delta = Math.Abs(Players[CountOfUknownPlayers + 1].Placement - user.Predictions.First(x => x.Value == Players[CountOfUknownPlayers + 1].Nickname).Key);
                Points += delta == 0 ? 5 : delta == 1 ? 3 : delta == 2 ? 2 : 1;
            }
            user.Points = Points;
        }

        public void CalculateDelta()
        {
            this.Users.ForEach(x => x.DeltaPosition = x.PreviousPosition - x.CurrentPosition);
        }

        public void SortUsersByRating()
        {
            Users.Sort((user1, user2) => user2.Points.CompareTo(user1.Points));
            //Сравнивает в коллекции двух людей по их общему количеству очков

            Users.ForEach(x => x.CurrentPosition = Users.IndexOf(x) + 1);
        }

        public void SortUsersByRatingAdvanced()
        {
            Users.Sort(delegate (UserToBuild u1, UserToBuild u2)
            {
                int compareDate = u2.Points.CompareTo(u1.Points);
                if (compareDate == 0)
                {
                    return u1.ID.CompareTo(u2.ID);
                }
                return compareDate;
            });
            Users.ForEach(x => x.CurrentPosition = Users.IndexOf(x) + 1);
        }

        public bool IsAllPlayersUnknown()
        {
            return (Players.Count(x => x.Value.Nickname.Contains("Jan")) == 20);
        }

        public void SaveAllInTxt()
        {
            //string num = string.Empty;
            //string pts = string.Empty;
            //string link = string.Empty;
            //string name = string.Empty;
            //string num = string.Empty;
            //for (int i = 0; i < Users.Count; i++)
            //{
            //    num += i + 1 + "\n";
            //    pts += Users[i].Points + "\n";
            //    link += Users[i].Link + "\n";
            //}



            //File.WriteAllText("num.txt", num);
        }
    }
}