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

CREATE TABLE user_acct (
	acct_id int IDENTITY(1,1) NOT NULL,
	user_id int NOT NULL,
	favorite_movies varchar(300) NULL,

	CONSTRAINT PK_user_acct PRIMARY KEY (acct_id),
	CONSTRAINT FK_user_acct_users FOREIGN KEY (user_id) references users (user_id)

)

CREATE TABLE genres (
	genre_id int IDENTITY(1, 1) NOT NULL,
	genre_name varchar(50) NOT NULL
	CONSTRAINT PK_genres PRIMARY KEY (genre_id)
)

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

GO