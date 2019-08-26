CREATE TABLE [dbo].[Inseminaciones] (
    [contador]            INT           IDENTITY (1, 1) NOT NULL,
    [Establo]             CHAR (7)      NULL,
    [ID]                  CHAR (14)     NULL,
    [NP]                  INT           NULL,
    [NIns]                INT           NULL,
    [Semental]            CHAR (14)     NULL,
    [NombreToro]          CHAR (30)     NULL,
    [RazaToro]            CHAR (2)      NULL,
    [FInseminacion]       SMALLDATETIME NULL,
    [EstabloIns]          CHAR (7)      NULL,
    [Tecnico]             CHAR (20)     NULL,
    [TipoIns]             CHAR (2)      NULL,
    [TipoDiagnostico]     CHAR (1)      NULL,
    [FDiagnostico]        SMALLDATETIME NULL,
    [Fecundantexxxxx]     CHAR (1)      NULL,
    [MetDiagnostico]      CHAR (2)      NULL,
    [MadreEmbrion]        CHAR (14)     NULL,
    [NumControlador]      INT           NULL,
    [Observaciones]       VARCHAR (250) NULL,
    [PrecioDosis]         REAL          NULL,
    [Dosis]               CHAR (1)      NULL,
    [FCONAFE]             SMALLDATETIME NULL,
    [FRegistroIns]        SMALLDATETIME NULL,
    [OrigenToro]          CHAR (5)      NULL,
    [SementalErroneo]     CHAR (14)     NULL,
    [FRegistroGes]        SMALLDATETIME NULL,
    [FConfirmacion]       SMALLDATETIME NULL,
    [TipoDiagnosticoConf] CHAR (1)      NULL
);


GO
CREATE CLUSTERED INDEX [IX_Inseminaciones]
    ON [dbo].[Inseminaciones]([Establo] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Inseminaciones_1]
    ON [dbo].[Inseminaciones]([ID] ASC, [NP] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Inseminaciones_2]
    ON [dbo].[Inseminaciones]([ID] ASC, [NIns] ASC, [Establo] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Inseminaciones_3]
    ON [dbo].[Inseminaciones]([FRegistroIns] ASC) WITH (FILLFACTOR = 90);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Para ver si el toro lo tomo de CL o REG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'Inseminaciones', @level2type = N'COLUMN', @level2name = N'OrigenToro';

