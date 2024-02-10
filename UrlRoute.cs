namespace EmployeeManagementApp
{
    public enum RouteType {

        HOME,
        LIST_EMPLOYEES,
        LIST_MANAGERS,
        LIST_LEAVES
    }

    public static class UrlRoute
    {
        public const string API_URL = "/api"; 
        public const string VERSION = "/v1";

        public const string HOME        
            = $"{API_URL}{VERSION}/";

        public const string LOGIN        
            = $"{API_URL}{VERSION}/login";

        public const string LOGOUT        
            = $"{API_URL}{VERSION}/logout";

        public const string EMPLOYEES   
            = $"{API_URL}{VERSION}/employees";

        public const string EMPLOYEE_VIEW   
            = $"{API_URL}{VERSION}/employees/profile";

        public const string EMPLOYEE_CREATE   
            = $"{API_URL}{VERSION}/employees/create";

        public const string LEAVE_REQUEST_CREATE   
            = $"{API_URL}{VERSION}/leaves/create";

        public const string LEAVE_REQUEST_APPROVE   
            = $"{API_URL}{VERSION}/leaves/approve";

        public const string MANAGERS    
            = $"{API_URL}{VERSION}/managers";

        public const string MANAGER_REPORTEES =
            "/managers/manager/reportees";
    }

}