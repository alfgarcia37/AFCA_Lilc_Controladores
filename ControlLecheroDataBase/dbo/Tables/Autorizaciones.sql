CREATE TABLE [dbo].[Autorizaciones] (
    [ID]            CHAR (20)  NOT NULL,
    [DET]           CHAR (30)  NOT NULL,
    [OBSERVACIONES] CHAR (200) NULL,
    CONSTRAINT [PK_Autorizaciones] PRIMARY KEY CLUSTERED ([ID] ASC, [DET] ASC) WITH (FILLFACTOR = 90)
);

