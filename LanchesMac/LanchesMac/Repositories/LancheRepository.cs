using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

        public IEnumerable<Lanche> LanchePreferido => _context.Lanches.Where(
            p => p.IsLanchePreferido).Include(c => c.Categoria);

        IEnumerable<Lanche> ILancheRepository.Lanches => throw new NotImplementedException();

        IEnumerable<Lanche> ILancheRepository.LanchePreferido => throw new NotImplementedException();

        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }

        Lanche ILancheRepository.GetLancheById(int lancheId)
        {
            throw new NotImplementedException();
        }
    }
}
