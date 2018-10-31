using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DZ4_4.BaseClassesAndInterfaces;

namespace DZ4_4.GameObjects.Objects
{
    /// <summary>
    /// Объект Космический корабль.
    /// </summary>
    class SpaceShip : BaseObject
    {
        /// <summary>
        /// Энергия Космического корабля.
        /// </summary>
        private int Energy = 100;

        /// <summary>
        /// Изображение космического корабля.
        /// </summary>
        Image newImage = Image.FromFile("D:\\Обучение\\C#\\C#_Level1_Level2\\DZ2_F\\DZ4_4\\DZ4_4\\Images\\spaceship.png");

        /// <summary>
        /// Метод получения энергии корабля.
        /// </summary>
        public int GetEnergy
        {
            get
            {
                return Energy;
            }
        }

        /// <summary>
        /// Метод наносящий урон энергии корабля.
        /// </summary>
        /// <param name="i">Цифра урона</param>
        public void DamageEnergy(int i)
        {
            if (Energy > 0 & Energy <= 150)
            {
                Energy -= i;
            }
        }

        /// <summary>
        /// Метод восстановления энергии корабля.
        /// </summary>
        /// <param name="i">Цифра восстановления</param>
        public void HealthEnergy(int i)
        {
            if (Energy < 150)
            {
                Energy += i;
            }
            else if (Energy > 150)
            {
                Energy = 150;
            }
        }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pos">Позиция корабля.</param>
        /// <param name="dir">Направление движения корабля.</param>
        /// <param name="size">Размер корабля.</param>
        public SpaceShip(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        /// <summary>
        /// Метод отрисовки корабля.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обновления позиции корабля.
        /// </summary>
        public override void Update()
        {

        }

        /// <summary>
        /// Метод возвращающий точку с координатами позиции корабля.
        /// </summary>
        public Point GetPosition
        {
            get
            {
                return Pos;
            }
        }

        /// <summary>
        /// Метод перемещающий корабль "вверх"
        /// </summary>
        public void MoveUp()
        {
            if (Pos.Y > 0)
            {
                Pos.Y -= Dir.Y;
            }
        }

        /// <summary>
        /// Метод перемещающий корабль "вниз"
        /// </summary>
        public void MoveDown()
        {
            if (Pos.Y < 500)
            {
                Pos.Y += Dir.Y;
            }
        }
    }
}
