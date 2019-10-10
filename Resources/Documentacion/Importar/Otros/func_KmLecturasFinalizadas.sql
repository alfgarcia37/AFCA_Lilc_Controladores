USE [controllechero]
GO

/****** Object:  UserDefinedFunction [dbo].[func_KmLecturasFinalizadas]    Script Date: 08/10/2019 10:40:11 ******/
DROP FUNCTION [dbo].[func_KmLecturasFinalizadas]
GO

/****** Object:  UserDefinedFunction [dbo].[func_KmLecturasFinalizadas]    Script Date: 08/10/2019 10:40:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[func_KmLecturasFinalizadas]
(	@numcontroler int,
	@Establo char(7) 
	
)
RETURNS int
AS
BEGIN
	DECLARE @km int
	SELECT @km=km  from LecturasFinalizadas 
	where  numcontroler=@numcontroler and establo=@Establo
	and Propio='0' and km<>0
	return COALESCE(@km,0)
END
GO

