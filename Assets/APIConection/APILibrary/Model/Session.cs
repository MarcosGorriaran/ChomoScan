using System;

namespace APILibraryDaltonismo.Model
{
    public class Session
    {
        public string SessionID { get; set; }
        public string ColorBlindType { get; set; }
        public DateTime DateGame { get; set; }
        public Patient player { get; set; }
    }
}
