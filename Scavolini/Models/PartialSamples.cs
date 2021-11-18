using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavolini.Models
{
    internal class PartialSamples
    {
    }

    public partial class Parziale
    {
        public string Id { get; set; }
    }
    public partial class Parziale
    {
        public string Name { get; set; }
    }
    public partial class Parziale
    {
        public override string ToString()
        {
            return $"{Id}{Name}";
        }
    }
    public class AggregazioneDellaClasseParziale
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"{Id}{Name}";
        }
    }
}
