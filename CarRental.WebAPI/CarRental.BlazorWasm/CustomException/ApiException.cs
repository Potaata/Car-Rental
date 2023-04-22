namespace CarRental.BlazorWasm.CustomException
{
    public class ApiException: Exception
    {
        public ApiException(string message): base(message)
        {
            
        }
    }
}
