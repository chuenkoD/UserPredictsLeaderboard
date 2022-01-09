using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drawer
{
    public static class Randomizer
    {
        static private Random rand = new Random();
        static private List<string> firstnames = new List<string>()
        {
                "Август", "Аврам", "Агафон", "Адам", "Александр", "Алексей", "Анатолий", "Андрей", "Антон", "Аполлон",
                "Аркадий", "Арнольд", "Арсений", "Артём", "Артур", "Бенедикт", "Богдан", "Борис", "Вадим", "Валентин",
                "Валерий", "Василий", "Виктор", "Виталий", "Владимир", "Владислав", "Всеволод", "Гаврил", "Георгий",
                "Глеб", "Григорий", "Давид", "Даниил", "Денис", "Дмитрий", "Добрыня", "Евгений", "Евдоким", "Егор",
                "Ефим", "Ефрем", "Захар", "Иван", "Игнат", "Игорь", "Илья", "Иннокентий", "Иоанн", "Карл", "Ким",
                "Кирилл", "Клавдий", "Клим", "Кондрат", "Кузьма", "Лев", "Леон", "Леонард", "Леонид", "Максим", "Марат",
                "Марк", "Матвей", "Мирон", "Мирослав", "Михаил", "Моисей", "Назар", "Нестор", "Никита", "Никодим",
                "Николай", "Олег", "Осип", "Оскар", "Остап", "Павел", "Панкрат", "Пётр", "Платон", "Потап", "Прокоп",
                "Радислав", "Ростислав", "Руслан", "Савелий", "Самсон", "Святополк", "Святослав", "Себастьян", "Семён",
                "Сергей", "Станислав", "Степан", "Тарас", "Тимофей", "Тимур", "Фёдор", "Федос", "Харитон", "Эдуард",
                "Эмилий", "Эрнест", "Юлий", "Юрий", "Яким", "Яков", "Ян", "Ярослав"
        };
        static private List<string> secondnames = new List<string>()
        {
                "Авдеев", "Агафонов", "Аксёнов", "Александров", "Алексеев", "Андреев", "Антонов", "Архипов", "Баранов",
                "Белов", "Белоусов", "Беляев", "Беспалов", "Блинов", "Блохин", "Бобров", "Богданов", "Борисов", "Брагин",
                "Буров", "Быков", "Васильев", "Веселов", "Виноградов", "Вишняков", "Власов", "Волков", "Воробьёв", "Воронов",
                "Воронцов", "Гаврилов", "Герасимов", "Голубев", "Горбачёв", "Горбунов", "Гордеев", "Горшков", "Григорьев",
                "Гришин", "Громов", "Гуляев", "Гурьев", "Гусев", "Гущин", "Давыдов", "Данилов", "Дементьев", "Денисов",
                "Дмитриев", "Доронин", "Дорофеев", "Дроздов", "Дьячков", "Евдокимов", "Евсеев", "Егоров", "Елисеев",
                "Емельянов", "Ермаков", "Ершов", "Ефимов", "Ефремов", "Жуков", "Зайцев", "Зимин", "Зиновьев", "Зуев", "Зыков",
                "Иванков", "Иванов", "Игнатов", "Ильин", "Исаев", "Исаков", "Казаков", "Калашников", "Калинин", "Капустин",
                "Карпов", "Кириллов", "Киселёв", "Князев", "Ковалёв", "Козлов", "Колесников", "Колобов", "Комаров", "Комиссаров",
                "Кондратьев", "Коновалов", "Кононов", "Константинов", "Копылов", "Корнилов", "Королёв", "Костин", "Котов",
                "Кошелев", "Красильников", "Крылов", "Крюков", "Кудрявцев", "Кудряшов", "Кузнецов", "Кузьмин", "Кулагин",
                "Кулаков", "Куликов", "Лаврентьев", "Лазарев", "Лапин", "Ларионов", "Лебедев", "Лихачёв", "Лобанов", "Логинов",
                "Лукин", "Лыткин", "Макаров", "Максимов", "Мамонтов", "Марков", "Мартынов", "Маслов", "Матвеев", "Медведев",
                "Мельников", "Меркушев", "Миронов", "Михайлов", "Михеев", "Мишин", "Моисеев", "Молчанов", "Морозов", "Муравьёв",
                "Мухин", "Мышкин", "Мясников", "Назаров", "Наумов", "Некрасов", "Нестеров", "Никитин", "Никифоров", "Николаев",
                "Никонов", "Новиков", "Носков", "Носов", "Овчинников", "Одинцов", "Орехов", "Орлов", "Осипов", "Павлов", "Панов",
                "Панфилов", "Пахомов", "Пестов", "Петров", "Петухов", "Поляков", "Пономарёв", "Попов", "Потапов", "Прохоров",
                "Рогов", "Родионов", "Рожков", "Романов", "Русаков", "Рыбаков", "Рябов", "Савельев", "Савин", "Сазонов", "Самойленко",
                "Самсонов", "Сафонов", "Селезнёв", "Селиверстов", "Семёнов", "Сергеев", "Сидоров", "Силин", "Симонов", "Ситников",
                "Соболев", "Соколов", "Соловьёв", "Сорокин", "Степанов", "Стрелков", "Субботин", "Суворов", "Суханов", "Сысоев",
                "Тарасов", "Терентьев", "Тетерин", "Тимофеев", "Титов", "Тихонов", "Третьяков", "Трофимов", "Туров", "Уваров",
                "Устинов", "Фадеев", "Фёдоров", "Федосеев", "Федотов", "Филатов", "Филиппов", "Фокин", "Фомин", "Фомичёв", "Фролов",
                "Харитонов", "Хохлов", "Цветков", "Чернов", "Шарапов", "Шаров", "Шашков", "Шестаков", "Шилов", "Ширяев", "Шубин",
                "Щербаков", "Щукин", "Юдин", "Яковлев", "Якушев", "Смирнов"
        };
        static private List<string> predicts = new List<string>()
        {
            "s1mple", "electronic", "Boombl4", "Perfecto", "B1T", "device", "REZ", "hampus", "Plopski", "LNZ", "shox", "apEx", "ZywOo",
            "misutaaa", "Kyojin", "Hobbit", "interz", "Ax1Le", "sh1ro", "nafany", "cadiaN", "refrezh", "stavn", "TeSeS", "sjuush",
            "valde", "Aleksib", "niko", "mantuu", "flameZ", "JaCkz", "NiKo", "huNter-", "AmaNEk", "nexa", "karrigan", "olofmeister",
            "rain", "Twistzz", "broky", "Xyp9x", "dupreeh", "gla1ve", "Magisk", "Lucky", "FalleN", "NAF", "EliGE", "Stewie2K", "Grim",
            "Snappi", "doto", "dycha", "hades", "Spinx", "SANJI", "buster", "Qikert", "Jame", "YEKINDAR", "tabseN", "tiziaN", "syrsoN",
            "gade", "k1to", "HooXi", "nicoodoz", "roeJ", "jabbi", "Zyphon", "dexter", "frozen", "acoR", "ropz", "Bymas", "alex", "mopoz",
            "DeathZz", "SunPayus", "dav1g", "bubble", "v1c7oR", "dream3r", "REDSTAR", "h4rn", "arT", "yuurih", "VINI", "KSCERATO", "drop",
            "NickelBack", "Krad", "Lack1", "El1an", "Forester", "chopper", "mir", "sdy", "degster", "magixx"
        };

        static public UserToBuild RandomizeUser()
        {
            string fullname = $"{firstnames.ElementAt(rand.Next(0, firstnames.Count))}" +
                $" {secondnames.ElementAt(rand.Next(0, secondnames.Count))}";

            Dictionary<int, string> Predictions = new Dictionary<int, string>();
            string PredictedPlayer = string.Empty;

            for (int i = 20; i > 0; i--)
            {
                do
                {
                    PredictedPlayer = predicts.ElementAt(rand.Next(0, predicts.Count));
                } while (Predictions.ContainsValue(PredictedPlayer));
                Predictions.Add(i, PredictedPlayer);
            }

            return new UserToBuild(fullname, Predictions);
        }

        static public List<UserToBuild> RandomizeGroupOfUsers(int Count)
        {
            List<UserToBuild> Users = new List<UserToBuild>();
            for (int i = 0; i < Count; i++)
            {
                Users.Add(RandomizeUser());
            }
            return Users;
        }

        static public Dictionary<int, PlayerToBuild> RandomizeTopTwenty(int Count)
        {
            Dictionary<int, PlayerToBuild> Rating = new Dictionary<int, PlayerToBuild>();
            string Nickname = string.Empty;
            PlayerToBuild Player;

            Count = Count < 0 ? 0 : Count;

            for (int i = 20; i > 20 - (Count > 20 ? 20 : Count); i--)
            {
                do
                {
                    Nickname = predicts.ElementAt(rand.Next(0, predicts.Count));
                    Player = new PlayerToBuild(Nickname, i);
                } while (Rating.Any(x => x.Value.Nickname == Player.Nickname));
                Rating.Add(i, new PlayerToBuild(Nickname, i));
            }
            if (Count < 20)
            {
                for (int i = 20 - Count; i > 0; i--)
                {
                    Rating.Add(i, new PlayerToBuild($"Jan{(i == 2 ? 20 : 21 - i)}", i));
                }
            }

            return Rating;
        }

        static public Dictionary<int, PlayerToBuild> GenerateTopTwenty(params string[] players)
        {
            Dictionary<int, PlayerToBuild> Rating = new Dictionary<int, PlayerToBuild>();
            string Nickname = string.Empty;
            PlayerToBuild Player;

            int Count = players.Length;
            Count = Count < 0 ? 0 : Count;

            for (int i = 20; i > 20 - (Count > 20 ? 20 : Count); i--)
            {
                do
                {
                    Nickname = players[20 - i];
                    Player = new PlayerToBuild(Nickname, i);
                } while (Rating.Any(x => x.Value.Nickname == Player.Nickname));
                Rating.Add(i, new PlayerToBuild(Nickname, i));
            }
            if (Count < 20)
            {
                for (int i = 20 - Count; i > 0; i--)
                {
                    Rating.Add(i, new PlayerToBuild($"Jan{(i == 2 ? 22 : i == 3 ? 22 : 23 - i)}", i));
                }
            }

            return Rating;
        }
    }
}