/****** Script for SelectTopNRows command from SSMS  ******/
  set identity_insert [Slavoj].[dbo].[Cleni] on 
  alter table [Slavoj].[dbo].[Cleni] disable trigger all

  insert into [Slavoj].[dbo].[Cleni] (ClenId, Jmeno, Prijmeni) values (0, 'Nevyplněno', '')

  alter table [Slavoj].[dbo].[Cleni] enable trigger all
  set identity_insert [Slavoj].[dbo].[Cleni] off 

  SELECT TOP 1000 [ClenId]
      ,[Jmeno]
      ,[Prijmeni]
      ,[TitulPred]
      ,[TitulZa]
      ,[JeClen]
      ,[RodneCislo]
      ,[DatumNarozeni]
      ,[PohlaviId]
      ,[PohlavyNazev]
      ,[Vek]
  FROM [Slavoj].[dbo].[Cleni]
  order by Prijmeni, Jmeno