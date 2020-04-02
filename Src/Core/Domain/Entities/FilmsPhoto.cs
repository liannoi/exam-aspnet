namespace Exam.Domain.Entities
{
    public class FilmsPhoto
    {
        public int PhotoId { get; set; }
        public int FilmId { get; set; }
        public string Path { get; set; }

        public Film Film { get; set; }
    }
}