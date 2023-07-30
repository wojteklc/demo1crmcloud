namespace OneCrmTestProject.Helpers
{
    public class MenuPath
    {
        public MainMenuTabs MainMenuTab { get; set; }
        public SubmenuOptions? SubmenuOption { get; set; }
        
        public MenuPath(MainMenuTabs mainMenuTab, SubmenuOptions? submenuOption)
        {
            MainMenuTab = mainMenuTab;
            SubmenuOption = submenuOption;
        }
    }
}
