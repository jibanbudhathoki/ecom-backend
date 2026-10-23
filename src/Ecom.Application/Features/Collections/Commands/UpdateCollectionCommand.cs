using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Ecom.Application.Common.Models;
using MediatR;

namespace Ecom.Application.Features.Collections.Commands
{
    public class UpdateCollectionCommand : IRequest<Result>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Collection title is required.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug is required.")]
        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string Type { get; set; } = "Manual";
        public string? ConditionsJson { get; set; }
        public bool IsActive { get; set; } = true;

        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }

        public List<int> ProductIds { get; set; } = new();
    }
}
