CREATE TABLE [dbo].[ESTADOS] (
    [ID]            CHAR (1)      NOT NULL,
    [DET]           CHAR (15)     NULL,
    [Observaciones] VARCHAR (200) NULL,
    CONSTRAINT [pk_estados] PRIMARY KEY CLUSTERED ([ID] ASC)
);

