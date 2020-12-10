namespace BibiBro.Client.Telegram.Model
{
    //TODO: Структура?
    public class Advertisement
    {
        public string Name { get; set; }
        public string Ref { get; set; }
        public string Engine { get; set; }
        public string Box { get; set; }
        public string Bodywork { get; set; }
        public string Drive { get; set; }
        public string Color { get; set; }
        public string Price { get; set; }
        public string Year { get; set; }
        public string KmAge { get; set; }
        public string Date { get; set; }
        public int Minutes { get; set; }

        public override string ToString()
        {
            return $"{Name}\n{Engine}\n{Box}\n{Bodywork}\n{Drive}\n{Color}\n{Price}\n{Year}\n{KmAge}\n{Date}\n{Ref}";
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Ref != null ? Ref.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Engine != null ? Engine.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Box != null ? Box.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Bodywork != null ? Bodywork.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Drive != null ? Drive.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Color != null ? Color.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Price != null ? Price.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Year != null ? Year.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (KmAge != null ? KmAge.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Advertisement other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other);
        }

        protected bool Equals(Advertisement other)
        {
            return Name == other.Name && Ref == other.Ref && Engine == other.Engine && Box == other.Box && Bodywork == other.Bodywork && Drive == other.Drive && Color == other.Color && Price == other.Price && Year == other.Year && KmAge == other.KmAge;
        }

        public static bool operator ==(Advertisement a, Advertisement b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Advertisement a, Advertisement b) => !(a == b);
    }
}
