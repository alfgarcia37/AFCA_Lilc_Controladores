CREATE TABLE [dbo].[Medidores] (
    [ID]               CHAR (6)      NOT NULL,
    [DET]              CHAR (100)    NULL,
    [Falta]            SMALLDATETIME NULL,
    [Fbaja]            SMALLDATETIME NULL,
    [FProxCalibracion] SMALLDATETIME NULL,
    [Controlador]      SMALLINT      NULL,
    CONSTRAINT [PK_Medidores] PRIMARY KEY CLUSTERED ([ID] ASC) WITH (FILLFACTOR = 90)
);

