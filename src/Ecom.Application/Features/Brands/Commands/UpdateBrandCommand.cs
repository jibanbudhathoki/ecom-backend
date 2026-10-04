using MediatR;
using Ecom.Application.Common.Models;

namespace Ecom.Application.Features.Brands.Commands
{
    public class UpdateBrandCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
    }
}
