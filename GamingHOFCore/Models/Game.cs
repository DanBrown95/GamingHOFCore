namespace GamingHOFCore.Models
{
    public class Game
    {
        public int Id { get; set; }
        public int PlatformId { get; set; }
        public string Name { get; set; }

        public virtual Platform Platform { get; set; }
    }
}
