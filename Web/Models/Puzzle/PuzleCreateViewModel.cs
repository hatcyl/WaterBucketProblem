using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models.Puzzle
{
    public class PuzleCreateViewModel
    {
        [Range(1, 1000)]
        public int BucketCapacity1 { get; set; }

        [Range(1, 1000)]
        public int BucketCapacity2 { get; set; }

        [Range(1, 1000)]
        public int DesiredAmount { get; set; }
    }
}