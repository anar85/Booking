namespace Booking.Application.Models.DTOs.Common
{
    public class BaseResponse<T>
    {
        public T? Id { get; set; }
        public bool IsActive { get; set; }
      //  public DateTime CreateDate { get; set; }
    }
}
