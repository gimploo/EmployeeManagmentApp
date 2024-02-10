using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Models 
{
    public enum RoleTypes {

        CEO         = 0,
        MANAGER     = 1,
        EMPLOYEE    = 2,

        UNKNOWN
    }


    public class Role 
    {
        [Key]
        public int Id {get; set;}

        [Required]
        public RoleTypes Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public static RoleTypes getRoleType(string name)
        {
            RoleTypes output;
            try {

                output = (RoleTypes)Enum.Parse(typeof(RoleTypes), name.ToUpper());

            } catch(ArgumentException) {

                output = RoleTypes.UNKNOWN;
            }
            return output;
        }
        public static string getRoleTypeString(int type)
        {
            RoleTypes output = (RoleTypes)type;
            Console.WriteLine($"{output} {output.ToString()}");
            return output.ToString();
        }
    }

}