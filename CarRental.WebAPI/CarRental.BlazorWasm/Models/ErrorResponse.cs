namespace CarRental.BlazorWasm.Models
{
    public class ErrorResponse
    {
        public string ErrorMessage { get; set; }

        public ErrorResponse(Dictionary<string, List<string>> errors)
        {
            //find any error in errors and return.
            string? error = errors[errors.Keys.First()][0];
            ErrorMessage = error ?? "The API did not return 200 OK but did not send any errorMessage either. Please check the endpoint.";
        }
    }
}
