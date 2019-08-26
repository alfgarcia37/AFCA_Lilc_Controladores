CREATE VIEW dbo.VistaPartosControlNoTXT
AS
selecT  MAX(res_TIT) AS TIT,max(res_gen) as gen,max(res_np) as NP ,max(res_fp) as FP
from control 
where res_fp is not null
AND NUCLEO='F'
group by res_TIT,res_GEN
