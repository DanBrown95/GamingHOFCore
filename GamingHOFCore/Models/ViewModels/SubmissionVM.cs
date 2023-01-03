using System;

namespace GamingHOFCore.Models.ViewModels
{
    public class SubmissionVM
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Gamertag { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; }
        public int Votes { get; set; }
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
        public string GameName { get; set; }
        public int Rank { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public DateTime Submitted { get; set; }

    }
}
