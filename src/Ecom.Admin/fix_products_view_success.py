with open('/Users/jibanmac/Desktop/develope/Ecom/src/Ecom.Admin/Areas/Admin/Views/Products/Index.cshtml', 'r') as f:
    content = f.read()

success_block = """
@if (TempData["SuccessMessage"] != null)
{
    <div class="alert alert-success alert-dismissible fade show rounded-4 shadow-sm mb-4" role="alert">
        <i class="bi bi-check-circle me-2"></i>@TempData["SuccessMessage"]
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>
}
"""

# Find where to insert it - right before <!-- Filters and Search -->
content = content.replace("<!-- Filters and Search -->", success_block + "\n<!-- Filters and Search -->")

with open('/Users/jibanmac/Desktop/develope/Ecom/src/Ecom.Admin/Areas/Admin/Views/Products/Index.cshtml', 'w') as f:
    f.write(content)
