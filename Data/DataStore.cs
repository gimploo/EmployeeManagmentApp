using System;
using EmployeeManagementApp.Models;

namespace EmployeeManagementApp.Data
{
    public class DataStore 
    {
        List<Employee> Employees;
        List<Role> Roles;
        List<LeaveRequest> LeaveRequests;

        public DataStore()
        {
            Employees = new List<Employee>();
            Roles = new List<Role>();
            LeaveRequests = new List<LeaveRequest>();
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
                && (employee.RoleId == (int)RoleTypes.EMPLOYEE))
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
                && (employee.RoleId == (int)RoleTypes.MANAGER))
                return employee;
            }
            return null;
            
        }
    }
}