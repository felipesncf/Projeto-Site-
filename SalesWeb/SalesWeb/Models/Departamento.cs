using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Models
{
    public class Departamento
    {
        public int Id{ get; set; }
        public string Nome{ get;  set; }
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        public Departamento()
        {
        }

        public Departamento(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void AdicionarVendedor(Vendedor vend)
        {
            Vendedores.Add(vend);
        }

        public double TotalSales(DateTime inicio, DateTime final)
        {
            return Vendedores.Sum(vendedores => vendedores.TotalVendas(inicio, final)); 
        }
    }
}
