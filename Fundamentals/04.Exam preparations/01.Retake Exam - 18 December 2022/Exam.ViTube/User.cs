namespace Exam.ViTube
{
    public class User
    {
        public User(string id, string username)
        {
            Id = id;
            Username = username;
        }

        public string Id { get; set; }

        public string Username { get; set; }

        public int Watched { get; set; }

        public int Activity { get; set; }

        public override bool Equals(object obj) 
            => Id.Equals(((User)obj).Id);        
    }
}
