CREATE TABLE [dbo].[RCS] (
    [ESTABLO]              CHAR (7)     NULL,
    [BARRAS]               CHAR (14)    NULL,
    [DET]                  CHAR (30)    NULL,
    [FENTREGACONTROLADOR]  DATETIME     NULL,
    [FENTREGAGANADERO]     DATETIME     NULL,
    [FANALISIS]            DATETIME     NULL,
    [FSUBIDO]              DATETIME     NULL,
    [RCS]                  REAL         NULL,
    [CONTROLADOR]          INT          NULL,
    [FACTURA]              CHAR (10)    NULL,
    [SEFACTURA]            CHAR (1)     NULL,
    [FREGISTRORCS]         DATETIME     NULL,
    [FDescargaControlador] DATETIME     NULL,
    [Fichero]              VARCHAR (50) NULL
);

