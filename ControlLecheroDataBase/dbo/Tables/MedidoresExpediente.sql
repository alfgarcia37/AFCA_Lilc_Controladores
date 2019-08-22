CREATE TABLE [dbo].[MedidoresExpediente] (
    [expediente]      VARCHAR (10)  NULL,
    [fechaExpediente] SMALLDATETIME NULL,
    [Controlador]     SMALLINT      NULL,
    [fechaFin]        SMALLDATETIME NULL,
    [lugar]           VARCHAR (200) NULL,
    [observaciones]   VARCHAR (200) NULL,
    [Tecnico]         VARCHAR (50)  NULL
);

