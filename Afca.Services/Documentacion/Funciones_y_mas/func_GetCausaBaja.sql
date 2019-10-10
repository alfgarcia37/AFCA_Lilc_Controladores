USE [controllechero]
GO

/****** Object:  UserDefinedFunction [dbo].[func_GetCausaBaja]    Script Date: 08/10/2019 10:39:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create function [dbo].[func_GetCausaBaja]
(	@codigocrotal char(14),
	@np int
)
RETURNS char(2)
AS
BEGIN
	DECLARE @Causa char(2)
  select  @Causa=res_cba from control where RES_GEN=@codigocrotal and RES_NP=@np and RES_AB='B' 
  return @Causa
END
GO

