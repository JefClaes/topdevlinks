using System;
using MongoDB.Bson;

namespace TopDevLinks.Models.Entities
{
    public class Link : Entity, IEquatable<Link>
    {
        public Uri Uri { get; private set; }
        public string Title { get; private set; }
        public ObjectId CategoryId { get; private set; }
        public ObjectId UserId { get; private set; }
        public bool Flagged { get; private set; }

        public Link(Uri uri, string title, ObjectId categoryId, ObjectId userId)
        {
            if (uri == null) throw new ArgumentNullException("uri");
            if (string.IsNullOrEmpty(title)) throw new ArgumentException("title can not be empty", "title");
            if (categoryId == ObjectId.Empty) throw new ArgumentException("categoryId must have a value", "categoryId");
            if (userId == ObjectId.Empty) throw new ArgumentException("userId must have a value", "userId");

            Uri = uri;
            Title = title;
            CategoryId = categoryId;
            UserId = userId;
            Flagged = false;
        }

        public void Flag(bool on) 
        {
            Flagged = on;
        }

        public bool Equals(Link other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Uri, Uri) && Equals(other.Title, Title) && other.CategoryId.Equals(CategoryId) && other.UserId.Equals(UserId);
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
                result = (result * 397) ^ UserId.GetHashCode();
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