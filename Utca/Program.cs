using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Utca
{
    class Program
    {
        public static readonly char FESTETLEN_KERITES = '#';
        public static readonly char NINCS_KERITES = ':';
        public static readonly int PAROS = 0;
        public static readonly int PARATLAN = 1;

        static List<Telek> telkek = new List<Telek>();

        static void Main(string[] args)
        {
            Beolvas(@"..\..\kerites.txt");
            Console.WriteLine("\n2. feladat:");
            Console.WriteLine($"Az eladott telkek száma: {telkek.Count}");

            Console.WriteLine("\n3. feladat:");
            if (telkek[telkek.Count-1].Oldal == PAROS)
            {
                Console.WriteLine("\ta. A páros oldalon adták el az utolsó telket.");
            }
            else
            {
                Console.WriteLine("\ta. A páratlan oldalon adták el az utolsó telket.");
            }
            Console.WriteLine($"\tb. Az utolsó telek házszáma: {telkek[telkek.Count - 1].Hazszam}");

            Console.WriteLine("\n4. feladat");
            List<Telek> paratlanok = telkek.FindAll(a => a.Oldal == PARATLAN).ToList();
            for (int i = 0; i < paratlanok.Count-1; i++)
            {
                if (paratlanok[i].Kerites.Equals(paratlanok[i+1].Kerites) && paratlanok[i].Kerites != FESTETLEN_KERITES && paratlanok[i].Kerites != NINCS_KERITES)
                {
                    Console.WriteLine($"\tA szomszédossal egyezik a kerítés színe: {paratlanok[i].Hazszam}");
                    break;
                }
            }

            Console.WriteLine("\n5. feladat");
            Console.Write("\tAdjon meg egy házszámot! ");
            int.TryParse(Console.ReadLine(), out int hazszam);
            Telek telek = telkek.Find(a => a.Hazszam == hazszam);
            Console.WriteLine($"\tA kerítés színe / állapota: {telek.Kerites}");
            char szomszed1 = telkek.Find(a => a.Hazszam == hazszam - 2).Kerites;
            char szomszed2 = telkek.Find(a => a.Hazszam == hazszam + 2).Kerites;
            Random r = new Random();
            char ujszin = '\0';
            do
            {
                ujszin = (char)('A' + r.Next('Z' - 'A'));
            } while (ujszin.Equals(szomszed1) || ujszin.Equals(szomszed2));
            Console.WriteLine($"\tEgy lehetséges festési szín: {ujszin}");

            Feladat_06();
            Console.WriteLine("\nProgram vége!");
            Console.ReadKey();
        }
        static void Beolvas(string forras)
        {
            try
            {
                using (StreamReader sr = new StreamReader(forras))
                {
                    while (!sr.EndOfStream)
                    {
                        telkek.Add(new Telek(sr.ReadLine().Split()));
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
        static void Feladat_06()
        {
            using (StreamWriter sw = new StreamWriter("utcakep.txt"))
            {
                /*  Az első sorban a páratlan oldal jelenjen meg, 
                 * a megfelelő méternyi szakasz kerítésszínét (vagy állapotát) 
                 * jelző karakterrel!
                 */
                string elsosor = string.Empty;
                foreach (Telek item in telkek.FindAll(a => a.Oldal == PARATLAN))
                {
                    elsosor += new String(item.Kerites, item.Szelesseg);
                }
                string masodiksor = string.Empty;
                foreach (Telek item in telkek.FindAll(a => a.Oldal == PARATLAN))
                {
                    masodiksor += item.Hazszam.ToString().PadRight(item.Szelesseg, ' ');
                }
                sw.WriteLine(elsosor);
                sw.WriteLine(masodiksor);
            }
        }
    }
}
