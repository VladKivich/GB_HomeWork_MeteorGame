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
    /// Перечисление направления полета пули.
    /// </summary>
    enum BulletDirection : byte
    {
        Direct = 1,
        Down = 2,
        Up = 3

    }

    /// <summary>
    /// Объект Пуля.
    /// </summary>
    class Bullet : BaseObject
    {
        BulletDirection CurrentDirection;

        /// <summary>
        /// Направление полета пули.
        /// </summary>
        public Point Direction { get; }

        /// <summary>
        /// Цвет пули.
        /// </summary>
        private Color color;

        /// <summary>
        /// Доступна ли пуля на экране.
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">Позиция пули</param>
        /// <param name="dir">Направление\Скорость полета пули</param>
        /// <param name="size">Размер пули</param>
        /// <param name="D">Направление полета объекта</param>
        /// <param name="col">Цвет пули</param>
        public Bullet(Point pos, Point dir, Size size, BulletDirection D, Color col) : base(pos, dir, size)
        {
            Direction = dir;
            CurrentDirection = D;
            color = col;
        }

        /// <summary>
        /// Метод отрисовки пули.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(new Pen(color, 4), Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height);
        }

        /// <summary>
        /// Метод обновления положения пули.
        /// </summary>
        public override void Update()
        {
            switch (CurrentDirection)
            {
                case (BulletDirection)2:
                    Pos.X += Dir.X; Pos.Y += Dir.Y;
                    break;
                case (BulletDirection)1:
                    Pos.X += Dir.X;
                    break;
                case (BulletDirection)3:
                    Pos.X += Dir.X; Pos.Y -= Dir.Y;
                    break;
            }

        }
    }
}
