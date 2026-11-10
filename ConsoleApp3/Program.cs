
using System;

namespace KontoNaMonety
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Konto kontonaMonety = new Konto();
            Automat automat = new Automat();

            Console.WriteLine("Numer konta: " + kontonaMonety.NumerKonta);

            automat.WrzucMonety("7 2 1 5 ", kontonaMonety);

            Console.WriteLine("\n--- PODSUMOWANIE ---");

            Console.WriteLine("Stan konta: " + kontonaMonety.StanKonta() + " zł");

            kontonaMonety.PokazHistorieWplat();

            automat.PokazMonety();
        }
    }
}
