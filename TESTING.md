# Testing Guide - Multilingual Menu Application

This guide provides detailed instructions for testing the multilingual menu application.

## Prerequisites for Testing

Before you start testing, ensure you have:
1. .NET 8 SDK installed
2. SQL Server running (LocalDB, Express, or full SQL Server)
3. Project built successfully (`dotnet build`)

## Database Setup for Testing

### Option 1: Using Entity Framework Migrations (Recommended)

1. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=MultilingualMenuDb;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

2. Apply migrations to create the database:
   ```bash
   export PATH="$PATH:$HOME/.dotnet/tools"
   dotnet ef database update
   ```

   This will:
   - Create the database if it doesn't exist
   - Create all tables (Languages, Menus, MenuTranslations)
   - Seed initial data (3 languages: English, Turkish, Spanish)

### Option 2: Using SQL Script

1. Open SQL Server Management Studio or Azure Data Studio
2. Execute the script in `Database/schema.sql`
3. This creates the database with sample menu items

## Running the Application

1. Start the application:
   ```bash
   dotnet run
   ```

2. The application will be available at:
   - HTTPS: `https://localhost:5001`
   - HTTP: `http://localhost:5000`

3. You should see output like:
   ```
   info: Microsoft.Hosting.Lifetime[14]
         Now listening on: https://localhost:5001
   info: Microsoft.Hosting.Lifetime[14]
         Now listening on: http://localhost:5000
   ```

## Test Scenarios

### Test 1: Home Page Navigation

1. **Action**: Navigate to `https://localhost:5001`
2. **Expected Result**: 
   - Home page displays with welcome message
   - Two main sections: Customer View and Admin Panel
   - Navigation bar at the top with Home, View Menu, and Admin dropdown
3. **Verification**: Check that all links are present and clickable

### Test 2: Language Management (Admin)

#### 2.1: View Languages
1. **Action**: Click "Admin" → "Manage Languages" or navigate to `/Admin/Languages`
2. **Expected Result**:
   - List of languages displayed in a table
   - Three default languages: English (en), Türkçe (tr), Español (es)
   - Each language shows: Code, Name, Status (Active/Inactive)
   - Action buttons: Edit, Delete

#### 2.2: Add New Language
1. **Action**: Click "Add New Language" button
2. **Expected Result**: Form appears with fields:
   - Language Code (e.g., "fr", "de")
   - Language Name (e.g., "Français", "Deutsch")
   - Active checkbox (checked by default)
3. **Action**: Enter:
   - Code: `fr`
   - Name: `Français`
   - Active: checked
4. **Action**: Click "Save"
5. **Expected Result**: 
   - Redirected to languages list
   - New language appears in the list
   - Success indication

#### 2.3: Edit Language
1. **Action**: Click "Edit" on any language
2. **Expected Result**: Form populated with existing data
3. **Action**: Change name or toggle active status
4. **Action**: Click "Save"
5. **Expected Result**: Changes reflected in the list

#### 2.4: Delete Language
1. **Action**: Click "Delete" on a language (not used by any menu)
2. **Expected Result**: Confirmation prompt appears
3. **Action**: Confirm deletion
4. **Expected Result**: Language removed from list

#### 2.5: Validation Tests
1. **Test**: Try to add language without code
   - **Expected**: Validation error "Language code is required"
2. **Test**: Try to add language without name
   - **Expected**: Validation error "Language name is required"

### Test 3: Menu Management (Admin)

#### 3.1: View Menus
1. **Action**: Click "Admin" → "Manage Menus" or navigate to `/Admin/Menus`
2. **Expected Result**:
   - List of menu items (if any exist)
   - Columns: Default Name, Default Description, Translations, Actions
   - "Add New Menu Item" button
   - If using schema.sql, sample items displayed

#### 3.2: Add Menu Item Without Translations
1. **Action**: Click "Add New Menu Item"
2. **Expected Result**: Form with:
   - Default Name field
   - Default Description field
   - Translations section (empty)
   - "Add Translation" button
3. **Action**: Enter:
   - Default Name: `Burger`
   - Default Description: `Juicy beef burger with fresh vegetables`
4. **Action**: Click "Save Menu Item"
5. **Expected Result**:
   - Redirected to menu list
   - New item appears with "No translations" badge

#### 3.3: Add Menu Item With Translations
1. **Action**: Click "Add New Menu Item"
2. **Action**: Enter default content:
   - Default Name: `Steak`
   - Default Description: `Premium ribeye steak`
3. **Action**: Click "Add Translation"
4. **Expected Result**: Translation form appears with language dropdown
5. **Action**: Fill first translation:
   - Language: Turkish (tr)
   - Name: `Biftek`
   - Description: `Premium dana biftek`
6. **Action**: Click "Add Translation" again
7. **Action**: Fill second translation:
   - Language: Spanish (es)
   - Name: `Bistec`
   - Description: `Bistec premium de lomo`
8. **Action**: Click "Save Menu Item"
9. **Expected Result**:
   - Redirected to menu list
   - New item shows "2 translation(s)" badge
   - Languages shown: (tr, es)

#### 3.4: Edit Menu Item
1. **Action**: Click "Edit" on any menu item
2. **Expected Result**: Form populated with:
   - Default content
   - All existing translations
3. **Action**: Modify default description
4. **Action**: Modify one translation's name
5. **Action**: Click "Save Menu Item"
6. **Expected Result**: Changes saved and reflected

#### 3.5: Remove Translation
1. **Action**: Edit a menu item with translations
2. **Action**: Click "Remove Translation" on one translation
3. **Action**: Click "Save Menu Item"
4. **Expected Result**: 
   - Translation removed
   - Translation count updated in list

#### 3.6: Delete Menu Item
1. **Action**: Click "Delete" on a menu item
2. **Expected Result**: Confirmation dialog
3. **Action**: Confirm deletion
4. **Expected Result**: Menu item and all its translations removed

#### 3.7: Validation Tests
1. **Test**: Try to save menu without default name
   - **Expected**: Validation error "Default name is required"

### Test 4: Customer Menu View

#### 4.1: View Menu in Default Language
1. **Action**: Navigate to "View Menu" or `/Customer/Menu`
2. **Expected Result**:
   - Page title: "Our Menu"
   - Language selector dropdown (top right) showing current language
   - Menu items displayed in cards
   - Each card shows name and description
   - "Back to Home" button

#### 4.2: Switch Language
1. **Action**: Click language dropdown
2. **Expected Result**: List of active languages displayed
3. **Action**: Select "Türkçe (tr)"
4. **Expected Result**:
   - Page reloads
   - Language selector now shows "Türkçe"
   - Menu items display in Turkish (if translations exist)
   - Items without Turkish translation show default content with info message

#### 4.3: Test All Languages
1. **Action**: Switch between all available languages
2. **Expected Result**: For each language:
   - Dropdown updates to show selected language
   - Menu items update immediately
   - Translations display correctly
   - Missing translations fall back to default with notice

#### 4.4: Test Session Persistence
1. **Action**: Select a non-default language (e.g., Spanish)
2. **Action**: Navigate to Home page
3. **Action**: Return to Menu page
4. **Expected Result**: 
   - Language selection persists (still Spanish)
   - Menu still displays in Spanish

#### 4.5: Test with Missing Translations
1. **Prerequisites**: Have at least one menu item with no Turkish translation
2. **Action**: Switch to Turkish language
3. **Expected Result**:
   - Menu item displays default (English) content
   - Small info message appears: "Translation not available, showing default content"

### Test 5: Fallback Mechanism

#### 5.1: Complete Fallback Test
1. **Setup**: Create a menu item with:
   - Default Name: "Ice Cream"
   - Default Description: "Vanilla ice cream"
   - Only Spanish translation
2. **Test in English**:
   - **Expected**: Shows default content (Ice Cream, Vanilla ice cream)
3. **Test in Spanish**:
   - **Expected**: Shows Spanish translation
4. **Test in Turkish**:
   - **Expected**: Shows default content with info message
5. **Test in French** (if added):
   - **Expected**: Shows default content with info message

### Test 6: Navigation and User Flow

#### 6.1: Complete Admin Flow
1. Start at Home page
2. Go to Manage Languages → Add language "French"
3. Go to Manage Menus → Add menu "Dessert" with French translation
4. Return to Home
5. Go to View Menu → Switch to French
6. **Expected**: New dessert appears in French

#### 6.2: Complete Customer Flow
1. Start at Home page
2. Click "View Menu"
3. Browse menu items
4. Switch language to Turkish
5. Browse menu in Turkish
6. Switch to Spanish
7. Browse menu in Spanish
8. Return to Home
9. **Expected**: All transitions smooth, no errors

### Test 7: Data Validation and Edge Cases

#### 7.1: Empty States
1. **Test**: Access menu page when no menu items exist
   - **Expected**: Info message "No menu items available"
2. **Test**: Access languages page (fresh database, migrations only)
   - **Expected**: Three default languages listed

#### 7.2: Duplicate Prevention
1. **Test**: Try to add language with existing code (e.g., "en")
   - **Expected**: Database constraint error or validation message

#### 7.3: Long Text Handling
1. **Test**: Add menu with very long description (500+ characters)
   - **Expected**: Text wraps properly in UI, no overflow
2. **Test**: Add menu with very long name (100+ characters)
   - **Expected**: Handled gracefully, possibly truncated with ellipsis

#### 7.4: Special Characters
1. **Test**: Add menu with special characters: "Crème Brûlée"
2. **Test**: Add Turkish text: "İçli Köfte"
3. **Test**: Add Spanish text: "Paella Española"
4. **Expected**: All characters display correctly

#### 7.5: Inactive Language
1. **Test**: Create language and mark as inactive
2. **Expected**: 
   - Language doesn't appear in customer dropdown
   - Still visible in admin panel with "Inactive" badge

### Test 8: Cascade Delete

#### 8.1: Test Menu Deletion
1. **Setup**: Create menu with 3 translations
2. **Action**: Delete the menu from admin panel
3. **Expected**: Menu and all translations removed from database
4. **Verification**: Check customer menu page - item not visible

### Test 9: Browser and Responsiveness

#### 9.1: Mobile View
1. **Test**: Access site on mobile viewport (or resize browser to <768px)
2. **Expected**:
   - Navigation collapses to hamburger menu
   - Tables become scrollable
   - Cards stack vertically
   - Forms remain usable

#### 9.2: Desktop View
1. **Test**: Access on desktop (>1024px width)
2. **Expected**:
   - Full navigation visible
   - Cards in grid (3 columns)
   - Tables show all columns
   - Optimal layout

### Test 10: Error Handling

#### 10.1: Invalid Routes
1. **Test**: Navigate to `/NonExistent`
2. **Expected**: Error page or 404 handler

#### 10.2: Invalid IDs
1. **Test**: Navigate to `/Admin/MenuEdit?id=99999`
2. **Expected**: 404 Not Found page

## Performance Testing

### Load Testing (Optional)
1. Create 50+ menu items with translations
2. Navigate to customer menu page
3. **Expected**: Page loads in <2 seconds

### Database Testing
1. Check database after operations:
   ```sql
   SELECT * FROM Languages;
   SELECT * FROM Menus;
   SELECT * FROM MenuTranslations;
   ```
2. Verify data integrity and relationships

## Automated Testing Checklist

- [ ] Application builds without errors
- [ ] All pages render without exceptions
- [ ] Database migrations apply successfully
- [ ] Home page loads correctly
- [ ] Admin languages page loads and lists items
- [ ] Admin menus page loads and lists items
- [ ] Customer menu page loads and displays items
- [ ] Language switching works
- [ ] CRUD operations work for languages
- [ ] CRUD operations work for menus
- [ ] Translations save correctly
- [ ] Missing translations fall back to default
- [ ] Cascade delete works
- [ ] Session persists language selection
- [ ] Validation prevents invalid data
- [ ] Responsive design works on mobile
- [ ] Special characters display correctly

## Common Issues and Solutions

### Issue: Database connection fails
**Solution**: 
- Check SQL Server is running
- Verify connection string in appsettings.json
- For LocalDB: Ensure SQL Server Express LocalDB is installed

### Issue: Migrations don't apply
**Solution**:
```bash
dotnet ef database drop
dotnet ef database update
```

### Issue: Changes don't appear
**Solution**:
- Clear browser cache
- Hard refresh (Ctrl+F5)
- Restart application

### Issue: Session doesn't persist
**Solution**:
- Check session middleware is enabled in Program.cs
- Verify cookies are not blocked in browser

## Reporting Test Results

When reporting test results, include:
1. Test scenario name
2. Steps performed
3. Expected result
4. Actual result
5. Pass/Fail status
6. Screenshots (if applicable)
7. Browser/environment details

## Success Criteria

The application passes all tests if:
- ✅ All CRUD operations work correctly
- ✅ Language switching is instant and accurate
- ✅ Translations display correctly
- ✅ Fallback mechanism works
- ✅ No unhandled exceptions
- ✅ Data persists correctly in database
- ✅ UI is responsive and user-friendly
- ✅ Validation prevents invalid data
- ✅ Session management works
- ✅ Navigation is intuitive
