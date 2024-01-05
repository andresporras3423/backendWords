using backendWords.Models;

namespace backendWords.MappingClasses
{
    public static class MappingContext
    {
        public static List<WordMap> wordsToWordMaps(List<Word> words)
        {
            var wordMaps = new List<WordMap>();
            foreach (var word_ in words)
            {
                wordMaps.Add(word_.generateWordMap());
            }
            return wordMaps;
        }

        public static List<TechnoMap> technosToTechnoMaps(List<Techno> technos)
        {
            var technoMaps = new List<TechnoMap>();
            foreach (var techno_ in technos)
            {
                technoMaps.Add(techno_.generateTechnoMap());
            }
            return technoMaps;
        }

        public static List<TestMap> testToTestMaps(List<Test> tests)
        {
            var testMaps = new List<TestMap>();
            foreach (var test_ in tests)
            {
                testMaps.Add(test_.generateTestMap());
            }
            return testMaps;
        }
    }
}
