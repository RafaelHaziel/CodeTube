CREATE DATABASE IF NOT EXISTS CodeTubeDb;

USE CodeTubeDb;

CREATE TABLE IF NOT EXISTS Tag (
    Id      		INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name    		VARCHAR(30) NOT NULL UNIQUE
);

CREATE TABLE IF NOT EXISTS Video (
    Id              INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY,
    Name   			VARCHAR(100) NOT NULL,
    Description     TEXT,
    UploadDate      DATETIME,
    Duration        SMALLINT(3) UNSIGNED,
    AgeRating       TINYINT UNSIGNED,
	Thumbnail		VARCHAR(200),
	VideoFile		VARCHAR(200) NOT NULL
);

CREATE TABLE IF NOT EXISTS VideoTag (
    VideoId 		INT     UNSIGNED NOT NULL,
    TagId 			INT UNSIGNED NOT NULL,
    PRIMARY KEY (VideoId, TagId),
    CONSTRAINT FK_VideoTag_Video FOREIGN KEY (VideoId) REFERENCES Video(Id),
    CONSTRAINT FK_VideoTag_Tag FOREIGN KEY (TagId) REFERENCES Tag(Id)
);
