﻿using bgbrokersapi.Data.Models;

namespace bgbrokersapi.Data.Models.Types
{
    public class Construction : Abstract.Type
    {
        public IEnumerable<Offer> Offers { get; set; } = new List<Offer>();

    }
}
