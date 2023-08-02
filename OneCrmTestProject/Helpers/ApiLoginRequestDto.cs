using NUnit.Framework;

namespace OneCrmTestProject.Helpers
{
    public class ApiLoginRequestDto
    {
        public int Res_width { get; set; } = 1497;
        public int Res_height { get; set; } = 963;
        public string Username { get; set; } = TestContext.Parameters["oneCrmUserName"];
        public string Password { get; set; } = TestContext.Parameters["oneCrmPassword"];
        public string Remember { get; set; } = string.Empty;
        public string Language { get; set; } = "en_us";
        public string Theme { get; set; } = "Flex";
        public string Login_module { get; set; } = "Home";
        public string Login_action { get; set; } = "index";
        public string Login_record { get; set; } = string.Empty;
        public string Login_layout { get; set; } = string.Empty;
        public string Mobile { get; set; } = "0";
        public int Gtmo { get; set; } = -60;
    }
}
