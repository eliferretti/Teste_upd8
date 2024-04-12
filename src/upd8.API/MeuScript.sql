IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Clientes] (
    [Id] nvarchar(450) NOT NULL,
    [Cpf] nvarchar(max) NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [BirthDate] datetime2 NULL,
    [Gender] int NOT NULL,
    [Adress] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [CardNumber] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240411214617_Primeira', N'7.0.18');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Clientes] DROP CONSTRAINT [PK_Clientes];
GO

EXEC sp_rename N'[Clientes]', N'Customers';
GO

ALTER TABLE [Customers] ADD CONSTRAINT [PK_Customers] PRIMARY KEY ([Id]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240411221811_Segunda', N'7.0.18');
GO

COMMIT;
GO

