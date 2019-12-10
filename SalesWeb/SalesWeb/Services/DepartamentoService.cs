using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWeb.Services
{
    public class DepartamentoService
    {
        private readonly SalesWebContext _context;

        public DepartamentoService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync();
        }

    }
}
