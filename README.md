# Multi-Language CMS Project

This project is a multilingual content management system built on .NET 8 with SQL Server, featuring a complete menu management application with dynamic translation support.

## Features

- 🌍 **Multilingual Support**: Manage content in multiple languages dynamically
- 📝 **Menu Management**: Full CRUD operations for menu items with translations
- 🔄 **Language Switching**: Instant language switching for customers
- 💾 **Database-Driven**: All content stored in MSSQL database
- 🎨 **Modern UI**: Responsive design with Bootstrap 5
- 🛠️ **Admin Panel**: Complete admin interface for content management

## Quick Start

1. **Prerequisites**: Install .NET 8 SDK and SQL Server
2. **Clone the repository**
3. **Update connection string** in `appsettings.json`
4. **Apply database migrations**:
   ```bash
   export PATH="$PATH:$HOME/.dotnet/tools"
   dotnet ef database update
   ```
5. **Run the application**:
   ```bash
   dotnet run
   ```
6. **Open browser** to `https://localhost:5001`

## Documentation

- 📖 [**SETUP.md**](SETUP.md) - Complete setup and installation instructions
- 🧪 [**TESTING.md**](TESTING.md) - Comprehensive testing guide
- 🗄️ [**Database/schema.sql**](Database/schema.sql) - Database schema with sample data

## Project Structure

- **Models/** - Data models (Menu, MenuTranslation, Language)
- **Services/** - Business logic and data access
- **Data/** - Entity Framework DbContext
- **Pages/** - Razor Pages (Admin and Customer interfaces)
- **Database/** - SQL scripts and documentation

## Technologies

- .NET 8
- ASP.NET Core Razor Pages
- Entity Framework Core 8
- SQL Server
- Bootstrap 5

## License

This project is provided as an example implementation for educational purposes.