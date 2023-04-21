namespace CarRental.BlazorWasm.CustomException
{
    public class ApiException: Exception
    {
        public string ErrorMessage { get; set; }

        public ApiException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
