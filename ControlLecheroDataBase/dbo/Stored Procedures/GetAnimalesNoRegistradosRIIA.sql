-- =============================================
CREATE PROCEDURE [dbo].[GetAnimalesNoRegistradosRIIA]
	-- Add the parameters for the stored procedure here
	@strEstabloConafe as char(7)
	--@strEstabloRIIA as char(14)
AS
BEGIN

SELECT MADRE,VACA 'TERNERA',@strEstabloConafe 'establo',FNAC_HIJO 'FPARTO'
	 FROM RIIAREMO..RIIA_EXPLOTACION,RIIAREMO..RIIA_PARTOS
	 where establosanitario IN ( SELECT CEA FROM RIIAREMO..REGA WHERE CODGAN=@strEstabloConafe)
	 AND vaca =HIJO
	 and sexo='02'--para sacar solo las terneras hembras
	 and raza='1111'
	 and fsalida is null
	 and vaca not in (select codigoidentif  from reggen..animales where ganpro=@strEstabloConafe)
	 --hago esto para que no vayan las que han eliminado los controladores de la hoja que les dimos
	 and vaca not in (select vaca from controllechero..VacasNoRIIA)
/*
	SELECT MADRE,[ea_id] 'TERNERA',@strEstabloConafe 'establo',FNAC_HIJO 'FPARTO'
	 FROM [V_SITRANC].[riia].[dbo].[T_EXPLOT_ANIMAL],RIIAREMO..RIIA_PARTOS
	 where ea_explotacion=@strEstabloRIIA
	 AND ea_id COLLATE Modern_Spanish_CI_AS=HIJO
	 and sexo='02'--para sacar solo las terneras hembras
	 and raza='1111'
	 and an_fc_salida is null
	 and cast(ea_id as char(14)) COLLATE Modern_Spanish_CI_AS not in (select codigoidentif  from reggen..animales where ganpro=@strEstabloConafe)
	 --hago esto para que no vayan las que han eliminado los controladores de la hoja que les dimos
	 and cast(ea_id as char(14)) COLLATE Modern_Spanish_CI_AS not in (select vaca from controllechero..VacasNoRIIA)
	 */
END
