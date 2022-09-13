using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Fill();
            Console.WriteLine("3. feladat:");
            harom();
            Console.WriteLine("4. feladat: ");
            negy();
            Console.WriteLine("5. feladat:");
            ot();
            Console.WriteLine("6. feladat:");
            hat();
            Console.WriteLine("7. feladat:");
            het();
            Console.WriteLine("8. feladat: ");
            nyolc();
            Console.WriteLine("9. feladat:");
            kilenc();
            tiz();
            Console.ReadKey();
        }
        static List<XD> lst = new List<XD>();
        static void Fill()
        {
            using (var sr = new StreamReader("kuldetesek.csv", Encoding.UTF8))
            {
                while (!sr.EndOfStream)
                {
                    string[] l = sr.ReadLine().Split(';');
                    lst.Add(new XD()
                    {
                        kod = l[0],
                        datum = l[1], nev = l[2], napszam = int.Parse(l[3]), 
                        ora = int.Parse(l[4]), tamaszpont = l[5], 
                        legenyseg = int.Parse(l[6])
                    });
                }
            }
        }


        static void harom() => Console.WriteLine($"\tÖsszesen {lst.Count} alkalommal indítottak űrhajót.");
        static void negy() => Console.WriteLine($"\t{lst.Sum(x => x.legenyseg)} utas indult az űrbe összesen.");
        static void ot() => Console.WriteLine($"\tÖsszesen {lst.Where(x => x.legenyseg < 5).Count()} alkalommal küldtek kevesebb, mint 5 embert az űrbe.");
        static void hat() => Console.WriteLine($"\t{lst.Last(x => x.nev == "Columbia").legenyseg} asztronauta volt a Columbia fedélzetén annak utolsó útján.");
        static void het()
        {
            var leghosszabb = lst.Where(x => x.napszam == lst.Max(y => y.napszam));

            Console.WriteLine($"\tA leghosszabb ideig a {leghosszabb.First().nev} volt az űrben a {leghosszabb.First().kod} küldetés során.\n\tÖsszesen {leghosszabb.Sum(x => x.napszam*24 + x.ora)} órát volt távol a Földtől.");
        }
        static void nyolc()
        {
            Console.Write("\tÉvszám: ");
            int lol = int.Parse(Console.ReadLine());
            Console.WriteLine(lst.Count(x => x.datum.Split('.')[0] == lol.ToString()) >= 1 ? $"\tEbben az évben {lst.Count(x => x.datum.Split('.')[0] == lol.ToString())} küldetés volt." : "\tEbben az évben nem indult küldetés.");
        }
        static void kilenc() => Console.WriteLine($"\tA küldetések {((double)lst.Where(x => x.tamaszpont == "Kennedy").Count() / (double)lst.Count())*100:0.00}%-a végződött a Kennedy űrközpontban.");
        static void tiz()
        {
            List<string> toFile = new List<string>();
            List<string> tamaszpontok = new List<string> { "Columbia", "Challenger", "Discovery", "Atlantis", "Endeavour" };

            using (var sw = new StreamWriter(new FileStream("ursiklok.txt", FileMode.Append), Encoding.UTF8))
            {
                foreach (var item in tamaszpontok)
                    sw.WriteLine($"{item}\t{(double)lst.Where(x => x.nev == item).Sum(x => (double)x.napszam) + (double)(lst.Where(x => x.nev == item).Sum(x => (double)x.ora)/24):0.00}");
                sw.Close();
            }
        }
    }



    class XD
    {
        public string kod { get; set; }
        public string datum { get; set; }
        public string nev { get; set; }
        public int napszam { get; set; }
        public int ora { get; set; }
        public string tamaszpont { get; set; }
        public int legenyseg { get; set; }
    }
}
