using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementApp.Models 
{
    public static class Role 
    {
        public enum Types {

            CEO         = 0,
            MANAGER     = 1,
            EMPLOYEE    = 2,

            UNKNOWN
        }

        public static Types getRoleType(string name)
        {
            Types output;
            try {

                output = (Types)Enum.Parse(typeof(Types), name.ToUpper());

            } catch(ArgumentException) {

                output = Role.Types.UNKNOWN;
            }
            return output;
        }
        public static string getRoleTypeString(int type)
        {
            Types output = (Types)type;
            Console.WriteLine($"{output} {output.ToString()}");
            return output.ToString();
        }
    }

}