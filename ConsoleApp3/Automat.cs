
using System;
using System.Globalization;
using System.Linq;

namespace KontoNaMonety
{
    internal class Automat
    {
        // Jedyna lista nominałów używanych w całym programie.
        // Bankomat korzysta z tej samej tablicy, więc nominały nie są kopiowane.
        public static readonly decimal[] PrzyjmowaneMonety =
        {
            5m, 2m, 1m, 0.5m, 0.2m, 0.1m
        };

        // Dla każdego nominału przechowujemy liczbę monet znajdujących się w automacie.
        // Indeks w tej tablicy odpowiada indeksowi w tablicy PrzyjmowaneMonety.
        private int[] iloscMonet = new int[PrzyjmowaneMonety.Length];

        // Automat może przechowywać maksymalnie trzy monety każdego nominału.
        private int pojemnosc = 3;

        public void WrzucMonety(string tekstMonet, Konto konto)
        {
            // Użytkownik podaje monety jako tekst rozdzielony spacjami,
            // dlatego najpierw rozdzielamy ten tekst na pojedyncze wartości.
            string[] monety = tekstMonet.Split(' ');

            // Liczniki pozwalają na końcu podsumować wynik wrzucania monet.
            int przyjete = 0;
            int zwrocone = 0;

            // Sprawdzamy każdą monetę osobno.
            foreach (string moneta in monety)
            {
                // Próbujemy zamienić tekst, np. "2", na liczbę dziesiętną.
                // TryParse nie przerywa programu, gdy podany tekst jest błędny.
                bool czyPoprawna = decimal.TryParse(
                    moneta,
                    NumberStyles.Number,
                    CultureInfo.InvariantCulture,
                    out decimal wartosc
                );

                if (!czyPoprawna)
                {
                    // Tekst nie przedstawia liczby, więc tej monety nie możemy przyjąć.
                    Console.WriteLine("Nieprawidłowy odczyt monety: " + moneta);
                    zwrocone++;
                    continue;
                }

                // Szukamy nominału wśród monet obsługiwanych przez automat.
                int indeks = Array.IndexOf(PrzyjmowaneMonety, wartosc);

                if (indeks == -1)
                {
                    // Liczba jest poprawna, ale nie jest jednym z dozwolonych nominałów.
                    Console.WriteLine("Nieprawidłowa moneta: " + wartosc);
                    zwrocone++;
                    continue;
                }

                // Sprawdzamy, czy w przegródce danego nominału jest jeszcze miejsce.
                if (iloscMonet[indeks] >= pojemnosc)
                {
                    Console.WriteLine("Brak miejsca na monetę: " + wartosc + " zł");
                    zwrocone++;
                    continue;
                }

                // Moneta przechodzi wszystkie kontrole, więc zapisujemy ją w automacie.
                iloscMonet[indeks]++;

                // Wartość przyjętej monety zostaje dopisana do konta.
                konto.Wplac(wartosc);

                Console.WriteLine("Moneta przyjęta: " + wartosc + " zł");

                przyjete++;
            }

            // Po sprawdzeniu wszystkich monet pokazujemy użytkownikowi podsumowanie.
            Console.WriteLine("-----------------------");
            Console.WriteLine("Monety przyjęte: " + przyjete);
            Console.WriteLine("Monety zwrócone: " + zwrocone);
            Console.WriteLine("-----------------------");
        }

        public void PokazMonety()
        {
            // Wyświetlamy, ile monet każdego nominału znajduje się obecnie w automacie.
            Console.WriteLine("Monety w automacie:");

            // Ta zmienna będzie przechowywać łączną wartość wszystkich monet.
            decimal suma = 0;

            // Przechodzimy po wszystkich obsługiwanych nominałach.
            for (int i = 0; i < PrzyjmowaneMonety.Length; i++)
            {
                Console.WriteLine(
                    PrzyjmowaneMonety[i] + " zł: " + iloscMonet[i]
                );

                // Dodajemy wartość wszystkich monet danego nominału do sumy.
                suma += PrzyjmowaneMonety[i] * iloscMonet[i];
            }

            // Na końcu pokazujemy całkowitą wartość pieniędzy w automacie.
            Console.WriteLine("Łączna wartość monet: " + suma + " zł");
        }
    }
}
  