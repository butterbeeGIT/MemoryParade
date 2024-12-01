namespace Assets.MemoryParade.Scripts.Game.MainMenu.Root
{
    /// <summary>
    /// входные параметры для сцены главного меню  
    /// </summary>
    public class EntryParamsMainMenu
    {
        /// <summary>
        /// количество убитых врагов за последний забег
        /// </summary>
        public int CountDeadEnemies { get; }

        /// <summary>
        /// приобретенные статы персонажа за последний забег
        /// </summary>
        public int[] CharacterStats { get; }

        /// <summary>
        /// передача входных параметров в конструкторе
        /// </summary>
        /// <param name="countDeadEnemies">количество убитых врагов</param>
        /// <param name="characterStats">статы набранные игроком за время забега</param>
        public EntryParamsMainMenu(int countDeadEnemies, int[] characterStats)
        {
            CountDeadEnemies = countDeadEnemies;
            CharacterStats = characterStats;
        }
    }
}