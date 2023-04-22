using CarRental.BlazorWasm.CustomException;

namespace CarRental.BlazorWasm.Models
{
    public class BaseResponse
    {
        // TODO make these methods which take some parameters about the error 
        // to make it easier for dubugging

        // Failed To Fetch API. Make sure API project is running and the endpoint is accessible.
        public static BaseResponse FailedToFetchError = new BaseResponse
        { ErrorMessage = "Failed to access server!" };
        
        // The API did not return 200 OK but did not send any errorMessage either. Please check the endpoint.
        public static BaseResponse NoErrorMessage = new BaseResponse
        { ErrorMessage = "Something went wrong!" };

        public string? ErrorMessage;
        public SuccessResponse? Response;

        public Resp GetResponse<Resp> () where Resp : SuccessResponse
        {
            if (ErrorMessage != null)
            {
                throw new ApiException(ErrorMessage);
            }
            return (Resp) Response;
        }
    }
}
