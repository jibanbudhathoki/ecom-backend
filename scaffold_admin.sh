#!/bin/bash

# Define the sections based on the folders
SECTIONS=("Banners" "Brands" "Categories" "Customers" "Dashboard" "Inventory" "Orders" "Payments" "Products" "Promotions" "Reports" "Reviews" "Roles" "Settings" "Users")

for SECTION in "${SECTIONS[@]}"; do
    
    # 1. Populate the Controller
    cat << CS_EOF > src/Ecom.Admin/Areas/Admin/Controllers/${SECTION}Controller.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ${SECTION}Controller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
CS_EOF

    # 2. Create the Views folder if it doesn't exist (though it should)
    mkdir -p src/Ecom.Admin/Areas/Admin/Views/${SECTION}
    
    # 3. Populate the simple Index.cshtml View
    cat << HTML_EOF > src/Ecom.Admin/Areas/Admin/Views/${SECTION}/Index.cshtml
@{
    ViewData["Title"] = "${SECTION}";
}

<div class="d-flex justify-content-between align-items-center mb-4">
    <h2 class="fw-bold mb-0">${SECTION}</h2>
</div>

<p class="text-muted">This is the simple, empty placeholder page for ${SECTION}. You can start adding your tables and forms here!</p>
HTML_EOF

done

