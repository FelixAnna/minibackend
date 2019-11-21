using BookingOffline.Entities;

namespace BookingOffline.Common
{
    public interface ITokenGeneratorService
    {
        string CreateJwtToken(AlipayUser alipayUser);
    }
}