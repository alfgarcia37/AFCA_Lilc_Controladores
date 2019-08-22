CREATE VIEW dbo.VistaPartosControlTXT
AS
selecT  MAX(TIT) AS TIT,max(gen) as gen,max(np) as NP ,max(fp) as FP
from controltxt where fp is not null
group by TIT,GEN
