using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scavolini.Models
{
    internal record Comune3(string Nome, string CodiceRegione, string Cap)
    {
        //public void AddSomething(string a)
        //{

        //}
        //public string GetSomething()
        //{
        //    return Nome + CodiceRegione;
        //}
        public override string ToString()
        {
            return $"{Nome};{CodiceRegione};{Cap}";
        }
    }
    class Comune2
    {
        public string Nome { get; init; }
        public string CodiceRegione { get; init; }
        public string Cap { get; init; }
        public Comune2() { }
        public Comune2(string nome, string codiceRegione, string cap)
        {
            this.Nome = nome;
            this.CodiceRegione = codiceRegione;
            this.Cap = cap;
        }
        
    }
    internal class Comune4
    {
        private string Nome;
        private string CodiceRegione;
        private string Cap;

        public string GetNome()
        {
            return Nome;
        }
        public void SetNome(string nome)
        {
            this.Nome = nome;
        }
        public string GetCodiceRegione()
        {
            return CodiceRegione;
        }
        public void SetCodiceRegione(string codiceRegione)
        {
            this.CodiceRegione = codiceRegione;
        }
        public Comune4(string nome, string codiceRegione, string cap)
        {
            this.Nome = nome;
            this.CodiceRegione = codiceRegione;
            this.Cap = cap;
        }
    }
}