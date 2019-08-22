CREATE TABLE [dbo].[Celos] (
    [VACA]          CHAR (14)     NULL,
    [fechacelo]     SMALLDATETIME NULL,
    [establo]       CHAR (7)      NULL,
    [Controlador]   INT           NULL,
    [quiste]        CHAR (1)      NULL,
    [ID]            INT           IDENTITY (1, 1) NOT NULL,
    [FRegistroCelo] SMALLDATETIME NULL
);

