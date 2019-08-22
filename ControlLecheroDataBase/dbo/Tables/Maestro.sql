CREATE TABLE [dbo].[Maestro] (
    [Maes_Gen]        CHAR (14)     NULL,
    [Maes_Nom]        VARCHAR (30)  NULL,
    [Maes_FP]         SMALLDATETIME NULL,
    [Maes_NP]         INT           NULL,
    [Maes_NC]         INT           NULL,
    [Maes_Tit]        CHAR (7)      NULL,
    [Maes_Col]        CHAR (4)      NULL,
    [Maes_FAE]        SMALLDATETIME NULL,
    [Maes_FBA]        SMALLDATETIME NULL,
    [Maes_Estado]     CHAR (1)      NULL,
    [Maes_FN]         SMALLDATETIME NULL,
    [Maes_Madre]      CHAR (14)     NULL,
    [Maes_Padre]      CHAR (14)     NULL,
    [maes_usuario]    CHAR (15)     NULL,
    [maes_fusuario]   SMALLDATETIME NULL,
    [maes_raza]       CHAR (2)      NULL,
    [maes_FRegistro]  SMALLDATETIME NULL,
    [Maes_FBajaCenso] SMALLDATETIME NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Maestro]
    ON [dbo].[Maestro]([Maes_Tit] ASC, [Maes_Gen] ASC, [Maes_Estado] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Maestro_1]
    ON [dbo].[Maestro]([Maes_Gen] ASC) WITH (FILLFACTOR = 90);

