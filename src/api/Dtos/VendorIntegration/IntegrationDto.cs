using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.VendorIntegration
{
    public class IntegrationDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty; // e.g., "Order", "POS", etc. 
        public int VendorId { get; set; }
        public string IntegrationType { get; set; } = "API"; // API, SFTP, etc.
        public string AuthMethod { get; set; } = "ApiKey"; // ApiKey, OAuth2, ClientCertificate, etc.

        public required string ApiUrl { get; set; } // Base API endpoint

        // Credentials — only relevant based on AuthMethod
        public string? ApiKey { get; set; }
        public string? OAuthClientId { get; set; }
        public string? OAuthClientSecret { get; set; }
        public string? OAuthTokenUrl { get; set; }
        public string? CertificateThumbprint { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedOn { get; set; }
    }
}