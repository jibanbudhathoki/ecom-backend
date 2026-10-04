import os
import re

files_with_old_pagination = [
    "Areas/Admin/Views/Customers/Index.cshtml",
    "Areas/Admin/Views/Products/Index.cshtml",
    "Areas/Admin/Views/Brands/Index.cshtml",
    "Areas/Admin/Views/Orders/Index.cshtml",
    "Areas/Admin/Views/Categories/Index.cshtml",
    "Areas/Admin/Views/Reviews/Index.cshtml"
]

files_without_pagination = [
    "Areas/Admin/Views/Inventory/Index.cshtml",
    "Areas/Admin/Views/Users/Index.cshtml",
    "Areas/Admin/Views/Roles/Index.cshtml",
    "Areas/Admin/Views/Collections/Index.cshtml",
    "Areas/Admin/Views/Pages/Index.cshtml"
]

for file in files_with_old_pagination:
    with open(file, 'r') as f:
        content = f.read()
    
    # We want to replace the whole <div class="d-flex ... justify-content-between ..."> ... <ul class="pagination ..."> ... </ul> ... </div> 
    # Or simply replace <nav aria-label="Page navigation"> ... </nav>
    # Let's use a regex to find the pagination block.
    # It's usually inside the card footer.
    
    content = re.sub(r'(<div class="card-footer[^>]*>).*?(</div>\s*</div>\s*<!--)', r'\1\n        <partial name="_Pagination" />\n    </div>\n</div>\n\n<!--', content, flags=re.DOTALL)
    
    # Let's try replacing from card-footer to its closing div
    # Wait, some might just have <ul class="pagination">
    
    with open(file, 'w') as f:
        f.write(content)

