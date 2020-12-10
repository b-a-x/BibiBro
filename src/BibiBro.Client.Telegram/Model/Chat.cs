namespace BibiBro.Client.Telegram.Model
{
    //TODO: Структура?
    public class Chat
    {
        public Chat(long id)
        {
            Id = id;
        }

        public long Id { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Chat other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        protected bool Equals(Chat other)
        {
            return Id == other.Id;
        }

        public static bool operator ==(Chat a, Chat b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Chat a, Chat b) => !(a == b);
    }
}
