CREATE TABLE [dbo].[MedidoresCalibraciones] (
    [ID]            CHAR (6)      NOT NULL,
    [FCalibracion]  SMALLDATETIME NOT NULL,
    [observaciones] VARCHAR (200) NULL,
    [KGAGUA1]       REAL          CONSTRAINT [DF_MedidoresCalibraciones_KGAGUA1] DEFAULT ((0)) NULL,
    [Lectura1]      REAL          CONSTRAINT [DF_MedidoresCalibraciones_Lectura1] DEFAULT ((0)) NULL,
    [KGAGUA2]       REAL          CONSTRAINT [DF_MedidoresCalibraciones_KGAGUA2] DEFAULT ((0)) NULL,
    [Lectura2]      REAL          CONSTRAINT [DF_MedidoresCalibraciones_Lectura2] DEFAULT ((0)) NULL,
    [KGAGUA3]       REAL          CONSTRAINT [DF_MedidoresCalibraciones_KGAGUA3] DEFAULT ((0)) NULL,
    [Lectura3]      REAL          CONSTRAINT [DF_MedidoresCalibraciones_Lectura3] DEFAULT ((0)) NULL,
    [expediente]    VARCHAR (10)  NULL,
    CONSTRAINT [PK_MedidoresCalibraciones] PRIMARY KEY CLUSTERED ([ID] ASC, [FCalibracion] ASC) WITH (FILLFACTOR = 90)
);

