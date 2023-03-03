using System;

namespace MeterReaderCMS.Models.DTO.Track
{
    public class TrackDTO
    {
        public int Id { get; set; }

        public int ElectricityMeterCalled { get; set; }

        public int ElectricityMeterUnCalled { get; set; }

        public string Desc { get; set; }

        public DateTime Date { get; set; }        
    }
}