using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace JobAssistant.Models
{
    public class Job
    {
        [Key]
        public int JobId {get;set;}



        [Required(ErrorMessage = "Please include company name.")]
        [StringLength(27, ErrorMessage = "Company name cannot be more than 27 characters.")] 
        public string Company {get;set;}
        


        [Required(ErrorMessage = "Please include how you applied.")]
        [StringLength(25, ErrorMessage = "Website name cannot be more than 25 characters.")] 
        public string AppliedWebsite {get;set;}



        [Required(ErrorMessage = "Please include date you applied.")]
        [Column(TypeName="Date")]
        public DateTime AppliedDate {get;set;}



        public string status {get;set;} = "Applied(Waiting)";



        [Column(TypeName="Date")]
        public DateTime ResponseDate {get;set;}



        [StringLength(233, ErrorMessage = "Company name cannot be more than 27 characters.")] 
        public string JobNotes {get;set;}

/* -------------------------------------------------------------------------------- */
// RELATIONS

        public int UserId{get;set;}
        public User Creator{get;set;}

/* -------------------------------------------------------------------------------- */
// DATETIMEs
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;

    }
}