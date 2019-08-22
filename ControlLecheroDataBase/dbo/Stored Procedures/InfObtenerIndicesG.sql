





CREATE PROCEDURE [dbo].[InfObtenerIndicesG] 

(
	@Establo char(7),
	@Clase char(1)
		)
AS
IF @Establo<>''
BEGIN
	IF @clase='1'
	BEGIN
		select m.maes_np,ig.KLEVG,IG.PGRAVG,IG.PPROVG,IG.IndPatasPies,IG.IndUbre,IG.IndGlobalT,IG.IcoT,IG.ICO
		from Maestro M,reggen..animales A,reggen..indicesG IG
		where Maes_tit=@Establo AND MAES_ESTADO<>'0' and a.ganpro=@Establo
		and 
		(Maes_gen=codigoidentif or Maes_gen=a.Pais+a.sexo+a.proani+a.numero)
		and
		a.Pais+a.sexo+a.proani+a.numero=ig.Pais+ig.sexo+ig.proani+ig.numero
		AND 
		(ISNUMERIC(INDGLOBALT)=1  AND ISNUMERIC(ICO)=1 AND KLEVG IS NOT NULL AND  PGRAVG IS NOT NULL AND  PPROVG IS NOT NULL
		 AND  REPTIP IS NOT NULL AND  ICOT IS NOT NULL)
	END
	ELSE
	BEGIN
		select m.maes_np,ig.KLEVG,IG.PGRAVG,IG.PPROVG,IG.IndPatasPies,IG.IndUbre,IG.IndGlobalT,IG.IcoT,IG.ICO
		from Maestro M,reggen..animales A,reggen..indicesG IG
		where Maes_tit=@Establo AND MAES_ESTADO<>'0' AND GANPRO=@Establo
		and 
		(Maes_gen=codigoidentif or Maes_gen=a.Pais+a.sexo+a.proani+a.numero)
		AND
		(Maes_PADRE=NUPAD1)
		and
		NUPAD1=ig.Pais+ig.sexo+ig.proani+ig.numero
		AND 
		(ISNUMERIC(INDGLOBALT)=1 AND ISNUMERIC(ICO)=1 AND KLEVG IS NOT NULL AND  PGRAVG IS NOT NULL AND  PPROVG IS NOT NULL
		 AND  REPTIP IS NOT NULL AND  ICOT IS NOT NULL)
	END
END
ELSE
BEGIN
	IF @clase='1'
	BEGIN
		select m.maes_np,ig.KLEVG,IG.PGRAVG,IG.PPROVG,IG.IndPatasPies,IG.IndUbre,IG.IndGlobalT,IG.IcoT,IG.ICO
		from Maestro M,reggen..animales A,reggen..indicesG IG
		where MAES_ESTADO<>'0' 
		and 
		(Maes_gen=codigoidentif or Maes_gen=a.Pais+a.sexo+a.proani+a.numero)
		and
		a.Pais+a.sexo+a.proani+a.numero=ig.Pais+ig.sexo+ig.proani+ig.numero
		AND 
		(ISNUMERIC(INDGLOBALT)=1 AND ISNUMERIC(ICO)=1 AND KLEVG IS NOT NULL AND  PGRAVG IS NOT NULL AND  PPROVG IS NOT NULL
		 AND  REPTIP IS NOT NULL AND  ICOT IS NOT NULL)
	END
	ELSE
	BEGIN
		select m.maes_np,ig.KLEVG,IG.PGRAVG,IG.PPROVG,IG.IndPatasPies,IG.IndUbre,IG.IndGlobalT,IG.IcoT,IG.ICO
		from Maestro M,reggen..animales A,reggen..indicesG IG
		where  MAES_ESTADO<>'0' 
		and 
		(Maes_gen=codigoidentif or Maes_gen=a.Pais+a.sexo+a.proani+a.numero)
		AND
		(Maes_PADRE=NUPAD1)
		and
		NUPAD1=ig.Pais+ig.sexo+ig.proani+ig.numero
		AND 
		(ISNUMERIC(INDGLOBALT)=1 AND  ISNUMERIC(ICO)=1 AND KLEVG IS NOT NULL AND  PGRAVG IS NOT NULL AND  PPROVG IS NOT NULL
		 AND  REPTIP IS NOT NULL AND  ICOT IS NOT NULL)
	END
END





