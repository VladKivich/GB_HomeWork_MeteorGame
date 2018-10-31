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
    /// Объект аптечка.
    /// </summary>
    class MedKit : BaseObject
    {
        /// <summary>
        /// Генератор случайных чисел.
        /// </summary>
        static Random rnd = new Random();

        /// <summary>
        /// Изображение аптечки.
        /// </summary>
        public Image newImage;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pos">Позиция аптечки</param>
        /// <param name="dir">Направление\Скорость полета аптечки</param>
        /// <param name="size">Размеры аптечки</param>
        public MedKit(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            newImage = Image.FromFile("D:\\Обучение\\C#\\C#_Level1_Level2\\DZ2_F\\DZ4_4\\DZ4_4\\Images\\medkit.png");
        }

        /// <summary>
        /// Метод отрисовки аптечки.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        /// <summary>
        /// Метод обновления позиции аптечки.
        /// </summary>
        public override void Update()
        {
            Pos.X -= Dir.X;
        }

        /// <summary>
        /// Метод проверяет находится ли аптечка за пределами экрана по оси Х.
        /// </summary>
        /// <returns>Истина\Ложь</returns>
        public override bool OverScreen()
        {
            if (Pos.X < 0)
            {
                return true;
            }
            else
                return false;
        }
    }
}
