using Scavolini.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavolini.Example
{
    internal static class Introduction
    {
        internal static void Invoke(string[] args)
        {
            if (args.Length > 0 && args[0] == "--help")
            {
                Console.WriteLine("Lista dei comandi:");
                Console.WriteLine("--config per le configurazioni");
                Console.WriteLine("--setup per installazione");
                Console.WriteLine("--setup per installazione");
                Console.WriteLine("--setup per installazione");
                Console.WriteLine("--setup per installazione");
                Console.WriteLine("--setup per installazione");
            }
            string entry = Console.ReadLine();
            Console.Error.WriteLine(entry);
            Console.WriteLine(entry);
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");

            Comune4 comune4 = new Comune4("a", "a", ":");
            comune4.SetNome("aaaa");
            comune4.GetNome();
            Comune comune = new Comune
            {
                Nome = "a",
                Cap = "b"
            };
            comune.CodiceRegione = "d";
            comune.Nome = "Roma";

            Comune2 comune2 = new Comune2
            {
                Nome = "Roma",
                Cap = "b"
            };
            //comune2.Nome = "Milano";

            Comune3 comune3 = new Comune3("a", "b", "s");
            //comune3.CondiceRegione = "dasdasdsa";
            var nuovoComune3 = comune3 with { Nome = "Roma" };
            Console.WriteLine(comune3);
            Console.WriteLine(nuovoComune3);

            string value = "";
            for (int i = 0; i < 1_000_000; i++)
                value += "dasdsd";

            DateTime start = DateTime.UtcNow;
            StringBuilder stringBuilder = new();
            for (int i = 0; i < 1_000_000; i++)
                stringBuilder.Append("dasdsd");
            DateTime end = DateTime.UtcNow;
            Console.WriteLine(end - start);

            string a = "a";
            string b = "b";
            string c = "c";
            string d = "d";
            //interpolazione come fosse uno stringbuilder
            string e = $"{a}{b}{c}{d}";
            //si trasforma in realtà in
            StringBuilder stringBuilder2 = new();
            stringBuilder2.Append(a);
            stringBuilder2.Append(b);
            stringBuilder2.Append(c);
            stringBuilder2.Append(d);
            string f = a + b + c + d;
        }
    }
}
