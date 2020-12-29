USE [eFundraisingProd]
GO
/****** Object:  UserDefinedFunction [dbo].[fct_GetGreaterLimit]    Script Date: 02/14/2014 13:09:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Cette fonction retourne la borne supérieure d'une intervalle dans laquelle se trouve une valeur
-- Entrée:
	-- @Value: la valeur recherchée
	-- @Range: l'intervalle
	-- @LastValue: la dernière borne qui part de @LastValue jusqu'à l'infini
	-- @StartValue: le nombre auquel on part (habituellement 0 ou 1)
-- Sortie: La borne supérieure d'une intervalle (sauf si c'est >= que @LastValue)
-- Nicolas Désy 27 juin 2003

/*Exemples: 

	On veut des intervalles de 20 qui débutent à 1 ([1,20], [21,40]...) et on veut savoir dans quelle 
	intervalle se trouve 45, le système va retourner 60 car ce chiffre se trouve dans l'intervalle [41,60] 
	On doit passer en paramètres: (45,20,(N'importe quoi),1)

*/

CREATE  FUNCTION [dbo].[fct_GetGreaterLimit] (@Value int, @Range int, @LastValue int, @StartValue int = 0) RETURNS Int
BEGIN
	DECLARE @Result int
	
	If @Value >= @LastValue
		Set @Result = @LastValue
	Else
		If @Value < @StartValue
			Set @Result = 0
		Else
			Set @Result = @Value - (@Value % @Range) + @StartValue + @Range - 1

	If @Result >= @LastValue AND @Value < @LastValue
		Set @Result = @LastValue - 1

	RETURN @Result
END
GO
