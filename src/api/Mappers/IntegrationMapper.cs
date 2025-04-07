using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Dtos.VendorIntegration;
using api.Models;

namespace api.Mappers
{
    public static class IntegrationMapper
    {
        public static IntegrationDto ToIntegrationDto(this VendorIntegration model)
        {
            return new IntegrationDto
            {
                Id = model.Id,
                VendorId = model.VendorId,
                Name = model.Name,
                Description = model.Description,
                Category = model.Category,
                ApiUrl = model.ApiUrl,
                AuthMethod = model.AuthMethod,
                ApiKey = model.ApiKey,
                OAuthClientId = model.OAuthClientId,
                OAuthClientSecret = model.OAuthClientSecret,
                OAuthTokenUrl = model.OAuthTokenUrl,
                CertificateThumbprint = model.CertificateThumbprint,
                IsActive = model.IsActive,
                IntegrationType = model.IntegrationType,
                CreatedOn = model.CreatedOn
            };
        }

        public static VendorIntegration ToIntegrationFromUpsertDto(this UpsertIntegrationDto requestDto)
        {
            return new VendorIntegration
            {
                VendorId = requestDto.VendorId,
                Name = requestDto.Name,
                Description = requestDto.Description,
                Category = requestDto.Category,
                ApiUrl = requestDto.ApiUrl,
                AuthMethod = requestDto.AuthMethod,
                ApiKey = requestDto.ApiKey,
                OAuthClientId = requestDto.OAuthClientId,
                OAuthClientSecret = requestDto.OAuthClientSecret,
                OAuthTokenUrl = requestDto.OAuthTokenUrl,
                CertificateThumbprint = requestDto.CertificateThumbprint,
                IsActive = requestDto.IsActive,
                IntegrationType = requestDto.IntegrationType
            };
        }

    }

}
