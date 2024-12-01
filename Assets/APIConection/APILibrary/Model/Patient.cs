

using System;

namespace APILibraryDaltonismo.Model
{
    
    public class Patient
    {
        public string DNI { get; set; }
        public string Name { get; set; }
        public  string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
