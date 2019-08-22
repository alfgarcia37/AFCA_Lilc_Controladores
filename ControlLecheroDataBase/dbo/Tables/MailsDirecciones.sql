CREATE TABLE [dbo].[MailsDirecciones] (
    [ID]       SMALLINT      IDENTITY (1, 1) NOT NULL,
    [TIPO]     CHAR (3)      NULL,
    [EMAIL]    VARCHAR (50)  NULL,
    [CONTACTO] VARCHAR (100) NULL,
    [NOTAS]    VARCHAR (500) NULL,
    [ESTABLO]  CHAR (7)      NULL,
    [ACTIVO]   CHAR (1)      NULL,
    [EMPRESA]  VARCHAR (50)  NULL
);

