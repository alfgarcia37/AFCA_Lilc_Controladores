CREATE VIEW VbAJAS(gen,np,NC,fprev) AS SELECT max(res_gen),max(res_np),
max(res_nc)-1,max(res_fprev)
from control where  res_fprev is null
group by res_gen,res_np,res_nc,res_fprev
