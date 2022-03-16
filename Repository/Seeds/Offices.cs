using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Seeds
{
    public static partial class TestData
    {
        public static List<Office> GetOffices()
        {
            return new List<Office>()
            {
                new Office
                {
                    Name = "Tbilisi office",
                    Country = "Georgia",
                    City = "Tbilisi",
                    Address = "24 Ilo Mosashvili St, Tbilisi 0162",
                    HasFreeParking = true,
                    IsDeleted = false
                },

                new Office
                {
                    Name = "Kiev office",
                    Country = "Ukraine",
                    City = "Kiev",
                    Address = "Yaroslaviv Val St, 15",
                    HasFreeParking = true,
                    IsDeleted = false
                },

                new Office
                {
                    Name = "Warsaw office",
                    Country = "Poland",
                    City = "Warsaw",
                    Address = "Emilii Plater 28",
                    HasFreeParking = true,
                    IsDeleted = false
                },

                new Office
                {
                    Name = "Berlin office",
                    Country = "Germany",
                    City = "Berlin",
                    Address = "Emilii Plater 28",
                    HasFreeParking = false,
                    IsDeleted = false
                },

                new Office
                {
                    Name = "Paris office",
                    Country = "France",
                    City = "Paris",
                    Address = "Emilii Plater 28",
                    HasFreeParking = false,
                    IsDeleted = false
                },
                new Office
                {
                    Name = "Stockholm office",
                    Country = "Sweden",
                    City = "Stockholm",
                    Address = "Emilii Plater 28",
                    HasFreeParking = true,
                    IsDeleted = false
                }

            };
        }
    }
}
