using System;
using KontoNaMonety;

namespace ConsoleApp3
{
    internal class Bankomat
    {
        // Tablica przechowuje liczbę monet każdego nominału załadowanych przez serwisanta.
        // Indeks w tej tablicy odpowiada temu samemu indeksowi w tablicy nominałów automatu.
        private readonly int[] iloscMonet = new int[Automat.PrzyjmowaneMonety.Length];

        public void ZaladujMonety(decimal nominal, int liczbaMonet)
        {
            // Szukamy indeksu podanego nominału we wspólnej tablicy nominałów.
            int indeks = Array.IndexOf(Automat.PrzyjmowaneMonety, nominal);

            // Jeżeli nominału nie ma na liście, bankomat niczego nie ładuje.
            if (indeks == -1)
            {
                Console.WriteLine("Nie można załadować nieobsługiwanego nominału: " + nominal + " zł.");
                return;
            }

            // Liczba monet musi być dodatnia.
            if (liczbaMonet <= 0)
            {
                Console.WriteLine("Liczba ładowanych monet musi być większa od zera.");
                return;
            }

            // Dodajemy monety do pojemnika bankomatu.
            iloscMonet[indeks] += liczbaMonet;
            Console.WriteLine("Załadowano " + liczbaMonet + " monet po " + nominal + " zł.");
        }

        public void Wyplac(Konto konto, decimal kwota)
        {
            // Najpierw sprawdzamy, czy żądana kwota jest poprawna i mieści się na koncie.
            if (kwota <= 0)
            {
                Console.WriteLine("Wypłata odrzucona: kwota musi być większa od zera.");
                return;
            }

            if (kwota > konto.StanKonta())
            {
                Console.WriteLine("Wypłata odrzucona: na koncie brakuje środków.");
                return;
            }

            // Ta tablica jest tylko planem wypłaty.
            // Dzięki temu przed potwierdzeniem nie zmieniamy ani konta, ani bankomatu.
            int[] monetyDoWydania = new int[iloscMonet.Length];
            decimal pozostalaKwota = kwota;

            // Przechodzimy od największego nominału do najmniejszego.
            for (int i = 0; i < Automat.PrzyjmowaneMonety.Length; i++)
            {
                // Obliczamy, ile monet tego nominału zmieści się w pozostałej kwocie.
                int potrzebneMonety = (int)(pozostalaKwota / Automat.PrzyjmowaneMonety[i]);

                // Nie możemy wydać więcej monet, niż znajduje się w pojemniku.
                monetyDoWydania[i] = Math.Min(potrzebneMonety, iloscMonet[i]);

                // Odejmujemy z planu wartość wybranych monet.
                pozostalaKwota -= monetyDoWydania[i] * Automat.PrzyjmowaneMonety[i];
            }

            // Zaokrąglenie usuwa drobne niedokładności obliczeń typu decimal.
            pozostalaKwota = Math.Round(pozostalaKwota, 2);

            // Jeżeli coś zostało, bankomat nie potrafi wydać dokładnej kwoty.
            if (pozostalaKwota != 0)
            {
                Console.WriteLine("Wypłata odrzucona: bankomat nie może wydać dokładnej kwoty.");
                return;
            }

            // Dopiero tutaj zmniejszamy saldo, bo wiemy już, że wypłata się uda.
            konto.Wyplac(kwota);

            // Usuwamy z pojemnika dokładnie te monety, które były w planie wypłaty.
            for (int i = 0; i < iloscMonet.Length; i++)
            {
                iloscMonet[i] -= monetyDoWydania[i];
            }

            // Informujemy klienta, jakie monety otrzymał.
            Console.WriteLine("Wypłata zakończona. Wydano:");
            for (int i = 0; i < monetyDoWydania.Length; i++)
            {
                if (monetyDoWydania[i] > 0)
                {
                    Console.WriteLine("- " + monetyDoWydania[i] + " monet po " + Automat.PrzyjmowaneMonety[i] + " zł");
                }
            }
        }

        public void PokazMonety()
        {
            // Pokazujemy stan pojemnika bankomatu bez zmieniania jego zawartości.
            Console.WriteLine("Monety w bankomacie:");
            decimal suma = 0;

            for (int i = 0; i < iloscMonet.Length; i++)
            {
                Console.WriteLine(Automat.PrzyjmowaneMonety[i] + " zł: " + iloscMonet[i]);
                suma += Automat.PrzyjmowaneMonety[i] * iloscMonet[i];
            }

            Console.WriteLine("Łączna wartość monet w bankomacie: " + suma + " zł");
        }
    }
}