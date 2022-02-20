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
        public static List<Map> GetMaps(List<Office> offices)
        {
            return new List<Map>
            {
                new Map
                {
                    FloorNumber = 1,
                    HasKitchen = true,
                    HasMeetingRoom = true,
                    IsDeleted = false,
                    OfficeId = offices.Where(office => office.City == "Warsaw").First().Id
                },
                new Map
                {
                    FloorNumber = 2,
                    HasKitchen = true,
                    HasMeetingRoom = true,
                    IsDeleted = false,
                    OfficeId = offices.Where(office => office.City == "Warsaw").First().Id
                },
                new Map
                {
                    FloorNumber = 3,
                    HasKitchen = true,
                    HasMeetingRoom = true,
                    IsDeleted = false,
                    OfficeId = offices.Where(office => office.City == "Warsaw").First().Id
                },
            };
        }         
    }
}
