namespace Assets.MemoryParade.Scripts.Game.Gameplay.Root
{
    public class EntryParamsGameplay
    {
        /// <summary>
        /// Уровень сложности
        /// </summary>
        public int DifficultyLevel { get; }

        /// <summary>
        /// Глобавльные статы персонажа
        /// </summary>
        public int[] GlobalStats { get; }

        /// <summary>
        /// является ли сюжетным прохождением
        /// </summary>
        public bool IsPlot { get; }

        public EntryParamsGameplay(int difficultyLevel, int[] globalStats, bool isPlot = false)
        {
            DifficultyLevel = difficultyLevel;
            GlobalStats = globalStats;
            IsPlot = isPlot;
        }
    }
}