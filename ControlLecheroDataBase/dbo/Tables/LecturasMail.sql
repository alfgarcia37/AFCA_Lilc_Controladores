CREATE TABLE [dbo].[LecturasMail] (
    [ID]             BIGINT        IDENTITY (1, 1) NOT NULL,
    [ESTABLO]        CHAR (7)      NULL,
    [MAIL]           VARCHAR (100) NULL,
    [OBSERVACIONES]  CHAR (300)    NULL,
    [ACTIVO]         CHAR (1)      NULL,
    [BAJAPUBLICIDAD] CHAR (1)      CONSTRAINT [DF_LecturasMail_BAJAPUBLICIDAD] DEFAULT ((0)) NULL
);

