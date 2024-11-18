using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services;

public class DiscountService (DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    //when we create the proto file, visual studio has created the our crud operations automatically like a presentation layer so we're going to override the methods which is generated automatically to perform our crud operations

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        dbContext.Coupons.Add(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("discount successfully created");

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Discount Not Found"));

        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();


        logger.LogInformation("discount successfully deleted");

        return new DeleteDiscountResponse { Success = true };
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

        if (coupon is null) 
            coupon = new Models.Coupon { ProductName = "No Discount", Amount = 0, Description = "No Description" };

        logger.LogInformation("Discount is retrieved for ProductName : {productName}", request.ProductName);

        var couponModel = coupon.Adapt<CouponModel>();
        return couponModel;

    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request"));
        dbContext.Coupons.Update(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("discount successfully updated");

        var couponModel = coupon.Adapt<CouponModel>();

        return couponModel;
    }

}
