namespace ParlayIO.Domain
{
    public class User
    {
        public string Username { get; set; }
        public string UID { get; set; }
        public string Initials { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }

        public User() { }
        public User(string username, string uID, string initials, byte red, byte green, byte blue)
        {
            Username = username;
            UID = uID;
            Initials = initials;
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}
