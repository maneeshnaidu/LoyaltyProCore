using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Data;
using api.Dtos.VendorIntegration;
using api.Helpers;
using api.Interfaces;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class IntegrationRepository : IIntegrationRepository
    {
        private readonly ApplicationDBContext _context;
        public IntegrationRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<VendorIntegration> CreateAsync(VendorIntegration model)
        {
            await _context.VendorIntegrations.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<VendorIntegration?> DeleteAsync(int id)
        {
            var integrationModel = await _context.VendorIntegrations.FirstOrDefaultAsync(x => x.Id == id);
            if (integrationModel == null)
            {
                return null;
            }
            _context.VendorIntegrations.Remove(integrationModel);
            await _context.SaveChangesAsync();
            return integrationModel;
        }

        public async Task<List<VendorIntegration>> GetAllAsync(QueryObject query)
        {
            var integrations = _context.VendorIntegrations.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    integrations = query.IsDecsending ? integrations.OrderByDescending(o => o.Name) : integrations.OrderBy(o => o.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await integrations.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<VendorIntegration?> GetByIdAsync(int id)
        {
            return await _context.VendorIntegrations.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<VendorIntegration?> UpdateAsync(int id, UpsertIntegrationDto requestDto)
        {
            var existingIntegration = await _context.VendorIntegrations.FirstOrDefaultAsync(x => x.Id == id);

            if (existingIntegration == null)
            {
                return null;
            }

            existingIntegration.Name = requestDto.Name;
            existingIntegration.Description = requestDto.Description;
            existingIntegration.Category = requestDto.Category;
            existingIntegration.VendorId = requestDto.VendorId;
            existingIntegration.IntegrationType = requestDto.IntegrationType;
            existingIntegration.AuthMethod = requestDto.AuthMethod;
            existingIntegration.ApiUrl = requestDto.ApiUrl;
            existingIntegration.ApiKey = requestDto.ApiKey;
            existingIntegration.OAuthClientId = requestDto.OAuthClientId;
            existingIntegration.OAuthClientSecret = requestDto.OAuthClientSecret;
            existingIntegration.OAuthTokenUrl = requestDto.OAuthTokenUrl;
            existingIntegration.CertificateThumbprint = requestDto.CertificateThumbprint;
            existingIntegration.IsActive = requestDto.IsActive;

            await _context.SaveChangesAsync();

            return existingIntegration;
        }

        public async Task<bool> VendorIntegrationExists(int id)
        {
            return await _context.VendorIntegrations.AnyAsync(v => v.Id == id);
        }
    }
}