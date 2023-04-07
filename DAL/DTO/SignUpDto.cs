using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PMS.Models.Models.DTO
{
    public class SignUpDto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Age is required")]
        [DisplayName("Age")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [DisplayName("Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email-Id is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email-Id is Invalid")]
        [DisplayName("Email-Id")]
        public string Email_Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [DisplayName("Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Confirm Your Password to Sign-up")]
        [DisplayName("Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Your Password Not Identical")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Roles is required")]
        [DisplayName("Roles")]
        public string Roles { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }
    public enum Roles
    {
        Admin,
        User
    }


}