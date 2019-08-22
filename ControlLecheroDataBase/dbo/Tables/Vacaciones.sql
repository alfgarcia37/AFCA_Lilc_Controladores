CREATE TABLE [dbo].[Vacaciones] (
    [CONTROLADOR] INT              NULL,
    [FINICIO]     SMALLDATETIME    NULL,
    [FFINAL]      SMALLDATETIME    NULL,
    [DET]         VARCHAR (200)    NULL,
    [NATURALES]   SMALLINT         NULL,
    [LABORABLES]  SMALLINT         NULL,
    [ID]          UNIQUEIDENTIFIER CONSTRAINT [DF_Vacaciones_ID] DEFAULT (newid()) NULL
);

