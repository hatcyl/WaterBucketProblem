using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NDatabase;
using Core;

namespace Service
{
    public class PuzzleService
    {
        private readonly string _dbName;
        
        public PuzzleService(string dbName)
        {
            _dbName = dbName;
        }

        public Puzzle CreatePuzzle(int bucketCapacity1, int bucketCapacity2, int desiredAmount)
        {
            Puzzle puzzle = new Puzzle(bucketCapacity1, bucketCapacity2, desiredAmount);

            using (var db = OdbFactory.Open(_dbName))
            {
                db.Store(puzzle);
            }

            return puzzle;
        }

        public Puzzle SolvePuzzle(Guid id)
        {
            using (var db = OdbFactory.Open(_dbName))
            {
                var puzzle = db.AsQueryable<Puzzle>().Single(x => x.Id.Equals(id));
                puzzle.Solve();
                db.Store(puzzle);
                return puzzle;
            }
        }

        public IEnumerable<Puzzle> GetLastTenPuzzles()
        {
            using (var db = OdbFactory.Open(_dbName))
            {
                return db.AsQueryable<Puzzle>().OrderByDescending(x => x.LastUpdated).Take(10).ToList().AsEnumerable();
            }
        }

        public Puzzle Get(Guid id)
        {
            using (var db = OdbFactory.Open(_dbName))
            {
                return db.AsQueryable<Puzzle>().Single(x => x.Id.Equals(id));
            }
        }
    }
}
