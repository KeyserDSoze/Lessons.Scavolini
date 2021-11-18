using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavolini
{
    internal class Comune
    {
        public string Nome { get; set; }
        public string CodiceRegione { get; set; }
        public string Cap { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is Comune comune)
            {
                return comune.Nome == Nome && comune.CodiceRegione == CodiceRegione && comune.Cap == Cap;
            }
            return false;
        }
        public static bool operator ==(Comune lhs, Comune rhs)
        {
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Comune lhs, Comune rhs)
        {
            return !lhs.Equals(rhs);
        }
    }
}