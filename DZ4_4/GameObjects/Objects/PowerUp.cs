using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DZ4_4.GameObjects.Objects
{
    /// <summary>
    /// Объект Усилитель.
    /// </summary>
    class PowerUp : MedKit
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pos">Позиция Усилителя.</param>
        /// <param name="dir">Направление\Скорость полета Усилителя.</param>
        /// <param name="size">Размер усилителя.</param>
        public PowerUp(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            newImage = Image.FromFile("D:\\Обучение\\C#\\C#_Level1_Level2\\DZ2_F\\DZ4_4\\DZ4_4\\Images\\powerup.png");
        }
    }
}
