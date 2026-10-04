import re

with open('/Users/jibanmac/Desktop/develope/Ecom/src/Ecom.Admin/Areas/Admin/Views/Products/Index.cshtml', 'r') as f:
    content = f.read()

content = content.replace(
    '@model IEnumerable<Ecom.Application.Features.Products.Queries.ProductDto>',
    '@model Ecom.Application.Common.Models.PagedResult<Ecom.Application.Features.Products.Queries.ProductDto>'
)

content = content.replace(
    '@Model.Count() items total',
    '@Model.TotalCount items total'
)

content = content.replace(
    '@foreach (var item in Model)',
    '@foreach (var item in Model.Items)'
)

# Fix filters and search
search_block = '''<form method="get" class="card border-0 shadow-sm rounded-4 mb-4">
    <div class="card-body p-4">
        <div class="row g-3">
            <div class="col-md-4">
                <div class="input-group">
                    <span class="input-group-text bg-light border-0 rounded-start-pill ps-3">
                        <i class="bi bi-search text-muted"></i>
                    </span>
                    <input type="text" name="SearchTerm" value="@Context.Request.Query["SearchTerm"]" class="form-control bg-light border-0 rounded-end-pill shadow-none" placeholder="Search products...">
                </div>
            </div>
            <div class="col-md-3">
                <select name="CategoryId" class="form-select border-0 bg-light rounded-pill shadow-none" onchange="this.form.submit()">
                    <option value="">All Categories</option>
                    @* TODO: Populate options dynamically or handle via JS *@
                </select>
            </div>
            <div class="col-md-3">
                <select name="SortBy" class="form-select border-0 bg-light rounded-pill shadow-none" onchange="this.form.submit()">
                    <option value="">Sort By</option>
                    <option value="name" selected="@(Context.Request.Query["SortBy"] == "name")">Name</option>
                    <option value="price" selected="@(Context.Request.Query["SortBy"] == "price")">Price</option>
                    <option value="stock" selected="@(Context.Request.Query["SortBy"] == "stock")">Stock</option>
                </select>
            </div>
            <div class="col-md-2 text-end">
                <button type="submit" class="btn btn-light rounded-pill w-100 border text-muted">
                    <i class="bi bi-funnel"></i> Filter
                </button>
            </div>
        </div>
    </div>
</form>'''

old_search_block = '''<div class="card border-0 shadow-sm rounded-4 mb-4">
    <div class="card-body p-4">
        <div class="row g-3">
            <div class="col-md-4">
                <div class="input-group">
                    <span class="input-group-text bg-light border-0 rounded-start-pill ps-3">
                        <i class="bi bi-search text-muted"></i>
                    </span>
                    <input type="text" class="form-control bg-light border-0 rounded-end-pill shadow-none" placeholder="Search products...">
                </div>
            </div>
            <div class="col-md-3">
                <select class="form-select border-0 bg-light rounded-pill shadow-none">
                    <option selected>All Categories</option>
                    <option value="1">Electronics</option>
                    <option value="2">Clothing</option>
                    <option value="3">Home & Garden</option>
                </select>
            </div>
            <div class="col-md-3">
                <select class="form-select border-0 bg-light rounded-pill shadow-none">
                    <option selected>All Statuses</option>
                    <option value="active">Active</option>
                    <option value="draft">Draft</option>
                    <option value="outofstock">Out of Stock</option>
                </select>
            </div>
            <div class="col-md-2 text-end">
                <button class="btn btn-light rounded-pill w-100 border text-muted">
                    <i class="bi bi-funnel"></i> Filter
                </button>
            </div>
        </div>
    </div>
</div>'''

content = content.replace(old_search_block, search_block)

with open('/Users/jibanmac/Desktop/develope/Ecom/src/Ecom.Admin/Areas/Admin/Views/Products/Index.cshtml', 'w') as f:
    f.write(content)
