using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace EmployeeManagementApp.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string FullName {get; set;}

        [Required]
        public string Username {get; set;}

        [Required]
        public string Password {get; set;}

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        public int RoleId { get; set; }

        [Required]
        public int ManagerId {get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }


        [ForeignKey("ManagerId")]
        public virtual Employee Manager { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
