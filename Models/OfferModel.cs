namespace bgbrokersapi.Models
{
    public class OfferModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string? SubTitle { get; set; }
        public string? Description { get; set; }

        public int? Floor { get; set; }
        public int? Squaring { get; set; }
        public int? RoomsCount { get; set; }
        public int? TotalFloors { get; set; }
        public int? BedroomsCount { get; set; }
        public int? TerracesCount { get; set; }
        public bool HasElevator { get; set; }
        public bool HasParkingPlace { get; set; }

        public string? OwnerPhoneNumber { get; set; }
        public string? ActualAddress { get; set; }


        public bool IsPublic { get; set; }
        public int? Priority { get; set; } // this will order the offers in TOP offers section
        public int? VisitsCount { get; set; }

        //Price and Currencies
        public double? Price { get; set; }
        public TypeOfferModel? Currency { get; set; }

        //Location
        public CityModel? City { get; set; }
        public HoodModel? Hood { get; set; }

        //Types
        public TypeOfferModel? Exposition { get; set; }
        public TypeOfferModel? Construction { get; set; }
        public TypeOfferModel? Furniture { get; set; }
        public TypeOfferModel? Heating { get; set; }
        public TypeOfferModel? OfferType { get; set; }
        public TypeOfferModel? SellType { get; set; }

        public List<ImageModel> Images { get; set; } = new List<ImageModel>();

        //Additional properties
        public bool IsDeleted { get; set; }

        public UserModel? Broker { get; set; }

    }
}
