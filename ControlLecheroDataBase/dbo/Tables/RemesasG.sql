CREATE TABLE [dbo].[RemesasG] (
    [ID]            CHAR (10)     NOT NULL,
    [DET]           CHAR (50)     NULL,
    [FGeneracion]   SMALLDATETIME NULL,
    [PrecioVaca]    REAL          NULL,
    [Observaciones] VARCHAR (300) NULL,
    [FCargo]        SMALLDATETIME NULL,
    [FPresentacion] SMALLDATETIME NULL,
    [FCalculo]      SMALLDATETIME NULL,
    [FCalculo2]     SMALLDATETIME NULL,
    CONSTRAINT [PK_RemesasG] PRIMARY KEY CLUSTERED ([ID] ASC)
);

