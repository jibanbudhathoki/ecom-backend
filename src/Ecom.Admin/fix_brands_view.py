import re

with open('/Users/jibanmac/Desktop/develope/Ecom/src/Ecom.Admin/Areas/Admin/Views/Brands/Index.cshtml', 'r') as f:
    content = f.read()

content = content.replace(
    '@model IEnumerable<Ecom.Application.Features.Brands.Queries.BrandDto>',
    '@model Ecom.Application.Common.Models.PagedResult<Ecom.Application.Features.Brands.Queries.BrandDto>'
)

content = content.replace(
    '@Model.Count() items total',
    '@Model.TotalCount items total'
)

content = content.replace(
    '@if (!Model.Any())',
    '@if (!Model.Items.Any())'
)

content = content.replace(
    '@foreach (var brand in Model)',
    '@foreach (var brand in Model.Items)'
)

search_block = '''<form method="get" class="card border-0 shadow-sm rounded-4 mb-4">
    <div class="card-body p-3">
        <div class="row g-2">
            <div class="col-md-4">
                <div class="input-group input-group-sm">
                    <span class="input-group-text bg-light border-0"><i class="bi bi-search text-muted"></i></span>
                    <input type="text" name="SearchTerm" value="@Context.Request.Query["SearchTerm"]" class="form-control bg-light border-0 shadow-none" placeholder="Search brands...">
                </div>
            </div>
            <div class="col-md-4">
                <select name="SortBy" class="form-select form-select-sm border-0 bg-light rounded-pill shadow-none" onchange="this.form.submit()">
                    <option value="">Sort By</option>
                    <option value="name" selected="@(Context.Request.Query["SortBy"] == "name")">Name</option>
                </select>
            </div>
            <div class="col-md-2">
                <!-- Spacing -->
            </div>
            <div class="col-md-2 text-end">
                <button type="submit" class="btn btn-light btn-sm rounded-pill w-100 border text-muted">
                    <i class="bi bi-funnel"></i> Filter
                </button>
            </div>
        </div>
    </div>
</form>'''

old_search_block = '''<div class="card border-0 shadow-sm rounded-4 mb-4">
    <div class="card-body p-3">
        <div class="row g-2">
            <div class="col-md-4">
                <div class="input-group input-group-sm">
                    <span class="input-group-text bg-light border-0"><i class="bi bi-search text-muted"></i></span>
                    <input type="text" class="form-control bg-light border-0 shadow-none" placeholder="Search brands...">
                </div>
            </div>
            <div class="col-md-6">
                <!-- Spacing -->
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

with open('/Users/jibanmac/Desktop/develope/Ecom/src/Ecom.Admin/Areas/Admin/Views/Brands/Index.cshtml', 'w') as f:
    f.write(content)
