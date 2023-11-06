namespace Booking.Application.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string code, string message) : base(code, message) { }
        public UnauthorizedException(string code, string message, List<string> errors) : base(code, message, errors) { }
    }
}
