namespace OneCrmTestProject.Helpers
{
    public class MenuPathDto
    {
        public MainMenuTabs MainMenuTab { get; set; }
        public SubmenuOptions? SubmenuOption { get; set; }
        
        public MenuPathDto(MainMenuTabs mainMenuTab, SubmenuOptions? submenuOption)
        {
            MainMenuTab = mainMenuTab;
            SubmenuOption = submenuOption;
        }
    }
}
