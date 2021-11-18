using Microsoft.EntityFrameworkCore;
using Scavolini.Database.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Scavolini
{
    public class Startup
    {
        const string FileName = @"C:\Users\aless\Downloads\Elenco-comuni-italiani.csv";
        const string FileNameJson = @"C:\Users\aless\Downloads\Serializzazione.json";
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
            var builder = new DbContextOptionsBuilder<ScavoliniNewContext>();
            builder.UseInMemoryDatabase("Scavolini");
            var options = builder.Options;
            using ScavoliniNewContext context = new(options);
            foreach (var comune in comuni)
            {
                context.Comunis.Add(new Comuni
                {
                    Cap = comune.Cap,
                    CodiceRegione = comune.CodiceRegione,
                    Nome = comune.Nome,
                    Labels = new List<Label>()
                    {
                        new Label
                        {
                            Label1 = comune.Nome.ToLower(),
                            SuperLabels = new List<SuperLabel>
                            {
                                new SuperLabel{ SuperLabel1 = comune.Nome.ToLower().Replace(" ",String.Empty)},
                                new SuperLabel{ SuperLabel1 = comune.Nome.ToLower().Replace("'",String.Empty)},
                            }
                        },
                        new Label
                        {
                            Label1 = comune.Nome.ToUpper(),
                            SuperLabels = new List<SuperLabel>
                            {
                                new SuperLabel{ SuperLabel1 = comune.Nome.ToUpper().Replace(" ",String.Empty)},
                                new SuperLabel{ SuperLabel1 = comune.Nome.ToUpper().Replace("'",String.Empty)},
                            }
                        },
                    }
                });
            }
            context.SaveChanges();
            var x = context.SuperLabels.ToList();
            var y = context.Comunis.Select(Selection).Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

            //var yN = (from label in
            //             (from comune in context.Comunis
            //              where comune.Labels.Any(label => label.Label1.Length <= 6)
            //              select comune.Labels.First().Label1)
            //          where !string.IsNullOrWhiteSpace(label)
            //          select label).ToList();

            var z = context.Comunis.Where(Where).ToList();
            var q = context.Labels.Where(x => x.Label1.Length <= 6).Select(x => x.Comune).Distinct().ToList();
            //var m = context.Comunis.SelectMany(x => x.Labels).ToList();
            //var m1 = context.Comunis.Select(x => x.Labels).ToList();
            var w = context.Labels.Where(x => x.Label1.Contains("ab")).Select(x => new NotAnonym
            {
                Name = x.Comune.Nome,
                SuperLabel = x.SuperLabels.FirstOrDefault().SuperLabel1
            }).Distinct().ToList();
            var w2 = context.Labels.Where(x => x.Label1.Contains("ca")).Select(x => new NotAnonym
            {
                Name = x.Comune.Nome,
                SuperLabel = x.SuperLabels.FirstOrDefault().SuperLabel1
            }).Distinct().ToList();
            var finalW = w.Concat(w2).Distinct().ToList();
            var intersectW = w.Intersect(w2).ToList();
            var exceptingW = w.Except(w2).ToList();
            var exceptingW2 = w2.Except(w).ToList();
            var unionW = w.Union(w2).ToList();
            var unionByW = w.UnionBy(w2, x => x.SuperLabel[1..4]).ToList();
            var distinctBy = w.DistinctBy(x => x.Name[0..4]).ToList();
            var groupByFirstChar = w.GroupBy(x => x.Name[..2]).ToList();

            //var groupJoinW = w.GroupJoin(w2, x => x.Name, x => x.Name, x => x.Name).ToList();
            w.ForEach(x => x.Name = x.Name.Replace("ab", string.Empty));
            foreach (var element in w)
            {
                element.Name = element.Name.Replace("ab", string.Empty);
                //element.SuperLabel = element.SuperLabel.Replace("bbbb", string.Empty);
                //element.SuperLabel = element.SuperLabel.Replace("bbbb", string.Empty);
                //element.SuperLabel = element.SuperLabel.Replace("bbbb", string.Empty);
                //element.SuperLabel = element.SuperLabel.Replace("bbbb", string.Empty);
                //element.SuperLabel = element.SuperLabel.Replace("bbbb", string.Empty);
                //element.SuperLabel = element.SuperLabel.Replace("bbbb", string.Empty);
            }
            int removed = w.RemoveAll(x => x.SuperLabel[0..1] == "a");

            //List<Normalizzatore> normalizzatores = new()
            //{
            //    new Normalizzatore { Name = "abcaceac", Normalizations = new() { "ab", "ca", "ce", "ac" } },
            //    new Normalizzatore { Name = "dide", Normalizations = new() { "di", "de" } },
            //    new Normalizzatore { Name = "fafe", Normalizations = new() { "fa", "fe" } },
            //};
            //string jsonString = JsonSerializer.Serialize(normalizzatores);
            //File.WriteAllText(FileNameJson, jsonString);
            var normalizzatores = JsonSerializer.Deserialize<List<Normalizzatore>>(File.ReadAllText(FileNameJson));
            var norm = NormalizeIt(normalizzatores, context);
        }
        public static List<NotAnonym> NormalizeIt(List<Normalizzatore> normalizzatores, ScavoliniNewContext context)
        {
            IQueryable<NotAnonym> query = null;
            foreach (var normalizzatore in normalizzatores)
            {
                foreach (var forLike in normalizzatore.Normalizations)
                {
                    var queryToAdd = context.Labels
                        .Where(x => x.Label1.Contains(forLike))
                        .Select(x => new NotAnonym
                        {
                            Name = x.Comune.Nome,
                            SuperLabel = x.SuperLabels.First().SuperLabel1
                        }).Distinct();
                    if (query == null)
                        query = queryToAdd;
                    else
                        query = query.Union(queryToAdd);
                }
            }
            return query?.ToList() ?? new();
        }
        public class Normalizzatore
        {
            public string Name { get; set; }
            public List<string> Normalizations { get; set; }
        }
        public class NotAnonym
        {
            public string Name { get; set; }
            public string SuperLabel { get; set; }
        }
        private static string Selection(Comuni comune)
        {
            return comune.Labels.FirstOrDefault(label => label.Label1.Length <= 6)?.Label1 ?? string.Empty;
        }
        private static bool Where(Comuni comune)
        {
            foreach (var label in comune.Labels)
            {
                if (label.SuperLabels.Any(x => x.SuperLabel1.Contains("ab")))
                    return true;
                if (label.SuperLabels.Any(x => x.SuperLabel1.Contains("cd")))
                    return true;
            }
            return false;
        }

    }
}