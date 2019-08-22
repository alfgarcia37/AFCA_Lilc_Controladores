CREATE TABLE [dbo].[VacasNoRIIA] (
    [vaca]          CHAR (14)     NOT NULL,
    [establo]       CHAR (7)      NOT NULL,
    [FechaRetirada] SMALLDATETIME NULL,
    [Notas]         VARCHAR (200) NULL,
    [nombre]        CHAR (30)     NULL,
    [Controlador]   INT           NULL,
    [FN]            SMALLDATETIME NULL,
    [FaltaRIIA]     SMALLDATETIME NULL,
    [FBajaRIIA]     SMALLDATETIME NULL,
    [meses]         INT           NULL,
    [FCalculo]      SMALLDATETIME NULL,
    CONSTRAINT [PK_VacasNoRIIA] PRIMARY KEY CLUSTERED ([vaca] ASC, [establo] ASC)
);

