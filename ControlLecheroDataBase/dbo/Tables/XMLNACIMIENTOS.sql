CREATE TABLE [dbo].[XMLNACIMIENTOS] (
    [CODIGO]             VARCHAR (14) NOT NULL,
    [ESTABLO]            VARCHAR (50) NULL,
    [MADRE]              VARCHAR (14) NULL,
    [PADRE]              VARCHAR (14) NULL,
    [FECHA_NACIMIENTO]   DATETIME     NULL,
    [COLLAR]             INT          NULL,
    [SEXO]               CHAR (10)    NULL,
    [NOMBRE]             VARCHAR (50) NULL,
    [PARTO_GEMELAR]      CHAR (1)     NULL,
    [SEXO_HERMANO]       CHAR (1)     NULL,
    [CAPA]               CHAR (2)     NULL,
    [ET]                 CHAR (1)     NULL,
    [TIPO]               INT          NULL,
    [FECHA_INSEMINACION] DATETIME     NULL,
    [FECHA_DECLARACION]  DATETIME     NULL,
    [SILUETAS]           IMAGE        NULL,
    [FIRMA]              IMAGE        NULL,
    [portadora]          CHAR (14)    NULL,
    [controlador]        CHAR (3)     NULL
);

