CREATE TABLE [dbo].[Gastos] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [Controlador] INT           NULL,
    [Fecha]       SMALLDATETIME NULL,
    [Concepto]    CHAR (50)     NULL,
    [BI]          REAL          NULL,
    [IVA]         SMALLINT      NULL,
    [RET]         SMALLINT      NULL,
    [Entidad]     CHAR (3)      NULL,
    [DNICIF]      CHAR (15)     NULL,
    [Documento]   CHAR (15)     NULL,
    [CodConcepto] CHAR (3)      NULL
);

