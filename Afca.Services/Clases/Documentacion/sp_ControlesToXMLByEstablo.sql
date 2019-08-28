USE [controllechero]
GO

/****** Object:  StoredProcedure [dbo].[sp_ControlesToXMLByEstablo]    Script Date: 28/08/2019 20:25:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[sp_ControlesToXMLByEstablo]
(
@Establo char(7) ,
@controlador int
)
AS
--Stored procedure created by FrameworkForge generator tool at 16/04/2019 18:52:40. Copyright : Fernando Pérez Pla.
DELETE FROM [dbo].XMLCONTROL WHERE RES_TIT=@Establo
--ORDEN COLLAR -> COLLAR O 4 ULTIMOS
DECLARE @Orden char(1)
select @Orden=orden from LecturasControl where establo=@Establo

insert into [dbo].XMLCONTROL 
		(RES_GEN, RES_NOM, RES_FN,RES_TIT,RES_COL, RES_FP,
         RES_NP,RES_NC,RES_AB,RES_FBA,
		 RES_KANT,RES_XAB,RES_VELORD,RES_TEMPERAMENTO,Res_Controlador)

 select Maes_Gen,Maes_Nom,Maes_FN,@Establo,CASE WHEN @Orden='1'  THEN Maes_col  ELSE Right(Maes_gen,4)  END,Maes_FP,
      Maes_NP, CASE WHEN Maes_Estado='A'  THEN Maes_NC   ELSE Maes_NC+1 END, Maes_Estado,Maes_FBA,
      0,Maes_Estado,'0','0',@controlador
 from Maestro
 WHERE Maes_Tit=@Establo and Maes_Estado<>'0'
 
 --'VELOCIDAD DE ORDEÑO......
UPDATE XMLCONTROL
SET RES_VELORD=velord FROM PARTOS 
where establo=@Establo and ID=RES_GEN AND NP=RES_NP
and velord IS not null
--TEMPERAMENTO ?????

--LAST KILOS
DECLARE @LastFprevista smalldatetime
SELECT @LastFprevista=max(fPrevista) FROM LecturasFinalizadas WHERE establo=@Establo 

UPDATE XMLControl SET RES_KANT = cast((RES_KLAM + RES_KLPM)/2  as decimal(5,2)) 
FROM CONTROL C,XMLCONTROL X 
WHERE X.RES_TIT = C.RES_TIT 
AND C.RES_TIT=@Establo 
AND C.RES_FPREV=@LastFprevista
AND X.RES_GEN=C.RES_GEN

UPDATE XMLControl SET RES_KANT =0
WHERE RES_KANT IS NULL

--VACAS JOVENES
DECLARE @MES1 INT,@MES2 INT
SELECT @MES1=PARAM1,@MES2=PARAM2  FROM ParametrosGenerales WHERE ID ='MES12'

insert into [dbo].XMLCONTROL 
		(RES_GEN, RES_NOM, RES_FN,RES_TIT, 
		 RES_NP,RES_NC,RES_AB,RES_XAB,RES_KANT, Res_Controlador)

SELECT CODIGOIdentif ,NOMBRE,CAST(CAST(DIANA1 AS VARCHAR(2)) + '/' +CAST(MESNA1 AS VARCHAR(2)) + '/' + CAST(AÑONA1 AS VARCHAR(4)) AS smalldatetime),@Establo,
		0,0,'B','B',0,@controlador
FROM RegGen..ANIMALES
Where GANPRO = @Establo
and NUMLAC=0 and AÑOBAJ=0
and Sexo='H'
and DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())>=@MES1
And DATEDIFF(month, cast(DIANA1 as char(2)) + '/' + cast(MESNA1 as char(2)) + '/'+ cast(AÑONA1 as char(4)), getdate())<=@MES2
and  CODIGOIdentif  not in(select Maes_Gen from maestro where Maes_tit=@Establo)

--ACTUALIZO COLLARES COLLARES DE TERNERAS (COLLAR U ORDEN)...............................................
UPDATE XMLCONTROL
SET RES_COL=CASE WHEN @Orden='1'  THEN Right(numestablo,4)  ELSE Right(RES_GEN,4)  END
FROM REGGEN..VACAS
WHERE RES_TIT=CODIGOGANADERIA
AND RES_TIT=@Establo
--AND (RES_GEN=NUMCRIA OR 
AND RES_GEN=CODIGOIDENTIF
and numestablo is not null
AND RES_COL IS  NULL
--ACTUALIZO NOMBRES.............................................................................................
--ACTUALIZO NOMBRES.............................................................................................
declare @NombreDB varchar(50)
DECLARE @Nombre varchar(50)
SET @Nombre=''
SELECT @NombreDB=RTRIM(LTRIM(nombrevaca)) from lecturascontrol where Establo=@Establo
--PRINT 'NOMBB... ' + @NombreDB

select *
Into   #Temp
From   XMLCONTROL WHERE Res_TIT=@Establo

Declare @Id CHAR(14)
--PRINT '..A VER  ' + dbo.fn_ReemplazarNombresVacas('BENERA PINTA','BENERA')
While (Select Count(*) From #Temp) > 0
Begin
    Select Top 1 @Id = RES_GEN ,@Nombre=RTRIM(LTRIM(RES_NOM)) From #Temp
	--PRINT 'NOMBRE: ' + @Nombre + ':' +'NOMDB . ' + @NombreDB + 'RESULTADO...' + dbo.fn_ReemplazarNombresVacas(@Nombre,'BENERA')

	UPDATE XMLCONTROL SET RES_NOM=dbo.fn_ReemplazarNombresVacas(@Nombre,@NombreDB) WHERE RES_GEN=@Id
    --PRINT '...elininado: ' + @Nombre + '...' + @Id
    Delete #Temp Where RES_GEN = @Id
End
GO

