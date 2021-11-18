using Microsoft.EntityFrameworkCore;
using Scavolini.Database.Models;

namespace Scavolini
{
    public class Startup
    {
        const string FileName = @"C:\Users\aless\Downloads\Elenco-comuni-italiani.csv";
        public static async Task Main(string[] args)
        {
            //List<Comune> comuni = new();
            //foreach (string value in File.ReadAllLines(FileName).Skip(1))
            //{
            //    var splittedValue = value.Split(';');
            //    comuni.Add(new Comune()
            //    {
            //        Nome = splittedValue[5],
            //        Cap = splittedValue[4],
            //        CodiceRegione = splittedValue[0],
            //    });
            //}
            //var builder = new DbContextOptionsBuilder<ScavoliniNewContext>();
            //builder.UseInMemoryDatabase("Scavolini");
            //var options = builder.Options;
            //using ScavoliniNewContext context = new*options);
            using ScavoliniNewContext context = new();
            using ScavoliniNewContext context1 = new();
            using ScavoliniNewContext context2 = new();
            using ScavoliniNewContext context3 = new();
            //foreach (var comune in comuni)
            //{
            //    context.Comunis.Add(new Comuni
            //    {
            //        Cap = comune.Cap,
            //        CodiceRegione = comune.CodiceRegione,
            //        Nome = comune.Nome,
            //        Labels = new List<Label>()
            //        {
            //            new Label
            //            {
            //                Label1 = comune.Nome.ToLower(),
            //                SuperLabels = new List<SuperLabel>
            //                {
            //                    new SuperLabel{ SuperLabel1 = comune.Nome.ToLower().Replace(" ",String.Empty)},
            //                    new SuperLabel{ SuperLabel1 = comune.Nome.ToLower().Replace("'",String.Empty)},
            //                }
            //            },
            //            new Label
            //            {
            //                Label1 = comune.Nome.ToUpper(),
            //                SuperLabels = new List<SuperLabel>
            //                {
            //                    new SuperLabel{ SuperLabel1 = comune.Nome.ToUpper().Replace(" ",String.Empty)},
            //                    new SuperLabel{ SuperLabel1 = comune.Nome.ToUpper().Replace("'",String.Empty)},
            //                }
            //            },
            //        }
            //    });
            //}
            //int quantiComuniHaCaricato = await context.SaveChangesAsync();
            List<Task<List<Comuni>>> tasks = new();
            var x = context.Comunis.Where(x => x.Nome.StartsWith("A")).ToListAsync();
            var y = context1.Comunis.Where(x => x.Nome.StartsWith("B")).ToListAsync();
            var z = context2.Comunis.Where(x => x.Nome.StartsWith("C")).ToListAsync();
            tasks.Add(x);
            tasks.Add(y);
            tasks.Add(z);
            var m = context3.Comunis.SelectMany(x => x.Labels).ToListAsync();
            await Task.WhenAll(Task.WhenAll(tasks), m);
            var t = x.Result;
            var t1 = y.Result;
            var t2 = z.Result;
            var t3 = m.Result;
        }
    }
}