using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FSDP.DATA.EF
{
    #region LocationMetadata
    public class LocationMetadata
    {
        [Display(Name = "Location ID")]
        public int LocationId { get; set; }

        [Display(Name = "Location Name")]
        [Required(ErrorMessage = "* Location Name is required")]
        [StringLength(50, ErrorMessage = "* Location Name cannot be longer than 50 characters")]
        public string LocationName { get; set; }

        [Required(ErrorMessage = "* Address is required")]
        [StringLength(100, ErrorMessage = "* Address cannot be longer than 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "* City is required")]
        [StringLength(100, ErrorMessage = "* City cannot be longer than 100 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "* State is required")]
        [StringLength(2, ErrorMessage = "* State cannot be longer than 2 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "* Zipcode is required")]
        [StringLength(5, ErrorMessage = "* Zipcode cannot be longer than 5 characters")]
        public string Zipcode { get; set; }

        [Display(Name = "Reservation Limit")]
        [Range(0,255,ErrorMessage ="* Must be a number between 0 and 255")]
        [Required(ErrorMessage ="*A Reservation limit must be set")]
        public byte ReservationLimit { get; set; }
    }

    [MetadataType(typeof(LocationMetadata))]
    public partial class Location
    {

    }
    #endregion

    #region ReservationMetadata
    public class ReservationMetadata
    {
        [Display(Name ="Reservation ID")]
        public int ReservationId { get; set; }

        [Display(Name = "Vase ID")]
        public int VaseID { get; set; }

        [Display(Name = "Location ID")]
        public int LocationId { get; set; }

        [Display(Name ="Reservation Date")]
        [DisplayFormat(DataFormatString ="{0:d}, ApplyFormatInEditMode=true")]
        [Required(ErrorMessage ="* Reservation date must be selected")]
        public System.DateTime ReservationDate { get; set; }
    }

    [MetadataType(typeof(ReservationMetadata))]
    public partial class Reservation
    {

    }

    #endregion

    #region OwnerDetailMetadata

    public class OwnerDetailMetadata
    {
        [Display(Name = "Owner ID")]
        public string OwnerId { get; set; }

        [Required(ErrorMessage ="* First Name is required")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "* First Name cannot be longer than 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="* Last Name is required")]
        [Display(Name ="Last Name")]
        [StringLength(50, ErrorMessage = "* Last Name cannot be longer than 50 characters")]
        public string LastName { get; set; }
    }

    [MetadataType(typeof(OwnerDetailMetadata))]
    public partial class OwnerDetail
    {
        [Display(Name ="Client Name")]
        public string ClientName
        {
            get { return FirstName + " " + LastName}
        }
    }

    #endregion

    #region VaseMetadata
    public class Vasemetadata
    {
        [Display(Name ="Vase ID")]
        public int VaseID { get; set; }

        [Display(Name ="Vase Material")]
        [Required(ErrorMessage ="* Material of the Vase must be given")]
        [StringLength(50,ErrorMessage ="* Material cannot be longer than 50 characters")]
        public string VaseMaterial { get; set; }

        [Display(Name ='Owner ID')]
        public string OwnerId { get; set; }

        [Display(Name ="Vase Photo")]
        [StringLength(50, ErrorMessage ="* photo cannot be longer than 50 characters")]
        public string VasePhoto { get; set; }

        [UIHint("MultilineText")]
        [Display(Name = "Special Notes")]
        [StringLength(300, ErrorMessage = "* photo cannot be longer than 300 characters")]
        public string SpecialNotes { get; set; }

        [Display(Name ="Still Being Serviced")]
        public bool IsActive { get; set; }

        [Display(Name = "Date Registered")]
        [DisplayFormat(DataFormatString = "{0:d}, ApplyFormatInEditMode=true")]
        [Required(ErrorMessage = "* Date must be selected")]
        public System.DateTime DateAdded { get; set; }
    }

    [MetadataType(typeof(Vasemetadata))]
    public partial class Vase
    {

    }
    #endregion
}
