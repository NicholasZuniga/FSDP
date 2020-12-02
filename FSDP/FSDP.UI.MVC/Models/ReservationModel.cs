using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FSDP.DATA.EF;

namespace FSDP.UI.MVC.Models
{
    public class ReservationModel
    {
        public int VaseID { get; set; }
        public string VaseMaterial { get; set; }
        public string OwnerId { get; set; }
        public string VasePhoto { get; set; }
        public string SpecialNotes { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime DateAdded { get; set; }
        public IEnumerable<Vase> OwnedVases { get; set; }

        public int ReservationId { get; set; }

        public System.DateTime ReservationDate { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public byte ReservationLimit { get; set; }

        public ReservationModel()
        {
            OwnedVases = new List<Vase>().ToArray();
        }

        public override string ToString()
        {
            return $"{VaseID} {LocationId} {DateAdded}";
        }
    }
}