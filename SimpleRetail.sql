USE [master]
GO
/****** Object:  Database [SimpleRetail]    Script Date: 03/03/2022 19:55:12 ******/
CREATE DATABASE [SimpleRetail]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SimpleRetail', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SimpleRetail.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SimpleRetail_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SimpleRetail_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SimpleRetail] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SimpleRetail].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SimpleRetail] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SimpleRetail] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SimpleRetail] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SimpleRetail] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SimpleRetail] SET ARITHABORT OFF 
GO
ALTER DATABASE [SimpleRetail] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SimpleRetail] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SimpleRetail] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SimpleRetail] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SimpleRetail] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SimpleRetail] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SimpleRetail] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SimpleRetail] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SimpleRetail] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SimpleRetail] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SimpleRetail] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SimpleRetail] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SimpleRetail] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SimpleRetail] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SimpleRetail] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SimpleRetail] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SimpleRetail] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SimpleRetail] SET RECOVERY FULL 
GO
ALTER DATABASE [SimpleRetail] SET  MULTI_USER 
GO
ALTER DATABASE [SimpleRetail] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SimpleRetail] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SimpleRetail] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SimpleRetail] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SimpleRetail] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SimpleRetail] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SimpleRetail', N'ON'
GO
ALTER DATABASE [SimpleRetail] SET QUERY_STORE = OFF
GO
USE [SimpleRetail]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 03/03/2022 19:55:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [varchar](5) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Phone] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT INTO [dbo].[Employees] (Id, Name, Email, Phone, Password)
VALUES ('E0001', 'Admin', 'admin@localhost', '+6281234567890', 'admin0')
GO
/****** Object:  Table [dbo].[Products]    Script Date: 03/03/2022 19:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [varchar](5) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Price] [int] NOT NULL,
	[Stock] [int] NOT NULL,
	[SupplierId] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 03/03/2022 19:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[Id] [varchar](5) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TransactionProducts]    Script Date: 03/03/2022 19:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TransactionProducts](
	[TransactionId] [varchar](5) NOT NULL,
	[ProductId] [varchar](5) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Price] [int] NOT NULL,
 CONSTRAINT [PK_TransactionProducts] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC,
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 03/03/2022 19:55:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [varchar](5) NOT NULL,
	[Date] [date] NOT NULL,
	[EmployeeId] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Suppliers] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([Id])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Suppliers]
GO
ALTER TABLE [dbo].[TransactionProducts]  WITH CHECK ADD  CONSTRAINT [FK_TransactionProducts_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[TransactionProducts] CHECK CONSTRAINT [FK_TransactionProducts_Products]
GO
ALTER TABLE [dbo].[TransactionProducts]  WITH CHECK ADD  CONSTRAINT [FK_TransactionProducts_Transactions] FOREIGN KEY([TransactionId])
REFERENCES [dbo].[Transactions] ([Id])
GO
ALTER TABLE [dbo].[TransactionProducts] CHECK CONSTRAINT [FK_TransactionProducts_Transactions]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Employees] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Employees]
GO
USE [master]
GO
ALTER DATABASE [SimpleRetail] SET  READ_WRITE 
GO
