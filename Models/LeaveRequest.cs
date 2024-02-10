using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp
{
    public enum LeaveStatus {
        PENDING,
        APPROVED,
        DENIED
    }

    public class LeaveRequest
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public int EmployeeId{ get; set; }

        [Required]
        public int ManagerId{ get; set; }

        [Required]
        public string Reason { get; set; }

        [Required]
        public LeaveStatus Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }

        [ForeignKey("ManagerId")]
        public virtual Employee Manager { get; set; }
    }
    
}