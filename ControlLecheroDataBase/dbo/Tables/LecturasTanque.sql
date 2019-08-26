CREATE TABLE [dbo].[LecturasTanque] (
    [ID]          CHAR (7)      NULL,
    [DET]         SMALLDATETIME NULL,
    [Agalactiae]  CHAR (6)      NULL,
    [Aureaus]     CHAR (6)      NULL,
    [Coag]        CHAR (6)      NULL,
    [NoAgal]      CHAR (6)      NULL,
    [Coliformes]  CHAR (6)      NULL,
    [Enterococos] CHAR (6)      NULL,
    [TANQUE]      CHAR (12)     NULL,
    [FCarga]      SMALLDATETIME NULL,
    [FAnalisis]   SMALLDATETIME NULL,
    [MuestraMal]  CHAR (1)      NULL,
    [SubidoArsys] CHAR (1)      CONSTRAINT [DF_LecturasTanque_SubidoArsys] DEFAULT ('0') NULL
);

