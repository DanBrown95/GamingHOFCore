using System;

namespace GamingHOFCore.Models
{
    public class Submission
    {
        public Submission()
        {
            Creator = new User();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Submitted { get; set; }
        public int Votes { get; set; }
        public int Rank { get; set; }
        public string Platform { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }

        public User Creator { get; set; }
    }
}
