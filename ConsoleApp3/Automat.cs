
using System;
using System.Globalization;

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
                    zwrocone++;
                    continue;
                }

                int indeks = Array.IndexOf(PrzyjmowaneMonety, wartosc);

                if (indeks == -1)
                {
                    zwrocone++;
                    continue;
                }

                if (iloscMonet[indeks] >= pojemnosc)
                {
                    zwrocone++;
                    continue;
                }

                iloscMonet[indeks]++;
                konto.Wplac(wartosc);

                przyjete++;
            }

            Console.WriteLine("\nPrzyjęte monety: " + przyjete);
            Console.WriteLine("Zwrócone monety: " + zwrocone);
        }

        public void PokazMonety()
        {
            Console.WriteLine("\nMonety w automacie:");

            decimal suma = 0;

            for (int i = 0; i < PrzyjmowaneMonety.Length; i++)
            {
                Console.WriteLine(
                    PrzyjmowaneMonety[i] + " zł: " + iloscMonet[i]
                );

                suma += PrzyjmowaneMonety[i] * iloscMonet[i];
            }

            Console.WriteLine("Łączna wartość: " + suma + " zł");
        }
    }
}
