using NUnit.Framework;

namespace OneCrmTestProject.Helpers
{
    /// <summary>
    /// Class used to define object used as API login request body
    /// </summary>
    public class ApiLoginRequestDto
    {
        public string Username { get; set; } = TestContext.Parameters["oneCrmUserName"];
        public string Password { get; set; } = TestContext.Parameters["oneCrmPassword"];
    }
}
