namespace bgbrokersapi.Data.Models
{
    using bgbrokersapi.Data.Models;
    using bgbrokersapi.Data.Models.Abstract;
    using bgbrokersapi.Data.Models.OfferLocation;
    using bgbrokersapi.Data.Models.Types;
    using bgbrokersapi.Data.Models.User;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Offer : BaseModel
    {
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
        public ApplicationUser? Broker { get; set; }

        public bool IsPublic { get; set; }
        public int? Priority { get; set; } // this will order the offers in TOP offers section
        public int? VisitsCount { get; set; }

        //Price and Currencies
        public double? Price { get; set; }

        public int? CurrencyId { get; set; }
        [ForeignKey(nameof(CurrencyId))]
        public Currency? Currency { get; set; }

        //Location
        public int? CityId { get; set; }
        public City? City { get; set; }
        public int? HoodId { get; set; }
        public Hood? Hood { get; set; }

        //Types
        public int? ЕxpositionId { get; set; }
        [ForeignKey(nameof(ЕxpositionId))]
        public Exposition? Exposition { get; set; }


        public int? ConstructionId { get; set; }
        [ForeignKey(nameof(ConstructionId))]
        public Construction? Construction { get; set; }


        public int? FurnitureId { get; set; }
        [ForeignKey(nameof(FurnitureId))]
        public Furniture? Furniture { get; set; }


        public int? HeatingId { get; set; }
        [ForeignKey(nameof(HeatingId))]
        public Heating? Heating { get; set; }


        public int? OfferTypeId { get; set; }
        [ForeignKey(nameof(OfferTypeId))]
        public OfferType? OfferType { get; set; }


        public int? SellTypeId { get; set; }
        [ForeignKey(nameof(SellTypeId))]
        public SellType? SellType { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();
    }
}
