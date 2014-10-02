USE [Slavoj]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] DROP CONSTRAINT [FK_webpages_UsersInRoles_webpages_Roles]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] DROP CONSTRAINT [FK_webpages_UsersInRoles_UserProfile]
GO
ALTER TABLE [dbo].[ELMAH_Error] DROP CONSTRAINT [DF_ELMAH_Error_ErrorId]
GO
ALTER TABLE [dbo].[Clanky] DROP CONSTRAINT [DF_Clanky_Kategorie]
GO
ALTER TABLE [dbo].[Clanky] DROP CONSTRAINT [DF_Clanky_DatumZmeny]
GO
ALTER TABLE [dbo].[Clanky] DROP CONSTRAINT [DF_Clanky_DatumVytvoreni]
GO
/****** Object:  Index [IX_FK_webpages_UsersInRoles_webpages_Roles]    Script Date: 29.6.2013 11:10:55 ******/
DROP INDEX [IX_FK_webpages_UsersInRoles_webpages_Roles] ON [dbo].[webpages_UsersInRoles]
GO
/****** Object:  Index [PK_webpages_UsersInRoles]    Script Date: 29.6.2013 11:10:55 ******/
ALTER TABLE [dbo].[webpages_UsersInRoles] DROP CONSTRAINT [PK_webpages_UsersInRoles]
GO
/****** Object:  Index [UQ__UserProf__C9F28456CC26E546]    Script Date: 29.6.2013 11:10:55 ******/
ALTER TABLE [dbo].[UserProfile] DROP CONSTRAINT [UQ__UserProf__C9F28456CC26E546]
GO
/****** Object:  Index [IX_ELMAH_Error_App_Time_Seq]    Script Date: 29.6.2013 11:10:55 ******/
DROP INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error]
GO
/****** Object:  Index [PK_ELMAH_Error]    Script Date: 29.6.2013 11:10:55 ******/
ALTER TABLE [dbo].[ELMAH_Error] DROP CONSTRAINT [PK_ELMAH_Error]
GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 29.6.2013 11:10:55 ******/
DROP TABLE [dbo].[webpages_UsersInRoles]
GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 29.6.2013 11:10:55 ******/
DROP TABLE [dbo].[webpages_Roles]
GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 29.6.2013 11:10:55 ******/
DROP TABLE [dbo].[webpages_OAuthMembership]
GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 29.6.2013 11:10:55 ******/
DROP TABLE [dbo].[webpages_Membership]
GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 29.6.2013 11:10:55 ******/
DROP TABLE [dbo].[UserProfile]
GO
/****** Object:  Table [dbo].[ELMAH_Error]    Script Date: 29.6.2013 11:10:55 ******/
DROP TABLE [dbo].[ELMAH_Error]
GO
/****** Object:  Table [dbo].[Clanky]    Script Date: 29.6.2013 11:10:55 ******/
DROP TABLE [dbo].[Clanky]
GO
/****** Object:  Table [dbo].[Clanky]    Script Date: 29.6.2013 11:10:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clanky](
	[ClanekId] [int] IDENTITY(1,1) NOT NULL,
	[Predmet] [nvarchar](50) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[DatumVytvoreni] [datetime2](7) NOT NULL,
	[DatumZmeny] [datetime2](7) NOT NULL,
	[Kategorie] [int] NOT NULL,
 CONSTRAINT [PK_Clanky] PRIMARY KEY CLUSTERED 
(
	[ClanekId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ELMAH_Error]    Script Date: 29.6.2013 11:10:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [ntext] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfile]    Script Date: 29.6.2013 11:10:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfile](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](56) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Membership]    Script Date: 29.6.2013 11:10:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Membership](
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
 CONSTRAINT [PK_webpages_Membership] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_OAuthMembership]    Script Date: 29.6.2013 11:10:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_OAuthMembership](
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_webpages_OAuthMembership] PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_Roles]    Script Date: 29.6.2013 11:10:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_webpages_Roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[webpages_UsersInRoles]    Script Date: 29.6.2013 11:10:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[webpages_UsersInRoles](
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Clanky] ON 

INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (1, N'Článek 1', N'To je to jejich velké "Haloooo"? Další <strong>virtuál</strong>, který mě zklamal. Polatek 45 Kč měsíčně, na roaming jistina 1000 Kč jistina vratná po 90 dnech od deaktivace, datový balíček o max. velikosti 200 Mb za 140 Kč pokud se opakuje a v případě, že budete potřebovat dat víc, je dalších 200 Mb za 160 Kč.. Takže kdo si vystačí s aspoň 400 Mb měsíčně, vyjde ho to na 300 Kč + 45 Kč poplatek za sim.', 0, CAST(0x07000000000039370B AS DateTime2), CAST(0x07000000000039370B AS DateTime2), 0)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (2, N'Článek 2', N'Volání už jsem přestal docela řešit, pro někoho je to možná moc, ale jestli je to 1,50 Kč nebo 2 Kč je mi celkem jedno, vezmu-li v úvahu, že nedávno jsem platil minimálně 6 Kč za minutu. Ale proboha, proč někdo nepřijde s nějakou lepší nabídkou internetu? Tohle je fakt hrozný.', 1, CAST(0x07000000000039370B AS DateTime2), CAST(0x07000000000039370B AS DateTime2), 0)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (4, N'Článek 3', N'To je pravda, datove balicky nic moc. Osobne nemusim mit zrovna destiky GB ale rekneme na bezne pouzivani v mobilu mam jen provoz na emailu kolem 300 MB a to jsem se jeste nemrknul na zadnou stranku ani nestalhl z PlayStore žádnou aktualizaci mimo wifi, takze alespon 500MB by urcite bylo vhodných.', 0, CAST(0x07000000000039370B AS DateTime2), CAST(0x072FC43E906D46370B AS DateTime2), 0)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (6, N'Článek 4', N'<a href="https://docs.google.com/spreadsheet/ccc?key=0ArWsVs1JytOndG92V3BybDB1SEJRLUpTOWUxSExYV0E&usp=sharing">Startovní listina</a> <img alt="Znak" src="C:\Users\petrk\SkyDrive\Programovani\SlavojMVC4-1\SlavojMVC4-1\Content\Images\Znak.KK.Slavoj.Zirovnice.150.jpg" />Nazdar', 0, CAST(0x07000000000044370B AS DateTime2), CAST(0x074B5951B36F46370B AS DateTime2), 0)
INSERT [dbo].[Clanky] ([ClanekId], [Predmet], [Text], [UserId], [DatumVytvoreni], [DatumZmeny], [Kategorie]) VALUES (7, N'Můj článek', N'Můj článek xxxxx', 0, CAST(0x07003A1B6F7C44370B AS DateTime2), CAST(0x0784A488C86C46370B AS DateTime2), 0)
SET IDENTITY_INSERT [dbo].[Clanky] OFF
SET IDENTITY_INSERT [dbo].[UserProfile] ON 

INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (3, N'karel1')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (2, N'pavel1')
INSERT [dbo].[UserProfile] ([UserId], [UserName]) VALUES (1, N'petrk')
SET IDENTITY_INSERT [dbo].[UserProfile] OFF
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (1, CAST(0x0000A1E700B3C657 AS DateTime), NULL, 1, CAST(0x0000A1EB00B5709D AS DateTime), 0, N'AFRND84xZhvSRclLS99267MS2udyD+81tMbnDXYjPeuO2ZB/vy2Yj3Fs4ID0bVSJjw==', CAST(0x0000A1E700B3C657 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (2, CAST(0x0000A1E700C13F53 AS DateTime), NULL, 1, CAST(0x0000A1E700C17597 AS DateTime), 0, N'ADUfICuWKTTa6EdiRxxJV2DKqy6tbMoOlXzTX5XxGvqiJnsN1wsEriCOA6N/sOhuGg==', CAST(0x0000A1E700C13F53 AS DateTime), N'', NULL, NULL)
INSERT [dbo].[webpages_Membership] ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (3, CAST(0x0000A1E700C59783 AS DateTime), NULL, 1, NULL, 0, N'ANx3GfBpEMeG6WqFEGREzELGgVdfz1I2NwBz7JxLYw7rLepy8Q4ySrG963eYAo9xpg==', CAST(0x0000A1E700C59783 AS DateTime), N'', NULL, NULL)
SET IDENTITY_INSERT [dbo].[webpages_Roles] ON 

INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (1, N'admin')
INSERT [dbo].[webpages_Roles] ([RoleId], [RoleName]) VALUES (2, N'clen')
SET IDENTITY_INSERT [dbo].[webpages_Roles] OFF
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (1, 2)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (2, 1)
INSERT [dbo].[webpages_UsersInRoles] ([UserId], [RoleId]) VALUES (3, 2)
/****** Object:  Index [PK_ELMAH_Error]    Script Date: 29.6.2013 11:10:55 ******/
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ELMAH_Error_App_Time_Seq]    Script Date: 29.6.2013 11:10:55 ******/
CREATE NONCLUSTERED INDEX [IX_ELMAH_Error_App_Time_Seq] ON [dbo].[ELMAH_Error]
(
	[Application] ASC,
	[TimeUtc] DESC,
	[Sequence] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__UserProf__C9F28456CC26E546]    Script Date: 29.6.2013 11:10:55 ******/
ALTER TABLE [dbo].[UserProfile] ADD UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [PK_webpages_UsersInRoles]    Script Date: 29.6.2013 11:10:55 ******/
ALTER TABLE [dbo].[webpages_UsersInRoles] ADD  CONSTRAINT [PK_webpages_UsersInRoles] PRIMARY KEY NONCLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FK_webpages_UsersInRoles_webpages_Roles]    Script Date: 29.6.2013 11:10:55 ******/
CREATE NONCLUSTERED INDEX [IX_FK_webpages_UsersInRoles_webpages_Roles] ON [dbo].[webpages_UsersInRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clanky] ADD  CONSTRAINT [DF_Clanky_DatumVytvoreni]  DEFAULT (getdate()) FOR [DatumVytvoreni]
GO
ALTER TABLE [dbo].[Clanky] ADD  CONSTRAINT [DF_Clanky_DatumZmeny]  DEFAULT (getdate()) FOR [DatumZmeny]
GO
ALTER TABLE [dbo].[Clanky] ADD  CONSTRAINT [DF_Clanky_Kategorie]  DEFAULT ((0)) FOR [Kategorie]
GO
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [DF_ELMAH_Error_ErrorId]  DEFAULT (newid()) FOR [ErrorId]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_webpages_UsersInRoles_UserProfile] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserProfile] ([UserId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [FK_webpages_UsersInRoles_UserProfile]
GO
ALTER TABLE [dbo].[webpages_UsersInRoles]  WITH CHECK ADD  CONSTRAINT [FK_webpages_UsersInRoles_webpages_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[webpages_Roles] ([RoleId])
GO
ALTER TABLE [dbo].[webpages_UsersInRoles] CHECK CONSTRAINT [FK_webpages_UsersInRoles_webpages_Roles]
GO
