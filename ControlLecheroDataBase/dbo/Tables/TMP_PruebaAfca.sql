CREATE TABLE [dbo].[TMP_PruebaAfca] (
    [Establo]         CHAR (7)      NULL,
    [Vaca]            CHAR (14)     NULL,
    [Nombre]          CHAR (30)     NULL,
    [FN]              SMALLDATETIME NULL,
    [usuario]         VARCHAR (50)  NULL,
    [FCalculo]        SMALLDATETIME NULL,
    [FaltaAFCA]       SMALLDATETIME NULL,
    [FBajaAFCA]       SMALLDATETIME NULL,
    [MOTIVO]          CHAR (7)      NULL,
    [FBajaRIIA]       SMALLDATETIME NULL,
    [FBajaMuerteRIIA] SMALLDATETIME NULL,
    [VACARIIA]        CHAR (14)     NULL,
    [MesesCalculo]    INT           NULL,
    [controlador]     VARCHAR (3)   NULL,
    [raza]            CHAR (4)      NULL,
    [CEA]             CHAR (14)     NULL
);

