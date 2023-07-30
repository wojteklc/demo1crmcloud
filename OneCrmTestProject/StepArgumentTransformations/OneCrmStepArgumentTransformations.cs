using OneCrmTestProject.Helpers;
using TechTalk.SpecFlow;

namespace OneCrmTestProject.StepArgumentTransformations
{
    [Binding]
    public class OneCrmStepArgumentTransformations
    {
        /// <summary>
        /// Transformation which allows to convert strings like 'Sales & Marketing -> Contacts' to 'MenuPath' object
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [StepArgumentTransformation]
        public MenuPath SubMenuPathTransform(string path)
        {
            var pathSplitted = path.Split("->");
            var mainMenuItemToSelectAsString = pathSplitted[0].Replace("&", "And").Replace(" ", "").Trim();
            Enum.TryParse(mainMenuItemToSelectAsString, out MainMenuTabs mainMenuItemToSelectAsEnum);

            if (pathSplitted.Length > 1)
            {
                var subMenuItemToSelectAsString = pathSplitted[1].Replace(" ", "").Trim();
                Enum.TryParse(subMenuItemToSelectAsString, out SubmenuOptions subMenuItemToSelectAsEnum);

                return new MenuPath(mainMenuItemToSelectAsEnum, subMenuItemToSelectAsEnum);
            }

            return new MenuPath(mainMenuItemToSelectAsEnum, null);
        }
    }
}
