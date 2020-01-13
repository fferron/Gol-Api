using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gol.Entities
{
    public class Passenger
    {
        [Key]
        public int ID { get; set; }

        [StringLength(500, ErrorMessage = "Name cannot be longer than 500 characters.")]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegistryCreationDate { get; set; }


        public int? AirplaneID { get; set; }
        [JsonIgnore]
        public virtual Airplane Airplane { get; set; }
    }
}
