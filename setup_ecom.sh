#!/bin/bash
set -e

# Setup basic structure
mkdir -p docs/architecture docs/api docs/deployment
mkdir -p scripts/database scripts/development
mkdir -p src tests

# Solution
dotnet new sln -n Ecom --force

# Projects
cd src
dotnet new classlib -n Ecom.Domain -f net10.0 --no-restore
dotnet new classlib -n Ecom.Application -f net10.0 --no-restore
dotnet new classlib -n Ecom.Infrastructure -f net10.0 --no-restore
dotnet new webapi -n Ecom.Api -f net10.0 --no-restore
dotnet new mvc -n Ecom.Admin -f net10.0 --no-restore
cd ..

cd tests
dotnet new xunit -n Ecom.Domain.Tests -f net10.0 --no-restore
dotnet new xunit -n Ecom.Application.Tests -f net10.0 --no-restore
dotnet new xunit -n Ecom.Api.Tests -f net10.0 --no-restore
dotnet new xunit -n Ecom.Integration.Tests -f net10.0 --no-restore
cd ..

# Strip Version attribute/elements from generated csproj files so CPM can work
find src tests -name "*.csproj" -type f -exec sed -i '' -e 's/ Version="[^"]*"//g' {} +
find src tests -name "*.csproj" -type f -exec sed -i '' -e 's/<Version>[^<]*<\/Version>//g' {} +

# Add to solution
dotnet sln add src/Ecom.Domain/Ecom.Domain.csproj
dotnet sln add src/Ecom.Application/Ecom.Application.csproj
dotnet sln add src/Ecom.Infrastructure/Ecom.Infrastructure.csproj
dotnet sln add src/Ecom.Api/Ecom.Api.csproj
dotnet sln add src/Ecom.Admin/Ecom.Admin.csproj

dotnet sln add tests/Ecom.Domain.Tests/Ecom.Domain.Tests.csproj
dotnet sln add tests/Ecom.Application.Tests/Ecom.Application.Tests.csproj
dotnet sln add tests/Ecom.Api.Tests/Ecom.Api.Tests.csproj
dotnet sln add tests/Ecom.Integration.Tests/Ecom.Integration.Tests.csproj

# Project References
dotnet add src/Ecom.Application/Ecom.Application.csproj reference src/Ecom.Domain/Ecom.Domain.csproj
dotnet add src/Ecom.Infrastructure/Ecom.Infrastructure.csproj reference src/Ecom.Domain/Ecom.Domain.csproj src/Ecom.Application/Ecom.Application.csproj
dotnet add src/Ecom.Api/Ecom.Api.csproj reference src/Ecom.Application/Ecom.Application.csproj src/Ecom.Infrastructure/Ecom.Infrastructure.csproj
dotnet add src/Ecom.Admin/Ecom.Admin.csproj reference src/Ecom.Application/Ecom.Application.csproj src/Ecom.Infrastructure/Ecom.Infrastructure.csproj

# Create Folders
# Ecom.Domain
mkdir -p src/Ecom.Domain/Common src/Ecom.Domain/Entities/Catalog src/Ecom.Domain/Entities/Customer src/Ecom.Domain/Entities/Cart \
src/Ecom.Domain/Entities/Order src/Ecom.Domain/Entities/Inventory src/Ecom.Domain/Entities/Payment src/Ecom.Domain/Entities/Promotion \
src/Ecom.Domain/Entities/Review src/Ecom.Domain/Entities/Content src/Ecom.Domain/Entities/Identity src/Ecom.Domain/Enums \
src/Ecom.Domain/ValueObjects src/Ecom.Domain/Exceptions src/Ecom.Domain/Interfaces

# Ecom.Application
mkdir -p src/Ecom.Application/Common/Behaviors src/Ecom.Application/Common/Interfaces src/Ecom.Application/Common/Exceptions \
src/Ecom.Application/Common/Models src/Ecom.Application/Mapping src/Ecom.Application/Features/Authentication src/Ecom.Application/Features/Products \
src/Ecom.Application/Features/Categories src/Ecom.Application/Features/Brands src/Ecom.Application/Features/Collections src/Ecom.Application/Features/Customers \
src/Ecom.Application/Features/Cart src/Ecom.Application/Features/Wishlist src/Ecom.Application/Features/Orders src/Ecom.Application/Features/Inventory \
src/Ecom.Application/Features/Payments src/Ecom.Application/Features/Promotions src/Ecom.Application/Features/Reviews src/Ecom.Application/Features/Banners \
src/Ecom.Application/Features/Newsletter src/Ecom.Application/Features/Dashboard src/Ecom.Application/Features/Reports src/Ecom.Application/Features/Settings

# Ecom.Infrastructure
mkdir -p src/Ecom.Infrastructure/Persistence/Configurations/Catalog src/Ecom.Infrastructure/Persistence/Configurations/Customer \
src/Ecom.Infrastructure/Persistence/Configurations/Cart src/Ecom.Infrastructure/Persistence/Configurations/Order \
src/Ecom.Infrastructure/Persistence/Configurations/Inventory src/Ecom.Infrastructure/Persistence/Configurations/Payment \
src/Ecom.Infrastructure/Persistence/Configurations/Promotion src/Ecom.Infrastructure/Persistence/Configurations/Review \
src/Ecom.Infrastructure/Persistence/Configurations/Identity src/Ecom.Infrastructure/Persistence/Repositories src/Ecom.Infrastructure/Persistence/Interceptors \
src/Ecom.Infrastructure/Migrations src/Ecom.Infrastructure/Identity src/Ecom.Infrastructure/Authentication/Jwt src/Ecom.Infrastructure/Authentication/Cookies \
src/Ecom.Infrastructure/Payments/Esewa src/Ecom.Infrastructure/Payments/Khalti src/Ecom.Infrastructure/Payments/Fonepay src/Ecom.Infrastructure/Payments/CashOnDelivery \
src/Ecom.Infrastructure/Storage/Local src/Ecom.Infrastructure/Storage/Cloud src/Ecom.Infrastructure/Caching/Redis src/Ecom.Infrastructure/Notifications/Email \
src/Ecom.Infrastructure/Notifications/Sms src/Ecom.Infrastructure/Notifications/Push src/Ecom.Infrastructure/BackgroundJobs src/Ecom.Infrastructure/Logging

# Ecom.Api
mkdir -p src/Ecom.Api/Controllers src/Ecom.Api/Middleware src/Ecom.Api/Filters src/Ecom.Api/Extensions src/Ecom.Api/OpenApi src/Ecom.Api/Models/Requests src/Ecom.Api/Models/Responses

# Ecom.Admin
mkdir -p src/Ecom.Admin/Areas/Admin/Controllers src/Ecom.Admin/Areas/Admin/Views/Dashboard src/Ecom.Admin/Areas/Admin/Views/Products src/Ecom.Admin/Areas/Admin/Views/Categories \
src/Ecom.Admin/Areas/Admin/Views/Brands src/Ecom.Admin/Areas/Admin/Views/Orders src/Ecom.Admin/Areas/Admin/Views/Customers src/Ecom.Admin/Areas/Admin/Views/Inventory \
src/Ecom.Admin/Areas/Admin/Views/Payments src/Ecom.Admin/Areas/Admin/Views/Promotions src/Ecom.Admin/Areas/Admin/Views/Banners src/Ecom.Admin/Areas/Admin/Views/Reviews \
src/Ecom.Admin/Areas/Admin/Views/Reports src/Ecom.Admin/Areas/Admin/Views/Users src/Ecom.Admin/Areas/Admin/Views/Roles src/Ecom.Admin/Areas/Admin/Views/Settings \
src/Ecom.Admin/Areas/Admin/ViewModels src/Ecom.Admin/Controllers src/Ecom.Admin/Views/Account src/Ecom.Admin/Views/Shared src/Ecom.Admin/ViewModels \
src/Ecom.Admin/Authorization src/Ecom.Admin/Filters src/Ecom.Admin/Extensions src/Ecom.Admin/wwwroot/css src/Ecom.Admin/wwwroot/js src/Ecom.Admin/wwwroot/images src/Ecom.Admin/wwwroot/lib

# Tests
mkdir -p tests/Ecom.Domain.Tests/Entities tests/Ecom.Domain.Tests/ValueObjects tests/Ecom.Domain.Tests/Common
mkdir -p tests/Ecom.Application.Tests/Features/Products tests/Ecom.Application.Tests/Features/Orders tests/Ecom.Application.Tests/Features/Customers tests/Ecom.Application.Tests/Features/Payments tests/Ecom.Application.Tests/Common
mkdir -p tests/Ecom.Api.Tests/Controllers tests/Ecom.Api.Tests/Middleware tests/Ecom.Api.Tests/Authentication
mkdir -p tests/Ecom.Integration.Tests/Fixtures tests/Ecom.Integration.Tests/Authentication tests/Ecom.Integration.Tests/Products tests/Ecom.Integration.Tests/Orders tests/Ecom.Integration.Tests/Payments tests/Ecom.Integration.Tests/Database

# Create empty files
touch src/Ecom.Domain/Common/Entity.cs src/Ecom.Domain/Common/BaseEntity.cs src/Ecom.Domain/Common/AuditableEntity.cs src/Ecom.Domain/Common/DomainEvent.cs src/Ecom.Domain/Common/Result.cs
touch src/Ecom.Domain/Interfaces/IRepository.cs src/Ecom.Domain/Interfaces/IReadRepository.cs src/Ecom.Domain/Interfaces/IUnitOfWork.cs
touch src/Ecom.Domain/Exceptions/DomainException.cs src/Ecom.Domain/Exceptions/BusinessRuleException.cs
touch src/Ecom.Domain/ValueObjects/Money.cs src/Ecom.Domain/ValueObjects/Address.cs src/Ecom.Domain/ValueObjects/Email.cs src/Ecom.Domain/ValueObjects/PhoneNumber.cs
touch src/Ecom.Domain/Enums/ProductStatus.cs src/Ecom.Domain/Enums/OrderStatus.cs src/Ecom.Domain/Enums/PaymentStatus.cs src/Ecom.Domain/Enums/PaymentMethod.cs src/Ecom.Domain/Enums/FulfillmentStatus.cs src/Ecom.Domain/Enums/UserStatus.cs

touch src/Ecom.Application/Common/Behaviors/ValidationBehavior.cs src/Ecom.Application/Common/Behaviors/LoggingBehavior.cs src/Ecom.Application/Common/Behaviors/TransactionBehavior.cs
touch src/Ecom.Application/Common/Interfaces/ICurrentUserService.cs src/Ecom.Application/Common/Interfaces/IDateTimeService.cs src/Ecom.Application/Common/Interfaces/IFileStorageService.cs src/Ecom.Application/Common/Interfaces/IEmailService.cs src/Ecom.Application/Common/Interfaces/ISmsService.cs src/Ecom.Application/Common/Interfaces/ICacheService.cs src/Ecom.Application/Common/Interfaces/IPaymentService.cs
touch src/Ecom.Application/Common/Exceptions/ValidationException.cs src/Ecom.Application/Common/Exceptions/NotFoundException.cs src/Ecom.Application/Common/Exceptions/UnauthorizedException.cs
touch src/Ecom.Application/Common/Models/PagedResult.cs src/Ecom.Application/Common/Models/PaginationRequest.cs src/Ecom.Application/Common/Models/Result.cs
touch src/Ecom.Application/Mapping/MappingProfile.cs
touch src/Ecom.Application/DependencyInjection.cs

touch src/Ecom.Infrastructure/Persistence/ApplicationDbContext.cs
touch src/Ecom.Infrastructure/Persistence/Interceptors/AuditingInterceptor.cs src/Ecom.Infrastructure/Persistence/Interceptors/DomainEventsInterceptor.cs
touch src/Ecom.Infrastructure/Identity/ApplicationUser.cs src/Ecom.Infrastructure/Identity/ApplicationRole.cs src/Ecom.Infrastructure/Identity/IdentityService.cs src/Ecom.Infrastructure/Identity/IdentitySeeder.cs
touch src/Ecom.Infrastructure/Authentication/Jwt/JwtService.cs src/Ecom.Infrastructure/Authentication/Jwt/JwtOptions.cs
touch src/Ecom.Infrastructure/Authentication/Cookies/CookieAuthenticationService.cs
touch src/Ecom.Infrastructure/DependencyInjection.cs

touch src/Ecom.Api/Controllers/AuthenticationController.cs src/Ecom.Api/Controllers/ProductsController.cs src/Ecom.Api/Controllers/CategoriesController.cs src/Ecom.Api/Controllers/BrandsController.cs src/Ecom.Api/Controllers/CustomersController.cs src/Ecom.Api/Controllers/CartController.cs src/Ecom.Api/Controllers/WishlistController.cs src/Ecom.Api/Controllers/OrdersController.cs src/Ecom.Api/Controllers/InventoryController.cs src/Ecom.Api/Controllers/PaymentsController.cs src/Ecom.Api/Controllers/PromotionsController.cs src/Ecom.Api/Controllers/ReviewsController.cs src/Ecom.Api/Controllers/BannersController.cs src/Ecom.Api/Controllers/NewsletterController.cs
touch src/Ecom.Api/Middleware/ExceptionHandlingMiddleware.cs src/Ecom.Api/Middleware/RequestLoggingMiddleware.cs src/Ecom.Api/Middleware/CorrelationIdMiddleware.cs
touch src/Ecom.Api/Extensions/ServiceCollectionExtensions.cs src/Ecom.Api/Extensions/ApplicationBuilderExtensions.cs

touch src/Ecom.Admin/Areas/Admin/Controllers/DashboardController.cs src/Ecom.Admin/Areas/Admin/Controllers/ProductsController.cs src/Ecom.Admin/Areas/Admin/Controllers/CategoriesController.cs src/Ecom.Admin/Areas/Admin/Controllers/BrandsController.cs src/Ecom.Admin/Areas/Admin/Controllers/OrdersController.cs src/Ecom.Admin/Areas/Admin/Controllers/CustomersController.cs src/Ecom.Admin/Areas/Admin/Controllers/InventoryController.cs src/Ecom.Admin/Areas/Admin/Controllers/PaymentsController.cs src/Ecom.Admin/Areas/Admin/Controllers/PromotionsController.cs src/Ecom.Admin/Areas/Admin/Controllers/BannersController.cs src/Ecom.Admin/Areas/Admin/Controllers/ReviewsController.cs src/Ecom.Admin/Areas/Admin/Controllers/ReportsController.cs src/Ecom.Admin/Areas/Admin/Controllers/UsersController.cs src/Ecom.Admin/Areas/Admin/Controllers/RolesController.cs src/Ecom.Admin/Areas/Admin/Controllers/SettingsController.cs
touch src/Ecom.Admin/Controllers/AccountController.cs
touch src/Ecom.Admin/Views/Shared/_AdminLayout.cshtml src/Ecom.Admin/Views/Shared/_AdminHeader.cshtml src/Ecom.Admin/Views/Shared/_AdminSidebar.cshtml src/Ecom.Admin/Views/Shared/_AdminFooter.cshtml src/Ecom.Admin/Views/Shared/_ValidationScriptsPartial.cshtml
touch src/Ecom.Admin/Authorization/Permissions.cs src/Ecom.Admin/Authorization/PermissionRequirement.cs src/Ecom.Admin/Authorization/PermissionHandler.cs
touch src/Ecom.Admin/wwwroot/css/admin.css src/Ecom.Admin/wwwroot/css/dashboard.css src/Ecom.Admin/wwwroot/css/components.css
touch src/Ecom.Admin/wwwroot/js/admin.js src/Ecom.Admin/wwwroot/js/products.js src/Ecom.Admin/wwwroot/js/orders.js src/Ecom.Admin/wwwroot/js/inventory.js src/Ecom.Admin/wwwroot/js/reports.js

# Remove default generated files
rm src/Ecom.Domain/Class1.cs || true
rm src/Ecom.Application/Class1.cs || true
rm src/Ecom.Infrastructure/Class1.cs || true
rm src/Ecom.Api/WeatherForecast.cs || true
rm src/Ecom.Api/Controllers/WeatherForecastController.cs || true
rm tests/Ecom.Domain.Tests/UnitTest1.cs || true
rm tests/Ecom.Application.Tests/UnitTest1.cs || true
rm tests/Ecom.Api.Tests/UnitTest1.cs || true
rm tests/Ecom.Integration.Tests/UnitTest1.cs || true

echo "Setup script completed."
