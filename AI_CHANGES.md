## AI Change Log

### 2025-09-25
- Add API request models: `Blog.API/Models/Auth/RegisterRequest.cs`, `LoginRequest.cs`.
- Add controllers: `Blog.API/Controllers/AuthController.cs`, `Blog.API/Controllers/PostsController.cs`.
- Add global error middleware: `Blog.API/Middleware/ErrorHandlingMiddleware.cs`.
- Create and configure `Blog.API/Program.cs` (Serilog, CORS, JWT auth, DI wiring, middleware, seeding).
- Add Application DI: `Blog.Application/DependencyInjection.cs` (MediatR, AutoMapper registration).
- Update packages: `Blog.Application/Blog.Application.csproj` (add AutoMapper.Extensions.Microsoft.DependencyInjection).
- Align package versions: `Blog.Infrastructure/Blog.Infrastructure.csproj` (AutoMapper 15.0.1).
- Add domain reference to API: `Blog.API/Blog.API.csproj` includes `Blog.Domain`.
- Add Db initializer: `Blog.Infrastructure/Data/DbInitializer.cs`.
- Fix middleware exception types mapping (use `NotFoundException`, `UnauthorizedException`, `ForbidException`).
- Use `JwtSettings.key` (lowercase) in `Program.cs` for signing key.
- Adjust FK cascade to avoid multiple cascade paths: set `PostLike.User` to `DeleteBehavior.Restrict` in `Blog.Infrastructure/Persistence/BlogDbContext.cs`.

### Migration/DB Status
- Initial migration was created and DB creation attempted during seeding.
- Seeding failed due to multiple cascade paths on `PostLikes`. FK updated; database needs to be recreated.

### Next Steps
- Drop `BlogPlatformDB` (localdb) and rerun the app to let seeding recreate schema successfully.
- Optionally resolve AutoMapper warning by aligning `AutoMapper.Extensions.Microsoft.DependencyInjection` to a version compatible with AutoMapper 15.x, or downgrade AutoMapper to 12.0.1 across projects.

### Notes
- Swagger enabled in Development.
- JWT settings read from `appsettings.json` section `JwtSettings` (properties: `key`, `Issuer`, `Audience`, `DurationInMinutes`). 

### 2025-09-25 (cont.)
- Dropped local database via EF CLI to resolve multiple cascade paths.
- Ran API; seeding will recreate schema with updated FK behavior. 