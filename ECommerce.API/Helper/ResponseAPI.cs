namespace ECommerce.API.Helper
{
    public class ResponseAPI
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ResponseAPI(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? MessageBasedOnStatusCode(StatusCode);
        }

        private string MessageBasedOnStatusCode(int statusCode)
        {
            return statusCode switch
            {
                200 => "Done",
                400 => "BadRequest",
                401 => "UnAuthorized",
                403 => "Forbidden",
                404 => "NotFound",
                500 => "InternalServerError",
                 _  => "Unknown Status"
            };
        }
        
    }
}
