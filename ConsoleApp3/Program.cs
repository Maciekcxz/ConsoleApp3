
using System;
using ConsoleApp3;
using KontoNaMonety;

namespace KontoNaMonety
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Tworzymy dwa niezależne konta, żeby pokazać działanie programu
            // dla różnych użytkowników.
            Konto kontonaMonety = new Konto();
            Konto kontoTestowe = new Konto();

            // Automat służy wyłącznie do wpłat na konto.
            Automat automat = new Automat();

            // Bankomat jest osobnym urządzeniem i na początku nie ma w sobie monet.
            Bankomat bankomat = new Bankomat();

            // Wyświetlamy numer pierwszego konta.
            Console.WriteLine($"Numer konta: {kontonaMonety.NumerKonta}");

            // Testujemy przyjmowanie poprawnych monet oraz błędnych danych:
            // "abc" nie jest liczbą, a 7 zł nie jest obsługiwanym nominałem.
            automat.WrzucMonety("1 2 0.5 abc 7", kontonaMonety);

            // Sprawdzamy przyjmowanie kolejnych poprawnych monet.
            automat.WrzucMonety("5 2 1 0.5", kontonaMonety);

            // Wrzucamy kilka monet 5-złotowych, aby sprawdzić ograniczenie pojemności.
            automat.WrzucMonety("5 5 5 5", kontonaMonety);

            Console.WriteLine();

            // Serwisant niezależnie ładuje monety do bankomatu.
            bankomat.ZaladujMonety(5m, 2);
            bankomat.ZaladujMonety(2m, 3);
            bankomat.ZaladujMonety(1m, 5);
            bankomat.ZaladujMonety(0.5m, 4);

            Console.WriteLine();

            // Wyświetlamy numer drugiego konta i wpłacamy na nie kilka monet.
            Console.WriteLine($"Numer konta: {kontoTestowe.NumerKonta}");

            automat.WrzucMonety("1 2 0.5", kontoTestowe);

            Console.WriteLine();

            // Pokazujemy aktualny stan pierwszego konta oraz historię jego wpłat.
            Console.WriteLine("Stan konta pierwszego:");
            Console.WriteLine($"{kontonaMonety.StanKonta()} zł");

            kontonaMonety.PokazHistorieWplat();

            Console.WriteLine();

            // Pokazujemy stan i historię wpłat drugiego konta.
            Console.WriteLine("Stan konta testowego:");
            Console.WriteLine($"{kontoTestowe.StanKonta()} zł");

            kontoTestowe.PokazHistorieWplat();

            Console.WriteLine();

            // Wypłacamy kwotę, którą bankomat może złożyć z posiadanych monet.
            bankomat.Wyplac(kontonaMonety, 7m);

            // Ta wypłata jest większa niż saldo konta, więc zostanie odrzucona.
            bankomat.Wyplac(kontonaMonety, 100m);

            // Konto ma wystarczające środki, ale bankomat nie ma monet 20 i 10 groszy,
            // dlatego nie może wydać dokładnie kwoty 4,30 zł.
            bankomat.Wyplac(kontonaMonety, 4.30m);

            Console.WriteLine();

            // Na końcu pokazujemy saldo i stan monet w osobnym bankomacie.
            Console.WriteLine("Końcowy stan konta pierwszego:");
            Console.WriteLine(kontonaMonety.StanKonta() + " zł");
            bankomat.PokazMonety();

        }
    }
}
