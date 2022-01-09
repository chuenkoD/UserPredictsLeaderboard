using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Drawer
{
    public static class FileManager
    {
        static public void SaveAll(List<UserToBuild> Users, Dictionary<int, PlayerToBuild> Players)
        {
            File.WriteAllText("us.lba", JsonSerializer.Serialize<List<UserToBuild>>(Users));
            File.WriteAllText("pl.lba", JsonSerializer.Serialize<Dictionary<int, PlayerToBuild>>(Players));
        }

        static public List<UserToBuild> ReadExistUsers()
        {
            List<UserToBuild> Users = new List<UserToBuild>();
            if (CheckBackupExist("us.lba") && File.ReadAllText("us.lba").Length != 0)
            {
                Users = JsonSerializer.Deserialize<List<UserToBuild>>(File.ReadAllText("us.lba"));
            }
            return Users;
        }

        static public Dictionary<int, PlayerToBuild> ReadExistPlayers()
        {
            Dictionary<int, PlayerToBuild> Players = new Dictionary<int, PlayerToBuild>();
            if (CheckBackupExist("pl.lba") && File.ReadAllText("pl.lba").Length != 0)
                Players = JsonSerializer.Deserialize<Dictionary<int, PlayerToBuild>>(File.ReadAllText("pl.lba"));
            return Players;
        }
        static private bool CheckBackupExist(string path)
        {
            if (File.Exists(path)) return true;
            else
            {
                File.Create(path);
            }
            return false;
        }
    }
}