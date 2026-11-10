
using System;
using System.Globalization;
using System.Linq;

namespace KontoNaMonety
{
    internal class Automat
    {
        private readonly decimal[] PrzyjmowaneMonety =
        {
            5m, 2m, 1m, 0.5m, 0.2m, 0.1m
        };

        private int[] iloscMonet = new int[6];

        private int pojemnosc = 3;

        public void WrzucMonety(string tekstMonet, Konto konto)
        {
            string[] monety = tekstMonet.Split(' ');

            int przyjete = 0;
            int zwrocone = 0;

            foreach (string moneta in monety)
            {
                bool czyPoprawna = decimal.TryParse(
                    moneta,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out decimal wartosc
                );

                if (!czyPoprawna)
                {
                    Console.WriteLine("Nieprawidłowy odczyt monety: " + moneta);
                    zwrocone++;
                    continue;
                }

                int indeks = Array.IndexOf(PrzyjmowaneMonety, wartosc);

                if (indeks == -1)
                {
                    Console.WriteLine("Nieprawidłowa moneta: " + wartosc);
                    zwrocone++;
                    continue;
                }

                if (iloscMonet[indeks] >= pojemnosc)
                {
                    Console.WriteLine("Brak miejsca na monetę: " + wartosc + " zł");
                    zwrocone++;
                    continue;
                }

                iloscMonet[indeks]++;

                konto.Wplac(wartosc);

                Console.WriteLine("Moneta przyjęta: " + wartosc + " zł");

                przyjete++;
            }

            Console.WriteLine("-----------------------");
            Console.WriteLine("Monety przyjęte: " + przyjete);
            Console.WriteLine("Monety zwrócone: " + zwrocone);
            Console.WriteLine("-----------------------");
        }

        public void PokazMonety()
        {
            Console.WriteLine("Monety w automacie:");

            decimal suma = 0;

            for (int i = 0; i < PrzyjmowaneMonety.Length; i++)
            {
                Console.WriteLine(
                    PrzyjmowaneMonety[i] + " zł: " + iloscMonet[i]
                );

                suma += PrzyjmowaneMonety[i] * iloscMonet[i];
            }

            Console.WriteLine("Łączna wartość monet: " + suma + " zł");
        }
    }
}
