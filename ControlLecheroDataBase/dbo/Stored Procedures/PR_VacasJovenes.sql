-- Batch submitted through debugger: SQLQuery2.sql|7|0|C:\Documents and Settings\alfongar\Configuración local\Temp\2\~vs15.sql
CREATE PROCEDURE [dbo].[PR_VacasJovenes] 
(
		@strEstablo char(14),
		@intNUMERO int,
		@MES1 int,
		@MES2 int,
		@USUARIO VARCHAR(50)
	)
AS
DELETE FROM VACASJOVENES
WHERE USUARIO=@USUARIO

insert into VacasJovenes 
SELECT ID = CASE WHEN NUMERO > @intNUMERO  THEN CODIGOIdentif 
ELSE (PAIS+SEXO+PROANI+NUMERO) END,
NOMBRE as det,
AÑONA1,MESNA1,DIANA1,'0000',@USUARIO
FROM RegGen..ANIMALES
 Where GANPRO = @strEstablo
and NUMLAC=0 and AÑOBAJ=0
and Sexo='H'
and DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())>=@MES1
And DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())<=@MES2
and (CASE WHEN NUMERO >01868130    THEN CODIGOIdentif 
ELSE (PAIS+SEXO+PROANI+NUMERO) END) not in(select Maes_Gen from maestro where Maes_tit=@StrEstablo)

UPDATE VACASJOVENES
SET COLLAR=right(NUMESTABLO,4)
FROM VACASJOVENES,REGGEN..VACAS
WHERE
USUARIO=@USUARIO
AND (ID=NUMCRIA OR ID=CODIGOIDENTIF) AND NUMESTABLO IS NOT NULL