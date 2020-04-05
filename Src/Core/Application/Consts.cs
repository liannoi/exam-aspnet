using System.IO;

namespace Exam.Application
{
    public static class Consts
    {
        public static string FilmsMockPath => SearchInBaseDirectory("mock-films.json");
        public static string ActorsMockPath => SearchInBaseDirectory("mock-actors.json");

        private static string SearchInBaseDirectory(string fileName)
        {
            var split = Path.DirectorySeparatorChar;
            var up = $"{split}..";
            return $"{Directory.GetCurrentDirectory()}{up}{up}{up}{split}mocks{split}seeding{split}{fileName}";
        }
    }
}