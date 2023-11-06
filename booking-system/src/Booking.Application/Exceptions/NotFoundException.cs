namespace Booking.Application.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string code, string message) : base(code, message) { }
        public NotFoundException(string code, string message, List<string> errors) : base(code, message, errors) { }
    }
}
