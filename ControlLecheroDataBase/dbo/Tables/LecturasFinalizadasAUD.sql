CREATE TABLE [dbo].[LecturasFinalizadasAUD] (
    [EXPEDIENTE]        CHAR (10)     NOT NULL,
    [AUD_AUDITOR]       INT           NULL,
    [AUD_Controlador]   INT           NULL,
    [AUD_FPREVISTA]     SMALLDATETIME NULL,
    [AUD_FAuditoria]    SMALLDATETIME NULL,
    [AUD_Establo]       CHAR (7)      NULL,
    [AUD_Observaciones] VARCHAR (200) NULL,
    [AUD_tipoControl]   CHAR (2)      NULL,
    [AUD_Finforme]      SMALLDATETIME NULL,
    [AUD_FREVISADO]     SMALLDATETIME NULL,
    [Usuario]           CHAR (15)     NULL,
    [FUSUARIO]          SMALLDATETIME NULL,
    [AUD_RUTINAS]       CHAR (2)      NULL
);

