
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/24/2019 15:22:25
-- Generated from EDMX file: C:\Users\acer\Desktop\SmartTools\SmartTools\SmartTools.Service\Data\STModule.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SmartTools];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(32)  NOT NULL,
    [UserPwd] nvarchar(128)  NOT NULL,
    [CreateDate] datetime  NOT NULL,
    [IsActivation] bit  NULL,
    [ActivationLevel] int  NULL,
    [ActivationDate] datetime  NULL
);
GO

-- Creating table 'Sys_Activation'
CREATE TABLE [dbo].[Sys_Activation] (
    [Sys_ActivationId] int IDENTITY(1,1) NOT NULL,
    [ActivationCode] nvarchar(max)  NOT NULL,
    [ActivationLevel] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [UserId] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [Sys_ActivationId] in table 'Sys_Activation'
ALTER TABLE [dbo].[Sys_Activation]
ADD CONSTRAINT [PK_Sys_Activation]
    PRIMARY KEY CLUSTERED ([Sys_ActivationId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------