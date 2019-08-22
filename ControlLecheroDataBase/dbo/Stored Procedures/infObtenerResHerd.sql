





CREATE PROCEDURE infObtenerResHerd
(
		@Establo char(7),
		@FPrevista smalldatetime
)
 AS
DECLARE @NUMCENSO AS INT,@MVTOSCOMPRA AS INT,@MVTOSVENTA AS INT


IF @Establo<>''
	BEGIN
		declare C1 cursor for 
		SELECT count(*) AS NumeroVacas FROM reggen..animales
		WHERE ganpro=@Establo
	END
ELSE
	BEGIN
		declare C1 cursor for 
		SELECT count(*) AS NumeroVacas FROM reggen..animales
		WHERE LEFT(GANPRO,1)='J'
	END

OPEN  C1 
FETCH NEXT FROM C1 INTO @NUMCENSO

IF @Establo<>''
	BEGIN
		declare C2 cursor for 
		SELECT count(*) AS NumeroVacas FROM reggen..TRANSFERENCIAS
		WHERE CODCOM=@Establo
		AND AÑOTR>2003
		AND (CASE WHEN MESTR=0    THEN NULL 
		ELSE  (cast(DIATR as char(2)) + '/' + cast(MESTR as char(2)) + '/'+ cast(AÑOTR as char(4))) END) >@FPrevista
	END
ELSE
	BEGIN
		declare C2 cursor for 
		SELECT count(*) AS NumeroVacas FROM reggen..TRANSFERENCIAS
		WHERE LEFT(CODCOM,1)='J'
		AND AÑOTR>2003
		AND (CASE WHEN MESTR=0    THEN NULL 
		ELSE  (cast(DIATR as char(2)) + '/' + cast(MESTR as char(2)) + '/'+ cast(AÑOTR as char(4))) END) >@FPrevista
	END

OPEN  C2 
FETCH NEXT FROM C2 INTO @MVTOSCOMPRA

IF @Establo<>''
	BEGIN
		declare C3 cursor for 
		SELECT count(*) AS NumeroVacas FROM reggen..TRANSFERENCIAS
		WHERE CODVEN=@Establo
		AND AÑOTR>2003
		AND (CASE WHEN MESTR=0    THEN NULL 
		ELSE  (cast(DIATR as char(2)) + '/' + cast(MESTR as char(2)) + '/'+ cast(AÑOTR as char(4))) END) >@FPrevista
	END
ELSE
	BEGIN
		declare C3 cursor for 
		SELECT count(*) AS NumeroVacas FROM reggen..TRANSFERENCIAS
		WHERE LEFT(CODVEN,1)='J'
		AND AÑOTR>2003
		AND (CASE WHEN MESTR=0    THEN NULL 
		ELSE  (cast(DIATR as char(2)) + '/' + cast(MESTR as char(2)) + '/'+ cast(AÑOTR as char(4))) END) >@FPrevista
	END

OPEN  C3 
FETCH NEXT FROM C3 INTO @MVTOSVENTA


CLOSE C1
DEALLOCATE C1

CLOSE C2
DEALLOCATE C2

CLOSE C3
DEALLOCATE C3

 select (@NUMCENSO-@MVTOSCOMPRA+@MVTOSVENTA) as numerovacas





