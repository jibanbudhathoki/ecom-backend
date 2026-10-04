import re
import glob

files = glob.glob('Areas/Admin/Views/**/*.cshtml', recursive=True)

for file in files:
    with open(file, 'r') as f:
        content = f.read()

    # Find the block starting with <!-- Pagination --> up to the next </div> that ends it, or just use a regex
    # Looking at the output, the pagination is often wrapped in a nav or a div
    # Let's replace the <nav ...> ... </nav> block inside the pagination div
    
    # Or, even simpler: Let's find <ul class="pagination...</ul> and replace the parent <nav> or <div>.
    
    new_content = re.sub(r'<!-- Pagination -->\s*<div[^>]*>.*?</ul>\s*</nav>\s*</div>', r'<partial name="_Pagination" />', content, flags=re.DOTALL)
    
    # Wait, in Customers it was:
    # <div class="card-footer ...">
    #     <nav ...>
    #         <ul class="pagination ..."> ... </ul>
    #     </nav>
    # </div>
    new_content = re.sub(r'<div class="card-footer[^>]*>\s*<nav[^>]*>\s*<ul class="pagination.*?</nav>\s*</div>', r'<div class="card-footer bg-white border-top py-3">\n        <partial name="_Pagination" />\n    </div>', new_content, flags=re.DOTALL)
    
    new_content = re.sub(r'<div class="d-flex justify-content-between align-items-center p-4 border-top">\s*<span[^>]*>.*?</span>\s*<nav>\s*<ul class="pagination.*?</nav>\s*</div>', r'<div class="border-top">\n        <partial name="_Pagination" />\n    </div>', new_content, flags=re.DOTALL)
    
    new_content = re.sub(r'<div class="d-flex justify-content-end mt-4">\s*<nav aria-label="Page navigation">\s*<ul class="pagination.*?</nav>\s*</div>', r'<partial name="_Pagination" />', new_content, flags=re.DOTALL)
    
    if new_content != content:
        with open(file, 'w') as f:
            f.write(new_content)
            print(f"Updated {file}")

