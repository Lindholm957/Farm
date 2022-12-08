using Project.Scripts.Plants;

namespace Project.Scripts.Events.Base
{
    public static class EventNames
    {
        public static readonly string[] TEXT_ALL_NAMES = new[]
        {
            Game.Started,
            Game.SeedHasChosen,
            Game.PullingPlantHasChosen,
            Player.PlantingStarted,
            Player.SeedWasPlanted,
            Player.PlantWasPulled,
            Plant.HasGrown,
        };

        public static class Game
        {
            public static string Started => "started";
            public static string SeedHasChosen => "seed_has_chosen";
            public static string PullingPlantHasChosen => "pulling_plant_has_chosen";
        }

        public static class Player
        {
            public static string PlantingStarted => "planting_started";
            public static string SeedWasPlanted => "seed_was_planted";
            public static string PlantWasPulled => "plant_was_pulled";
        }
        
        public static class Plant
        {
            public static string HasGrown => "has_grown";
        }

        public static class Ui
        {

        }
    }
}