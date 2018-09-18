
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/18/2018 22:35:34
-- Generated from EDMX file: \\mac\iCloud\Step\TestsProject\Kursak_Ol\Tests_DB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Tests_DB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_UserRole];
GO
IF OBJECT_ID(N'[dbo].[FK_TestCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Test] DROP CONSTRAINT [FK_TestCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_TestQuestionTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestQuestionSet] DROP CONSTRAINT [FK_TestQuestionTest];
GO
IF OBJECT_ID(N'[dbo].[FK_TestQuestionAnswerTestQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestQuestionAnswer] DROP CONSTRAINT [FK_TestQuestionAnswerTestQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTestCreator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestCreator] DROP CONSTRAINT [FK_UserTestCreator];
GO
IF OBJECT_ID(N'[dbo].[FK_TestTestCreator]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TestCreator] DROP CONSTRAINT [FK_TestTestCreator];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTestUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTest] DROP CONSTRAINT [FK_UserTestUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTestTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTest] DROP CONSTRAINT [FK_UserTestTest];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTestAnswerUserTest]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTestAnswer] DROP CONSTRAINT [FK_UserTestAnswerUserTest];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTestAnswerTestQuestion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTestAnswer] DROP CONSTRAINT [FK_UserTestAnswerTestQuestion];
GO
IF OBJECT_ID(N'[dbo].[FK_TestQuestionAnswerUserTestAnswer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserTestAnswer] DROP CONSTRAINT [FK_TestQuestionAnswerUserTestAnswer];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Test]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Test];
GO
IF OBJECT_ID(N'[dbo].[TestCreator]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestCreator];
GO
IF OBJECT_ID(N'[dbo].[TestQuestionSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestQuestionSet];
GO
IF OBJECT_ID(N'[dbo].[TestQuestionAnswer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TestQuestionAnswer];
GO
IF OBJECT_ID(N'[dbo].[UserTest]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTest];
GO
IF OBJECT_ID(N'[dbo].[UserTestAnswer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserTestAnswer];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Role'
CREATE TABLE [dbo].[Role] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(30)  NOT NULL,
    [Password] nvarchar(255)  NOT NULL,
    [LastName] nvarchar(30)  NOT NULL,
    [FirstName] nvarchar(30)  NOT NULL,
    [MiddleName] nvarchar(30)  NOT NULL,
    [Phone] nchar(12)  NOT NULL,
    [Address] nvarchar(255)  NOT NULL,
    [RoleId] int  NOT NULL
);
GO

-- Creating table 'Category'
CREATE TABLE [dbo].[Category] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Test'
CREATE TABLE [dbo].[Test] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(70)  NOT NULL,
    [IsActual] tinyint  NOT NULL,
    [CategoryId] int  NOT NULL
);
GO

-- Creating table 'TestCreator'
CREATE TABLE [dbo].[TestCreator] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [TestId] int  NOT NULL
);
GO

-- Creating table 'TestQuestion'
CREATE TABLE [dbo].[TestQuestion] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Question] nvarchar(max)  NOT NULL,
    [IsActual] tinyint  NOT NULL,
    [TestId] int  NOT NULL
);
GO

-- Creating table 'TestQuestionAnswer'
CREATE TABLE [dbo].[TestQuestionAnswer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Answer] nvarchar(255)  NOT NULL,
    [IsAnswer] tinyint  NOT NULL,
    [TestQuestionId] int  NOT NULL
);
GO

-- Creating table 'UserTest'
CREATE TABLE [dbo].[UserTest] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [TestId] int  NOT NULL,
    [Result] nvarchar(max)  NOT NULL,
    [StartDate] datetime  NOT NULL,
    [EndDate] datetime  NOT NULL
);
GO

-- Creating table 'UserTestAnswer'
CREATE TABLE [dbo].[UserTestAnswer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserTestId] int  NOT NULL,
    [TestQuestionId] int  NOT NULL,
    [UserTestQuestionAnswerId] int  NOT NULL,
    [AnswerDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Test'
ALTER TABLE [dbo].[Test]
ADD CONSTRAINT [PK_Test]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestCreator'
ALTER TABLE [dbo].[TestCreator]
ADD CONSTRAINT [PK_TestCreator]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestQuestion'
ALTER TABLE [dbo].[TestQuestion]
ADD CONSTRAINT [PK_TestQuestion]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TestQuestionAnswer'
ALTER TABLE [dbo].[TestQuestionAnswer]
ADD CONSTRAINT [PK_TestQuestionAnswer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserTest'
ALTER TABLE [dbo].[UserTest]
ADD CONSTRAINT [PK_UserTest]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserTestAnswer'
ALTER TABLE [dbo].[UserTestAnswer]
ADD CONSTRAINT [PK_UserTestAnswer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleId] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_UserRole]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Role]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole'
CREATE INDEX [IX_FK_UserRole]
ON [dbo].[User]
    ([RoleId]);
GO

-- Creating foreign key on [CategoryId] in table 'Test'
ALTER TABLE [dbo].[Test]
ADD CONSTRAINT [FK_TestCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Category]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestCategory'
CREATE INDEX [IX_FK_TestCategory]
ON [dbo].[Test]
    ([CategoryId]);
GO

-- Creating foreign key on [TestId] in table 'TestQuestion'
ALTER TABLE [dbo].[TestQuestion]
ADD CONSTRAINT [FK_TestQuestionTest]
    FOREIGN KEY ([TestId])
    REFERENCES [dbo].[Test]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestQuestionTest'
CREATE INDEX [IX_FK_TestQuestionTest]
ON [dbo].[TestQuestion]
    ([TestId]);
GO

-- Creating foreign key on [TestQuestionId] in table 'TestQuestionAnswer'
ALTER TABLE [dbo].[TestQuestionAnswer]
ADD CONSTRAINT [FK_TestQuestionAnswerTestQuestion]
    FOREIGN KEY ([TestQuestionId])
    REFERENCES [dbo].[TestQuestion]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestQuestionAnswerTestQuestion'
CREATE INDEX [IX_FK_TestQuestionAnswerTestQuestion]
ON [dbo].[TestQuestionAnswer]
    ([TestQuestionId]);
GO

-- Creating foreign key on [UserId] in table 'TestCreator'
ALTER TABLE [dbo].[TestCreator]
ADD CONSTRAINT [FK_UserTestCreator]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTestCreator'
CREATE INDEX [IX_FK_UserTestCreator]
ON [dbo].[TestCreator]
    ([UserId]);
GO

-- Creating foreign key on [TestId] in table 'TestCreator'
ALTER TABLE [dbo].[TestCreator]
ADD CONSTRAINT [FK_TestTestCreator]
    FOREIGN KEY ([TestId])
    REFERENCES [dbo].[Test]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestTestCreator'
CREATE INDEX [IX_FK_TestTestCreator]
ON [dbo].[TestCreator]
    ([TestId]);
GO

-- Creating foreign key on [UserId] in table 'UserTest'
ALTER TABLE [dbo].[UserTest]
ADD CONSTRAINT [FK_UserTestUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTestUser'
CREATE INDEX [IX_FK_UserTestUser]
ON [dbo].[UserTest]
    ([UserId]);
GO

-- Creating foreign key on [TestId] in table 'UserTest'
ALTER TABLE [dbo].[UserTest]
ADD CONSTRAINT [FK_UserTestTest]
    FOREIGN KEY ([TestId])
    REFERENCES [dbo].[Test]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTestTest'
CREATE INDEX [IX_FK_UserTestTest]
ON [dbo].[UserTest]
    ([TestId]);
GO

-- Creating foreign key on [UserTestId] in table 'UserTestAnswer'
ALTER TABLE [dbo].[UserTestAnswer]
ADD CONSTRAINT [FK_UserTestAnswerUserTest]
    FOREIGN KEY ([UserTestId])
    REFERENCES [dbo].[UserTest]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTestAnswerUserTest'
CREATE INDEX [IX_FK_UserTestAnswerUserTest]
ON [dbo].[UserTestAnswer]
    ([UserTestId]);
GO

-- Creating foreign key on [TestQuestionId] in table 'UserTestAnswer'
ALTER TABLE [dbo].[UserTestAnswer]
ADD CONSTRAINT [FK_UserTestAnswerTestQuestion]
    FOREIGN KEY ([TestQuestionId])
    REFERENCES [dbo].[TestQuestion]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTestAnswerTestQuestion'
CREATE INDEX [IX_FK_UserTestAnswerTestQuestion]
ON [dbo].[UserTestAnswer]
    ([TestQuestionId]);
GO

-- Creating foreign key on [UserTestQuestionAnswerId] in table 'UserTestAnswer'
ALTER TABLE [dbo].[UserTestAnswer]
ADD CONSTRAINT [FK_TestQuestionAnswerUserTestAnswer]
    FOREIGN KEY ([UserTestQuestionAnswerId])
    REFERENCES [dbo].[TestQuestionAnswer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TestQuestionAnswerUserTestAnswer'
CREATE INDEX [IX_FK_TestQuestionAnswerUserTestAnswer]
ON [dbo].[UserTestAnswer]
    ([UserTestQuestionAnswerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

INSERT INTO dbo.[Role] VALUES
('Преподаватель'),
('Студент');
GO

INSERT INTO dbo.[User] VALUES
('stdudent', '123123', 'Интерперунников', 'Авегундар', 'Мыхяцуевич', '380123214567', 'Нету адреса', 2),
('teacher', '321321', 'Простой', 'Иван', 'Сергеевич', '380554443322', 'Кабинетный переулок, д. 54', 1);
GO

INSERT INTO dbo.[Category] VALUES
('Первая категория для тестов'),
('Вторая категория для тестов');
GO

INSERT INTO dbo.[Test] VALUES
('Самый скучный тест на планете Нептун', 1, 1),
('О, смотри, какие тут вопросы!', 1, 1);
GO

INSERT INTO dbo.[TestCreator] VALUES
(2,1),
(2,2);
GO

INSERT INTO dbo.[TestQuestion] VALUES
('Ты суслика видишь?', 1, 1),
('Есть закурить?', 1, 1),
('А если найду?', 1, 1);
GO

INSERT INTO dbo.[TestQuestionAnswer] VALUES
('Да', 0, 1),
('Нет', 0, 1),
('Кто такой суслик?', 1, 1),
('Незнаю', 0, 1),
('Да', 1, 1),
('Нет', 0, 1),
('Я не курю и Вам не советую', 0, 1),
('И.....', 0, 1),
('Нечего искать!', 1, 1);
GO