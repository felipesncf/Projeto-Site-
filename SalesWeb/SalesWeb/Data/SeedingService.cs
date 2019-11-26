using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesWeb.Models;

namespace SalesWeb.Data
{
    public class SeedingService
    {
        private SalesWebContext _context;

        public SeedingService(SalesWebContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if (_context.Departamento.Any() || _context.Vendedor.Any() || _context.Registro.Any())
            {

            }
        }
    }
}
