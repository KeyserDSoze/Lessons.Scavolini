using Microsoft.EntityFrameworkCore;
using Scavolini.Database;
using Scavolini.Models;
using System.IO;
using System.Linq;
namespace Scavolini
{
    public class Startup
    {
        const string FileName = @"C:\Users\aless\Downloads\Elenco-comuni-italiani.csv";
        internal static List<Comune> DammiTuttiIComuni()
        {
            List<Comune> comuni = new();
            foreach (string value in File.ReadAllLines(FileName).Skip(1))
            {
                var splittedValue = value.Split(';');
                comuni.Add(new Comune()
                {
                    Nome = splittedValue[5],
                    Cap = splittedValue[4],
                    CodiceRegione = splittedValue[0],
                });
            }
            return comuni;
        }
        internal static List<Comune> DammiSoloIComuniCheInizianoPerA(List<Comune> comuni)
        {
            List<Comune> comuni1 = new();
            if (comuni[0].CodiceRegione != "ddddd")
                comuni[0].CodiceRegione = "ddddd";
            foreach (var comune in comuni)
                if (comune.Nome.StartsWith("A"))
                    comuni1.Add(comune);
            return comuni1;
        }
        public static void Sample2()
        {
            var comuni = DammiTuttiIComuni();
            var comuni2 = DammiTuttiIComuni();
            //comuni[0].Nome = "Roma";
            Console.WriteLine(comuni2[0].Nome);

            var comuni3 = DammiSoloIComuniCheInizianoPerA(comuni);
            var comuni4 = DammiSoloIComuniCheInizianoPerA(comuni);
            Console.WriteLine(comuni4[0].Nome);
            Console.WriteLine(comuni4[0].CodiceRegione);
            Console.WriteLine(comuni[0].CodiceRegione);
        }
        internal static void Sample3(List<Comune> comuni)
        {
            Console.WriteLine(comuni.Count);
            Console.WriteLine(comuni.Where(x => x.Cap.Contains("001")).Count());
            Console.WriteLine(comuni.Where(x => x.Cap.StartsWith("001")).Count());
            var comuniCercati = comuni.Where(comune => comune.Cap.StartsWith("001")).ToList();
            //string a = "pippo";
            //string b = "pippo";
            //int a1 = 20;
            //int b1 = 20;
            //bool c1 = a1 == b1;
            //bool d1 = a1.Equals(b1);
            Comune a = new Comune { Cap = "dsad", CodiceRegione = "ddd", Nome = "dddd" };
            Comune b = new Comune { Cap = "dsad", CodiceRegione = "ddd", Nome = "dddd" };
            bool c = a == b;
            bool d = a.Equals(b);

            Comune3 a1 = new Comune3("dsad", "ddd", "dddd");
            Comune3 b1 = new Comune3("dsad", "ddd", "dddd");
            bool c1 = a1 == b1;
            bool d1 = a1.Equals(b1);

            var comuniCercati2 = comuni.ToList();

            List<Comune> similComuniCercati2 = new();
            foreach (var comune in comuni)
                if (comune.Cap.StartsWith("001"))
                    similComuniCercati2.Add(comune);

            List<Comune> deepCopy = new();
            foreach (var comune in comuni)
                deepCopy.Add(new Comune
                {
                    Cap = comune.Cap,
                    CodiceRegione = comune.CodiceRegione,
                    Nome = comune.Nome,
                });

            deepCopy[0].Nome = "Milano";

            comuniCercati2[0].Nome = "dasdsadsda";
            similComuniCercati2[0].Nome = "Roma";


            List<MoreComplex> complexes = new();
            complexes.Add(new MoreComplex { Id = 0, Comune = comuniCercati2[0] });
            complexes.Add(new MoreComplex { Id = 1, Comune = comuniCercati2[1] });


            List<MoreComplex> deepCopyComplexes = new();
            foreach (var complex in complexes)
                deepCopyComplexes.Add(new MoreComplex
                {
                    Comune = new Comune
                    {
                        Cap = complex.Comune.Cap,
                        CodiceRegione = complex.Comune.CodiceRegione,
                        Nome = complex.Comune.Nome,
                    },
                    Id = complex.Id,
                });

            complexes[0].Comune.Nome = "Verona";
            complexes[0].Id = 23;

            Console.WriteLine($"{deepCopyComplexes[0].Comune.Nome} - {deepCopyComplexes[0].Id}");
        }
        public static void Main(string[] args)
        {
            List<Comune> comuni = new();
            foreach (string value in File.ReadAllLines(FileName).Skip(1))
            {
                var splittedValue = value.Split(';');
                comuni.Add(new Comune()
                {
                    Nome = splittedValue[5],
                    Cap = splittedValue[4],
                    CodiceRegione = splittedValue[0],
                });
            }
            var builder = new DbContextOptionsBuilder<ScavolinidatabaseContext>();
            builder.UseInMemoryDatabase("Scavolini");
            var options = builder.Options;
            using ScavolinidatabaseContext context = new(options);
            foreach (var comune in comuni.Where(x => x.Nome.ToLower().StartsWith("a")))
            {
                context.Comuni.Add(new Comuni
                {
                    Cap = comune.Cap,
                    CodiceRegione = comune.CodiceRegione,
                    Nome = comune.Nome,
                });
            }
            context.SaveChanges();
            var tuttiQuelliMinoriDi10 = context.Comuni.Where(x => x.Id < 10).ToList();
            //bari,brindisi,milano,mantova
            //b:(bari,brindisi),m:(milano,mantova)
            //mantova,milano
            //milano
            //milano
            //bari,mantova
            var x1 = RaggruppaIComuni(context);
            var x2 = RaggruppaIComuni(context);
            var x3 = RaggruppaIComuni(context);
            var y1 = x1.ToList();
            var y2 = x2.ToList();
            var y3 = x3.ToList();
        }

        public static IQueryable<string> RaggruppaIComuni(ScavolinidatabaseContext context)
        {
            return context.Comuni.GroupBy(x => x.Nome[0]).Select(x => x.OrderBy(x => x.Nome).Skip(3).Take(1).First().Nome);
        }
    }
}