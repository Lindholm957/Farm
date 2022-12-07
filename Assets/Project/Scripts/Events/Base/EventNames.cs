namespace Project.Scripts.Events.Base
{
    public static class EventNames
    {
        public static readonly string[] TEXT_ALL_NAMES = new[]
        {
            Game.SeedHasChosen,
            Game.PullingPlantHasChosen,
            Player.SeedWasPlanted,
            Player.PlantWasPulled,
        };

        public static class Game
        {
            public static string SeedHasChosen => "seed_has_chosen";
            public static string PullingPlantHasChosen => "pulling_plant_has_chosen";
        }

        public static class Player
        {
            public static string SeedWasPlanted => "seed_was_planted";
            public static string PlantWasPulled => "plant_was_pulled";
        }

        public static class Ui
        {

        }
    }
}