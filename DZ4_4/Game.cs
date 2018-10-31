using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using DZ4_4.BaseClassesAndInterfaces;
using DZ4_4.GameObjects;
using DZ4_4.GameObjects.Objects;
using DZ4_4.Objects.Stars;

namespace DZ4_4
{
    class Game
    {
        //Делегат для вывода в консоль.
        public delegate void MyConsole<T>(T x);

        private static BufferedGraphicsContext Context;

        /// <summary>
        /// Буфер для графики.
        /// </summary>
        public static BufferedGraphics Buffer;

        /// <summary>
        /// Размеры игрового поля.
        /// </summary>
        public static int Width { get; set; }
        public static int Height { get; set; }

        /// <summary>
        ///  Генератор случайных чисел.
        /// </summary>
        static Random rnd1 = new Random();

        /// <summary>
        /// Таймеры.
        /// </summary>
        static Timer timer = new Timer { Interval = 16 };
        static Timer MedTimer = new Timer { Interval = 25000 };
        
        /// <summary>
        /// Экземпляр делегата для вывода данных в консоль.
        /// </summary>
        static MyConsole<string> LogConsole = new MyConsole<string>(Log);

        /// <summary>
        /// Переменные очков игрока, лимита очков, сбитых метеоров.
        /// </summary>
        static private int GameScore = 0;
        static private int PowerScore = 0;
        static private int AsteroidScore = 0;
        static private int ListLimit = 25;

        /// <summary>
        /// Дата.
        /// </summary>
        static DateTime Date = DateTime.Now;

        //Игровые объекты.
        static List<Pop> PopList = new List<Pop>(100);
        static BaseObject[] Stars;
        static List<Meteor> Meteor;
        static List<Bullet> BulletList = new List<Bullet>(10);
        static MedKit Med;
        static PowerUp Power;
        private static SpaceShip Ship = new SpaceShip(new Point(10, 100), new Point(0, 5), new Size(50, 50));

        /// <summary>
        /// Метод проверки высоты поля.
        /// </summary>
        private static void WidthCheck()
        {
            if (Width < 0 || Width > 1000)
            {
                throw new ArgumentOutOfRangeException("Width", Width, $"Screen is {Width.ToString()}");
            }
        }

        /// <summary>
        /// Метод проверки ширины поля.
        /// </summary>
        private static void HeightCheck()
        {
            if (Height < 0 || Height > 1000)
            {
                throw new ArgumentOutOfRangeException("Width", Height, $"Screen is {Height.ToString()}");
            }
        }

        /// <summary>
        /// Инициализация игры.
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            Graphics G;
            Context = BufferedGraphicsManager.Current;
            G = form.CreateGraphics();
            Width = form.Width;
            Height = form.Height;
            WidthCheck();
            HeightCheck();
            Buffer = Context.Allocate(G, new Rectangle(0, 0, Width, Height));
            timer.Start();
            timer.Tick += Timer_Tick;
            form.KeyDown += Key_Down;
            MedTimer.Start();
            MedTimer.Tick += MedTimer_Tick;
            Load();
        }

        static Game()
        {

        }

        /// <summary>
        /// Метод отрисовки объектов на поле.
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject obj in Stars)
            {
                obj.Draw();
            }

            foreach (Meteor met in Meteor)
            {
                if (met.Enable == true)
                {
                    met?.Draw();
                }
            }

            foreach (Bullet b in BulletList)
            {
                if (b.Enable == true)
                {
                    b?.Draw();
                }
            }

            foreach (Pop p in PopList)
            {
                if (p.Enable == true)
                {
                    p?.Draw();

                }
            }

            Ship?.Draw();
            Power?.Draw();
            Med?.Draw();

            if (Ship != null)
            {
                Buffer.Graphics.DrawString("Energy: " + Ship.GetEnergy, SystemFonts.DialogFont, Brushes.AliceBlue, 5, 1);
                Buffer.Graphics.DrawString("Score: " + GameScore, SystemFonts.DialogFont, Brushes.AliceBlue, 600, 1);
                if (PowerScore > 0)
                {
                    Buffer.Graphics.DrawString("PowerUp Limit: " + PowerScore, SystemFonts.DialogFont, Brushes.AliceBlue, 400, 1);
                }
            }

            Buffer.Render();

            #region CustomException
            //try
            //{
            //    BulletCheck(bullet.GetPosY);
            //}

            //catch (GameObjectException e)
            //{
            //    timer.Stop();
            //    MessageBox.Show(e.Message);
            //    bullet.IfCollision();
            //    timer.Start();
            //}
            #endregion

        }

        /// <summary>
        /// Метод добавления объектов в начале игры.
        /// </summary>
        public static void Load()
        {
            #region Meteor

            Meteor = new List<Meteor>();
            for (int i = 0; i < 5; i++)
            {
                Meteor.Add(new Meteor(new Point(rnd1.Next(800, Game.Width + 100), rnd1.Next(30, 500)), new Point(rnd1.Next(1, 5), 0), new Size(15 * rnd1.Next(5, 10), 15 * rnd1.Next(5, 10))));
            }

            #endregion

            #region Stars
            Stars = new BaseObject[80];
            for (int i = 0; i < Stars.Length; i++)
            {
                switch (rnd1.Next(3))
                {
                    case 1:
                        Stars[i] = new Star_Violet(new Point(rnd1.Next(0, 800), rnd1.Next(0, 600)), new Point(1, 0), new Size(4, 4));
                        break;

                    case 2:
                        Stars[i] = new Star(new Point(rnd1.Next(0, 800), rnd1.Next(0, 600)), new Point(3, 0), new Size(2, 2));
                        break;

                    default:
                        Stars[i] = new Star_Blue(new Point(rnd1.Next(0, 800), rnd1.Next(0, 600)), new Point(2, 0), new Size(3, 3));
                        break;
                }
            }
            #endregion

        }

        /// <summary>
        /// Метод обновления положения объектов и их видимости.
        /// </summary>
        public static void Update()
        {
            foreach (BaseObject str in Stars)
            {
                str.Update();
            }

            foreach (Meteor a in Meteor)
            {
                if (a.Enable == true)
                {
                    for (int i = 0; i < BulletList.Count; i++)
                    {
                        if (a.Collision(BulletList.ElementAt(i)) & BulletList.ElementAt(i).Enable == true)
                        {
                            switch (a.Power)
                            {
                                case 0:
                                    BulletList.ElementAt(i).Enable = false;
                                    PopList.Add(new Pop(new Point(a.GetPosX+2, a.GetPosY-2), new Point(0, 0), new Size(1, 1)) { Color = Color.Orange });
                                    a.Enable = false;
                                    a.Regenerate();
                                    GameScore += 1;
                                    AsteroidScore = GameScore;
                                    LogConsole(string.Format("Now your score is: {0}. Time is {1}", GameScore, DateTime.Now - Date));
                                    break;

                                case 3:
                                case 2:
                                case 1:
                                    BulletList.ElementAt(i).Enable = false;
                                    a.Damage();
                                    break;
                            }
                        }

                        if (BulletList.ElementAt(i).OverScreen())
                        {
                            BulletList.ElementAt(i).Enable = false;
                        }
                    }

                    if (a.Collision(Ship))
                    {
                        Ship.DamageEnergy(3);
                        PopList.Add(new Pop(new Point(a.GetPosX, a.GetPosY), new Point(0, 0), new Size(1, 1)) { Color = Color.Blue });
                        a.Enable = false;
                        a.Regenerate();
                        LogConsole(string.Format("You get damage! Now your energy is: {0}. Time is {1}", Ship.GetEnergy, DateTime.Now - Date));
                    }
                }

                a?.Update();
            }

            foreach (Bullet b in BulletList)
            {
                if (b.Enable == true)
                {
                    b?.Update();
                }
            }

            foreach (Pop p in PopList)
            {
                if (p.Enable == true)
                {
                    p.Update();
                }
            }

            for (int i = 0; i < BulletList.Count; i++)
            {
                if (BulletList.ElementAt(i).Enable == false)
                {
                    BulletList.RemoveAt(i);
                }
            }

            for (int i = 0; i < PopList.Count; i++)
            {
                if (PopList.ElementAt(i).Enable == false)
                {
                    PopList.RemoveAt(i);
                }
            }

            if (Med != null)
            {
                if (Ship.Collision(Med))
                {
                    Ship.HealthEnergy(10);
                    Med = null;
                    LogConsole(string.Format("You get damage! Now your energy is: {0}. Time is {1}", Ship.GetEnergy, DateTime.Now - Date));
                }

                else if (Med.OverScreen() == true)
                {
                    Med = null;
                }
                Med?.Update();
            }

            if (Power != null)
            {
                if (Ship.Collision(Power))
                {
                    PowerScore = 100;
                    Power = null;
                }

                else if (Power.OverScreen() == true)
                {
                    Power = null;
                }
                Power?.Update();
            }

            if (Ship.GetEnergy <= 0)
            {
                Finish();
                LogConsole(string.Format("GAMEOVER! Your finall score is: {0}. Time is {1}", GameScore, DateTime.Now - Date));
            }

            Reload();
        }

        /// <summary>
        /// Метод для завершения игры, когда у корабля нет энергии.
        /// </summary>
        public static void Finish()
        {
            timer.Stop();
            MedTimer.Stop();
            Buffer.Graphics.DrawString("Game Over", new Font(FontFamily.GenericMonospace, 30, FontStyle.Bold), Brushes.Bisque, 250, 200);
            Buffer.Render();
        }

        /// <summary>
        /// Таймер обновления и отрисовки объектов на поле.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Таймер Аптечки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MedTimer_Tick(object sender, EventArgs e)
        {
            Med = new MedKit(new Point(800, rnd1.Next(0, Game.Height - 100)), new Point(5, 0), new Size(30, 30));
            Power = new PowerUp(new Point(800, rnd1.Next(0, Game.Height - 100)), new Point(7, 0), new Size(35, 30));
        }

        /// <summary>
        /// Событие нажатия кнопок на клавиатуре.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Key_Down(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                if (PowerScore > 0)
                {
                    BulletList.Add(new Bullet(new Point(Ship.Rect.X + 25, Ship.Rect.Y + 20), new Point(10, 2), new Size(4, -4), BulletDirection.Up, Color.GreenYellow));
                    BulletList.Add(new Bullet(new Point(Ship.Rect.X + 25, Ship.Rect.Y + 20), new Point(10, 0), new Size(4, 0), BulletDirection.Direct, Color.GreenYellow));
                    BulletList.Add(new Bullet(new Point(Ship.Rect.X + 25, Ship.Rect.Y + 20), new Point(10, 2), new Size(4, 4), BulletDirection.Down, Color.GreenYellow));
                    PowerScore--;
                }
                else
                {
                    BulletList.Add(new Bullet(new Point(Ship.Rect.X + 25, Ship.Rect.Y + 20), new Point(10, 0), new Size(8, 0), BulletDirection.Direct, Color.Green));
                }
            }

            if (e.KeyCode == Keys.Up)
            {
                Ship.MoveUp();
            }

            if (e.KeyCode == Keys.Down)
            {
                Ship.MoveDown();
            }
        }

        /// <summary>
        /// Метод вывода в консоль.
        /// </summary>
        /// <param name="x"></param>
        public static void Log(string x)
        {
            Console.WriteLine(x);
        }

        /// <summary>
        /// Метод увеличивающий коллекцию метеоров, когда игрок набирает очки.
        /// </summary>
        public static void Reload()
        {
            int dif = 1;
            if (AsteroidScore > ListLimit)
            {
                ListLimit *= 2;
                AsteroidScore = 0;
                Meteor.Add(new Meteor(new Point(rnd1.Next(800, Game.Width + 100), rnd1.Next(30, 500)), new Point(rnd1.Next(1, 5) * dif, rnd1.Next(-1, 1)), new Size(15 * rnd1.Next(5, 10), 15 * rnd1.Next(5, 10))));
            }
        }
    }
}
