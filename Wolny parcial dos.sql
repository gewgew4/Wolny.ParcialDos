USE [master]
GO
/****** Object:  Database [wpdos]    Script Date: 9/7/2024 12:08:02 ******/
CREATE DATABASE [wpdos]
 CONTAINMENT = NONE
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [wpdos] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [wpdos].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [wpdos] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [wpdos] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [wpdos] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [wpdos] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [wpdos] SET ARITHABORT OFF 
GO
ALTER DATABASE [wpdos] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [wpdos] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [wpdos] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [wpdos] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [wpdos] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [wpdos] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [wpdos] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [wpdos] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [wpdos] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [wpdos] SET  ENABLE_BROKER 
GO
ALTER DATABASE [wpdos] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [wpdos] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [wpdos] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [wpdos] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [wpdos] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [wpdos] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [wpdos] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [wpdos] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [wpdos] SET  MULTI_USER 
GO
ALTER DATABASE [wpdos] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [wpdos] SET DB_CHAINING OFF 
GO
ALTER DATABASE [wpdos] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [wpdos] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [wpdos] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [wpdos] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [wpdos] SET QUERY_STORE = OFF
GO
USE [wpdos]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 9/7/2024 12:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Camiones]    Script Date: 9/7/2024 12:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Camiones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Disponible] [bit] NOT NULL,
	[Patente] [nvarchar](max) NOT NULL,
	[Ubicacion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Camiones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciudades]    Script Date: 9/7/2024 12:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciudades](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Distancias] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Ubicacion] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Ciudades] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 9/7/2024 12:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Entregado] [bit] NOT NULL,
	[CiudadId] [int] NOT NULL,
	[RecorridoId] [int] NULL,
 CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlanRecorridos]    Script Date: 9/7/2024 12:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlanRecorridos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FechaFin] [datetime2](7) NULL,
	[Finalizado] [bit] NOT NULL,
	[Prioridad] [int] NOT NULL,
	[CiudadId] [int] NOT NULL,
	[RecorridoId] [int] NOT NULL,
 CONSTRAINT [PK_PlanRecorridos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recorridos]    Script Date: 9/7/2024 12:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recorridos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CamionId] [int] NULL,
	[FechaFin] [datetime2](7) NULL,
	[FechaInicio] [datetime2](7) NULL,
	[Finalizado] [bit] NOT NULL,
 CONSTRAINT [PK_Recorridos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240708045106_Inicial', N'8.0.6')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240708060332_Datos1', N'8.0.6')
GO
SET IDENTITY_INSERT [dbo].[Camiones] ON 
GO
INSERT [dbo].[Camiones] ([Id], [Disponible], [Patente], [Ubicacion]) VALUES (1, 0, N'XYZ0', N'{"latitud":-26.95,"longitud":-58.83}')
GO
INSERT [dbo].[Camiones] ([Id], [Disponible], [Patente], [Ubicacion]) VALUES (2, 1, N'ABC2', N'{"latitud":-34.6,"longitud":-58.37}')
GO
INSERT [dbo].[Camiones] ([Id], [Disponible], [Patente], [Ubicacion]) VALUES (3, 1, N'ABC3', N'{"latitud":-34.6,"longitud":-58.37}')
GO
INSERT [dbo].[Camiones] ([Id], [Disponible], [Patente], [Ubicacion]) VALUES (4, 1, N'ABC4', N'{"latitud":-34.6,"longitud":-58.37}')
GO
SET IDENTITY_INSERT [dbo].[Camiones] OFF
GO
SET IDENTITY_INSERT [dbo].[Ciudades] ON 
GO
INSERT [dbo].[Ciudades] ([Id], [Distancias], [Nombre], [Ubicacion]) VALUES (1, N'{"2":646,"3":792,"4":933,"5":53,"6":986,"7":985,"8":989}', N'CABA', N'{"latitud":-34.6,"longitud":-58.37}')
GO
INSERT [dbo].[Ciudades] ([Id], [Distancias], [Nombre], [Ubicacion]) VALUES (2, N'{"1":646,"3":677,"4":824,"5":698,"6":340,"7":466,"8":907}', N'Córdoba', N'{"latitud":-31.42,"longitud":-64.18}')
GO
INSERT [dbo].[Ciudades] ([Id], [Distancias], [Nombre], [Ubicacion]) VALUES (3, N'{"1":792,"2":677,"4":157,"5":830,"6":814,"7":1131,"8":1534}', N'Corrientes', N'{"latitud":-27.47,"longitud":-58.83}')
GO
INSERT [dbo].[Ciudades] ([Id], [Distancias], [Nombre], [Ubicacion]) VALUES (4, N'{"1":933,"2":824,"3":157,"5":968,"6":927,"7":1269,"8":1690}', N'Formosa', N'{"latitud":-26.18,"longitud":-58.17}')
GO
INSERT [dbo].[Ciudades] ([Id], [Distancias], [Nombre], [Ubicacion]) VALUES (5, N'{"1":53,"2":698,"3":830,"4":968,"6":1038,"7":1029,"8":1005}', N'La Plata', N'{"latitud":-34.92,"longitud":-57.95}')
GO
INSERT [dbo].[Ciudades] ([Id], [Distancias], [Nombre], [Ubicacion]) VALUES (6, N'{"1":986,"2":340,"3":814,"4":927,"5":1038,"7":427,"8":1063}', N'La Rioja', N'{"latitud":-29.41,"longitud":-66.85}')
GO
INSERT [dbo].[Ciudades] ([Id], [Distancias], [Nombre], [Ubicacion]) VALUES (7, N'{"1":985,"2":466,"3":1131,"4":1269,"5":1029,"6":427,"8":676}', N'Mendoza', N'{"latitud":-32.88,"longitud":-68.84}')
GO
INSERT [dbo].[Ciudades] ([Id], [Distancias], [Nombre], [Ubicacion]) VALUES (8, N'{"1":989,"2":907,"3":1534,"4":1690,"5":1005,"6":1063,"7":676}', N'Neuquén', N'{"latitud":-38.95,"longitud":-68.05}')
GO
SET IDENTITY_INSERT [dbo].[Ciudades] OFF
GO
SET IDENTITY_INSERT [dbo].[Pedidos] ON 
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (1, 1, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (2, 1, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (3, 1, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (4, 1, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (5, 1, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (6, 1, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (7, 1, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (8, 1, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (9, 1, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (10, 1, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (11, 1, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (12, 1, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (13, 1, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (14, 1, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (15, 1, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (16, 1, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (17, 1, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (18, 1, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (19, 1, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (20, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (21, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (22, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (23, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (24, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (25, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (26, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (27, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (28, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (29, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (30, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (31, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (32, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (33, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (34, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (35, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (36, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (37, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (38, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (39, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (40, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (41, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (42, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (43, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (44, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (45, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (46, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (47, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (48, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (49, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (50, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (51, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (52, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (53, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (54, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (55, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (56, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (57, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (58, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (59, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (60, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (61, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (62, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (63, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (64, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (65, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (66, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (67, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (68, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (69, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (70, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (71, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (72, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (73, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (74, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (75, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (76, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (77, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (78, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (79, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (80, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (81, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (82, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (83, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (84, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (85, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (86, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (87, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (88, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (89, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (90, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (91, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (92, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (93, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (94, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (95, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (96, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (97, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (98, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (99, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (100, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (101, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (102, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (103, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (104, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (105, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (106, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (107, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (108, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (109, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (110, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (111, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (112, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (113, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (114, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (115, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (116, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (117, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (118, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (119, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (120, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (121, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (122, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (123, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (124, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (125, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (126, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (127, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (128, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (129, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (130, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (131, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (132, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (133, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (134, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (135, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (136, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (137, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (138, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (139, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (140, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (141, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (142, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (143, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (144, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (145, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (146, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (147, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (148, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (149, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (150, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (151, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (152, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (153, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (154, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (155, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (156, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (157, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (158, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (159, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (160, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (161, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (162, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (163, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (164, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (165, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (166, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (167, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (168, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (169, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (170, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (171, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (172, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (173, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (174, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (175, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (176, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (177, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (178, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (179, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (180, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (181, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (182, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (183, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (184, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (185, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (186, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (187, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (188, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (189, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (190, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (191, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (192, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (193, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (194, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (195, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (196, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (197, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (198, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (199, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (200, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (201, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (202, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (203, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (204, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (205, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (206, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (207, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (208, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (209, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (210, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (211, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (212, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (213, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (214, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (215, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (216, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (217, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (218, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (219, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (220, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (221, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (222, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (223, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (224, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (225, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (226, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (227, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (228, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (229, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (230, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (231, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (232, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (233, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (234, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (235, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (236, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (237, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (238, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (239, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (240, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (241, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (242, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (243, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (244, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (245, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (246, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (247, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (248, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (249, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (250, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (251, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (252, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (253, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (254, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (255, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (256, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (257, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (258, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (259, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (260, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (261, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (262, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (263, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (264, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (265, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (266, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (267, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (268, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (269, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (270, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (271, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (272, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (273, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (274, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (275, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (276, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (277, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (278, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (279, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (280, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (281, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (282, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (283, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (284, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (285, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (286, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (287, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (288, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (289, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (290, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (291, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (292, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (293, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (294, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (295, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (296, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (297, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (298, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (299, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (300, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (301, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (302, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (303, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (304, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (305, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (306, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (307, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (308, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (309, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (310, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (311, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (312, 0, 8, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (313, 0, 1, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (314, 0, 2, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (315, 0, 3, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (316, 0, 4, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (317, 0, 5, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (318, 0, 6, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (319, 0, 7, NULL)
GO
INSERT [dbo].[Pedidos] ([Id], [Entregado], [CiudadId], [RecorridoId]) VALUES (320, 0, 8, NULL)
GO
SET IDENTITY_INSERT [dbo].[Pedidos] OFF
GO
SET IDENTITY_INSERT [dbo].[PlanRecorridos] ON 
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (1, CAST(N'2056-01-12T01:00:00.0000000' AS DateTime2), 1, 1, 1, 1)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (2, CAST(N'2000-08-04T18:00:00.0000000' AS DateTime2), 1, 2, 5, 1)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (3, CAST(N'2060-10-05T01:00:00.0000000' AS DateTime2), 1, 3, 2, 1)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (4, CAST(N'2067-01-20T18:00:00.0000000' AS DateTime2), 1, 4, 6, 1)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (5, CAST(N'1941-12-05T19:00:00.0000000' AS DateTime2), 1, 5, 7, 1)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (6, CAST(N'1920-09-23T17:00:00.0000000' AS DateTime2), 1, 6, 8, 1)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (7, CAST(N'1955-10-28T09:00:00.0000000' AS DateTime2), 1, 7, 3, 1)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (8, CAST(N'2055-05-02T12:00:00.0000000' AS DateTime2), 1, 8, 4, 1)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (9, CAST(N'2029-05-01T21:00:00.0000000' AS DateTime2), 1, 9, 1, 1)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (10, CAST(N'1955-12-03T02:00:00.0000000' AS DateTime2), 1, 1, 8, 2)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (11, CAST(N'1962-07-22T03:00:00.0000000' AS DateTime2), 1, 2, 5, 2)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (12, CAST(N'1969-12-11T12:00:00.0000000' AS DateTime2), 1, 3, 1, 2)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (13, CAST(N'2009-02-23T22:00:00.0000000' AS DateTime2), 1, 4, 4, 2)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (14, CAST(N'1938-05-02T13:00:00.0000000' AS DateTime2), 1, 5, 3, 2)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (15, CAST(N'2021-06-20T04:00:00.0000000' AS DateTime2), 1, 6, 2, 2)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (16, CAST(N'2070-10-09T01:00:00.0000000' AS DateTime2), 1, 7, 6, 2)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (17, CAST(N'2063-07-22T22:00:00.0000000' AS DateTime2), 1, 8, 7, 2)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (18, CAST(N'2030-08-21T04:00:00.0000000' AS DateTime2), 1, 1, 5, 3)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (19, CAST(N'2030-08-22T05:00:00.0000000' AS DateTime2), 1, 2, 1, 3)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (20, CAST(N'2030-08-23T06:00:00.0000000' AS DateTime2), 1, 3, 4, 3)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (21, NULL, 0, 4, 3, 3)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (22, NULL, 0, 5, 2, 3)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (23, NULL, 0, 6, 6, 3)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (24, NULL, 0, 7, 7, 3)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (25, NULL, 0, 8, 8, 3)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (26, CAST(N'2054-06-04T10:00:00.0000000' AS DateTime2), 1, 1, 1, 4)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (27, CAST(N'2076-07-04T17:00:00.0000000' AS DateTime2), 1, 2, 2, 4)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (28, CAST(N'2068-05-19T14:00:00.0000000' AS DateTime2), 1, 1, 1, 5)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (29, CAST(N'1948-07-30T16:00:00.0000000' AS DateTime2), 1, 2, 3, 5)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (30, CAST(N'2032-03-17T14:00:00.0000000' AS DateTime2), 1, 3, 4, 5)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (31, CAST(N'1917-07-18T02:00:00.0000000' AS DateTime2), 1, 1, 1, 6)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (32, CAST(N'2010-12-19T15:00:00.0000000' AS DateTime2), 1, 2, 5, 6)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (33, CAST(N'1917-11-27T10:00:00.0000000' AS DateTime2), 1, 3, 6, 6)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (34, CAST(N'2038-12-19T11:00:00.0000000' AS DateTime2), 1, 1, 1, 7)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (35, CAST(N'1922-03-09T15:00:00.0000000' AS DateTime2), 1, 2, 7, 7)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (36, CAST(N'2054-12-07T15:00:00.0000000' AS DateTime2), 1, 3, 8, 7)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (37, CAST(N'1978-12-27T13:00:00.0000000' AS DateTime2), 1, 1, 1, 8)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (38, CAST(N'2024-03-05T23:00:00.0000000' AS DateTime2), 1, 2, 2, 8)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (39, CAST(N'1962-10-06T13:00:00.0000000' AS DateTime2), 1, 1, 1, 9)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (40, CAST(N'2022-08-16T16:00:00.0000000' AS DateTime2), 1, 2, 3, 9)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (41, CAST(N'1977-03-27T13:00:00.0000000' AS DateTime2), 1, 3, 4, 9)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (42, CAST(N'1954-09-06T17:00:00.0000000' AS DateTime2), 1, 1, 1, 10)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (43, CAST(N'2027-08-23T13:00:00.0000000' AS DateTime2), 1, 2, 5, 10)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (44, CAST(N'1906-04-10T08:00:00.0000000' AS DateTime2), 1, 3, 6, 10)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (45, CAST(N'2046-09-02T05:00:00.0000000' AS DateTime2), 1, 1, 1, 11)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (46, CAST(N'1984-09-01T05:00:00.0000000' AS DateTime2), 1, 2, 7, 11)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (47, CAST(N'1960-12-20T06:00:00.0000000' AS DateTime2), 1, 3, 8, 11)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (48, CAST(N'2006-11-06T17:00:00.0000000' AS DateTime2), 1, 1, 1, 12)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (49, CAST(N'1986-01-02T01:00:00.0000000' AS DateTime2), 1, 2, 2, 12)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (50, CAST(N'1957-01-17T11:00:00.0000000' AS DateTime2), 1, 1, 1, 13)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (51, CAST(N'2041-05-19T11:00:00.0000000' AS DateTime2), 1, 2, 3, 13)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (52, CAST(N'1906-09-23T23:00:00.0000000' AS DateTime2), 1, 3, 4, 13)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (53, CAST(N'2045-07-08T05:00:00.0000000' AS DateTime2), 1, 1, 1, 14)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (54, CAST(N'1980-05-27T02:00:00.0000000' AS DateTime2), 1, 2, 5, 14)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (55, CAST(N'2037-02-12T18:00:00.0000000' AS DateTime2), 1, 3, 6, 14)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (56, CAST(N'1938-07-10T03:00:00.0000000' AS DateTime2), 1, 1, 1, 15)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (57, CAST(N'1915-04-28T03:00:00.0000000' AS DateTime2), 1, 2, 7, 15)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (58, CAST(N'1973-02-02T02:00:00.0000000' AS DateTime2), 1, 3, 8, 15)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (59, NULL, 0, 1, 2, 16)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (60, NULL, 0, 2, 3, 16)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (61, NULL, 0, 3, 4, 16)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (62, NULL, 0, 4, 1, 16)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (63, NULL, 0, 5, 5, 16)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (64, NULL, 0, 6, 8, 16)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (65, NULL, 0, 7, 7, 16)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (66, NULL, 0, 8, 6, 16)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (67, NULL, 0, 1, 4, 17)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (68, NULL, 0, 2, 1, 17)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (69, NULL, 0, 3, 5, 17)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (70, NULL, 0, 4, 8, 17)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (71, NULL, 0, 5, 7, 17)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (72, NULL, 0, 6, 6, 17)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (73, NULL, 0, 7, 2, 17)
GO
INSERT [dbo].[PlanRecorridos] ([Id], [FechaFin], [Finalizado], [Prioridad], [CiudadId], [RecorridoId]) VALUES (74, NULL, 0, 8, 3, 17)
GO
SET IDENTITY_INSERT [dbo].[PlanRecorridos] OFF
GO
SET IDENTITY_INSERT [dbo].[Recorridos] ON 
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (1, 1, CAST(N'2031-08-23T06:17:00.0000000' AS DateTime2), CAST(N'2031-08-23T00:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (2, 1, CAST(N'2053-07-24T11:17:00.0000000' AS DateTime2), CAST(N'2053-07-24T05:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (3, 1, NULL, CAST(N'2065-07-21T06:00:00.0000000' AS DateTime2), 0)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (4, 1, CAST(N'2035-04-16T23:17:00.0000000' AS DateTime2), CAST(N'2035-04-16T17:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (5, 1, CAST(N'1951-02-06T11:17:00.0000000' AS DateTime2), CAST(N'1951-02-06T05:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (6, 1, CAST(N'1948-12-19T18:17:00.0000000' AS DateTime2), CAST(N'1948-12-19T12:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (7, 1, CAST(N'1953-06-22T11:17:00.0000000' AS DateTime2), CAST(N'1953-06-22T05:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (8, 1, CAST(N'2024-08-25T17:17:00.0000000' AS DateTime2), CAST(N'2024-08-25T11:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (9, 1, CAST(N'1937-09-11T08:17:00.0000000' AS DateTime2), CAST(N'1937-09-11T02:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (10, 1, CAST(N'1948-09-14T03:17:00.0000000' AS DateTime2), CAST(N'1948-09-13T21:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (11, 1, CAST(N'1921-01-10T15:17:00.0000000' AS DateTime2), CAST(N'1921-01-10T09:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (12, 1, CAST(N'2040-09-16T13:17:00.0000000' AS DateTime2), CAST(N'2040-09-16T07:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (13, 1, CAST(N'2058-01-19T03:17:00.0000000' AS DateTime2), CAST(N'2058-01-18T21:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (14, 1, CAST(N'2045-08-08T07:17:00.0000000' AS DateTime2), CAST(N'2045-08-08T01:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (15, 1, CAST(N'1903-02-18T10:17:00.0000000' AS DateTime2), CAST(N'1903-02-18T04:00:00.0000000' AS DateTime2), 1)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (16, NULL, NULL, NULL, 0)
GO
INSERT [dbo].[Recorridos] ([Id], [CamionId], [FechaFin], [FechaInicio], [Finalizado]) VALUES (17, NULL, NULL, NULL, 0)
GO
SET IDENTITY_INSERT [dbo].[Recorridos] OFF
GO
/****** Object:  Index [IX_Pedidos_CiudadId]    Script Date: 9/7/2024 12:08:02 ******/
CREATE NONCLUSTERED INDEX [IX_Pedidos_CiudadId] ON [dbo].[Pedidos]
(
	[CiudadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Pedidos_RecorridoId]    Script Date: 9/7/2024 12:08:02 ******/
CREATE NONCLUSTERED INDEX [IX_Pedidos_RecorridoId] ON [dbo].[Pedidos]
(
	[RecorridoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlanRecorridos_CiudadId]    Script Date: 9/7/2024 12:08:02 ******/
CREATE NONCLUSTERED INDEX [IX_PlanRecorridos_CiudadId] ON [dbo].[PlanRecorridos]
(
	[CiudadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PlanRecorridos_RecorridoId]    Script Date: 9/7/2024 12:08:02 ******/
CREATE NONCLUSTERED INDEX [IX_PlanRecorridos_RecorridoId] ON [dbo].[PlanRecorridos]
(
	[RecorridoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Recorridos_CamionId]    Script Date: 9/7/2024 12:08:02 ******/
CREATE NONCLUSTERED INDEX [IX_Recorridos_CamionId] ON [dbo].[Recorridos]
(
	[CamionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Ciudades_CiudadId] FOREIGN KEY([CiudadId])
REFERENCES [dbo].[Ciudades] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Ciudades_CiudadId]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Recorridos_RecorridoId] FOREIGN KEY([RecorridoId])
REFERENCES [dbo].[Recorridos] ([Id])
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Recorridos_RecorridoId]
GO
ALTER TABLE [dbo].[PlanRecorridos]  WITH CHECK ADD  CONSTRAINT [FK_PlanRecorridos_Ciudades_CiudadId] FOREIGN KEY([CiudadId])
REFERENCES [dbo].[Ciudades] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlanRecorridos] CHECK CONSTRAINT [FK_PlanRecorridos_Ciudades_CiudadId]
GO
ALTER TABLE [dbo].[PlanRecorridos]  WITH CHECK ADD  CONSTRAINT [FK_PlanRecorridos_Recorridos_RecorridoId] FOREIGN KEY([RecorridoId])
REFERENCES [dbo].[Recorridos] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PlanRecorridos] CHECK CONSTRAINT [FK_PlanRecorridos_Recorridos_RecorridoId]
GO
ALTER TABLE [dbo].[Recorridos]  WITH CHECK ADD  CONSTRAINT [FK_Recorridos_Camiones_CamionId] FOREIGN KEY([CamionId])
REFERENCES [dbo].[Camiones] ([Id])
GO
ALTER TABLE [dbo].[Recorridos] CHECK CONSTRAINT [FK_Recorridos_Camiones_CamionId]
GO
USE [master]
GO
ALTER DATABASE [wpdos] SET  READ_WRITE 
GO
