using NUnit.Framework;

namespace OneCrmTestProject.Helpers
{
    public class ApiLoginRequestDto
    {
        public string Username { get; set; } = TestContext.Parameters["oneCrmUserName"];
        public string Password { get; set; } = TestContext.Parameters["oneCrmPassword"];
    }
}
