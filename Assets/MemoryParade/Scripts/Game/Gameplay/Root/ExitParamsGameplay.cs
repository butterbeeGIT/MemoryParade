using Assets.MemoryParade.Scripts.Game.MainMenu.Root;
namespace Assets.MemoryParade.Scripts.Game.Gameplay.Root
{
    public class ExitParamsGameplay
    {
        public EntryParamsMainMenu EntryParamsMainMenu;
        public ExitParamsGameplay(EntryParamsMainMenu entryParamsMainMenu){
            EntryParamsMainMenu = entryParamsMainMenu;
        }
    }
}