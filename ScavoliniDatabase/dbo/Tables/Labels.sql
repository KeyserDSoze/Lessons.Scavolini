CREATE TABLE [dbo].[Labels] (
    [Id]       INT        IDENTITY (1, 1) NOT NULL,
    [ComuneId] INT        NOT NULL,
    [Label]    NCHAR (10) NOT NULL,
    CONSTRAINT [PK_Labels] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Labels_Comuni] FOREIGN KEY ([ComuneId]) REFERENCES [dbo].[Comuni] ([Id])
);

