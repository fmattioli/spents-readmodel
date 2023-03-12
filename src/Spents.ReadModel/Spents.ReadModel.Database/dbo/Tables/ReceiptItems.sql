CREATE TABLE [dbo].[ReceiptItems] (
    [Id]            INT              IDENTITY (1, 1) NOT NULL,
    [FK_Receipt_Id] INT              NULL,
    [ReceiptItemId] UNIQUEIDENTIFIER NULL,
    [ItemName]      VARCHAR (200)    NULL,
    [Quantity]      SMALLINT         NULL,
    [ItemPrice]     MONEY            NULL,
    [Observation]   VARCHAR (200)    NULL,
    [TotalPrice]    AS               ([ItemPrice]*[Quantity])
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([FK_Receipt_Id]) REFERENCES [dbo].[Receipts] ([Id])
);

