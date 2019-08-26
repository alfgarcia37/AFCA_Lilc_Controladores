CREATE PROCEDURE [PR_ACTU_COLLAR] AS
update maestro set maes_col=RIGHT(LTRIM(RTRIM(numestablo)),4)
 FROM RegGen..Vacas
 Where (Numcria=maes_gen OR CodigoIdentif = maes_gen)
 and numestablo is not null
and LTRIM(RTRIM(numestablo))<>''
and numestablo<>'00000'
 and CodigoGanaderia=maes_tit
-- and maes_tit='J390024'
and maes_gen=codigoidentif

and (maes_col is null or maes_col='' or maes_col='0000')