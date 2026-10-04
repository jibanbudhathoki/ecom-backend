using System.ComponentModel.DataAnnotations;
using Ecom.Application.Common.Models;
using MediatR;

namespace Ecom.Application.Features.Brands.Commands
{
    public class CreateBrandCommand : IRequest<Result>
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }
}
