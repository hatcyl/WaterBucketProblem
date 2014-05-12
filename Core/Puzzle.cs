using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core
{
    public class Puzzle
    {

        public Guid Id { get; set; }

        public int BucketCapacity1 { get; set; }
        public int BucketCapacity2 { get; set; }
        public int DesiredAmount { get; set; }

        public Solution Solution { get; private set; }

        public DateTime LastUpdated { get; private set; }

        public Puzzle(int bucketCapacity1, int bucketCapacity2, int desiredAmount)
        {

            Id = Guid.NewGuid();

            BucketCapacity1 = bucketCapacity1;
            BucketCapacity2 = bucketCapacity2;
            DesiredAmount = desiredAmount;

            LastUpdated = DateTime.Now;
        }

        public Solution Solve()
        {

            Solution solution1 = null;
            Solution solution2 = null;

            Parallel.Invoke(
                () => { solution1 = SolveUp(BucketCapacity1, BucketCapacity2, DesiredAmount); },
                () => { solution2 = SolveDown(BucketCapacity1, BucketCapacity2, DesiredAmount); }
                );

            Solution = 
                solution1.Solveable && solution1.Steps.Count <= solution2.Steps.Count ? solution1 :
                solution1.Solveable && !solution2.Solveable ? solution1 :
                solution2.Solveable && solution2.Steps.Count <= solution1.Steps.Count ? solution2 :
                solution2.Solveable && !solution1.Solveable ? solution2 :
                null;

            LastUpdated = DateTime.Now;

            return Solution;
        }

        private Solution SolveUp(int bucket1Capacity, int bucket2Capacity, int desiredLevel)
        {
            if (bucket1Capacity == bucket2Capacity)
            {
                throw new Exception();
            }

            int smallBucketCapacity = bucket1Capacity < bucket2Capacity ? bucket1Capacity : bucket2Capacity;
            int largeBucketCapacity = bucket1Capacity > bucket2Capacity ? bucket1Capacity : bucket2Capacity;

            Bucket smallBucket = new Bucket(smallBucketCapacity);
            Bucket largeBucket = new Bucket(largeBucketCapacity);
            List<Step> steps = new List<Step>();

            while ((smallBucket.Level != desiredLevel) && (largeBucket.Level != desiredLevel) && (smallBucket.Level + largeBucket.Level != desiredLevel))
            {
                Step step;

                if (largeBucket.IsEmpty)
                {
                    largeBucket.Fill();
                    step = new Step(smallBucket.Level, largeBucket.Level, Action.FILL);
                }
                else if (!smallBucket.IsFull)
                {
                    largeBucket.TransferTo(smallBucket);
                    step = new Step(smallBucket.Level, largeBucket.Level, Action.TRANSFER);
                }
                else
                {
                    smallBucket.Dump();
                    step = new Step(smallBucket.Level, largeBucket.Level, Action.DUMP);
                }

                if (steps.Exists(x => x.SmallBucketLevel == step.SmallBucketLevel && x.LargeBucketLevel == step.LargeBucketLevel))
                {
                    return new Solution(false, steps);
                }

                steps.Add(step);
            }

            return new Solution(true, steps);
        }

        private Solution SolveDown(int bucket1Capacity, int bucket2Capacity, int desiredLevel)
        {
            if (bucket1Capacity == bucket2Capacity)
            {
                throw new Exception();
            }

            int smallBucketCapacity = bucket1Capacity < bucket2Capacity ? bucket1Capacity : bucket2Capacity;
            int largeBucketCapacity = bucket1Capacity > bucket2Capacity ? bucket1Capacity : bucket2Capacity;

            Bucket smallBucket = new Bucket(smallBucketCapacity);
            Bucket largeBucket = new Bucket(largeBucketCapacity);
            List<Step> steps = new List<Step>();

            while ((smallBucket.Level != desiredLevel) && (largeBucket.Level != desiredLevel) && (smallBucket.Level + largeBucket.Level != desiredLevel))
            {
                Step step;

                if (smallBucket.IsEmpty)
                {
                    smallBucket.Fill();
                    step = new Step(smallBucket.Level, largeBucket.Level, Action.FILL);
                }
                else if (!largeBucket.IsFull)
                {
                    smallBucket.TransferTo(largeBucket);
                    step = new Step(smallBucket.Level, largeBucket.Level, Action.TRANSFER);
                }
                else
                {
                    largeBucket.Dump();
                    step = new Step(smallBucket.Level, largeBucket.Level, Action.DUMP);
                }

                if(steps.Exists(x => x.SmallBucketLevel == step.SmallBucketLevel && x.LargeBucketLevel == step.LargeBucketLevel))
                {
                    return new Solution(false, steps);
                }

                steps.Add(step);
            }

            return new Solution(true, steps);
        }
    }
}
