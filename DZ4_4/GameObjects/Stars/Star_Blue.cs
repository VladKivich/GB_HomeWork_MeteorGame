using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DZ4_4.Objects.Stars
{
    /// <summary>
    /// Объект Синяя Звезда
    /// </summary>
    class Star_Blue : Star
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">Позиция объекта.</param>
        /// <param name="dir">Направление движения объекта.</param>
        /// <param name="size">Размеры объекта.</param>
        public Star_Blue(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        /// <summary>
        /// Метод отрисовки объекта.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.IndianRed, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
            Game.Buffer.Graphics.DrawLine(Pens.DarkRed, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height);
        }
    }
}
