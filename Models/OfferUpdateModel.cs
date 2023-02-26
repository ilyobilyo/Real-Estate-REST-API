namespace bgbrokersapi.Models
{
    public class OfferUpdateModel
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
        public string? BrokerId { get; set; }

        public bool IsPublic { get; set; }
        public int? Priority { get; set; } // this will order the offers in TOP offers section

        //Price and Currencies
        public double? Price { get; set; }

        public int? CurrencyId { get; set; }

        //Location
        public int? CityId { get; set; }
        public int? HoodId { get; set; }

        //Types
        public int? ExpositionId { get; set; }

        public int? ConstructionId { get; set; }

        public int? FurnitureId { get; set; }

        public int? HeatingId { get; set; }

        public int? OfferTypeId { get; set; }

        public int? SellTypeId { get; set; }

        public IEnumerable<string> Images { get; set; } = new List<string>();
    }
}
