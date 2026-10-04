#!/bin/bash
FILES=(
    "Areas/Admin/Views/Products/Create.cshtml"
    "Areas/Admin/Views/Products/Edit.cshtml"
    "Areas/Admin/Views/Brands/Create.cshtml"
    "Areas/Admin/Views/Brands/Edit.cshtml"
    "Areas/Admin/Views/Categories/Create.cshtml"
    "Areas/Admin/Views/Categories/Edit.cshtml"
    "Areas/Admin/Views/Collections/Create.cshtml"
)

for file in "${FILES[@]}"; do
    if [ -f "$file" ]; then
        echo "Processing $file"
        
        # Remove 'checked' from toggleRichText input
        sed -i '' 's/id="toggleRichText" checked/id="toggleRichText"/g' "$file"
        
        # Remove the toggleRichText JS block. It looks like:
        # const toggleRichText = document.getElementById('toggleRichText');
        # const richTextToolbar = document.getElementById('richTextToolbar');
        # if (toggleRichText && richTextToolbar) {
        #     toggleRichText.addEventListener('change', (e) => {
        #         if(e.target.checked) {
        #             richTextToolbar.classList.remove('d-none');
        #         } else {
        #             richTextToolbar.classList.add('d-none');
        #         }
        #     });
        # }
        # Let's just use perl or awk to delete this block. 
        # The block starts with "const toggleRichText" and ends after the '}' of the if statement.
        perl -0777 -pi -e 's/^[ \t]*const toggleRichText = document\.getElementById\('\'toggleRichText\''\);\s*const richTextToolbar = document\.getElementById\('\'richTextToolbar\''\);\s*if \(toggleRichText && richTextToolbar\) \{\s*toggleRichText\.addEventListener\('\''change'\'', \(e\) => \{\s*if ?\(e\.target\.checked\) \{\s*richTextToolbar\.classList\.remove\('\''d-none'\''\);\s*\} else \{\s*richTextToolbar\.classList\.add\('\''d-none'\''\);\s*\}\s*\}\);\s*\}//ms' "$file"

    fi
done
