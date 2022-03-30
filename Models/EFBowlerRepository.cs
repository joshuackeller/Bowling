using System;
using System.Linq;

namespace Bowling.Models
{
    public class EFBowlerRepository : IBowlerRepository
    {
        private BowlersDbContext _context { get; set; }


        public EFBowlerRepository ( BowlersDbContext temp)
        {
            _context = temp;
        }


        public IQueryable<Bowler> Bowlers => _context.Bowlers;

        public IQueryable<Team> Teams => _context.Teams;

        public void SaveBowler(Bowler b)
        {
            _context.Update(b);
            _context.SaveChanges();
        }

        public void CreateBowler(Bowler b)
        {
            _context.Add(b);
            _context.SaveChanges();
        }

        public void DeleteBowler(Bowler b)
        {
            _context.Remove(b);
            _context.SaveChanges();
        }
    }
}
