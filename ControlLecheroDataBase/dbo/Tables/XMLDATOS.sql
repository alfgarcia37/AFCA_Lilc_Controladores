CREATE TABLE [dbo].[XMLDATOS] (
    [CODIGO]        CHAR (7)        NOT NULL,
    [ACT]           CHAR (1)        NULL,
    [FCARGA]        DATETIME        NULL,
    [FPREV]         DATETIME        NULL,
    [FCTRL]         DATETIME        NULL,
    [ANTORD_HH]     CHAR (2)        NULL,
    [ANTORD_MM]     CHAR (2)        NULL,
    [RAZON]         CHAR (50)       NULL,
    [TCONTROL]      CHAR (2)        NULL,
    [TELEF]         CHAR (50)       NULL,
    [RUTINAS]       CHAR (2)        NULL,
    [MENSAJE]       CHAR (200)      NULL,
    [RESPUESTA]     CHAR (200)      NULL,
    [TANQUE]        CHAR (12)       NULL,
    [CONTROLADOR]   CHAR (3)        NULL,
    [NOTAS]         CHAR (200)      NULL,
    [LITROS_TANQUE] CHAR (10)       NULL,
    [FCOMCONTROL]   DATETIME        NULL,
    [MOROSO]        CHAR (1)        NULL,
    [Cetosis]       CHAR (1)        NULL,
    [Firma]         VARBINARY (MAX) NULL
);

