using System.Collections.Generic;

namespace ECS_Framework
{
    /// <summary>
    /// Manages the loading and retrieval of game levels.
    /// </summary>
    public class LevelManager
    {
        private Dictionary<LevelID, Level> levels;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelManager"/> class and loads all available levels.
        /// </summary>
        public LevelManager()
        {
            levels = new Dictionary<LevelID, Level>();
            InitializeLevels();
        }

        /// <summary>
        /// Populates the levels dictionary with Level objects.
        /// </summary>
        private void InitializeLevels()
        {
            levels.Add(LevelID.HomeScreen, new Level(LevelID.HomeScreen, new HomeScreenInitializer()));
            levels.Add(LevelID.Level1, new Level(LevelID.Level1, new Level1Initializer()));
            levels.Add(LevelID.Level2, new Level(LevelID.Level2, new Level2Initializer()));
            levels.Add(LevelID.Level3, new Level(LevelID.Level3, new Level3Initializer()));
            levels.Add(LevelID.Level5, new Level(LevelID.Level5, new Level5Initializer()));
            // Add more levels here
        }

        /// <summary>
        /// Retrieves a Level object by its LevelID.
        /// </summary>
        /// <param name="levelID">The ID of the level to retrieve.</param>
        /// <returns>The Level object associated with the given LevelID.</returns>
        public Level GetLevel(LevelID levelID)
        {
            return levels[levelID];
        }
    }
}