CREATE TABLE [dbo].[Table1] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Nome]          NVARCHAR (100) NOT NULL,
    [CodiceRegione] NVARCHAR (50)  NOT NULL,
    [Cap]           NCHAR (10)     NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

