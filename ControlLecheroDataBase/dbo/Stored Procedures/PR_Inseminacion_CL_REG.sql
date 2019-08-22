CREATE PROCEDURE PR_Inseminacion_CL_REG
(@datDESDE smalldatetime,@datHASTA smalldatetime,@ESTABLO char(7))
AS
--PRIMERO TENGO QUE RENUMERAR INSEMINACIIONES

--DESPUES TENGO QUE EJECUTAR ESTE PROCEDIMIENTO

--DESPUES EJECUTO VINCULAR_INSEMINACION_PARTO (mirar la última linea para quitar comentarios del procedimiento)
	--con este procedimiento sólo se si hay un parto de esa vaca. Si es un ternero o una vaca sin registrar por ser mala de indice, puede
	-- ser que no esté en Registro:animales. 
	--Por eso primero vinculo con Registro, para ver si está ahí y asignar el toro correcto si no coincide.
	
DECLARE @ID CHAR(14),@NINS INT,@WID CHAR(14),@CONTADOR INT,@WNINS INT,@PADRE CHAR(14),@FINS SMALLDATETIME,@NP INT,@FPARTO SMALLDATETIME
DECLARE  @PARTOSID CHAR(14),@PARTOSFP SMALLDATETIME
/*HALLO LA ULTIMA INSEMINACION*/
IF @ESTABLO<>'*'
BEGIN
	DECLARE C1 cursor LOCAL SCROLL OPTIMISTIC FOR
	SELECT MAX(ID),MAX(NP),MAX(FINSEMINACION)
	FROM INSEMINACIONES
	WHERE ESTABLO=@ESTABLO
	GROUP BY ID,NP
END
ELSE
BEGIN
	DECLARE C1 CURSOR LOCAL SCROLL OPTIMISTIC FOR
	SELECT MAX(ID),MAX(NP),MAX(FINSEMINACION)
	FROM INSEMINACIONES
	--WHERE FINSEMINACION BETWEEN @datDESDE AND @datHASTA
	WHERE FINSEMINACION > @datDESDE -- 
	GROUP BY ID,NP
	
	/*PRINT CONVERT(CHAR,@@FETCH_STATUS) + ' '+@ID*/
END
-- Tengo que ver que hay un parto en registro de esa vaca entre 7 y 10 meses en RegGen..animales
-- Si existe -> cojo el Padre y lo llevo a la tabla de Inseminaciones (Semental) . Si no coincide lo reflejo en la tabla de Incidencias
--Si NO existe voy a la tabla de ControlLechero..Partos (veo si el parto está entre 7 y 10 meses desde la FInseminación).
--Finalmente: Si hace más de12 meses desde la Fins hasta Hoy y no hay parto en CL ni en REG  -> pongo NP=99 para diferenciar de los que están en curso
OPEN C1 
FETCH NEXT FROM C1 INTO @ID,@NP,@FINS
WHILE (@@FETCH_STATUS=0)
BEGIN
	DECLARE C2 cursor LOCAL SCROLL OPTIMISTIC FOR
	SELECT  NUPAD1 FROM REGGEN..ANIMALES
	WHERE NUMAD1=@ID
	AND DATEDIFF(MONTH,@FINS,cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4))) BETWEEN 6 AND 10
	OPEN C2
	FETCH NEXT FROM C2 INTO @PADRE
	
	IF @@FETCH_STATUS=0  --.....VOY A REGISTRO ....Cojo el Padre y lo llevo a la tabla de Inseminaciones (Semental) .SI NO COINCIDE CON EL SEMENTAL ASIGNADO POR EL CONTROLADOR
		BEGIN
			UPDATE INSEMINACIONES
			SET SEMENTAL=@PADRE,
			ORIGENTORO='RG',
			SEMENTALERRONEO=SEMENTAL
			WHERE ID=@ID
			AND NP=@NP
			AND FINSEMINACION=@FINS
			AND SEMENTAL<>@PADRE
		END
	ELSE			--.... VOY A CL......Marco La inseminación como buena temporal hasta que se registra.
		BEGIN
			DECLARE C3 CURSOR LOCAL SCROLL OPTIMISTIC FOR
			SELECT  ID,FPARTO FROM PARTOS
			WHERE ID=@ID
			AND DATEDIFF(MONTH,@FINS,FPARTO)  BETWEEN 7 AND 10
			OPEN C3
			FETCH NEXT FROM C3 INTO @PARTOSID,@PARTOSFP
			IF @@FETCH_STATUS=0
				BEGIN
					UPDATE INSEMINACIONES
					SET ORIGENTORO='CL'
					WHERE ID=@ID
					AND NP=@NP
					AND FINSEMINACION=@FINS
				END
			ELSE
				BEGIN
					UPDATE INSEMINACIONES
					SET NP=99 -- ESTO ES QUE NO SE HA ENCONTRADO NINGUN PARTO.
					WHERE DATEDIFF(MONTH,FINSEMINACION,GETDATE())  >14
					AND NP=0
					AND ORIGENTORO IS NULL
					
				END
			CLOSE C3
			DEALLOCATE C3
		END
	CLOSE C2
	DEALLOCATE C2
	FETCH NEXT FROM C1 INTO @ID,@NP,@FINS
END
CLOSE C1
DEALLOCATE C1

/*
	IF @@FETCH_STATUS<>0  OR @FINS>@FPARTONO EXISTE PARTO PARA ESE NP
		BEGIN
			UPDATE INSEMINACIONES
			SET NP=0
	 		WHERE CURRENT OF C1
		END
	*/

--EXEC PR_VINCULOINSEMINACIONPARTO @datDESDE,@datHASTA