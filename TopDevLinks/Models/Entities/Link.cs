using System;

namespace TopDevLinks.Models.Entities
{
    public class Link : IEquatable<Link>
    {
        public Uri Uri { get; private set; }
        public string Title { get; private set; }

        public Link(Uri uri, string title)
        {
            if (uri == null) throw new ArgumentNullException("uri");
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("title");

            Uri = uri;
            Title = title;
        }

        public bool Equals(Link other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Uri, Uri) && Equals(other.Title, Title);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Link)) return false;
            return Equals((Link) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Uri.GetHashCode()*397) ^ Title.GetHashCode();
            }
        }

        public static bool operator ==(Link left, Link right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Link left, Link right)
        {
            return !Equals(left, right);
        }
    }
}