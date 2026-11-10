
using System;
using KontoNaMonety;

namespace KontoNaMonety
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Konto kontonaMonety = new Konto();
            Konto kontoTestowe = new Konto();

            Automat automat = new Automat();

            Console.WriteLine($"Numer konta: {kontonaMonety.NumerKonta}");

            automat.WrzucMonety("1 2 0.5 abc 7", kontonaMonety);

            automat.WrzucMonety("5 2 1 0.5", kontonaMonety);

            automat.WrzucMonety("5 5 5 5", kontonaMonety);

            Console.WriteLine();

            Console.WriteLine($"Numer konta: {kontoTestowe.NumerKonta}");

            automat.WrzucMonety("1 2 0.5", kontoTestowe);

            Console.WriteLine();

            Console.WriteLine("Stan konta pierwszego:");
            Console.WriteLine($"{kontonaMonety.StanKonta()} zł");

            kontonaMonety.PokazHistorieWplat();

            Console.WriteLine();

            Console.WriteLine("Stan konta testowego:");
            Console.WriteLine($"{kontoTestowe.StanKonta()} zł");

            kontoTestowe.PokazHistorieWplat();

            Console.WriteLine();

            automat.PokazMonety();
        }
    }
}
