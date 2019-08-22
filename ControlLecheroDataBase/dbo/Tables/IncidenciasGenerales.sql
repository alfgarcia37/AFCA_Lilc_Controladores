CREATE TABLE [dbo].[IncidenciasGenerales] (
    [ID]          INT           IDENTITY (1, 1) NOT NULL,
    [DET]         VARCHAR (200) NULL,
    [TIPO]        CHAR (3)      NULL,
    [FECHAINCID]  SMALLDATETIME NULL,
    [CONTROLADOR] CHAR (8)      NULL,
    [ESTABLO]     CHAR (7)      NULL,
    [RES]         CHAR (14)     NULL,
    [USUARIO]     CHAR (30)     NULL,
    [FECHA1]      SMALLDATETIME NULL
);

