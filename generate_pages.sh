#!/bin/bash

CONTROLLERS=(
    "Catalog:Products,Categories,Brands,Collections,Attributes"
    "Orders:Index,Pending,Processing,Shipped,Delivered,Cancelled,Returns"
    "Inventory:Stock,LowStock,Movements,Warehouses"
    "Customers:Index"
    "Payments:Transactions,Refunds"
    "Marketing:Promotions,Coupons,Banners"
    "Reviews:Index"
    "Content:Pages,Newsletter"
    "Reports:Sales,Orders,Products,Customers,Inventory"
    "Users:AdminUsers,Roles,Permissions,ActivityLogs"
    "Settings:General,StoreSetup"
)

for entry in "${CONTROLLERS[@]}"; do
    CONTROLLER_NAME="${entry%%:*}"
    ACTIONS="${entry##*:}"
    
    # Create Controller
    cat << CS_EOF > src/Ecom.Admin/Controllers/${CONTROLLER_NAME}Controller.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Admin.Controllers
{
    [Authorize]
    public class ${CONTROLLER_NAME}Controller : Controller
    {
CS_EOF
    
    mkdir -p src/Ecom.Admin/Views/${CONTROLLER_NAME}
    
    IFS=',' read -ra ACTION_ARRAY <<< "$ACTIONS"
    for ACTION in "${ACTION_ARRAY[@]}"; do
        # Add action to controller
        cat << CS_EOF >> src/Ecom.Admin/Controllers/${CONTROLLER_NAME}Controller.cs
        public IActionResult ${ACTION}()
        {
            return View();
        }
CS_EOF

        # Create View
        cat << HTML_EOF > src/Ecom.Admin/Views/${CONTROLLER_NAME}/${ACTION}.cshtml
@{
    ViewData["Title"] = "${ACTION}";
}
<h2>${CONTROLLER_NAME} - ${ACTION}</h2>
<p>This is a simple placeholder page for ${ACTION}.</p>
HTML_EOF
    done
    
    # Close Controller class
    cat << CS_EOF >> src/Ecom.Admin/Controllers/${CONTROLLER_NAME}Controller.cs
    }
}
CS_EOF
done

rm src/Ecom.Admin/Controllers/ProductsController.cs
rm -rf src/Ecom.Admin/Views/Products

