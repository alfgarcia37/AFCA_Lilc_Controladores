DROP FUNCTION dbo.fn_ReemplazarNombresVacas
GO


create function dbo.fn_ReemplazarNombresVacas
(
	@wnombre varchar(50),
	@NombreComaVaca varchar(50),
	@establo char(1)
)
-- @NombreComaVaca=xxx,yyy	@Nombre= xxx paloma yyy
returns varchar(40)

as
BEGIN
 --Declare @NombreComaVaca varchar(50) ='casablanca,culi' 
declare @Nombre varchar(50)--='casablanca zorra  culi '
 --declare @wnombre varchar(50)

    --PRINT  'NombreInicial: ' +@Nombre
    While CHARINDEX(',',@NombreComaVaca ) > 0
	BEGIN
        SET  @wnombre = RTrim(LTRIM(SUBSTRING(UPPER(@NombreComaVaca), 1, CHARINDEX( ',',UPPER(@NombreComaVaca)) - 1)))
		SET @Nombre=Replace (UPPER(@Nombre) ,UPPER(@wnombre),'')
        SET  @NombreComaVaca = RTrim(SUBSTRING(UPPER(@NombreComaVaca), CHARINDEX(',',@NombreComaVaca )+1, 50 ))
		
    END
	return @wnombre
END
