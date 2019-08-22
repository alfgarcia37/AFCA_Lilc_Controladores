CREATE TABLE [dbo].[hijasaberekin] (
    [PAIS]           NVARCHAR (3)   NOT NULL,
    [SEXO]           NVARCHAR (1)   NOT NULL,
    [PROANI]         NVARCHAR (2)   NOT NULL,
    [NUMERO]         NVARCHAR (8)   NOT NULL,
    [PUREZA]         NVARCHAR (1)   NULL,
    [NOMBRE]         NVARCHAR (30)  NULL,
    [AÑONA1]         INT            NULL,
    [MESNA1]         INT            NULL,
    [DIANA1]         INT            NULL,
    [NUPAD1]         NVARCHAR (14)  NULL,
    [NUMAD1]         NVARCHAR (14)  NULL,
    [GANCRI]         NVARCHAR (7)   NULL,
    [GANPRO]         NVARCHAR (7)   NULL,
    [CODIGOIDENTIF]  NVARCHAR (14)  NULL,
    [Expr1]          NVARCHAR (255) NULL,
    [Expr2]          SMALLDATETIME  NULL,
    [SANITARIOMADRE] CHAR (14)      NULL,
    CONSTRAINT [PK_hijasaberekin] PRIMARY KEY CLUSTERED ([PAIS] ASC, [SEXO] ASC, [PROANI] ASC, [NUMERO] ASC) WITH (FILLFACTOR = 90)
);


GO
CREATE NONCLUSTERED INDEX [IX_hijasaberekin]
    ON [dbo].[hijasaberekin]([NUMAD1] ASC) WITH (FILLFACTOR = 90);

