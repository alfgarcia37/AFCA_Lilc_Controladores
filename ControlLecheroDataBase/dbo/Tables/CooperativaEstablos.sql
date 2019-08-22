CREATE TABLE [dbo].[CooperativaEstablos] (
    [ESTABLO] CHAR (7)  NULL,
    [IDCOOP]  CHAR (10) NULL,
    [existe]  CHAR (1)  NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_CooperativaEstablos]
    ON [dbo].[CooperativaEstablos]([IDCOOP] ASC) WITH (FILLFACTOR = 90);


GO
CREATE NONCLUSTERED INDEX [IX_CooperativaEstablos_1]
    ON [dbo].[CooperativaEstablos]([ESTABLO] ASC) WITH (FILLFACTOR = 90);

