namespace Booking.Application.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string code, string message) : base(code, message) { }
        public BadRequestException(string code, string message, List<string> errors) : base(code, message, errors) { }
    }
}
