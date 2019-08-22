CREATE TABLE [dbo].[BuzonLaboratorio] (
    [ID]          CHAR (14) NULL,
    [ESTABLO]     CHAR (7)  NULL,
    [GRASA]       REAL      NULL,
    [PROTEINA]    REAL      NULL,
    [LACTOSA]     REAL      NULL,
    [RCS]         REAL      NULL,
    [FPREVISTA]   CHAR (10) NULL,
    [FCONTROL]    CHAR (10) NULL,
    [FENTREGA]    CHAR (10) NULL,
    [FANALISIS]   CHAR (10) NULL,
    [FICHERO]     CHAR (20) NULL,
    [Controlador] CHAR (3)  NULL,
    [Agalactiae]  CHAR (6)  NULL,
    [Aureaus]     CHAR (6)  NULL,
    [Coag]        CHAR (6)  NULL,
    [NoAgal]      CHAR (6)  NULL,
    [Coliformes]  CHAR (6)  NULL,
    [Enterococos] CHAR (6)  NULL,
    [TANQUE]      CHAR (10) NULL,
    [Acetona]     REAL      NULL,
    [bhb]         REAL      NULL,
    [Urea]        REAL      NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_BuzonLaboratorio]
    ON [dbo].[BuzonLaboratorio]([ID] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_BuzonLaboratorio_1]
    ON [dbo].[BuzonLaboratorio]([ESTABLO] ASC) WITH (FILLFACTOR = 90);

