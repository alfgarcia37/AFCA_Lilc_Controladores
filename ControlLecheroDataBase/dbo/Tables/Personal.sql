CREATE TABLE [dbo].[Personal] (
    [ID]             CHAR (5)      NULL,
    [DET]            CHAR (75)     NULL,
    [Empresa]        CHAR (5)      NULL,
    [FNacto]         SMALLDATETIME NULL,
    [FIncorporacion] SMALLDATETIME NULL,
    [FBaja]          SMALLDATETIME NULL,
    [DNI]            CHAR (11)     NULL,
    [Sueldo]         REAL          NULL,
    [Cargo]          CHAR (5)      NULL,
    [Observaciones]  VARCHAR (250) NULL,
    [Telefono]       CHAR (11)     NULL,
    [Localidad]      CHAR (30)     NULL,
    [Provincia]      CHAR (10)     NULL,
    [Direccion]      CHAR (10)     NULL,
    [LOGIN]          CHAR (20)     NULL,
    [JORNADA]        CHAR (10)     NULL
);

