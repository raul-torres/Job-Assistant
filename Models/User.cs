using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace JobAssistant.Models
{
    public class User
    {
        [Key]
        public int UserId{get;set;}


        [Required(ErrorMessage = "First name is required")]
        [MinLength(2, ErrorMessage ="First name must be atleast 2 characters long")]
        public string Name {get;set;}


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email address is not valid")]
        public string Email {get;set;}


        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage ="Password must be atleast 8 characters long")]
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression("^.*(?=.{6,18})(?=.*)(?=.*[A-Za-z]).*$", ErrorMessage = "Password must contain atleast 1 letter, 1 number")]
        public string Password {get;set;}

        public bool HasGoal {get;set;} = false;

/* -------------------------------------------------------------------------------- */
// DATETIMEs
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;


/* -------------------------------------------------------------------------------- */
// RELATIONS

        public List<Job> AppliedJobs {get;set;}

        public List<Goal> CreatedGoald {get;set;}


/* -------------------------------------------------------------------------------- */
// PASSWORD COMPARING

        [NotMapped]
        [Compare("Password", ErrorMessage="Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmedPassword {get;set;}

        [NotMapped]
        [DataType(DataType.Password)]
        public string OldPassword {get;set;}

        
    }
}