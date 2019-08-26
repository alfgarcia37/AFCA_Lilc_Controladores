CREATE TABLE [dbo].[Ingresos] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [controlador] INT           NULL,
    [NumFac]      SMALLINT      NULL,
    [Fecha]       SMALLDATETIME NULL,
    [Concepto]    CHAR (50)     NULL,
    [BI]          REAL          NULL,
    [IVA]         SMALLINT      NULL,
    [RET]         SMALLINT      NULL,
    [entidad]     CHAR (3)      NULL,
    [CodConcepto] CHAR (3)      NULL,
    [DNICIF]      CHAR (5)      NULL
);

