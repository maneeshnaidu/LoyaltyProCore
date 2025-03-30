using api.Models;
using api.Dtos.Points;

namespace api.Mappers
{
    public static class PointsMapper
    {
        public static PointsDto ToPointDto(this Points pointModel)
        {
            return new PointsDto
            {
                Id = pointModel.Id,
                CustomerId = pointModel.CustomerId,
                VendorId = pointModel.VendorId,
                OutletId = pointModel.OutletId,
                Point = pointModel.Point,
                Level = pointModel.Level,
                LastUpdatedOn = pointModel.LastUpdatedOn
            };
        }

        public static Points ToPointFromCreateDto(this UpsertPointsDto pointDto)
        {
            return new Points
            {
                CustomerId = pointDto.CustomerId,
                VendorId = pointDto.VendorId,
                OutletId = pointDto.OutletId,
                Point = pointDto.Point,
                Level = pointDto.Level,
                LastUpdatedOn = pointDto.LastUpdatedOn
            };
        }
    }
}