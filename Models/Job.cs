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
        public string Position {get;set;}

        [Required(ErrorMessage = "Please include company name.")]
        [StringLength(27, ErrorMessage = "Company name cannot be more than 27 characters.")] 
        public string Company {get;set;}
        


        [Required(ErrorMessage = "Please include how you applied.")]
        [StringLength(25, ErrorMessage = "Website name cannot be more than 25 characters.")] 
        public string Website {get;set;}



        [Required(ErrorMessage = "Please include date you applied.")]
        [Column(TypeName="Date")]
        public DateTime ADate {get;set;}



        public string Status {get;set;} = "Application Sent";



        [Column(TypeName="Date")]
        public DateTime RDate {get;set;}



        [StringLength(233, ErrorMessage = "Notes cannot exceed 233 characters")] 
        public string Notes {get;set;}

        [StringLength(233, ErrorMessage = "Closed notes cannot exceed 233 characters")] 
        public string ClosedNotes {get;set;}


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