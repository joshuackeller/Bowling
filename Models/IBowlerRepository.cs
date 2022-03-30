using System;
using System.Linq;

namespace Bowling.Models
{
    public interface IBowlerRepository
    {
        IQueryable<Bowler> Bowlers { get;  }

        IQueryable<Team> Teams { get;  }


        public void SaveBowler(Bowler b);

        public void CreateBowler(Bowler b);

        public void DeleteBowler(Bowler b);
    }
}
