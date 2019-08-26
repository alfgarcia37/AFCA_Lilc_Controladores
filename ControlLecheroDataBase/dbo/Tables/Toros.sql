CREATE TABLE [dbo].[Toros] (
    [ID]            CHAR (14)     NOT NULL,
    [DET]           VARCHAR (30)  NULL,
    [DBF]           CHAR (1)      NULL,
    [MARCA]         CHAR (5)      NULL,
    [PRECIO]        REAL          NULL,
    [RAZA]          CHAR (2)      NULL,
    [OBSERVACIONES] VARCHAR (250) NULL,
    [Inventado]     CHAR (1)      NULL,
    CONSTRAINT [PK_Toros] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);

