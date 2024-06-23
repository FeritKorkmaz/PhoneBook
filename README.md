# PhoneBook Application

This is a PhoneBook application built with Blazor Server and .NET 6.

## Getting Started

### Prerequisites

- .NET 6 SDK
- SQL Server
- Visual Studio Code (VSCode)

### Setting Up the Database

1. **Download the Repository:**

   - Clone the repository to your local machine:
     ```bash
     git clone https://github.com/yourusername/your-repo-name.git
     cd your-repo-name
     ```

2. **Restore the Database:**
   - Open SQL Server Management Studio (SSMS).
   - In the Object Explorer, right-click on `Databases` and select `Restore Database...`.
   - In the `Source` section, select `Device` and click the `...` button.
   - Click `Add` and navigate to the `Data` folder in your cloned repository. Select the `PhoneBooks.bak` file.
   - Enter `PhoneBooks` as the database name in the `Destination` section.
   - Click `OK` to start the restore process.

### Configuring the Application

1. **Update Connection String:**
   - Open the `appsettings.json` file located in the root directory of the project.
   - Update the `DefaultConnection` string with your SQL Server details:
     - `Server=YOUR_SERVER_NAME`
     - `Database=PhoneBooks`
     - `User Id=YOUR_USERNAME`
     - `Password=YOUR_PASSWORD`

### Running the Application

1. **Open the Project in VSCode:**

   - Open Visual Studio Code.
   - Select `File` > `Open Folder...` and choose the folder where you cloned the repository.

2. **Install Required Extensions:**

   - Make sure you have the C# extension installed in VSCode for full .NET development support.

3. **Run the Application:**

   - Open a terminal in VSCode (View > Terminal).
   - Run the following command:
     ```bash
     dotnet watch run
     ```

## Project Structure

- `Data/PhoneBooks.bak`: Contains the database backup file.
- `Pages/`: Contains the Razor pages for the Blazor application.
- `Services/`: Contains the service classes for handling business logic.
- `Models/`: Contains the data models used in the application.
