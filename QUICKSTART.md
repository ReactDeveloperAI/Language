# Quick Start Guide

Get the Multilingual Menu Application running in 5 minutes!

## Step 1: Prerequisites

Install these if you don't have them:
- **.NET 8 SDK**: https://dotnet.microsoft.com/download/dotnet/8.0
- **SQL Server**: https://www.microsoft.com/sql-server/sql-server-downloads
  - Or use SQL Server Express LocalDB (included with Visual Studio)

## Step 2: Clone and Navigate

```bash
git clone <repository-url>
cd Language
```

## Step 3: Configure Database

Edit `appsettings.json` with your connection string:

**For LocalDB** (easiest):
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MultilingualMenuDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**For SQL Server Express**:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=MultilingualMenuDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

**For Full SQL Server**:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MultilingualMenuDb;User Id=YOUR_USER;Password=YOUR_PASSWORD;TrustServerCertificate=True;"
}
```

## Step 4: Create Database

```bash
# Install EF tools (first time only)
dotnet tool install --global dotnet-ef

# Add tools to PATH
export PATH="$PATH:$HOME/.dotnet/tools"

# Apply migrations to create database
dotnet ef database update
```

This creates:
- Languages table with English, Turkish, Spanish
- Menus table
- MenuTranslations table
- All relationships and indexes

## Step 5: Run Application

```bash
dotnet run
```

You should see:
```
Now listening on: https://localhost:5001
Now listening on: http://localhost:5000
```

## Step 6: Explore the Application

Open your browser and visit:

### 🏠 Home Page
**URL**: https://localhost:5001

### 👥 Customer View
**URL**: https://localhost:5001/Customer/Menu
- View menu items
- Switch languages using dropdown
- See translations in action

### 🛠️ Admin Panel

**Languages Management**:
**URL**: https://localhost:5001/Admin/Languages
- View all languages
- Add new languages (e.g., French, German)
- Edit or deactivate languages

**Menu Management**:
**URL**: https://localhost:5001/Admin/Menus
- View all menu items
- Add new menu items with translations
- Edit existing items

## Quick Test

1. **Add a menu item**:
   - Go to Admin → Manage Menus
   - Click "Add New Menu Item"
   - Default Name: "Coffee"
   - Default Description: "Hot brewed coffee"
   - Click "Add Translation"
   - Language: Turkish (tr)
   - Name: "Kahve"
   - Description: "Sıcak filtre kahve"
   - Click "Save Menu Item"

2. **View in customer interface**:
   - Go to "View Menu"
   - See "Coffee" displayed
   - Switch language to "Türkçe"
   - See "Kahve" displayed

## Adding Sample Data (Optional)

If you want more sample data, run the SQL script:

```bash
# Using sqlcmd (if available)
sqlcmd -S "(localdb)\mssqllocaldb" -d MultilingualMenuDb -i Database/schema.sql

# Or use SQL Server Management Studio:
# 1. Open SSMS
# 2. Connect to your server
# 3. Open Database/schema.sql
# 4. Execute (F5)
```

## Troubleshooting

### Can't connect to database?
- Make sure SQL Server is running
- Test connection string in SSMS first
- For LocalDB: `sqllocaldb start mssqllocaldb`

### Port already in use?
Change the port in `Properties/launchSettings.json` or use:
```bash
dotnet run --urls="https://localhost:5002"
```

### Migrations fail?
Delete and recreate:
```bash
dotnet ef database drop
dotnet ef database update
```

## Next Steps

- 📖 Read [SETUP.md](SETUP.md) for detailed documentation
- 🧪 Follow [TESTING.md](TESTING.md) to test all features
- 🎨 Customize the UI in `wwwroot/css/site.css`
- 🌍 Add more languages in the admin panel
- 🍕 Add your own menu items

## Need Help?

- Check the detailed guides: SETUP.md and TESTING.md
- Review the code comments in the source files
- Check the database schema in Database/schema.sql

Enjoy your multilingual menu application! 🎉
