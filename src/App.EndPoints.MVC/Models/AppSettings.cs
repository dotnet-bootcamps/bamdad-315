namespace App.EndPoints.MVC.Models
{
    public class AppSettings
    {
        public Connectionstrings ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
        public string AllowedHosts { get; set; }
        public string AppName { get; set; }

    }

    public class Connectionstrings
    {
        public string AppDb { get; set; }
    }

    public class Logging
    {
        public Loglevel LogLevel { get; set; }
    }

    public class Loglevel
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }


}
