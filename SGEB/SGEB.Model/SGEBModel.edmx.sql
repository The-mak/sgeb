
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 04/22/2013 14:14:15
-- Generated from EDMX file: C:\Users\themak\Desktop\Projetos\SGEB\SGEB9\SGEB\SGEB.Model\SGEBModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SGEBDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DriverRg]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Drivers] DROP CONSTRAINT [FK_DriverRg];
GO
IF OBJECT_ID(N'[dbo].[FK_DriverCpf]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Drivers] DROP CONSTRAINT [FK_DriverCpf];
GO
IF OBJECT_ID(N'[dbo].[FK_DriverCnh]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Drivers] DROP CONSTRAINT [FK_DriverCnh];
GO
IF OBJECT_ID(N'[dbo].[FK_DriverAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Drivers] DROP CONSTRAINT [FK_DriverAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_DriverContact]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Drivers] DROP CONSTRAINT [FK_DriverContact];
GO
IF OBJECT_ID(N'[dbo].[FK_VehicleVehicle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehicles] DROP CONSTRAINT [FK_VehicleVehicle];
GO
IF OBJECT_ID(N'[dbo].[FK_OwnerAddress]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Owners] DROP CONSTRAINT [FK_OwnerAddress];
GO
IF OBJECT_ID(N'[dbo].[FK_VehicleOwner]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Vehicles] DROP CONSTRAINT [FK_VehicleOwner];
GO
IF OBJECT_ID(N'[dbo].[FK_DriverSheet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sheets] DROP CONSTRAINT [FK_DriverSheet];
GO
IF OBJECT_ID(N'[dbo].[FK_SheetVehicle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sheets] DROP CONSTRAINT [FK_SheetVehicle];
GO
IF OBJECT_ID(N'[dbo].[FK_SheetCart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sheets] DROP CONSTRAINT [FK_SheetCart];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Rgs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rgs];
GO
IF OBJECT_ID(N'[dbo].[Cpfs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cpfs];
GO
IF OBJECT_ID(N'[dbo].[Cnhs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cnhs];
GO
IF OBJECT_ID(N'[dbo].[Addresses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Addresses];
GO
IF OBJECT_ID(N'[dbo].[Contacts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contacts];
GO
IF OBJECT_ID(N'[dbo].[Drivers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Drivers];
GO
IF OBJECT_ID(N'[dbo].[Configurations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Configurations];
GO
IF OBJECT_ID(N'[dbo].[Vehicles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Vehicles];
GO
IF OBJECT_ID(N'[dbo].[Owners]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Owners];
GO
IF OBJECT_ID(N'[dbo].[Sheets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sheets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Rgs'
CREATE TABLE [dbo].[Rgs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Number] nvarchar(11)  NOT NULL,
    [BornDate] datetime  NOT NULL,
    [EmittedDate] datetime  NOT NULL,
    [FathersName] nvarchar(40)  NOT NULL,
    [MothersName] nvarchar(40)  NOT NULL,
    [City] nvarchar(30)  NOT NULL,
    [State] nvarchar(2)  NOT NULL,
    [Image] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Cpfs'
CREATE TABLE [dbo].[Cpfs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(11)  NOT NULL,
    [Image] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Cnhs'
CREATE TABLE [dbo].[Cnhs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Number] nvarchar(11)  NOT NULL,
    [Category] nvarchar(2)  NOT NULL,
    [Record] nvarchar(11)  NOT NULL,
    [EmittedDate] datetime  NOT NULL,
    [DueDate] datetime  NOT NULL,
    [Image] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Addresses'
CREATE TABLE [dbo].[Addresses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Street] nvarchar(50)  NOT NULL,
    [Number] int  NOT NULL,
    [Neighborhood] nvarchar(50)  NOT NULL,
    [ZipCode] nvarchar(8)  NOT NULL,
    [City] nvarchar(30)  NOT NULL,
    [State] nvarchar(2)  NOT NULL
);
GO

-- Creating table 'Contacts'
CREATE TABLE [dbo].[Contacts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [HomePhone] nvarchar(11)  NOT NULL,
    [CelPhone] nvarchar(11)  NOT NULL,
    [RefPhone1] nvarchar(11)  NOT NULL,
    [RefPhone2] nvarchar(11)  NOT NULL,
    [RefContact1] nvarchar(25)  NOT NULL,
    [RefContact2] nvarchar(25)  NOT NULL
);
GO

-- Creating table 'Drivers'
CREATE TABLE [dbo].[Drivers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Rg_Id] int  NOT NULL,
    [Cpf_Id] int  NOT NULL,
    [Cnh_Id] int  NOT NULL,
    [Address_Id] int  NOT NULL,
    [Contact_Id] int  NOT NULL
);
GO

-- Creating table 'Configurations'
CREATE TABLE [dbo].[Configurations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(70)  NOT NULL,
    [DocNumber] nvarchar(20)  NOT NULL,
    [StateRegistration] nvarchar(20)  NOT NULL,
    [Telephone] nvarchar(11)  NOT NULL,
    [CelPhone] nvarchar(11)  NOT NULL,
    [RadioPhone] nvarchar(11)  NOT NULL,
    [Email] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Vehicles'
CREATE TABLE [dbo].[Vehicles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Type] nvarchar(9)  NOT NULL,
    [Plate] nvarchar(7)  NOT NULL,
    [Model] nvarchar(50)  NOT NULL,
    [Chassi] nvarchar(20)  NOT NULL,
    [Renavam] nvarchar(11)  NOT NULL,
    [Color] nvarchar(20)  NOT NULL,
    [Year] int  NOT NULL,
    [City] nvarchar(30)  NOT NULL,
    [State] nvarchar(2)  NOT NULL,
    [ANTT] nvarchar(20)  NOT NULL,
    [Image] nvarchar(max)  NOT NULL,
    [VehicleVehicle_Vehicle1_Id] int  NULL,
    [Owner_Id] int  NOT NULL
);
GO

-- Creating table 'Owners'
CREATE TABLE [dbo].[Owners] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [DocNumber] nvarchar(20)  NOT NULL,
    [Phone] nvarchar(11)  NOT NULL,
    [Address_Id] int  NOT NULL
);
GO

-- Creating table 'Sheets'
CREATE TABLE [dbo].[Sheets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Driver_Id] int  NOT NULL,
    [Truck_Id] int  NOT NULL,
    [Cart_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Rgs'
ALTER TABLE [dbo].[Rgs]
ADD CONSTRAINT [PK_Rgs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cpfs'
ALTER TABLE [dbo].[Cpfs]
ADD CONSTRAINT [PK_Cpfs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Cnhs'
ALTER TABLE [dbo].[Cnhs]
ADD CONSTRAINT [PK_Cnhs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Addresses'
ALTER TABLE [dbo].[Addresses]
ADD CONSTRAINT [PK_Addresses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contacts'
ALTER TABLE [dbo].[Contacts]
ADD CONSTRAINT [PK_Contacts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [PK_Drivers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Configurations'
ALTER TABLE [dbo].[Configurations]
ADD CONSTRAINT [PK_Configurations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Vehicles'
ALTER TABLE [dbo].[Vehicles]
ADD CONSTRAINT [PK_Vehicles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [PK_Owners]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sheets'
ALTER TABLE [dbo].[Sheets]
ADD CONSTRAINT [PK_Sheets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Rg_Id] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [FK_DriverRg]
    FOREIGN KEY ([Rg_Id])
    REFERENCES [dbo].[Rgs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DriverRg'
CREATE INDEX [IX_FK_DriverRg]
ON [dbo].[Drivers]
    ([Rg_Id]);
GO

-- Creating foreign key on [Cpf_Id] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [FK_DriverCpf]
    FOREIGN KEY ([Cpf_Id])
    REFERENCES [dbo].[Cpfs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DriverCpf'
CREATE INDEX [IX_FK_DriverCpf]
ON [dbo].[Drivers]
    ([Cpf_Id]);
GO

-- Creating foreign key on [Cnh_Id] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [FK_DriverCnh]
    FOREIGN KEY ([Cnh_Id])
    REFERENCES [dbo].[Cnhs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DriverCnh'
CREATE INDEX [IX_FK_DriverCnh]
ON [dbo].[Drivers]
    ([Cnh_Id]);
GO

-- Creating foreign key on [Address_Id] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [FK_DriverAddress]
    FOREIGN KEY ([Address_Id])
    REFERENCES [dbo].[Addresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DriverAddress'
CREATE INDEX [IX_FK_DriverAddress]
ON [dbo].[Drivers]
    ([Address_Id]);
GO

-- Creating foreign key on [Contact_Id] in table 'Drivers'
ALTER TABLE [dbo].[Drivers]
ADD CONSTRAINT [FK_DriverContact]
    FOREIGN KEY ([Contact_Id])
    REFERENCES [dbo].[Contacts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DriverContact'
CREATE INDEX [IX_FK_DriverContact]
ON [dbo].[Drivers]
    ([Contact_Id]);
GO

-- Creating foreign key on [VehicleVehicle_Vehicle1_Id] in table 'Vehicles'
ALTER TABLE [dbo].[Vehicles]
ADD CONSTRAINT [FK_VehicleVehicle]
    FOREIGN KEY ([VehicleVehicle_Vehicle1_Id])
    REFERENCES [dbo].[Vehicles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_VehicleVehicle'
CREATE INDEX [IX_FK_VehicleVehicle]
ON [dbo].[Vehicles]
    ([VehicleVehicle_Vehicle1_Id]);
GO

-- Creating foreign key on [Address_Id] in table 'Owners'
ALTER TABLE [dbo].[Owners]
ADD CONSTRAINT [FK_OwnerAddress]
    FOREIGN KEY ([Address_Id])
    REFERENCES [dbo].[Addresses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_OwnerAddress'
CREATE INDEX [IX_FK_OwnerAddress]
ON [dbo].[Owners]
    ([Address_Id]);
GO

-- Creating foreign key on [Owner_Id] in table 'Vehicles'
ALTER TABLE [dbo].[Vehicles]
ADD CONSTRAINT [FK_VehicleOwner]
    FOREIGN KEY ([Owner_Id])
    REFERENCES [dbo].[Owners]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_VehicleOwner'
CREATE INDEX [IX_FK_VehicleOwner]
ON [dbo].[Vehicles]
    ([Owner_Id]);
GO

-- Creating foreign key on [Driver_Id] in table 'Sheets'
ALTER TABLE [dbo].[Sheets]
ADD CONSTRAINT [FK_DriverSheet]
    FOREIGN KEY ([Driver_Id])
    REFERENCES [dbo].[Drivers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_DriverSheet'
CREATE INDEX [IX_FK_DriverSheet]
ON [dbo].[Sheets]
    ([Driver_Id]);
GO

-- Creating foreign key on [Truck_Id] in table 'Sheets'
ALTER TABLE [dbo].[Sheets]
ADD CONSTRAINT [FK_SheetVehicle]
    FOREIGN KEY ([Truck_Id])
    REFERENCES [dbo].[Vehicles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SheetVehicle'
CREATE INDEX [IX_FK_SheetVehicle]
ON [dbo].[Sheets]
    ([Truck_Id]);
GO

-- Creating foreign key on [Cart_Id] in table 'Sheets'
ALTER TABLE [dbo].[Sheets]
ADD CONSTRAINT [FK_SheetCart]
    FOREIGN KEY ([Cart_Id])
    REFERENCES [dbo].[Vehicles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_SheetCart'
CREATE INDEX [IX_FK_SheetCart]
ON [dbo].[Sheets]
    ([Cart_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------