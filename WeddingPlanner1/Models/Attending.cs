using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

    namespace WeddingPlanner.Models
    {
        public class Attending// This is the joined side
        {
           [Key]
           public int AttendingId {get;set;}//every table needs a id of its own
           public int WeddingId {get;set;} //This had the id of the one side witch makes this the many side and the id links them together
           public int UserId {get;set;} //This is the id of the one side and this id is here to connect this many side to the one side

           public User User {get;set;} // This needs to be here so the attending table knows what the userid is
           public Wedding Wedding {get;set;} // This needs to be here so the table knows where the weeding id is comming from

        }
    }
