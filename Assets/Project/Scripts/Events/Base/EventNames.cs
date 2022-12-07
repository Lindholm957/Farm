namespace Project.Scripts.Events.Base
{
    public static class EventNames
    {
        public static readonly string[] TEXT_ALL_NAMES = new[]
        {
            Game.SeedWasChosen,
            Player.PlantedTheSeed,
        };

        public static class Game
        {
            public static string SeedWasChosen => "seed_was_chosen";
        }

        public static class Player
        {
            public static string PlantedTheSeed => "planted_the_seed";
        }

        public static class Ui
        {

        }
    }
}