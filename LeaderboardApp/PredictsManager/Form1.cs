using Dapper;
using Drawer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace PredictsManager
{
    public partial class Form1 : Form
    {
        public static Point point;
        public Timer AppStartAnim;
        private object _locker = new object();
        public Form1()
        {
            InitializeComponent();

            this.Opacity = 0;

            AppStartAnim = new Timer();
            AppStartAnim.Interval = 1;
            AppStartAnim.Tick += ChangeOpacity;
            AppStartAnim.Start();

            this.ResizeRedraw = true;
            this.DoubleBuffered = true;
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            //Task MoveRight = new Task(() =>
            //{
            //    while (pictureBox1.Location.X < 0)
            //    {
            //        pictureBox1.Location = new Point(pictureBox1.Location.X + 1, pictureBox1.Location.Y);
            //    }
            //});

            //MoveRight.Start();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            //Task MoveLeft = new Task(() =>
            //{
            //    while (pictureBox1.Location.X > -100)
            //    {
            //        pictureBox1.Location = new Point(pictureBox1.Location.X - 1, pictureBox1.Location.Y);
            //    }
            //});
            //MoveLeft.Start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button6_MouseEnter(object sender, EventArgs e)
        {
            button6.ForeColor = Color.FromArgb(228, 0, 43);
        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.ForeColor = Color.FromArgb(233, 238, 234);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point Delta = new Point(point.X - this.Location.X, point.Y - this.Location.Y);
                this.Location = new Point(this.Location.X + e.X - point.X, this.Location.Y + e.Y - point.Y);
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                point = new Point(e.X, e.Y);
            }
        }

        private void label5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                point = new Point(e.X, e.Y);
            }
        }

        private void label5_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point Delta = new Point(point.X - this.Location.X, point.Y - this.Location.Y);
                this.Location = new Point(this.Location.X + e.X - point.X, this.Location.Y + e.Y - point.Y);
            }
        }

        private void ChangeOpacity(object sender, System.EventArgs e)
        {
            this.Opacity += 0.04;
            if (this.Opacity >= 1)
                AppStartAnim.Stop();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (DB.Connect(textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text)) label12.Text = "Connected";
            else label12.Text = "Unconnected";
        }

        public static List<UserToBuild> GetUsers(SqlConnection connection)
        {
            List<User> users = connection.Query<User>("SELECT * FROM [USER]").ToList();
            List<Player> players = connection.Query<Player>("SELECT * FROM [PLAYER]").ToList();
            users.ForEach(x => Console.WriteLine(x.Display(players)));

            List<UserToBuild> usersToBuild = new List<UserToBuild>() { };
            users.ForEach(x => usersToBuild.Add(new UserToBuild(x.ID, $"{x.FIRSTNAME} {x.SECONDNAME}", x.VKLINK, 0, x.GetPredictions(players))));

            return usersToBuild;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Task create = new Task(() =>
            {
                Leaderboard leaderboard = new Leaderboard();

                //leaderboard.ReadAllSaves();

                string ConnectionString = "Data Source=sql5108.site4now.net;" +
                              "Initial Catalog=db_a7df7a_newsgo;" +
                              "User Id=db_a7df7a_newsgo_admin;" +
                              "Password=newsgodb1;";
                try
                {
                    SqlConnection conn = new SqlConnection(ConnectionString);
                    conn.Open();
                    //Console.WriteLine("Инициализация базы данных началась.");
                    //InitBlankDb(conn);
                    leaderboard.Users = GetUsers(conn);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                //this.Opacity = 0.9;
                //leaderboard.Users = Randomizer.RandomizeGroupOfUsers(5000);
                //leaderboard.Players = Randomizer.RandomizeTopTwenty(2);
                //leaderboard.Players.ElementAt(0).Value = new PlayerToBuild("s1mple", 20);
                leaderboard.Players = Randomizer.GenerateTopTwenty("broky", "EliGE", "ropz", "Twistzz", "stavn", "KSCERATO", "blameF");

                leaderboard.Start();
                leaderboard.SaveAll();
                button5.Text = $"Progress 0%";

                leaderboard.CountPicks("EliGE");

                //leaderboard.SaveAllInTxt();

                Image background = Image.FromFile("../../../Content/background.png");
                Image footer = Image.FromFile("../../../Content/footer.png");
                Image footerline = Image.FromFile("../../../Content/footerline.png");
                Image upperline = Image.FromFile("../../../Content/upperline.png");

                Image greenblanket = Image.FromFile("../../../Content/greenblanket.png");
                Image blueblanket = Image.FromFile("../../../Content/blueblanket.png");
                Image orangeblanket = Image.FromFile("../../../Content/orangeblanket.png");
                Image yellowblanket = Image.FromFile("../../../Content/yellowblanket.png");
                Image redblanket = Image.FromFile("../../../Content/redblanket.png");

                Image blackboard = Image.FromFile("../../../Content/blackboard.png");
                Image grayboard = Image.FromFile("../../../Content/grayboard.png");
                Image darkblackboard = Image.FromFile("../../../Content/darkblackboard.png");
                Image darkgrayboard = Image.FromFile("../../../Content/darkgrayboard.png");
                Image usernameblock = Image.FromFile("../../../Content/usernameblock.png");
                Image headerblock = Image.FromFile("../../../Content/headerblock.png");

                Image goldenshine = Image.FromFile("../../../Content/goldensshine.png");
                Image silvershine = Image.FromFile("../../../Content/silvershine.png");
                Image bronzeshine = Image.FromFile("../../../Content/bronzeshine.png");

                Image goldenbar = Image.FromFile("../../../Content/goldenbar.png");
                Image silverbar = Image.FromFile("../../../Content/silverbar.png");
                Image bronzebar = Image.FromFile("../../../Content/bronzebar.png");
                Image graybar = Image.FromFile("../../../Content/graybar.png");
                Image blackbar = Image.FromFile("../../../Content/blackbar.png");

                Image deltaup = Image.FromFile("../../../Content/deltaup.png");
                Image deltadown = Image.FromFile("../../../Content/deltadown.png");

                Image vkicon = Image.FromFile("../../../Content/vklogo.png");

                SolidBrush brush = new SolidBrush(Color.FromArgb(223, 223, 223));
                SolidBrush unknownbrush = new SolidBrush(Color.FromArgb(86, 86, 86));
                SolidBrush goldenbrush = new SolidBrush(Color.FromArgb(251, 176, 52));
                SolidBrush silverbrush = new SolidBrush(Color.FromArgb(243, 243, 243));
                SolidBrush bronzebrush = new SolidBrush(Color.FromArgb(249, 58, 2));

                SolidBrush down = new SolidBrush(Color.FromArgb(191, 3, 59));
                SolidBrush up = new SolidBrush(Color.FromArgb(14, 178, 78));

                StringFormat format = new StringFormat();
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;

                StringFormat nameformat = new StringFormat();
                nameformat.LineAlignment = StringAlignment.Center;

                PrivateFontCollection pfc = new PrivateFontCollection();
                pfc.AddFontFile("../../../Content/Montserrat-SemiBold.ttf");
                pfc.AddFontFile("../../../Content/Montserrat-Regular.ttf");
                pfc.AddFontFile("../../../Content/Kayak Sans Bold.otf");
                Font font = new Font(pfc.Families[2], 20, FontStyle.Regular);
                Font fonttop = new Font(pfc.Families[0], 15, FontStyle.Regular);
                Font softsauce = new Font(pfc.Families[2], 55, FontStyle.Regular);

                PictureBox pictureBox = new PictureBox();

                int pages = (int)(Math.Ceiling((decimal)(leaderboard.Users.Count / 50)));
                var bitmap = new Bitmap(4355, 3970);


                File.WriteAllText("sss.txt", $"{leaderboard.Users.Count}");

                List<Task> Pictures = new List<Task>();

                List<Leaderboard> leaderboards = new List<Leaderboard>();

                leaderboards.Add(leaderboard);
                leaderboards.Add(leaderboard);
                leaderboards.Add(leaderboard);

                for (int a = 0; a <= pages; a++)
                {
                    button5.Text = $"Progress {Math.Round((decimal)(a / pages+1) * 100, 2)}%";
                    int Page = a;

                    pictureBox.Image = bitmap;
                    Graphics g = Graphics.FromImage(bitmap);

                    g.DrawImage(background, 0, 0, 4355, 3970);
                    g.DrawImage(footer, 0, 3737, 4355, 233);
                    g.DrawImage(upperline, 76, 67, 4203, 2);

                    g.DrawString("@crustbrns", softsauce, brush, new RectangleF(168, 69, 520, 233), nameformat);
                    g.DrawString("newsgo", softsauce, brush, new RectangleF(3968, 69, 520, 233), nameformat);
                    g.DrawImage(vkicon, 76, 149, 74, 74);

                    for (int i = 0; i < 20; i++)
                    {
                        g.DrawImage(i % 2 == 0 ? blackboard : grayboard, 659 + 181 * i, 302, 181, 65);
                    }

                    for (int i = 0; i < 50; i++)
                    {
                        for (int j = 1; j < 22; j++)
                        {
                            if (i % 2 == 0)
                            {
                                g.DrawImage(j % 2 == 0 ? blackboard : grayboard, 478 + 181 * (j - 1), 482 + i * 65, 181, 65);
                            }
                            else
                            {
                                g.DrawImage(j % 2 == 0 ? darkblackboard : darkgrayboard, 478 + 181 * (j - 1), 482 + i * 65, 181, 65);
                            }
                        }
                        if (i % 2 == 0)
                        {
                            g.DrawImage(usernameblock, 76, 482 + i * 65, 402, 65);
                        }
                    }

                    if (a == 0)
                    {
                        for (int i = 0; i < (leaderboards[Page].Users.Count > 3 ? 3 : leaderboards[Page].Users.Count); i++)
                        {
                            g.DrawImage(i == 0 ? goldenshine : i == 1 ? silvershine : bronzeshine, 478, 482 + 65 * i, 180, 65);
                        }
                    }
                    for (int i = 0; i < (leaderboards[Page].Users.Count - Page * 50 > 50 ? 50 : leaderboards[Page].Users.Count - Page * 50); i++)
                    {
                        int number = 0;
                        foreach (var item in leaderboards[Page].Users[i + Page * 50].Predictions)
                        {
                            if (leaderboards[Page].Players.Any(x => x.Value.Nickname == item.Value))
                            {
                                int delta = Math.Abs(item.Key - leaderboards[Page].Players.First(x => x.Value.Nickname == item.Value).Key);
                                g.DrawImage(delta == 0 ? greenblanket : delta == 1 ? blueblanket : delta == 2 ? orangeblanket : yellowblanket, 659 + 181 * (number), 482 + i * 65, 181, 65);
                            }
                            else
                            {
                                g.DrawImage(redblanket, 659 + 181 * (number), 482 + i * 65, 181, 65);
                            }
                            number++;
                        }

                        string CorrectName = leaderboards[Page].Users[i + Page * 50].Name.Length <= 17 ? leaderboards[Page].Users[i + Page * 50].Name : (leaderboards[Page].Users[i + Page * 50].Name.Remove(16) + "...");
                        g.DrawString(leaderboards[Page].Users[i + Page * 50].Points.ToString(), font, Page == 0 ? (i == 0 ? goldenbrush : i == 1 ? silverbrush : i == 2 ? bronzebrush : brush) : brush, new RectangleF(478, 482 + 65 * i, 180, 65), format);
                        g.DrawString(CorrectName, font, brush, new RectangleF(146, 482 + 65 * i, 332, 65), nameformat);
                        g.DrawString(leaderboards[Page].Users[i + Page * 50].CurrentPosition.ToString(), font, brush, new RectangleF(76, 482 + 65 * i, 70, 65), format);

                        if (leaderboards[Page].Users[i + Page * 50].DeltaPosition > 0)
                        {
                            g.DrawImage(deltaup, 0, 482 + i * 65, 64, 65);
                            g.DrawString(Math.Abs(leaderboards[Page].Users[i + Page * 50].DeltaPosition).ToString(), font, up, new RectangleF(0, 494 + i * 65, 65, 65), format);
                        }
                        else if (leaderboards[Page].Users[i + Page * 50].DeltaPosition < 0)
                        {
                            g.DrawImage(deltadown, 0, 482 + i * 65, 64, 65);
                            g.DrawString(Math.Abs(leaderboards[Page].Users[i + Page * 50].DeltaPosition).ToString(), font, down, new RectangleF(0, 472 + i * 65, 64, 65), format);
                        }

                        int Pos = leaderboards[Page].Users[i].CurrentPosition;
                        g.DrawImage(Page == 0 ? (Pos == 1 ? goldenbar : Pos == 2 ? silverbar : Pos == 3 ? bronzebar : Pos <= 5 ? graybar : blackbar) : blackbar, 64, 484 + i * 65, 6, 61);

                        for (int j = 0; j < 20; j++)
                        {
                            g.DrawString(leaderboards[Page].Users[i + Page * 50].Predictions.GetValueOrDefault(20 - j), font, brush, new RectangleF(660 + j * 181, 483 + 65 * i, 180, 65), format);
                        }
                    }

                    g.DrawImage(headerblock, 76, 302, 583, 65);
                    g.DrawImage(footerline, 76, 3732, 4203, 5);
                    g.DrawString("TOTAL POINTS", fonttop, brush, new RectangleF(478, 417, 180, 65), format);
                    g.DrawString("NAME", fonttop, brush, new RectangleF(147, 417, 297, 65), nameformat);
                    g.DrawString("TOP 20 PLAYERS BY HLTV", font, brush, new RectangleF(76, 302, 583, 65), format);

                    for (int i = 0; i < leaderboards[Page].Players.Count; i++)
                    {
                        g.DrawString($"Top {20 - i}", fonttop, brush, new RectangleF(660 + i * 181, 417, 180, 65), format);
                    }

                    for (int i = 0; i < leaderboards[Page].Players.Count; i++)
                    {
                        g.DrawString(leaderboards[Page].Players.GetValueOrDefault(20 - i).Nickname, font, leaderboards[Page].Players.GetValueOrDefault(20 - i).Nickname.Contains("Jan") ? unknownbrush : brush, new RectangleF(660 + i * 181, 302, 180, 65), format);
                    }

                    pictureBox.Image.Save($"leaderboard_{Page + 1}.png", ImageFormat.Png);
                    pictureBox.Image = null;

                    //GC.Collect();
                }
            });
            create.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        public GraphicsPath RoundedRect(Rectangle baseRect, int radius)
        {
            var diameter = radius * 2;
            var sz = new Size(diameter, diameter);
            var arc = new Rectangle(baseRect.Location, sz);
            var path = new GraphicsPath();

            path.AddArc(arc, 180, 90);

            arc.X = baseRect.Right - diameter;
            path.AddArc(arc, 270, 90);

            arc.Y = baseRect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            arc.X = baseRect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private const int cGrip = 16;
        private const int cCaption = 32;
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rc = new Rectangle(this.ClientSize.Width - cGrip, this.ClientSize.Height - cGrip, cGrip, cGrip);
            ControlPaint.DrawSizeGrip(e.Graphics, this.BackColor, rc);
            rc = new Rectangle(0, 0, this.ClientSize.Width, cCaption);
            //e.Graphics.FillRectangle(Brushes.DarkBlue, rc);

        }

        const int delta = 10;

        private const int
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17;


        Rectangle Top { get { return new Rectangle(0, 0, this.ClientSize.Width, delta); } }
        Rectangle Left { get { return new Rectangle(0, 0, delta, this.ClientSize.Height); } }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox2.Width = this.Width + 20;
            pictureBox1.Height = this.Height - 24;
        }

        private void pictureBox2_DoubleClick(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            this.button8.Text = this.WindowState == FormWindowState.Maximized ? "▢" : "⛶";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.WindowState = this.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            this.button8.Text = this.WindowState == FormWindowState.Maximized ? "▢" : "⛶";
        }

        Rectangle Bottom { get { return new Rectangle(0, this.ClientSize.Height - delta, this.ClientSize.Width, delta); } }
        Rectangle Right { get { return new Rectangle(this.ClientSize.Width - delta, 0, delta, this.ClientSize.Height); } }

        Rectangle TopLeft { get { return new Rectangle(0, 0, delta, delta); } }
        Rectangle TopRight { get { return new Rectangle(this.ClientSize.Width - delta, 0, delta, delta); } }
        Rectangle BottomLeft { get { return new Rectangle(0, this.ClientSize.Height - delta, delta, delta); } }
        Rectangle BottomRight { get { return new Rectangle(this.ClientSize.Width - delta, this.ClientSize.Height - delta, delta, delta); } }


        protected override void WndProc(ref Message message)
        {
            base.WndProc(ref message);

            if (message.Msg == 0x84) // WM_NCHITTEST
            {
                var cursor = this.PointToClient(Cursor.Position);

                if (TopLeft.Contains(cursor)) message.Result = (IntPtr)HTTOPLEFT;
                else if (TopRight.Contains(cursor)) message.Result = (IntPtr)HTTOPRIGHT;
                else if (BottomLeft.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMLEFT;
                else if (BottomRight.Contains(cursor)) message.Result = (IntPtr)HTBOTTOMRIGHT;

                else if (Top.Contains(cursor)) message.Result = (IntPtr)HTTOP;
                else if (Left.Contains(cursor)) message.Result = (IntPtr)HTLEFT;
                else if (Right.Contains(cursor)) message.Result = (IntPtr)HTRIGHT;
                else if (Bottom.Contains(cursor)) message.Result = (IntPtr)HTBOTTOM;
            }
        }
    }
}