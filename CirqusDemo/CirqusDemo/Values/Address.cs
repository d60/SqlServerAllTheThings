using System;

namespace CirqusDemo.Values
{
    public class Address : IEquatable<Address>
    {
        public Address(string street, string houseNumnber, string postalCode, string city)
        {
            if (street == null) throw new ArgumentNullException("street");
            if (houseNumnber == null) throw new ArgumentNullException("houseNumnber");
            if (postalCode == null) throw new ArgumentNullException("postalCode");
            if (city == null) throw new ArgumentNullException("city");
            Street = street;
            HouseNumnber = houseNumnber;
            PostalCode = postalCode;
            City = city;
        }

        public string Street { get; private set; }

        public string HouseNumnber { get; private set; }

        public string PostalCode { get; private set; }

        public string City { get; private set; }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Street, other.Street) && string.Equals(HouseNumnber, other.HouseNumnber) && string.Equals(PostalCode, other.PostalCode) && string.Equals(City, other.City);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Address) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Street.GetHashCode();
                hashCode = (hashCode*397) ^ HouseNumnber.GetHashCode();
                hashCode = (hashCode*397) ^ PostalCode.GetHashCode();
                hashCode = (hashCode*397) ^ City.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Address left, Address right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !Equals(left, right);
        }
    }
}