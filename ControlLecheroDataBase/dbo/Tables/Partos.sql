CREATE TABLE [dbo].[Partos] (
    [ID]               CHAR (14)     NULL,
    [DET]              CHAR (50)     NULL,
    [NP]               INT           NULL,
    [DIFPARTO]         CHAR (2)      NULL,
    [RETPLACENTA]      CHAR (2)      NULL,
    [CONFORMACIONCRIA] CHAR (2)      NULL,
    [VELORD]           CHAR (2)      NULL,
    [FPARTO]           SMALLDATETIME NULL,
    [NUMCONTROLADOR]   INT           NULL,
    [ESTABLO]          CHAR (7)      NULL,
    [TAMANOCRIA]       CHAR (2)      NULL,
    [FINS]             SMALLDATETIME NULL,
    [SEXOTERNERO]      CHAR (2)      NULL,
    [TORO]             CHAR (14)     NULL,
    [NINS]             INT           NULL,
    [FBAJA]            SMALLDATETIME NULL,
    [CBAJA]            CHAR (2)      NULL,
    [FBAJAANTERIOR]    SMALLDATETIME NULL,
    [VACA_GEN]         CHAR (14)     NULL,
    [importada]        CHAR (1)      NULL
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Partos]
    ON [dbo].[Partos]([ID] ASC, [NP] ASC) WITH (FILLFACTOR = 90, IGNORE_DUP_KEY = ON);


GO
CREATE NONCLUSTERED INDEX [IX_Partos_1]
    ON [dbo].[Partos]([FPARTO] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_Partos_2]
    ON [dbo].[Partos]([FPARTO] ASC) WITH (FILLFACTOR = 90);

