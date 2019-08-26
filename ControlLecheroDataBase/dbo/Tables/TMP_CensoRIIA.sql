CREATE TABLE [dbo].[TMP_CensoRIIA] (
    [Establo]         CHAR (7)      NULL,
    [Vaca]            CHAR (14)     NULL,
    [Nombre]          CHAR (30)     NULL,
    [FN]              SMALLDATETIME NULL,
    [usuario]         VARCHAR (50)  NULL,
    [FAltaAFCA]       SMALLDATETIME NULL,
    [FBajaAFCA]       SMALLDATETIME NULL,
    [FAltaRIIA]       SMALLDATETIME NULL,
    [FBajaRIIA]       SMALLDATETIME NULL,
    [destino]         VARCHAR (7)   NULL,
    [VACARIIA]        VARCHAR (14)  NULL,
    [MOTIVO]          CHAR (7)      NULL,
    [FBajaMuertaRIIA] SMALLDATETIME NULL,
    [MesesCalculo]    INT           NULL,
    [FCalculo]        SMALLDATETIME NULL,
    [Controlador]     VARCHAR (3)   NULL,
    [Raza]            VARCHAR (4)   NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_TMP_CensoRIIA]
    ON [dbo].[TMP_CensoRIIA]([Vaca] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_TMP_CensoRIIA_1]
    ON [dbo].[TMP_CensoRIIA]([Establo] ASC);

