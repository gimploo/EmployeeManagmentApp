using System;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data
{
    public class DataStore 
    {
        List<Employee> Employees;
        List<LeaveRequest> LeaveRequests;

        public DataStore()
        {
            Employees = new List<Employee>();
            LeaveRequests = new List<LeaveRequest>();

            _Seeder();
        }

        private void _Seeder()
        {
            Employees.Add(new Employee
            {
                Username = "admin",
                FullName = "Administrator",
                Password = "password",
                Role = Role.Types.MANAGER
            });
        }
        public IEnumerable<Employee> GetReportersForManager(int id)
        {
            List<Employee> employees = new List<Employee>();
            foreach(var employee in Employees)
            {
                if (employee.ManagerId == id)
                    employees.Add(employee);
            }

            var employess = new List<Employee>();

            foreach (var employee in employees)
            {
                var employeeWithoutPassword = new Employee
                {
                    Id = employee.Id,
                    FullName = employee.FullName,
                    Role = employee.Role,
                    ManagerId = employee.ManagerId,
                    Username = employee.Username,
                    Password = "*************",
                };

                employess.Add(employeeWithoutPassword);
            }
            return employess;
        }

        public void AddEmployee(Employee emp)
        {
            Employees.Add(emp);
        }
        public void AddLeaveRequest(LeaveRequest leave)
        {
            LeaveRequests.Add(leave);
        }

        public List<LeaveRequest> GetPendingLeaveRequests(int employeeId)
        {
            List<LeaveRequest> leaves = new List<LeaveRequest>();
            foreach(var leave in LeaveRequests)
            {
                if ((leave.EmployeeId == employeeId) 
                && (leave.Status == LeaveStatus.PENDING))
                    leaves.Add(leave);
            }
            return leaves;
        }

        public Employee ? isValidEmployee(int employeeId)
        {
            foreach(var employee in Employees)
            {
                if ((employee.Id ==  employeeId)
                && (employee.Role == Role.Types.EMPLOYEE))
                return employee;
            }
            return null;
        }

        public Employee ? isValidEmployee(string username, string password)
        {
            foreach(var employee in Employees)
            {
                if ((employee.Username ==  username) 
                && (employee.Password == password))
                    return employee;
            }
            return null;
        }

        public Employee ? isValidManager(int employeeId)
        {
            foreach(var employee in Employees)
            {
                if ((employee.Id ==  employeeId)
                && (employee.Role == Role.Types.MANAGER))
                return employee;
            }
            return null;
            
        }
    }
}