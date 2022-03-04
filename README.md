# SimpleRetail

An application that stores use to record product stock, transactions and report sales results.  
Full documentation > [SimpleRetail.pdf](./SimpleRetail.pdf)

## 🗂️ Database Structure

![DatabaseStucture](https://media.discordapp.net/attachments/946013429200723989/949245548282667008/SimpleRetailDb.png?width=1440&height=648)

## 🔧 Development

#### 📥 Clone the Project

```
$ git clone https://github.com/BayuDC/SimpleRetail.git
```

#### 📖 Open in Visual Studio

```
$ start SimpleRetail.sln

# or simply just double click the .sln file

# or using your favorit code editor
```

#### 🪛 Database Configuration

-   Import database using [SimpleRetail.sql](./SimpleRetail.sql) file or you can craete it manually  
    default admin account for login access from the SimpleRetail.sql file is
    ```
    Email     = admin@localhost
    Password  = admin0
    ```
-   Change the connection string in [Database.cs](./SimpleRetail/Database.cs) with yours
    ```
    private readonly string _connectionString = "Your connection string";
    ```

#### 🧬 Install Dependencies

```
$ dotnet restore
```

#### 🚀 Compile and Run

```
# Using terminal

# if you are in the root directory of the project
$ dotnet run --project SimpleRetail

# or go to same directory with .csproj file first
$ cd SimpleRetail
$ dotnet run

# Using Visual Studio
# simply just click F5 on your keyboard
```

## 📝 Todo

-   Form Profile
-   Form Export Data
-   Form Monthly Report
