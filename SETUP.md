# Multilingual Menu Application

A .NET 8 Razor Pages application that provides a multilingual menu management system with MSSQL database support. The application allows customers to view menu items in their preferred language and provides an admin interface to manage menu content and translations dynamically.

## Features

- **Customer Interface**: View menu items with language selection
- **Admin Interface**: Manage menu items, translations, and languages
- **Dynamic Content**: All content stored in database, no code changes required
- **Multilingual Support**: Support for multiple languages with automatic fallback
- **Modern Stack**: Built with .NET 8, Entity Framework Core, and Bootstrap 5

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server 2019+](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Express)
- A code editor (Visual Studio 2022, VS Code, or Rider)

## Database Setup

### Option 1: Using Entity Framework Migrations (Recommended)

1. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER;Database=MultilingualMenuDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

2. Run Entity Framework migrations:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

### Option 2: Using SQL Script

1. Open SQL Server Management Studio (SSMS)
2. Connect to your SQL Server instance
3. Execute the script in `Database/schema.sql`

## Running the Application

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd Language
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Update the database connection string in `appsettings.json`

4. Run the application:
   ```bash
   dotnet run
   ```

5. Open your browser and navigate to:
   - Home page: `https://localhost:5001`
   - Customer menu: `https://localhost:5001/Customer/Menu`
   - Admin panel: `https://localhost:5001/Admin/Menus`

## Application Structure

```
MultilingualMenuApp/
├── Data/
│   └── ApplicationDbContext.cs       # EF Core database context
├── Models/
│   ├── Menu.cs                       # Menu and MenuTranslation models
│   └── Language.cs                   # Language model
├── Services/
│   ├── IMenuService.cs               # Menu service interface
│   ├── MenuService.cs                # Menu service implementation
│   ├── ILanguageService.cs           # Language service interface
│   └── LanguageService.cs            # Language service implementation
├── Pages/
│   ├── Admin/
│   │   ├── Languages.cshtml          # Language management page
│   │   ├── LanguageEdit.cshtml       # Add/Edit language
│   │   ├── Menus.cshtml              # Menu management page
│   │   └── MenuEdit.cshtml           # Add/Edit menu with translations
│   ├── Customer/
│   │   └── Menu.cshtml               # Customer menu view with language selector
│   ├── Shared/
│   │   ├── _Layout.cshtml            # Main layout
│   │   └── _ValidationScriptsPartial.cshtml
│   └── Index.cshtml                  # Home page
├── Database/
│   └── schema.sql                    # Database creation script
└── Program.cs                        # Application startup
```

## Database Schema

### Languages Table
- Stores supported languages (e.g., English, Turkish, Spanish)
- Fields: Id, Code (e.g., "en", "tr"), Name, IsActive

### Menus Table
- Stores menu items with default content
- Fields: Id, DefaultName, DefaultDescription

### MenuTranslations Table
- Stores translations for each menu item
- Fields: Id, Language, Name, Description, MenuId (FK)
- Cascade delete when menu is deleted

## Usage Guide

### For Administrators

1. **Manage Languages**:
   - Navigate to Admin → Manage Languages
   - Add new languages with ISO 639-1 codes (e.g., "en", "fr", "de")
   - Enable/disable languages as needed

2. **Manage Menu Items**:
   - Navigate to Admin → Manage Menus
   - Click "Add New Menu Item" to create a menu item
   - Enter default name and description (used as fallback)
   - Add translations for each language
   - Click "Add Translation" to add more languages
   - Save the menu item

3. **Edit/Delete**:
   - Click "Edit" to modify menu items or languages
   - Click "Delete" to remove items (confirmations are shown)

### For Customers

1. **View Menu**:
   - Navigate to "View Menu" from the home page
   - Use the language dropdown to select your preferred language
   - Menu items automatically display in the selected language
   - If a translation is missing, the default content is shown

2. **Language Persistence**:
   - Selected language is stored in session
   - Preference persists until browser is closed

## Testing the Application

### Test Multilingual Support

1. Add at least 2-3 languages (e.g., English, Turkish, Spanish)
2. Create several menu items with translations in all languages
3. Visit the customer menu page
4. Switch between languages using the dropdown
5. Verify that content updates immediately
6. Test with a menu item that has missing translations to see fallback behavior

### Test Admin Features

1. Add a new language
2. Create a new menu item without translations
3. Edit the menu item to add translations
4. Verify translations appear correctly
5. Delete a menu item and verify it's removed
6. Try to add duplicate language codes (should fail)

## Troubleshooting

### Database Connection Issues
- Verify SQL Server is running
- Check connection string in `appsettings.json`
- Ensure database exists (create if needed)
- Check firewall settings for SQL Server port (1433)

### Migration Issues
- Delete the Migrations folder and recreate: `dotnet ef migrations add InitialCreate`
- Drop the database and recreate it
- Use the SQL script instead of migrations

### Build Errors
- Ensure .NET 6 SDK is installed: `dotnet --version`
- Clean and rebuild: `dotnet clean && dotnet build`
- Restore packages: `dotnet restore`

## Technologies Used

- **.NET 6**: Web application framework
- **ASP.NET Core Razor Pages**: UI framework
- **Entity Framework Core 6**: ORM for database access
- **SQL Server**: Database engine
- **Bootstrap 5**: CSS framework for responsive UI
- **Bootstrap Icons**: Icon library

## Best Practices Implemented

1. **Separation of Concerns**: Services layer separates business logic from UI
2. **Dependency Injection**: Services are injected via DI container
3. **Repository Pattern**: Service layer abstracts data access
4. **Async/Await**: All database operations are asynchronous
5. **Input Validation**: Model validation on all forms
6. **Cascade Delete**: Foreign key relationships properly configured
7. **Session Management**: Language preference stored in session
8. **Fallback Mechanism**: Default content shown when translations missing
9. **Responsive Design**: Bootstrap for mobile-friendly interface

## Future Enhancements

- Add authentication and authorization for admin pages
- Implement role-based access control
- Add menu categories and pricing
- Support for menu item images
- Export/import translations (CSV/Excel)
- API endpoints for mobile apps
- Caching for improved performance
- Audit logging for admin actions

## License

This project is provided as an example implementation for educational purposes.
