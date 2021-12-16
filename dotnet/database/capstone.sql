USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,

	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE genres (
	genre_id int NOT NULL,
	genre_name varchar(50) NOT NULL
	CONSTRAINT PK_genres PRIMARY KEY (genre_id)
)

CREATE TABLE users_excludedMovies (
	user_id int NOT NULL,
	movie_id int NOT NULL,
	opinion varchar(12) NOT NULL,
	removal_tracker int

	CONSTRAINT [PK_users_excludedMovies] PRIMARY KEY (movie_id, user_id),
	CONSTRAINT [FK_users_excludedMovies_users] FOREIGN KEY (user_id) references users (user_id)
)

CREATE TABLE movieCards (
	movie_id int NOT NULL,
	title varchar(100) NOT NULL,
	poster_path varchar(100),
	genre_ids varchar(100)

	CONSTRAINT [PK_movieCards] PRIMARY KEY (movie_id)
)
INSERT INTO dbo.genres (genre_id, genre_name)
VALUES
(28, 'Action'),
(12, 'Adventure'),
(16, 'Animation'),
(35, 'Comedy'),
(80, 'Crime'),
(99, 'Documentary'),
(18, 'Drama'),
(10751, 'Family'),
(14, 'Fantasy'),
(36, 'History'),
(27, 'Horror'),
(10402, 'Music'),
(9648, 'Mystery'),
(10749, 'Romance'),
(878, 'Science Fiction'),
(10770, 'TV Movie'),
(53, 'Thriller'),
(10752, 'War'),
(37, 'Western')

CREATE TABLE [genres_users] (
	genre_id int NOT NULL,
	user_id int NOT NULL,
	CONSTRAINT [PK_genres_users] PRIMARY KEY (genre_id, user_id),
	CONSTRAINT [FK_genres_users_genres] FOREIGN KEY (genre_id) references genres (genre_id),
	CONSTRAINT [FK_genres_users_users] FOREIGN KEY (user_id) references users (user_id)
);

--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

--populate presentation data
INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (96984, 'All Dogs Go To Heaven', '/lpwCNexA5N6gUZeXlR8bXv8ZCI1.jpg', '53')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (96984, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (22705, 'Air Bud', '/7gqcEgJfTltbLo76nZTl8umgM1Y.jpg', '35|18|10749')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (22705, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (463998, 'The Inappropriate Movie Example', '/zavz3iYAOgBpE05UqamLmah0Tlj.jpg', '16')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (463998, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (464052, 'Beethoven', '/8UlWHLMpgZm9bx6QYh0NFoq67TZ.jpg', '28|12|14')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (464052, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (582186, 'Old Yeller', '/8UlWHLMpgZm9bx6QYh0NFoq67TZ.jpg', '10751')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (582186, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (899338, 'Clifford', '/8UlWHLMpgZm9bx6QYh0NFoq67TZ.jpg', '10751')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (899338, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (664413, '101 Dalmations', '/8UlWHLMpgZm9bx6QYh0NFoq67TZ.jpg', '10751')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (664413, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (50701, 'Marley & Me', '/8UlWHLMpgZm9bx6QYh0NFoq67TZ.jpg', '10751')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (59507, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (67308, 'Balto', '/8UlWHLMpgZm9bx6QYh0NFoq67TZ.jpg', '28|14')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (67308, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (403310, 'Snow Dogs', '/8UlWHLMpgZm9bx6QYh0NFoq67TZ.jpg', '28|14')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (403310, 2, 'Banned', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (26728, 'Garfield''s Fun Fest', '/vsCHc3vb5xdS9nJ5YdFSY8p2ujO.jpg', '35|16|10751')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (26728, 1, 'Favorite', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (56202, 'Pettson and Findus', '/8mGYnUkthgdvs0ol6gyjqpNYCoe.jpg', '35|16|10751')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (56202, 1, 'Favorite', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (16390, 'Scooby-Doo! and the Samurai Sword', '/qJ0UlWWrBn2fnAg4SifTJbDUuaf.jpg', '10751|9648|16|35|28|14')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (16390, 1, 'Favorite', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (587807, 'Tom & Jerry', '/8XZI9QZ7Pm3fVkigWJPbrXCMzjq.jpg', '10751|16|35')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (587807, 1, 'Favorite', 0)

INSERT INTO movieCards (movie_id, title, poster_path, genre_ids) VALUES (53002, 'Tom & Jerry', '/lNU2o8eMy0FjSpALCxtxf9lEZ1Q.jpg', '10751|16|35')
INSERT INTO users_excludedMovies (movie_id, user_id, opinion, removal_tracker) VALUES (53002, 1, 'Favorite', 0)
GO
