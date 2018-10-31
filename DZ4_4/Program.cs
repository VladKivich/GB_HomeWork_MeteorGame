using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DZ4_4
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Main
            Form form = new Form();
            form.Width = 800;
            form.Height = 600;
            form.Show();
            Game.Init(form);
            try
            {
                Game.Draw();
            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            Application.Run(form);
            #endregion

            #region Multi Delegate Test
            //Console.WriteLine("Test");
            //int[] A1 = new int[5] { 1, -9, 8, 13, -4 };
            //Del1 F1 = ArrayTest.ArrayToScreen;
            //F1 += ArrayTest.ArraySort1;
            //F1 += ArrayTest.ArraySort2;
            //F1.Invoke(ref A1);
            #endregion

            #region Special Delegeta Test(Func, Action, Predicate)

            //Action<string, ConsoleColor, int> NewAction = new Action<string, ConsoleColor, int>(DisplayMassage);
            //NewAction("Message", ConsoleColor.Cyan, 5);

            //Func<int, int, int> NewFunc = new Func<int, int, int>(Add);
            //Console.WriteLine(Add(5, 4));

            //Predicate<int> NewPredicate = new Predicate<int>(Res);
            //Console.WriteLine(NewPredicate(-1));

            #endregion

            #region Pattern Observer

            //SourceFunction s = new SourceFunction();
            //Observer1 Obs1 = new Observer1();
            //Observer2 Obs2 = new Observer2();
            //MyDelegate d1 = new MyDelegate(Obs1.Do);
            //s.Add(d1);
            //s.Add(Obs2.Do);
            //s.Run();
            //s.Remove(Obs2.Do);
            //s.Run();

            #endregion

            #region Event Test

            //SourceFunction Source2 = new SourceFunction();
            //Observer1 Obs1 = new Observer1();
            //Observer2 Obs2 = new Observer2();

            ////MyDelegate Del = new MyDelegate(Obs1.Do);
            //Source2.Run += Obs2.Do;
            //Source2.Run += Obs2.Do;
            //Source2.Start();
            //Source2.Run -= Obs2.Do;
            //Source2.Start();

            #endregion

            #region Event Test#2

            //Source S1 = new Source();
            //S1.RemoveUser("Perr", GetMessage);
            //S1.RemoveUser("Petr", GetMessage);
            //foreach(object obj in S1)
            //{
            //    Console.WriteLine(obj);
            //}

            #endregion
        }
    }
}
