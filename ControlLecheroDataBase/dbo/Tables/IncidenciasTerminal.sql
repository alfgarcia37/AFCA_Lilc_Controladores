CREATE TABLE [dbo].[IncidenciasTerminal] (
    [NumControlador] INT           NULL,
    [NumTerminal]    CHAR (15)     NULL,
    [ID]             INT           IDENTITY (1, 1) NOT NULL,
    [FIncidencia]    SMALLDATETIME NULL,
    [DET]            VARCHAR (200) NULL,
    [FSalida]        SMALLDATETIME NULL,
    [FDevolucion]    SMALLDATETIME NULL,
    [Importe]        REAL          NULL,
    [Observaciones]  VARCHAR (250) NULL,
    [FBaja]          SMALLDATETIME NULL,
    [DOCasociado]    CHAR (15)     NULL,
    [numcuna]        CHAR (15)     NULL
);

