namespace OneCrmTestProject.Helpers
{
    /// <summary>
    /// Class used to specify to which menu items user would like to navigate
    /// </summary>
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
