namespace OneCrmTestProject.Helpers
{
    /// <summary>
    /// Class used to deserialize API login response content
    /// </summary>
    public class ApiLoginResponseDto
    {
        public string Result { get; set; }
        public string Json_session_id { get; set; }
        public string User_id { get; set; }
        public string Session_name { get; set; }
        public string Redirect { get; set; }
    }
}
