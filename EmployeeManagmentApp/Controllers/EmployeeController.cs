using System.Security.Claims;
using EmployeeManagementApp;
using EmployeeManagementApp.Data;
using EmployeeManagementApp.DTOs;
using EmployeeManagementApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementApp.Filters;

namespace EmployeeManagementApp.Controllers
{
    [ApiController]
    public class EmployeeController: ControllerBase
    {
        private readonly DataStore _dataStore;

        public EmployeeController(DataStore dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpPost(UrlRoute.LOGIN)]
        public IActionResult Login([FromBody] LoginDto log)
        {
            if (!ModelState.IsValid)
                return BadRequest( new {
                    ErrorMessage = "Invalid request body",
                    Errors = ModelState.Values
                });
            
            Employee emp = _dataStore.isValidEmployee(log.Username, log.Password);
            if (emp == null) 
                return Unauthorized("Invalid credentils");
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, emp.Id.ToString()),
                new Claim(ClaimTypes.Name, emp.FullName),
                new Claim(ClaimTypes.Role, Role.getRoleTypeString(emp.RoleId)),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return Ok("Login succesfull!");
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return Ok("Logged out successfully");
        }

        [HttpPost(UrlRoute.EMPLOYEE_CREATE)]
        [EMA_Auth(Roles = "MANAGER")]
        public IActionResult CreateEmployee([FromBody] EmployeeDto emp)
        {
            Employee employee = new Employee() {
                FullName = emp.FullName,
                Username = emp.Username,
                Password = emp.Password,
                DOB = emp.DOB,
                RoleId = (int)RoleTypes.EMPLOYEE,
                ManagerId = emp.ManagerId
            };

            _dataStore.AddEmployee(employee);

            return Ok("Employee Successfully Created!");
        }

        [HttpPost(UrlRoute.LEAVE_REQUEST_APPROVE)]
        [EMA_Auth(Roles = "MANAGER")]
        //Note: Approves the oldest leave
        public IActionResult ApproveLeave([FromBody] LeaveApproveDto leaveReq)
        {
            if (!ModelState.IsValid)
                return BadRequest( new {
                    ErrorMessage = "Invalid request body",
                    Errors = ModelState.Values
                });

            Employee ? emp = _dataStore.isValidEmployee(leaveReq.EmployeeId);
            Employee ? manager = _dataStore.isValidManager(leaveReq.ManagerId);

            if (emp == null) 
                return BadRequest(new {
                    ErrorMessage = "Non existent Employee id passed",
                });

            if (manager == null) 
                return BadRequest(new {
                    ErrorMessage = "Non existent Manager id passed",
                });
            
            if (emp.ManagerId != manager.Id)
                return BadRequest(new {
                    ErrorMessage = "Different manager assigned!"
                });
            
            List<LeaveRequest> leaves = _dataStore.GetPendingLeaveRequests(emp.Id);
            if (leaves.Count == 0) 
                return Ok("No Pending leaves");

            foreach(var leave in leaves)
            {
                if (leave.Id == leaveReq.LeaveId)
                {
                    leave.Status = LeaveStatus.APPROVED;
                    return Ok($"Approved {leaves[0].Id} of Employee `{emp.Id}` by Manager `{manager.Id}` is approved!");
                }
            }
            return BadRequest(new {
                ErrorMessage = "Invalid Leave id"
            });
        }

        [HttpPost(UrlRoute.LEAVE_REQUEST_CREATE)]
        [EMA_Auth(Roles = "EMPLOYEE")]
        public IActionResult ApplyLeave([FromBody] LeaveRequestDto leave)
        {
            if (!ModelState.IsValid)
                return BadRequest( new {
                    ErrorMessage = "Invalid request body",
                    Errors = ModelState.Values
                });
            
            Employee ? emp = _dataStore.isValidEmployee(leave.EmployeeId);
            Employee ? manager = _dataStore.isValidManager(leave.ManagerId);

            if (emp == null) 
                return BadRequest(new {
                    ErrorMessage = "Non existent Employee id passed",
                });

            if (manager == null) 
                return BadRequest(new {
                    ErrorMessage = "Non existent Manager id passed",
                });

            LeaveRequest leaveReq = new LeaveRequest() {
                EmployeeId = leave.EmployeeId,
                ManagerId = leave.ManagerId,
                Status = LeaveStatus.PENDING,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason,
            };
            
            _dataStore.AddLeaveRequest(leaveReq);
            

            return Ok("Leave successfully created!");
        }

        
    }
}