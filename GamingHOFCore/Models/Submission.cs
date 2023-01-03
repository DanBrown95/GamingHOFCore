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
        public int PlatformId { get; set; }
        public int GameId { get; set; }
        public int Rank { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public bool IsProcessed { get; set; }
        public bool IsApproved { get; set; }

        public virtual User Creator { get; set; }
        public virtual Platform Platform { get; set; }
        public virtual Game Game { get; set; }


    }
}
