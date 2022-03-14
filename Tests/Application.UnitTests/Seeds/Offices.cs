using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Seeds
{
    public static partial class TestData
    {
        public static List<Office> GetOffices()
        {
            return new List<Office>()
            {
                new Office
                {
                    Id = 1,
                    Name = "Tbilisi office",
                    Country = "Georgia",
                    City = "Tbilisi",
                    Address = "24 Ilo Mosashvili St, Tbilisi 0162",
                    HasFreeParking = true,
                    IsDeleted = false
                },

                new Office
                {
                    Id=2,
                    Name = "Kiev office",
                    Country = "Ukraine",
                    City = "Kiev",
                    Address = "Yaroslaviv Val St, 15",
                    HasFreeParking = true,
                    IsDeleted = false
                },

                new Office
                {
                    Id =3,
                    Name = "Warsaw office",
                    Country = "Poland",
                    City = "Warsaw",
                    Address = "Emilii Plater 28",
                    HasFreeParking = true,
                    IsDeleted = false
                }

            };
        }
    }
}
