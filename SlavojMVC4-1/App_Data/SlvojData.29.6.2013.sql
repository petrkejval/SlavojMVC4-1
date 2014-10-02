USE [Slavoj]
GO

SET IDENTITY_INSERT [dbo].[UserProfile] ON 
GO
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (3, N'karel1')
GO
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (2, N'pavel1')
GO
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (1, N'petrk')
GO
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
GO

SET IDENTITY_INSERT [dbo].[webpages_Roles] ON 
GO
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'admin')
GO
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'clen')
GO
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF
GO

INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 1)
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 2)
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (2, 1)
GO
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (3, 2)
GO

SET IDENTITY_INSERT [dbo].[Clanky] ON 
GO
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (1, N'Článek 1', N'To je to jejich velké "Haloooo"? Další <strong>virtuál</strong>, který mě zklamal. Polatek 45 Kč měsíčně, na roaming jistina 1000 Kč jistina vratná po 90 dnech od deaktivace, datový balíček o max. velikosti 200 Mb za 140 Kč pokud se opakuje a v případě, že budete potřebovat dat víc, je dalších 200 Mb za 160 Kč.. Takže kdo si vystačí s aspoň 400 Mb měsíčně, vyjde ho to na 300 Kč + 45 Kč poplatek za sim.', 0, CAST(0x07000000000039370B AS DateTime2), CAST(0x07000000000039370B AS DateTime2), 0)
GO
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (2, N'Článek 2', N'Volání už jsem přestal docela řešit, pro někoho je to možná moc, ale jestli je to 1,50 Kč nebo 2 Kč je mi celkem jedno, vezmu-li v úvahu, že nedávno jsem platil minimálně 6 Kč za minutu. Ale proboha, proč někdo nepřijde s nějakou lepší nabídkou internetu? Tohle je fakt hrozný.', 1, CAST(0x07000000000039370B AS DateTime2), CAST(0x07000000000039370B AS DateTime2), 0)
GO
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (4, N'Článek 3', N'To je pravda, datove balicky nic moc. Osobne nemusim mit zrovna destiky GB ale rekneme na bezne pouzivani v mobilu mam jen provoz na emailu kolem 300 MB a to jsem se jeste nemrknul na zadnou stranku ani nestalhl z PlayStore žádnou aktualizaci mimo wifi, takze alespon 500MB by urcite bylo vhodných.', 0, CAST(0x07000000000039370B AS DateTime2), CAST(0x072FC43E906D46370B AS DateTime2), 0)
GO
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (6, N'Článek 4', N'<a href="https://docs.google.com/spreadsheet/ccc?key=0ArWsVs1JytOndG92V3BybDB1SEJRLUpTOWUxSExYV0E&usp=sharing">Startovní listina</a> <img alt="Znak" src="C:\Users\petrk\SkyDrive\Programovani\SlavojMVC4-1\SlavojMVC4-1\Content\Images\Znak.KK.Slavoj.Zirovnice.150.jpg" />Nazdar', 0, CAST(0x07000000000044370B AS DateTime2), CAST(0x074B5951B36F46370B AS DateTime2), 0)
GO
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (7, N'Můj článek', N'Můj článek xxxxx', 0, CAST(0x07003A1B6F7C44370B AS DateTime2), CAST(0x0784A488C86C46370B AS DateTime2), 0)
GO
SET IDENTITY_INSERT [dbo].[Clanky] OFF
GO

INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (1, CAST(0x0000A1E700B3C657 AS DateTime), NULL, 1, CAST(0x0000A1EB00B5709D AS DateTime), 0, N'AFRND84xZhvSRclLS99267MS2udyD+81tMbnDXYjPeuO2ZB/vy2Yj3Fs4ID0bVSJjw==', CAST(0x0000A1E700B3C657 AS DateTime), N'', NULL, NULL)
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (2, CAST(0x0000A1E700C13F53 AS DateTime), NULL, 1, CAST(0x0000A1E700C17597 AS DateTime), 0, N'ADUfICuWKTTa6EdiRxxJV2DKqy6tbMoOlXzTX5XxGvqiJnsN1wsEriCOA6N/sOhuGg==', CAST(0x0000A1E700C13F53 AS DateTime), N'', NULL, NULL)
GO
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (3, CAST(0x0000A1E700C59783 AS DateTime), NULL, 1, NULL, 0, N'ANx3GfBpEMeG6WqFEGREzELGgVdfz1I2NwBz7JxLYw7rLepy8Q4ySrG963eYAo9xpg==', CAST(0x0000A1E700C59783 AS DateTime), N'', NULL, NULL)
GO
