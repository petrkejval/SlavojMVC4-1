use Slavoj
go
select ZapasId AS AkceId, DatumZapasu AS DatumAkce,  CasZapasuOd AS CasAkceOd, Akce = 'Z�pas' from Zapasy 
union
select KuzelnaProgramId AS AkceId, DatumProgramu AS DatumAkce, CasProgramuOd AS CasAkceOd, Akce = 'Program na ku�eln�' from KuzelnaProgramy
order by DatumAkce, CasAkceOd