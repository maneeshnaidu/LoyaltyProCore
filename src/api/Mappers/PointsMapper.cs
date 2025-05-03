using api.Models;
using api.Dtos.RewardPoints;

namespace api.Mappers
{
    public static class PointsMapper
    {
        public static PointsDto ToPointDto(this RewardPoints pointModel)
        {
            return new PointsDto
            {
                Id = pointModel.Id,
                CustomerId = pointModel.CustomerId,
                RewardId = pointModel.RewardId,
                VendorId = pointModel.VendorId,
                OutletId = pointModel.OutletId,
                Point = pointModel.Points,
                Level = pointModel.Level,
                LastUpdatedOn = pointModel.LastUpdatedOn
            };
        }

        public static RewardPoints ToPointFromCreateDto(this UpsertPointsDto pointDto)
        {
            return new RewardPoints
            {
                CustomerId = pointDto.CustomerId,
                RewardId = pointDto.RewardId,
                VendorId = pointDto.VendorId,
                OutletId = pointDto.OutletId,
                StaffId = pointDto.StaffId,
                Points = pointDto.Point,
                Level = pointDto.Level,
                LastUpdatedOn = pointDto.LastUpdatedOn
            };
        }

        public static UpsertPointsDto ToUpsertDtoFromModel(this RewardPoints pointModel)
        {
            return new UpsertPointsDto
            {
                CustomerId = pointModel.CustomerId,
                RewardId = pointModel.RewardId,
                VendorId = pointModel.VendorId,
                OutletId = pointModel.OutletId,
                StaffId = pointModel.StaffId,
                Point = pointModel.Points,
                Level = pointModel.Level,
                LastUpdatedOn = pointModel.LastUpdatedOn
            };
        }
    }
}