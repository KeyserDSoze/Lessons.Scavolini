CREATE TABLE [dbo].[SuperLabels] (
    [Id]         INT        IDENTITY (1, 1) NOT NULL,
    [LabelId]    INT        NOT NULL,
    [SuperLabel] NCHAR (10) NOT NULL,
    CONSTRAINT [PK_SuperLabels] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuperLabels_Labels] FOREIGN KEY ([LabelId]) REFERENCES [dbo].[Labels] ([Id])
);

