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
    /// Объект метеор.
    /// </summary>
    class Meteor : BaseObject
    {
        /// <summary>
        /// Прочность метеора.
        /// </summary>
        public int Power { get; set; } = 3;

        /// <summary>
        /// Доступен ли метеор на экране.
        /// </summary>
        public bool Enable { get; set; } = true;

        /// <summary>
        /// Изображение метеора.
        /// </summary>
        Image newImage = Image.FromFile("D:\\Обучение\\C#\\C#_Level1_Level2\\DZ2_F\\DZ4_4\\DZ4_4\\Images\\meteor.png");

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="pos">Позиция метеора</param>
        /// <param name="dir">Направление\Скорость движения метеора.</param>
        /// <param name="size">Размер метеора</param>
        public Meteor(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
            Flip();
        }

        /// <summary>
        /// Метод отрисовки метеора.
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(newImage, Pos.X, Pos.Y, Size.Width / 2, Size.Height / 2);
        }

        /// <summary>
        /// Метод обновления позиции метеора.
        /// </summary>
        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < -25)
            {
                Regenerate();
            }

            Pos.Y = Pos.Y - Dir.Y;
            if (Pos.Y < -25 || Pos.Y > Game.Height)
            {
                Regenerate();
            }
        }

        /// <summary>
        /// Метод сбрасывающий позицию метеора, когда он за пределами экрана.
        /// </summary>
        public void Regenerate()
        {
            Random rnd = new Random();
            Pos.X = 800;
            Pos.Y = rnd.Next(30, 500);
            Enable = true;
            Power = 3;
            Flip();
        }

        /// <summary>
        /// Метод уменьшения прочности метеора.
        /// </summary>
        public void Damage()
        {
            Power -= 1;
            if (Power < 0)
            {
                Power = 0;
            }
        }

        /// <summary>
        /// Метод случайного поворота метеора.
        /// </summary>
        private void Flip()
        {
            Random rnd1 = new Random();

            switch (rnd1.Next(0, 4))
            {
                case 0:
                    newImage.RotateFlip(RotateFlipType.RotateNoneFlipNone);
                    break;
                case 1:
                    newImage.RotateFlip(RotateFlipType.Rotate270FlipXY);
                    break;
                case 2:
                    newImage.RotateFlip(RotateFlipType.RotateNoneFlipXY);
                    break;
                case 3:
                    newImage.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    break;
                default:
                    newImage.RotateFlip(RotateFlipType.Rotate180FlipXY);
                    break;
            }
        }
    }
}
