using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

    namespace WeddingPlanner.Models
    {
        public class User
        {
            
            [Key]
            public int UserId { get; set; }

            [Required]
            [MinLength(3)]
            public string FirstName { get; set; }

            [Required]
            [MinLength(3)]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [DataType(DataType.Password)]
            [Required]
            [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
            public string Password { get; set; }
           
            public DateTime CreatedAt {get;set;} = DateTime.Now;
            public DateTime UpdatedAt {get;set;} = DateTime.Now;

            [NotMapped]
            [Compare("Password")]
            [DataType(DataType.Password)]
            public string Confirm {get;set;}

            public List <Attending> WeddingsAttending {get;set;} // This is a list of all things from the many table that connects them 
            public List <Wedding> WeddingsCreated {get;set;} //This is the many sides created  wedding list  this is the UserId and the user user part this is only a list of things that connects them together
        }
        public class LoginUser
        {
            public string Email {get; set;}
            public string Password { get; set; }
        }

    }