CREATE PROCEDURE PR_GETNUMTERNERAS
(@datDesde smalldatetime,@datHasta smalldatetime,@Controlador smallint,@ESTABLO CHAR(7))
 AS
DECLARE @MES1 AS CHAR(2),@MES2 AS CHAR(2),@TOTAL AS INT

DECLARE C1 CURSOR FOR
SELECT PARAM1 FROM parametrosgenerales WHERE ID='MES12'
OPEN  C1 
FETCH NEXT FROM C1 INTO @MES1
CLOSE C1
DEALLOCATE C1

DECLARE C1 CURSOR FOR
SELECT PARAM2 FROM parametrosgenerales WHERE ID='MES12'
OPEN  C1 
FETCH NEXT FROM C1 INTO @MES2
CLOSE C1
DEALLOCATE C1

  IF RTRIM(LTRIM(@ESTABLO)) <> '*'
	BEGIN
		--DECLARE C1 CURSOR FOR
		SELECT COUNT(*)
		FROM RegGen..ANIMALES,lecturasfinalizadas
	 	WHERE GANPRO=establo
		AND GANPRO=@ESTABLO
		AND fprevista between  @datDesde and @datHasta 
		AND numcontroler=@Controlador
	 	AND NUMLAC=0 AND AÑOBAJ=0
	 	AND Sexo='H'
		AND DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)),   @datDesde)>=CAST(@Mes1 AS INT)
		AND DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), @datHasta)<=CAST(@Mes2 AS INT)
		AND codigoidentif not in (select distinct res_gen from control
		where  RES_TIT=@ESTABLO AND res_fprev between   @datDesde and @datHasta and controlador=@Controlador)
		--OPEN  C1 
		--FETCH NEXT FROM C1 INTO @TOTAL
	END
	ELSE
	BEGIN
		--DECLARE C1 CURSOR FOR
		SELECT COUNT(*)
		FROM RegGen..ANIMALES,lecturasfinalizadas
	 	WHERE GANPRO=establo
		AND fprevista between  @datDesde and @datHasta and numcontroler=@Controlador
	 	AND NUMLAC=0 and AÑOBAJ=0
	 	AND Sexo='H'
		AND DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)),   @datDesde)>=CAST(@Mes1 AS INT)
		AND DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), @datHasta)<=CAST(@Mes2 AS INT)
		AND codigoidentif not in (select distinct res_gen from control
		where res_fprev between   @datDesde and @datHasta and controlador=@Controlador)
		--OPEN  C1 
		--FETCH NEXT FROM C1 INTO @TOTAL
	END