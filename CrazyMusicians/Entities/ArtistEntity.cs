using System.Threading.Tasks.Dataflow;

namespace CrazyMusicians.Entities
{
    public class ArtistEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profession { get; set; }
        public string FunFeature { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
    }
}
