namespace EmployeeManagementApp.DTOs
{

    public class LeaveRequestDto
    {
        public int EmployeeId{ get; set; }

        public int ManagerId{ get; set; }

        public string Reason { get; set; }

        public LeaveStatus Status {get; set;}

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}