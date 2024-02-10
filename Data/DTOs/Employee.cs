namespace EmployeeManagementApp.DTOs
{
    public class EmployeeDto
    {
        public string FullName {get; set;}
        public string Username {get; set;}
        public string Password {get; set;}

        public DateTime DOB { get; set; }

        public int ManagerId {get; set;}

    }
}