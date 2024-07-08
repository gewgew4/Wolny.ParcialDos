using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wolny.P.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Datos1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql =
        $"""
            USE [wpdos]
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

        
        """;
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
