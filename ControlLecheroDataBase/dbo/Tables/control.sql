CREATE TABLE [dbo].[control] (
    [RES_ORD]            CHAR (14)     NULL,
    [RES_REG]            CHAR (2)      NULL,
    [RES_GEN]            CHAR (14)     NULL,
    [RES_NOM]            VARCHAR (30)  NULL,
    [RES_FN]             SMALLDATETIME NULL,
    [RES_COL]            CHAR (4)      NULL,
    [RES_FP]             SMALLDATETIME NULL,
    [RES_NP]             INT           NULL,
    [RES_NC]             INT           NULL,
    [RES_PAD]            CHAR (14)     NULL,
    [RES_MAD]            CHAR (14)     NULL,
    [RES_FAE]            SMALLDATETIME NULL,
    [RES_AB]             CHAR (1)      NULL,
    [RES_FBA]            SMALLDATETIME NULL,
    [RES_CBA]            CHAR (2)      NULL,
    [RES_KLAM]           REAL          CONSTRAINT [DF_control_RES_KLAM] DEFAULT (0) NULL,
    [RES_KLPM]           REAL          CONSTRAINT [DF_control_RES_KLPM] DEFAULT (0) NULL,
    [RES_KGR]            REAL          CONSTRAINT [DF_control_RES_KGR] DEFAULT (0) NULL,
    [RES_TGR]            REAL          CONSTRAINT [DF_control_RES_TGR] DEFAULT (0) NULL,
    [RES_TPR]            REAL          CONSTRAINT [DF_control_RES_TPR] DEFAULT (0) NULL,
    [RES_TLA]            REAL          CONSTRAINT [DF_control_RES_TLA] DEFAULT (0) NULL,
    [RES_TRC]            REAL          CONSTRAINT [DF_control_RES_TRC] DEFAULT (0) NULL,
    [RES_TIT]            CHAR (7)      NULL,
    [RES_FIN]            SMALLDATETIME NULL,
    [RES_NIN]            INT           NULL,
    [RES_SEM]            CHAR (14)     NULL,
    [RES_TIN]            CHAR (1)      NULL,
    [RES_FCTR]           SMALLDATETIME NULL,
    [RES_HCTR]           CHAR (8)      NULL,
    [RES_VELORD]         CHAR (1)      NULL,
    [RES_INT]            INT           NULL,
    [RES_XAB]            CHAR (1)      NULL,
    [RES_SW0]            CHAR (1)      NULL,
    [RES_FPREV]          SMALLDATETIME NULL,
    [CONTROLADOR]        INT           NULL,
    [LoginID]            VARCHAR (20)  NULL,
    [FechaID]            BINARY (8)    NULL,
    [Res_INC]            CHAR (2)      NULL,
    [Res_INCrecal]       CHAR (2)      NULL,
    [RES_FACP]           CHAR (2)      NULL,
    [usuario]            CHAR (15)     NULL,
    [fusuario]           SMALLDATETIME NULL,
    [RES_TAM]            CHAR (2)      NULL,
    [RES_sexo]           CHAR (1)      NULL,
    [RES_CONFORMACION]   CHAR (1)      NULL,
    [RES_PLACENTA]       CHAR (1)      NULL,
    [nucleo]             CHAR (1)      NULL,
    [RES_FCTRM]          DATETIME      NULL,
    [RES_FCTRT]          DATETIME      NULL,
    [RES_AUD_KILOS_AFCA] REAL          CONSTRAINT [DF_control_RES_AUD_KILOS_AFCA] DEFAULT ((0)) NULL,
    [RES_FACTU]          SMALLDATETIME NULL,
    [Res_Acetona]        REAL          CONSTRAINT [DF_control_Res_Acetona] DEFAULT ((0)) NULL,
    [Res_BHB]            REAL          CONSTRAINT [DF_control_Res_BHB] DEFAULT ((0)) NULL,
    [Res_Temperamento]   CHAR (1)      NULL,
    [res_urea]           REAL          DEFAULT ((0)) NOT NULL
);


GO
CREATE CLUSTERED INDEX [IX_control_1]
    ON [dbo].[control]([RES_TIT] ASC, [RES_FPREV] ASC) WITH (FILLFACTOR = 90);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_control]
    ON [dbo].[control]([RES_TIT] ASC, [RES_GEN] ASC, [RES_NP] ASC, [RES_NC] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_control_2]
    ON [dbo].[control]([RES_GEN] ASC, [RES_NP] ASC, [RES_NC] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_control_3]
    ON [dbo].[control]([RES_FPREV] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [I_ord]
    ON [dbo].[control]([RES_ORD] ASC, [RES_FPREV] ASC);

