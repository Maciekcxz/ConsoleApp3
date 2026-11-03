using System;
using KontoNaMonety;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Konto kontonaMonety = new Konto();
            Automat automat = new Automat();

            Console.WriteLine($"Numer konta: {kontonaMonety.NumerKonta}");

            automat.Wrzucmonete("1", kontonaMonety);    
            automat.Wrzucmonete("2", kontonaMonety);
             automat.Wrzucmonete("1", kontonaMonety);
            Console.WriteLine($"Stan konta: {kontonaMonety.StanKonta()} zł.");
            Konto kontoTestowe = new Konto();
            Console.WriteLine($"Numer konta: {kontoTestowe.NumerKonta}");
            automat.Wrzucmonete("1", kontoTestowe);
            automat.Wrzucmonete("5", kontoTestowe);
            automat.Wrzucmonete("1", kontoTestowe);
            Console.WriteLine($"Stan konta: {kontoTestowe.StanKonta()} zł.");




        }





    }
}