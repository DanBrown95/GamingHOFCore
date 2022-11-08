using System;

namespace GamingHOFCore.Models
{
    public class Submission
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime Submitted { get; set; }
        public int Votes { get; set; }
        public int Rank { get; set; }
        public string Platform { get; set; }
    }
}
