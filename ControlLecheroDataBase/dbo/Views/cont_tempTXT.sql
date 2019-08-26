CREATE VIEW cont_tempTXT(gen,np,fp) AS SELECT max(gen),max(np),
max(fp)
from controltxt where  fp is not null
group by gen,np,fp


