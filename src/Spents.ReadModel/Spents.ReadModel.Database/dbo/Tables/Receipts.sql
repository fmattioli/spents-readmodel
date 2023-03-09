CREATE TABLE [dbo].[Receipts] (
    [Id]                INT              IDENTITY (1, 1) NOT NULL,
    [ReceiptId]         UNIQUEIDENTIFIER NULL,
    [EstablishmentName] VARCHAR (200)    NULL,
    [ReceiptDate]       DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

