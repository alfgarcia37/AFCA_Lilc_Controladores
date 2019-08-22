

CREATE PROCEDURE PR_ACTU_MAESTRO_CENSO
AS
declare @mgen char(14),@mtit char (7),@Fcontrol smalldatetime

DECLARE C1 cursor  FOR
SELECT Maes_gen,Maes_tit
  FROM Maestro  WHERE  maes_fregistro IS NULL
  --and maes_gen='ES000603892086'
  order by maes_gen
  --AND MAES_ESTADO='0'
  --and maes_fp>'01/01/1900'
OPEN C1
FETCH NEXT FROM C1 INTO @mgen,@mtit
WHILE (@@FETCH_STATUS=0)
BEGIN
	DECLARE C2 cursor  FOR
	SELECT min(res_fprev) FROM control  WHERE  res_gen=@mgen and res_tit=@mtit
	OPEN C2
	FETCH NEXT FROM C2 INTO @Fcontrol
	close c2 
	deallocate c2
	update maestro
	set maes_fregistro=@Fcontrol,
	maes_fae=getdate()
	where maes_fregistro is null 
	and maes_tit=@mtit
	and maes_gen=@mgen
	FETCH NEXT FROM C1 INTO @mgen,@mtit
end
close c1 
deallocate c1
--update maestro
--set maes_fregistro='01/01/1990',
--maes_fae='01/01/1990'
--where maes_fregistro is null 
