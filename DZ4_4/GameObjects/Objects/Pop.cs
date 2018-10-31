using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DZ4_4.BaseClassesAndInterfaces;
using System.Drawing;

namespace DZ4_4.GameObjects.Objects
{
    /// <summary>
    /// Объект Попадание.
    /// </summary>
    class Pop : BaseObject
    {
        /// <summary>
        /// Виден ли объект на поле.
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// Цвет объекта.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pos">Позиция Попадания.</param>
        /// <param name="dir">Направление движения попадания.</param>
        /// <param name="size">Размер попадания.</param>
        public Pop(Point pos, Point dir, Size size) : base(pos, dir, size)
        {

        }

        /// <summary>
        /// Метод отрисовки попадания.
        /// </summary>
        public override void Draw()
        {
            SolidBrush Brush = new SolidBrush(Color);
            Game.Buffer.Graphics.FillEllipse(Brush, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обновления позиции попадания.
        /// </summary>
        public override void Update()
        {
            for (int i = 0; i < 10; i++)
            {
                Size.Width++;
                Size.Height++;
            }
            if (Size.Width >= 25)
            {
                Enable = false;
            }
        }
    }
}
