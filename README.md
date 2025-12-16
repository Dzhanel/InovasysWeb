# InovaSys Web Application

A .NET 8.0 MVC web application for managing users with SQL Server database integration.

## Prerequisites

- .NET 8.0 SDK or later
- SQL Server (LocalDB, Express, or Full Edition)
- Visual Studio 2022 or VS Code with C# extensions

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/Dzhanel/InovasysWeb.git
cd InovasysWeb
```

### 2. Restore NuGet Packages

The project uses several NuGet packages that need to be restored before running:

**Using Visual Studio:**
- Open `InovasysWeb.sln`
- Visual Studio will automatically restore packages on first open
- Or right-click on the solution and select "Restore NuGet Packages"

**Using .NET CLI:**
```bash
cd Inovasys.Web
dotnet restore
```

### 3. Configure User Secrets

This application requires a SQL Server connection string to function properly. For security reasons, sensitive configuration data should **not** be stored in `appsettings.json`. Instead, use .NET User Secrets for local development.

**Option 1: Using Visual Studio (Recommended)**

1. Right-click on the `Inovasys.Web` project in Solution Explorer
2. Select **Manage User Secrets**
3. This will open a `secrets.json` file. Add your configuration (replace with your connection string):
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Your connection string goes here"
     }
   }
   ```
   
   **Example (SQL Server LocalDB):**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=InovasysDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
   }
   ```
4. Save the file

**Option 2: Using .NET CLI**

1. Open a terminal in the project directory:
   ```bash
   cd Inovasys.Web
   ```

2. Initialize user secrets for the project:
   ```bash
   dotnet user-secrets init
   ```

3. Add your SQL Server connection string (replace with your connection string):
   ```bash
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Your connection string goes here"
   ```

4. Verify your secrets:
   ```bash
   dotnet user-secrets list
   ```

### 4. Run the Application

**Using Visual Studio:**
- Open `InovasysWeb.sln`
- Press `F5` or click the "Run" button

**Using .NET CLI:**
```bash
cd Inovasys.Web
dotnet run
```

The database will be created automatically on first run. The application will be available at `https://localhost:7xxx` (check console for exact port).
