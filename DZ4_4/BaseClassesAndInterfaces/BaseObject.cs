using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace DZ4_4.BaseClassesAndInterfaces
{
    /// <summary>
    /// Базовый объект.
    /// </summary>
    abstract class BaseObject : ICollision
    {
        
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        /// <summary>
        /// Прямоугольник в котором находится объект, используется для столкновений.
        /// </summary>
        public Rectangle Rect => new Rectangle(Pos, Size);

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="pos">Позиция объекта.</param>
        /// <param name="dir">Направление движения объекта.</param>
        /// <param name="size">Размеры объекта.</param>
        public BaseObject(Point pos, Point dir, Size size)
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }

        /// <summary>
        /// Метод отрисовки объекта
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Метод обновления положения объекта
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Реализация интерфейса столкновения объектов.
        /// </summary>
        public bool Collision(ICollision o) => o.Rect.IntersectsWith(this.Rect);

        
        //public bool IsCollision(ICollision e)
        //{
        //    if (e.Rect.IntersectsWith(this.Rect))
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        /// <summary>
        /// Метод получения координаты Y, позиции объетка.
        /// </summary>
        public int GetPosY
        {
            get
            {
                return Pos.Y;
            }
        }

        /// <summary>
        /// Метод получения координаты X, позиции объетка.
        /// </summary>
        public int GetPosX
        {
            get
            {
                return Pos.X;
            }
        }
        
        /// <summary>
        /// Метод проверки выхода за границы игрового поля.
        /// </summary>
        /// <returns>Истина\Ложь</returns>
        public virtual bool OverScreen()
        {
            if (Pos.X > Game.Width || Pos.Y > Game.Height || Pos.Y < -15)
            {
                return true;
            }
            else return false;
        }
    }
}
