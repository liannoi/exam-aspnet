using System.IO;

namespace Exam.Application
{
    public static class Consts
    {
        public static string FilmsMockPath
        {
            get
            {
                var split = Path.DirectorySeparatorChar;
                var up = $"{split}..";
                return $"{Directory.GetCurrentDirectory()}{up}{up}{up}{split}mocks{split}seeding{split}mock-films.json";
            }
        }
    }
}