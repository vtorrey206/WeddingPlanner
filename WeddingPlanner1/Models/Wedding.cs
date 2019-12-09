using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

    namespace WeddingPlanner.Models
    {
        public class Wedding
        {
           [Key]
           public int WeddingId {get;set;}

           [Required]
           [MinLength(3)]
           public string WedderOne {get;set;}

           [Required]
           [MinLength(3)]
           public string WedderTwo {get;set;}

           [Required]
           
           public DateTime Date {get;set;}

           [Required]
           [MinLength(3)]
           public string Address {get;set;}
           public DateTime CreatedAt {get;set;} = DateTime.Now;
           public DateTime UpdatedAt {get;set;} = DateTime.Now;
           
           public List <Attending> Guest {get;set;}
           public int UserId {get;set;} //This is connecting the one side to the many relationship having a primary key as a forieng key is setting the many side to the one side its connecting the relationships
           public User User {get;set;} //This is the wedding creatore this is the one side of the one to many rlationship other nown as related name 

        }
    }

    