namespace ProxyAdminConsole.Controllers
{
    public static class ServerVariable
    {
        // Server variables
        public const string SERVER_VARIABLE_TARGET_URI = "TARGET_URI";
        public const string SERVER_VARIABLE_CLIENT_REQUEST_ID = "HTTP_client_request_Id";
        public const string SERVER_VARIABLE_REMOTE_ADDR = "REMOTE_ADDR";
        public const string SERVER_VARIABLE_HTTP_COOKIE = "HTTP_COOKIE";
        public const string SERVER_VARIABLE_CACHE_URL = "CACHE_URL";
        public const string SERVER_VARIABLE_ACCEPT_ENCODING = "HTTP_ACCEPT_ENCODING";
        public const string SERVER_VARIABLE_ORIGINAL_ACCEPT_ENCODING = "ORIGINAL_HTTP_ACCEPT_ENCODING";
        public const string SERVER_VARIABLE_RESPONSE_CONTENT_TYPE = "RESPONSE_CONTENT_TYPE";
        public const string SERVER_VARIABLE_RESPONSE_STATUS = "RESPONSE_STATUS";
        public const string SERVER_VARIABLE_RESPONSE_LOCATION = "RESPONSE_LOCATION";
        public const string SERVER_VARIABLE_OUTBOUND_RULE_FILTER = "OUTBOUND_RULE_FILTER";
        public const string SERVER_VARIABLE_HTTP_REFERER = "HTTP_REFERER";
        public const string SERVER_VARIABLE_HTTP_X_REMOTE_USER = "HTTP_X_REMOTE_USER";
        public const string SERVER_VARIABLE_HTTP_X_TENANT = "HTTP_X_TENANT";
        public const string SERVER_VARIABLE_PROCESS_OUTBOUND_CONTENT_REWRITE = "PROCESS_OUTBOUND_CONTENT_REWRITE";
        public const string SERVER_VARIABLE_PROCESS_OUTBOUND_CONTENT_REWRITE_RETURNURL = "PROCESS_OUTBOUND_CONTENT_REWRITE_RETURNURL";
        public const string SERVER_VARIABLE_PROCESS_OUTBOUND_CONTENT_BADCHARS = "PROCESS_OUTBOUND_CONTENT_BADCHARS";
        public const string SERVER_VARIABLE_PROCESS_OUTBOUND_LOCATION_REDIRECT = "PROCESS_OUTBOUND_LOCATION_REDIRECT";
        public const string SERVER_VARIABLE_PROCESS_RESTORE_ACCEPT_ENCODING = "PROCESS_RESTORE_ACCEPT_ENCODING";
        public const string SERVER_VARIABLE_INTERNAL_URLS = "INTERNAL_URLS";
        public const string SERVER_VARIABLE_GATEWAY_ROOT_PATH = "GATEWAY_ROOT_PATH";
        public const string SERVER_VARIABLE_GATEWAYSERVER_ROOT_PATH = "WEBSERVER_ROOT_PATH";
        public const string SERVER_VARIABLE_HTTP_HOST = "HTTP_HOST";
        public const string SERVER_VARIABLE_ORIGINAL_HTTP_HOST = "ORIGINAL_HTTP_HOST";
        
        public const string ELEMENT_OUTBOUND_REMOVE_BADCHARS_PRECONDITION = "Remove <![ Characters Temporarily";
        public const string ELEMENT_OUTBOUND_REMOVE_BADCHARS_RULE_PROBLEM_STRING_BEING = @"<!\["; // add the \ to escape the regex special character [
        public const string ELEMENT_OUTBOUND_REMOVE_BADCHARS_RULE_PROBLEM_STRING_END = @"<![";
        public const string ELEMENT_OUTBOUND_REMOVE_BADCHARS_RULE_TEMP_STRING = "!Replace_Breaking_String!";
        
        public const string SERVER_VARIABLE_HTTP_X_UNPROXIED_URL = "HTTP_X_UNPROXIED_URL";
        public const string SERVER_VARIABLE_HTTP_X_ORIGINAL_ACCEPT_ENCODING = "HTTP_X_ORIGINAL_ACCEPT_ENCODING";
        public const string SERVER_VARIABLE_HTTP_X_ORIGINAL_HOST = "HTTP_X_ORIGINAL_HOST";
    }
}