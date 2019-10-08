USE [controllechero]
GO

/****** Object:  UserDefinedFunction [dbo].[func_ExisteVelocidad]    Script Date: 08/10/2019 10:39:52 ******/
DROP FUNCTION [dbo].[func_ExisteVelocidad]
GO

/****** Object:  UserDefinedFunction [dbo].[func_ExisteVelocidad]    Script Date: 08/10/2019 10:39:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[func_ExisteVelocidad]
(	@codigocrotal char(14)
)
RETURNS int
AS
BEGIN
	DECLARE @velocidad int
  SELECT @velocidad=count(*)  from  control Where res_gen=@codigocrotal and Res_NP=1 and Res_NC BETWEEN 2 AND 6 and Res_VELORD BETWEEN '0' AND '3'
  return @velocidad
END
GO

