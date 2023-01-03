using System;

namespace GamingHOFCore.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Gamertag { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveBanDate { get; set; }
    }
}
