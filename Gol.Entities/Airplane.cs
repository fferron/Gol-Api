using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Newtonsoft.Json;

namespace Gol.Entities
{
    public class Airplane
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50, ErrorMessage = "Airplane Model cannot be longer than 50 characters.")]
        public string AirplaneModel { get; set; }
        public int NumberOfPassengers { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegistryCreationDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Passenger> Passengers { get; set; }
        public Airplane()
        {
            Passengers = new List<Passenger>();
        }
    }
}
