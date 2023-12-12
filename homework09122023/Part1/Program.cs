using System;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Collections.Generic;

namespace Part1
{
    internal class Program
    {
        static void PrintNumbers()
        {
            for (int i = 1; i < 11; i++)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine("\n");
        }
        static int CalculateSquare(int number)
        {
            return number * number;
        }
        static async Task<long> CalculateFactorial(int number)
        {
            long factorial = 1;
            await Task.Run(() => {
                Thread.Sleep(8000);
                for (int i = 1; i <= number; i++)
                {
                    factorial *= i;
                }
            });
            return factorial;
        }
        static void Task1()
        {
            Console.WriteLine("Task1\nНеобходимо создать 3 потока каждый из которых выводит числа от 1 до 10 на экран\n");

            List<Thread> threads = new List<Thread>
            {
                new Thread(PrintNumbers),
                new Thread(PrintNumbers),
                new Thread(PrintNumbers)
            };
            
            for (int i = 0; i < threads.Count; i++)
            {
                Console.Write($"Поток {i + 1}: ");
                threads[i].Start();
                Thread.Sleep(100);
            }
        }
        static async Task Task2()
        {
            Console.WriteLine("Task2\nНеобходимо написать программу, которая вычисляет факториал от введённого числа асинхронно, а квадрат синхронно");

            Console.Write("Введите число: ");
            if (int.TryParse(Console.ReadLine(), out int number))
            {
                long factorial = await CalculateFactorial(number);
                Console.WriteLine($"Факториал числа {number} равен: {factorial}");

                Console.WriteLine($"Квадрат числа {number} равен: {CalculateSquare(number)}\n");
            }
            else
            {
                Console.WriteLine("Вы ввели не число или не целое число!\n");
            }
        }
        static void Task3()
        {
            Console.WriteLine("Task3\nНеобходимо вывести список всех методов объекта\n");

            Refl reflection = new Refl();
            Type type = reflection.GetType();
            MethodInfo[] methodsList = type.GetMethods();

            Console.WriteLine("Методы объекта:");

            foreach ( MethodInfo method in methodsList )
            {
                Console.WriteLine(method.Name);
            }
        }
        static async Task Main()
        {
            Console.Title = "Skynet";
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;

            Task1();
            await Task2();
            Task3();

            Console.ReadKey();
        }
    }
}
