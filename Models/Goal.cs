using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace JobAssistant.Models
{
    public class Goal
    {
        [Key]
        public int GoalId {get;set;}

        [Required(ErrorMessage = "Include how many jobs you aim to apply to")]
        [Range(1, 1000, ErrorMessage="Amount has to be between 1 and 1000")]
        public int? Amount {get;set;}

        public int SoFar {get;set;} = 0;

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