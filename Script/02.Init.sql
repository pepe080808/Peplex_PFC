CREATE SEQUENCE UserNickNameSeq AS BIGINT START WITH 1 INCREMENT BY 1

GO

CREATE TABLE Format (
	Id				INT IDENTITY(1,1) NOT NULL,
	Name			NVARCHAR (5) NOT NULL,
	Quality			NVARCHAR (10) NOT NULL,
	
	CONSTRAINT PK_Format PRIMARY KEY(Id)
);

GO

INSERT INTO Format (Name, Quality) VALUES ('NA', 'NA')
INSERT INTO Format (Name, Quality) VALUES ('mkv', '720p')
INSERT INTO Format (Name, Quality) VALUES ('mkv', '1080p')
INSERT INTO Format (Name, Quality) VALUES ('avi', 'Dvd-Rip')
INSERT INTO Format (Name, Quality) VALUES ('mp4', 'Mp4-HD')

GO

CREATE TABLE Genre (
	Id				INT IDENTITY(1,1) NOT NULL,
	Name			NVARCHAR (50) NOT NULL,
	
	CONSTRAINT PK_Genre PRIMARY KEY(Id)
)

GO

INSERT INTO Genre (Name) VALUES ('NA')
INSERT INTO Genre (Name) VALUES ('Terror')
INSERT INTO Genre (Name) VALUES ('Thriller')
INSERT INTO Genre (Name) VALUES ('Acción')
INSERT INTO Genre (Name) VALUES ('Aventura')
INSERT INTO Genre (Name) VALUES ('Ciencia ficción')
INSERT INTO Genre (Name) VALUES ('Comedia')
INSERT INTO Genre (Name) VALUES ('Drama')
INSERT INTO Genre (Name) VALUES ('Romántica')
INSERT INTO Genre (Name) VALUES ('Animación')
INSERT INTO Genre (Name) VALUES ('Histórica')

GO

CREATE TABLE Film (
	Id				INT IDENTITY(1,1) NOT NULL,
    Title 			NVARCHAR (80) NOT NULL,
    Subtitle		NVARCHAR (50) NOT NULL,
    FormatId 		INT DEFAULT 1,
	GenreId01		INT DEFAULT 1,
	GenreId02		INT DEFAULT 1,
	Synopsis 		NVARCHAR (1000) NOT NULL,
	DurationMin		INT DEFAULT 0,
    DownloadDate 	DATETIME NOT NULL,
	PremiereDate	DATETIME NOT NULL,
    TrailerURL 		NVARCHAR (30),
    Note 			DECIMAL (4,2) DEFAULT 5.0,
	Cover			VARBINARY(MAX),
	Background		VARBINARY(MAX),
	
	CONSTRAINT PK_Film PRIMARY KEY(Id),
	CONSTRAINT FK_Film_FormatId FOREIGN KEY(FormatId) REFERENCES Format(Id),
	CONSTRAINT FK_Film_GenreId01 FOREIGN KEY(GenreId01) REFERENCES Genre(Id),
	CONSTRAINT FK_Film_GenreId02 FOREIGN KEY(GenreId02) REFERENCES Genre(Id)
);

GO

CREATE TABLE Serie (
	Id				INT IDENTITY(1,1) NOT NULL,
    Title 			NVARCHAR (80) NOT NULL,
	GenreId01		INT DEFAULT 1,
	GenreId02		INT DEFAULT 1,
    Synopsis 		NVARCHAR (1000) NOT NULL,
	DurationMin		INT DEFAULT 0,
    DownloadDate 	DATETIME NOT NULL,
	PremiereDate	DATETIME NOT NULL,
    Note 			DECIMAL (4,2)  DEFAULT 5.0,
	Cover			VARBINARY(MAX),
	Background		VARBINARY(MAX),
	
	CONSTRAINT PK_Serie PRIMARY KEY(Id),
	CONSTRAINT FK_Serie_GenreId01 FOREIGN KEY(GenreId01) REFERENCES Genre(Id),
	CONSTRAINT FK_Serie_GenreId02 FOREIGN KEY(GenreId02) REFERENCES Genre(Id)
)

GO

CREATE TABLE Episode (
	SerieId 		INT NOT NULL,
    Season			INT NOT NULL,
    Number 			INT NOT NULL,
	Name 			NVARCHAR (80) NOT NULL,
    FormatId 		INT	DEFAULT 1,
	
	CONSTRAINT PK_Episode PRIMARY KEY (SerieId, Season, Number),
	CONSTRAINT FK_Episode_FormatId FOREIGN KEY(FormatId) REFERENCES Format(Id),
	CONSTRAINT FK_Episode_SerieId FOREIGN KEY(SerieId) REFERENCES Serie(Id) ON DELETE CASCADE,
	
)

GO

CREATE TABLE UserInfo (
	Id				INT IDENTITY(1,1) NOT NULL,
	Name			NVARCHAR (50) NOT NULL,
	NickName		NVARCHAR (50) DEFAULT CONCAT('User ', NEXT VALUE FOR UserNickNameSeq),
	Email			NVARCHAR (50) NOT NULL,
	Password		NVARCHAR (50) NOT NULL,
	Bitmap			VARBINARY(MAX),
	Permissions		INT DEFAULT 0,

	CONSTRAINT PK_UserInfo PRIMARY KEY(Id)
)

GO

INSERT INTO UserInfo (Name, NickName, Email, Password, Bitmap, Permissions) VALUES ('Default', 'Default', 'er_pepe1@hotmail.com', 'Default', null, 0)
INSERT INTO UserInfo (Name, NickName, Email, Password, Bitmap, Permissions) VALUES ('Administrador', 'Admin', 'er_pepe1@hotmail.com', 'Admin', null, 1)

GO

CREATE TABLE FilmSeen (
    UserId 			INT NOT NULL,
    FilmId 			INT NOT NULL,
	
	CONSTRAINT PK_FilmSeen PRIMARY KEY(UserId, FilmId),
	CONSTRAINT FK_FilmSeen_UserId FOREIGN KEY(UserId) REFERENCES UserInfo(Id) ON DELETE CASCADE,
	CONSTRAINT FK_FilmSeen_FilmId FOREIGN KEY(FilmId) REFERENCES Film(Id) ON DELETE CASCADE
);

GO

CREATE TABLE SerieSeen (
    UserId 			INT NOT NULL,
    SerieId 		INT NOT NULL,
	
	CONSTRAINT PK_SerieSeen PRIMARY KEY(UserId, SerieId),
	CONSTRAINT FK_SerieSeen_UserId FOREIGN KEY(UserId) REFERENCES UserInfo(Id) ON DELETE CASCADE,
	CONSTRAINT FK_SerieSeen_SerieId FOREIGN KEY(SerieId) REFERENCES Serie(Id) ON DELETE CASCADE
);

GO

CREATE TABLE EpisodeSeen (
	UserId 			INT NOT NULL,
    SerieId			INT NOT NULL,
	Season			INT NOT NULL,
	Number			INT NOT NULL,
	
	CONSTRAINT PK_EpisodeSeen PRIMARY KEY(UserId, SerieId, Season, Number),
	CONSTRAINT FK_EpisodeSeen_UserId FOREIGN KEY(UserId) REFERENCES UserInfo(Id) ON DELETE CASCADE,
	CONSTRAINT FK_EpisodeSeen_EpisodeId FOREIGN KEY(SerieId, Season, Number) REFERENCES Episode(SerieId, Season, Number) ON DELETE CASCADE
)

GO

CREATE TABLE UserFilmTime (
	UserId			INT NOT NULL,
	FilmId			INT NOT NULL,
	SecondTime		INT DEFAULT 0,
	
	CONSTRAINT PK_UserFilmTime PRIMARY KEY(UserId, FilmId),
	CONSTRAINT FK_UserFilmTime_UserId FOREIGN KEY(UserId) REFERENCES UserInfo(Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserFilmTime_FilmId FOREIGN KEY(FilmId) REFERENCES Film(Id) ON DELETE CASCADE
)

GO

CREATE TABLE UserEpisodeTime (
	UserId			INT NOT NULL,
	SerieId			INT NOT NULL,
	Season			INT NOT NULL,
	Number			INT NOT NULL,
	SecondTime		INT DEFAULT 0,
	
	CONSTRAINT PK_UserEpisodeTime PRIMARY KEY(UserId, SerieId, Season, Number),
	CONSTRAINT FK_UserEpisodeTime_UserId FOREIGN KEY(UserId) REFERENCES UserInfo(Id) ON DELETE CASCADE,
	CONSTRAINT FK_UserEpisodeTime_EpisodeId FOREIGN KEY(SerieId, Season, Number) REFERENCES Episode(SerieId, Season, Number) ON DELETE CASCADE
)
