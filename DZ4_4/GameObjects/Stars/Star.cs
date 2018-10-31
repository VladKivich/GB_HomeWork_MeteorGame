using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ4_4.BaseClassesAndInterfaces;
using System.Drawing;

namespace DZ4_4.Objects.Stars
{
    /// <summary>
    /// Объект Звезда.
    /// </summary>
    class Star : BaseObject
    {
        /// <summary>
        /// Генератор случайных чисел.
        /// </summary>
        static Random Rnd = new Random();

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pos">Позиция объекта.</param>
        /// <param name="dir">Направление движения объекта.</param>
        /// <param name="size">Размеры объекта.</param>
        public Star(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Pos.Y = Rnd.Next(1, 800);
        }

        /// <summary>
        /// Метод отрисовки объекта.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.Yellow, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);

        }

        /// <summary>
        /// Метод обновления положения объекта.
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0)
            {
                Pos.X = Game.Width + Size.Width;
                Pos.Y = Rnd.Next(1, 800);
            }
        }
    }
}
