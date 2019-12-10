using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Este campo é obrigatório")]
        [StringLength(60, MinimumLength =3, ErrorMessage ="O tamanho do {0} deve ser entre {2} e {1}")]
        public string Nome{ get; set; }
        [DataType(DataType.EmailAddress)]

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
        public string Email{ get; set; }
        [Display(Name = "Salário Base")]
        [DisplayFormat(DataFormatString="{0:F2}")]
        public double SalarioBase { get; set; }
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public DateTime DataNascimento{ get; set; }
        public Departamento Departamento { get; set; }
        [Display(Name="Departamento")]
        public int DepartamentoId { get; set; }
        public ICollection<RegistroVendas> Vendas { get; set; } = new List<RegistroVendas>();

        public Vendedor()
        {
        }

        public Vendedor(int id, string nome, string email, double salarioBase, DateTime dataNascimento, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            SalarioBase = salarioBase;
            DataNascimento = dataNascimento;
            Departamento = departamento;
        }

        public void AdicionarVenda(RegistroVendas rv)
        {
            Vendas.Add(rv);
        }
        public void RemoverVenda(RegistroVendas rv)
        {
            Vendas.Remove(rv);
        }

        public double TotalVendas(DateTime inicio, DateTime final)
        {
            return Vendas.Where(rv => rv.Data >= inicio && rv.Data <= final).Sum(rv => rv.Quantidade);
        }
    }
}
