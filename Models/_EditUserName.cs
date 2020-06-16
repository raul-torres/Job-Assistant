using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace JobAssistant.Models
{
    public class EditUserName
    {
        [Required(ErrorMessage = "Name is required")]
        [MinLength(2, ErrorMessage ="Name must be atleast 2 characters long")]
        public string eName {get;set;}
    }

}