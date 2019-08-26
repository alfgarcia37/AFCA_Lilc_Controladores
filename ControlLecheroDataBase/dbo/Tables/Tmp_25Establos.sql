CREATE TABLE [dbo].[Tmp_25Establos] (
    [establo]        CHAR (7)      NOT NULL,
    [FPrevista]      SMALLDATETIME NULL,
    [Fcontrol_am]    DATE          NULL,
    [Fcontrol_pm]    DATE          NULL,
    [Fcontrol]       DATE          NULL,
    [FDescarga]      SMALLDATETIME NULL,
    [FEntrega]       SMALLDATETIME NULL,
    [FCorregido]     SMALLDATETIME NULL,
    [FAnalisis]      SMALLDATETIME NULL,
    [FInforme]       SMALLDATETIME NULL,
    [Numcontroler]   INT           NULL,
    [Motivo]         CHAR (2)      NULL,
    [Fichero]        VARCHAR (20)  NULL,
    [ControlInforme] INT           NULL
);

