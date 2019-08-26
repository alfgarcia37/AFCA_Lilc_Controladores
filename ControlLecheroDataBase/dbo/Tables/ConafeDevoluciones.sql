CREATE TABLE [dbo].[ConafeDevoluciones] (
    [CODIGO]            VARCHAR (5)   NULL,
    [VACACONAFE]        CHAR (14)     NULL,
    [SANITARIO]         CHAR (14)     NULL,
    [ESTABLO]           CHAR (7)      NULL,
    [NP]                CHAR (2)      NULL,
    [FP]                SMALLDATETIME NULL,
    [KILOS305]          CHAR (8)      NULL,
    [KILOSNATURAL]      CHAR (8)      NULL,
    [GANPRO]            CHAR (7)      NULL,
    [FechaCarga]        SMALLDATETIME NULL,
    [FicheroConafe]     VARCHAR (100) NULL,
    [ReEnviado]         CHAR (1)      NULL,
    [Periododevolucion] VARCHAR (50)  NULL,
    [Nombre]            VARCHAR (35)  NULL
);

