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
        public MenuPathDto SubMenuPathTransform(string path)
        {
            var pathSplitted = path.Split("->");
            var mainMenuItemToSelectAsString = pathSplitted[0].Replace("&", "And").Replace(" ", string.Empty).Trim();
            Enum.TryParse(mainMenuItemToSelectAsString, out MainMenuTabs mainMenuItemToSelectAsEnum);

            if (pathSplitted.Length > 1)
            {
                var subMenuItemToSelectAsString = pathSplitted[1].Replace(" ", string.Empty).Trim();
                Enum.TryParse(subMenuItemToSelectAsString, out SubmenuOptions subMenuItemToSelectAsEnum);

                return new MenuPathDto(mainMenuItemToSelectAsEnum, subMenuItemToSelectAsEnum);
            }

            return new MenuPathDto(mainMenuItemToSelectAsEnum, null);
        }
    }
}
