--Kategorie
INSERT [dbo].[Kategorie] ([KategorieId], [Nazev]) VALUES (0, N'Obecné')
INSERT [dbo].[Kategorie] ([KategorieId], [Nazev]) VALUES (1, N'Dospělí')
INSERT [dbo].[Kategorie] ([KategorieId], [Nazev]) VALUES (2, N'Mládež')
INSERT [dbo].[Kategorie] ([KategorieId], [Nazev]) VALUES (3, N'Turnaje')
INSERT [dbo].[Kategorie] ([KategorieId], [Nazev]) VALUES (4, N'Schůze')

--UserProfile
SET IDENTITY_INSERT [dbo].[UserProfile] ON 
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (3, N'karel1')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (2, N'pavel1')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (1, N'petrk')
SET IDENTITY_INSERT [dbo].[UserProfile] OFF

--Clanky
SET IDENTITY_INSERT [dbo].[Clanky] ON 
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [KategorieId]) VALUES (1, N'Článek 1', N'To je to jejich velké "Haloooo"? Další <strong>virtuál</strong>, který mě zklamal. Polatek 45 Kč měsíčně, na roaming jistina 1000 Kč jistina vratná po 90 dnech od deaktivace, datový balíček o max. velikosti 200 Mb za 140 Kč pokud se opakuje a v případě, že budete potřebovat dat víc, je dalších 200 Mb za 160 Kč.. Takže kdo si vystačí s aspoň 400 Mb měsíčně, vyjde ho to na 300 Kč + 45 Kč poplatek za sim.', 3, CAST(0x07000000000039370B AS DateTime2), CAST(0x07000000000039370B AS DateTime2), 1)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [KategorieId]) VALUES (2, N'Článek 2', N'Volání už jsem přestal docela řešit, pro někoho je to možná moc, ale jestli je to 1,50 Kč nebo 2 Kč je mi celkem jedno, vezmu-li v úvahu, že nedávno jsem platil minimálně 6 Kč za minutu. Ale proboha, proč někdo nepřijde s nějakou lepší nabídkou internetu? Tohle je fakt hrozný.', 1, CAST(0x07000000000039370B AS DateTime2), CAST(0x07000000000039370B AS DateTime2), 0)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [KategorieId]) VALUES (4, N'Článek 3', N'To je pravda, datove balicky nic moc. Osobne nemusim mit zrovna destiky GB ale rekneme na bezne pouzivani v mobilu mam jen provoz na emailu kolem 300 MB a to jsem se jeste nemrknul na zadnou stranku ani nestalhl z PlayStore žádnou aktualizaci mimo wifi, takze alespon 500MB by urcite bylo vhodných.', 2, CAST(0x07000000000039370B AS DateTime2), CAST(0x072FC43E906D46370B AS DateTime2), 3)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [KategorieId]) VALUES (6, N'Článek 4', N'<a href="https://docs.google.com/spreadsheet/ccc?key=0ArWsVs1JytOndG92V3BybDB1SEJRLUpTOWUxSExYV0E&usp=sharing">Startovní listina</a> <img alt="" src="c:\Users\petrk\Documents\Visual Studio 2012\Projects\SlavojMVC4-1\SlavojMVC4-1\Content\Images\Znak.KK.Slavoj.Zirovnice.150.jpg" />Nazdar.', 2, CAST(0x07000000000044370B AS DateTime2), CAST(0x075ABCA6E34D53370B AS DateTime2), 3)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [KategorieId]) VALUES (7, N'Můj článek', N'Můj článek xxxxx', 2, CAST(0x07003A1B6F7C44370B AS DateTime2), CAST(0x07ECFC55327153370B AS DateTime2), 2)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [KategorieId]) VALUES (12, N'Schůze na kuželně', N'aaaaaaaacccccc', 1, CAST(0x07008049D87153370B AS DateTime2), CAST(0x0743EA32997554370B AS DateTime2), 4)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [KategorieId]) VALUES (13, N'Nový článek', N'Nový článek', 1, CAST(0x078086A1A57554370B AS DateTime2), CAST(0x07954191E47655370B AS DateTime2), 3)
SET IDENTITY_INSERT [dbo].[Clanky] OFF

--webpages_Membership
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (1, CAST(0x0000A1E700B3C657 AS DateTime), NULL, 1, CAST(0x0000A1EB00B5709D AS DateTime), 0, N'AFRND84xZhvSRclLS99267MS2udyD+81tMbnDXYjPeuO2ZB/vy2Yj3Fs4ID0bVSJjw==', CAST(0x0000A1E700B3C657 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (2, CAST(0x0000A1E700C13F53 AS DateTime), NULL, 1, CAST(0x0000A1E700C17597 AS DateTime), 0, N'ADUfICuWKTTa6EdiRxxJV2DKqy6tbMoOlXzTX5XxGvqiJnsN1wsEriCOA6N/sOhuGg==', CAST(0x0000A1E700C13F53 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (3, CAST(0x0000A1E700C59783 AS DateTime), NULL, 1, NULL, 0, N'ANx3GfBpEMeG6WqFEGREzELGgVdfz1I2NwBz7JxLYw7rLepy8Q4ySrG963eYAo9xpg==', CAST(0x0000A1E700C59783 AS DateTime), N'', NULL, NULL)

--webpages_Roles
SET IDENTITY_INSERT [dbo].[webpages_Roles] ON 
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'admin')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'clen')
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF

-- webpages_UsersInRoles
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 2)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (2, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (3, 2)

--Cleni
SET IDENTITY_INSERT [dbo].[Cleni] ON 
INSERT [dbo].[Cleni] ([ClenId], [Jmeno], [Prijmeni], [TitulPred], [TitulZa], [Ulice], [CisloPopisne], [Obec], [Psc], [JeClen]) VALUES (1, N'Petr', N'Kejval', N'Ing.', NULL, N'Sídliště', 680, N'Žirovnice', 39468, 1)
INSERT [dbo].[Cleni] ([ClenId], [Jmeno], [Prijmeni], [TitulPred], [TitulZa], [Ulice], [CisloPopisne], [Obec], [Psc], [JeClen]) VALUES (2, N'Pavel', N'Ryšavý', NULL, NULL, N'Dláždění', 302, N'Žirovnice', 39468, 1)
SET IDENTITY_INSERT [dbo].[Cleni] OFF

--UserClen
INSERT [dbo].[UserClen] ([UserId], [ClebId]) VALUES (1, 1)
INSERT [dbo].[UserClen] ([UserId], [ClebId]) VALUES (2, 2)
