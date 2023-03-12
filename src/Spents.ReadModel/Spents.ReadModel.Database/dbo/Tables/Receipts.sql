CREATE TABLE [dbo].[Receipts] (
    [Id]                INT              IDENTITY (1, 1) NOT NULL,
    [ReceiptId]         UNIQUEIDENTIFIER NOT NULL,
    [EstablishmentName] VARCHAR (200)    NOT NULL,
    [ReceiptDate]       DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

