CREATE TABLE [dbo].[TipoBIC] (
    [idTipo]       INT           IDENTITY (1, 1) NOT NULL,
    [bic]          VARCHAR (100) NOT NULL,
    [denominacion] VARCHAR (250) NOT NULL,
    [codigo]       VARCHAR (10)  NOT NULL,
    CONSTRAINT [PK_tipoBIC] PRIMARY KEY CLUSTERED ([idTipo] ASC)
);

