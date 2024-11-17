using Assets.MemoryParade.Scripts.Game.Gameplay.Root;

namespace Assets.MemoryParade.Scripts.Game.MainMenu.Root
{
    public class ExitParamsMainMenu
    {   
        public EntryParamsGameplay EntryParamsGameplay;

        public ExitParamsMainMenu(EntryParamsGameplay entryParamsGameplay){
            EntryParamsGameplay = entryParamsGameplay;
        }
    }       
}