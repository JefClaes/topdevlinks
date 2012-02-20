using System;
using MongoDB.Bson;

namespace TopDevLinks.Models.Entities
{
    public class Link : IEquatable<Link>
    {
        public Uri Uri { get; private set; }
        public string Title { get; private set; }
        public ObjectId CategoryId { get; private set; }

        public Link(Uri uri, string title, ObjectId categoryId)
        {
            if (uri == null) throw new ArgumentNullException("uri");
            if (string.IsNullOrEmpty(title)) throw new ArgumentNullException("title");
            if (categoryId == ObjectId.Empty) throw new ArgumentNullException("categoryId");

            Uri = uri;
            Title = title;
            CategoryId = categoryId;
        }

        public bool Equals(Link other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Uri, Uri) && Equals(other.Title, Title) && other.CategoryId.Equals(CategoryId);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Link)) return false;
            return Equals(( Link )obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Uri != null ? Uri.GetHashCode() : 0);
                result = (result * 397) ^ (Title != null ? Title.GetHashCode() : 0);
                result = (result * 397) ^ CategoryId.GetHashCode();
                return result;
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