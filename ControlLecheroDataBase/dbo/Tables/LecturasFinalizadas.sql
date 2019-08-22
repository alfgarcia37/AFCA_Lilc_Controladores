CREATE TABLE [dbo].[LecturasFinalizadas] (
    [NumControler]        INT           NULL,
    [Establo]             CHAR (7)      NULL,
    [Calculado]           CHAR (1)      NULL,
    [VacasControl]        INT           NULL,
    [FPrevista]           SMALLDATETIME NULL,
    [FReal]               SMALLDATETIME NULL,
    [LoginID]             CHAR (20)     NULL,
    [FechaID]             DATETIME      NULL,
    [Observaciones]       VARCHAR (200) NULL,
    [AnteriorOrdeno]      CHAR (5)      NULL,
    [ActualOrdeno]        CHAR (5)      NULL,
    [GeneraTXT]           CHAR (1)      NULL,
    [tipoControl]         CHAR (2)      NULL,
    [informe]             CHAR (1)      CONSTRAINT [DF_LecturasFinalizadas_informe] DEFAULT ((0)) NULL,
    [Fentrega]            SMALLDATETIME NULL,
    [Fanalisis]           SMALLDATETIME NULL,
    [Fcarga]              SMALLDATETIME NULL,
    [FDescarga]           SMALLDATETIME NULL,
    [PrecioEstablo]       REAL          NULL,
    [SeguridadSocial]     REAL          NULL,
    [sueldoglobal]        CHAR (1)      NULL,
    [nucleo]              CHAR (1)      NULL,
    [ActualOrdenoM]       DATETIME      NULL,
    [ActualOrdenoT]       DATETIME      NULL,
    [Rutinas]             CHAR (2)      NULL,
    [informeGanadero]     CHAR (1)      CONSTRAINT [DF_LecturasFinalizadas_informeGanadero_1] DEFAULT ('0') NULL,
    [Respuesta]           CHAR (200)    NULL,
    [VistoRespuesta]      CHAR (1)      CONSTRAINT [DF_LecturasFinalizadas_VistoRespuesta_1] DEFAULT ('0') NULL,
    [Corregido]           CHAR (1)      CONSTRAINT [DF_LecturasFinalizadas_Corregido] DEFAULT ((0)) NULL,
    [FCorregido]          SMALLDATETIME NULL,
    [MotivoCorregido]     CHAR (2)      NULL,
    [Usuario]             CHAR (15)     NULL,
    [ANALITICA]           CHAR (1)      CONSTRAINT [DF_LecturasFinalizadas_ANALITICA] DEFAULT ('0') NULL,
    [Subido]              CHAR (1)      CONSTRAINT [DF_LecturasFinalizadas_Subido] DEFAULT ((0)) NULL,
    [FInforme]            SMALLDATETIME NULL,
    [Tanque]              CHAR (6)      NULL,
    [NoLeido]             CHAR (1)      CONSTRAINT [DF_LecturasFinalizadas_NoLeido] DEFAULT ((0)) NULL,
    [MotivoNoLeido]       CHAR (150)    NULL,
    [Fichero]             CHAR (30)     NULL,
    [LitrosTanque]        SMALLINT      NULL,
    [RazonCorregido]      VARCHAR (300) NULL,
    [KM]                  INT           CONSTRAINT [DF_LecturasFinalizadas_KM] DEFAULT ((0)) NULL,
    [Propio]              CHAR (1)      CONSTRAINT [DF_LecturasFinalizadas_Propio] DEFAULT ('1') NULL,
    [ControladorNoPropio] SMALLINT      CONSTRAINT [DF_LecturasFinalizadas_ControladorNoPropio] DEFAULT ((0)) NOT NULL,
    [KMAsignado]          INT           CONSTRAINT [DF_LecturasFinalizadas_KMAsignado] DEFAULT ((0)) NOT NULL
);


GO
CREATE UNIQUE CLUSTERED INDEX [IX_LecturasFinalizadas]
    ON [dbo].[LecturasFinalizadas]([Establo] ASC, [FPrevista] ASC) WITH (FILLFACTOR = 90, IGNORE_DUP_KEY = ON);


GO
CREATE NONCLUSTERED INDEX [IX_LecturasFinalizadas_1]
    ON [dbo].[LecturasFinalizadas]([NumControler] ASC) WITH (FILLFACTOR = 90);

