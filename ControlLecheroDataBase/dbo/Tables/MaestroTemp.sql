CREATE TABLE [dbo].[MaestroTemp] (
    [Maes_Gen]       CHAR (14)     NULL,
    [Maes_Nom]       VARCHAR (30)  NULL,
    [Maes_FP]        SMALLDATETIME NULL,
    [Maes_NP]        INT           NULL,
    [Maes_NC]        INT           NULL,
    [Maes_Tit]       CHAR (7)      NULL,
    [Maes_Col]       CHAR (4)      NULL,
    [Maes_FAE]       SMALLDATETIME NULL,
    [Maes_FBA]       SMALLDATETIME NULL,
    [Maes_Estado]    CHAR (1)      NULL,
    [Maes_FN]        SMALLDATETIME NULL,
    [Maes_Madre]     CHAR (14)     NULL,
    [Maes_Padre]     CHAR (14)     NULL,
    [maes_usuario]   CHAR (15)     NULL,
    [maes_fusuario]  SMALLDATETIME NULL,
    [maes_raza]      CHAR (2)      NULL,
    [Maes_Sanitario] CHAR (14)     NULL,
    [maes_FRegistro] SMALLDATETIME NULL,
    [USUARIO]        VARCHAR (50)  NULL,
    [maes_ESPH]      CHAR (14)     NULL
);

