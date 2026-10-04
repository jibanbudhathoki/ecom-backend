import re
import glob

files = [
    "Areas/Admin/Views/Inventory/Index.cshtml",
    "Areas/Admin/Views/Users/Index.cshtml",
    "Areas/Admin/Views/Roles/Index.cshtml",
    "Areas/Admin/Views/Collections/Index.cshtml"
]

for file in files:
    with open(file, 'r') as f:
        content = f.read()
    
    if "_Pagination" not in content:
        # insert before the final </div></div> if table-responsive is there
        new_content = re.sub(r'(</tbody>\s*</table>\s*</div>\s*</div>)', r'</tbody>\n        </table>\n    </div>\n    <div class="card-footer bg-white border-top py-3">\n        <partial name="_Pagination" />\n    </div>\n</div>', content)
        
        if new_content != content:
            with open(file, 'w') as f:
                f.write(new_content)
                print(f"Added to {file}")

