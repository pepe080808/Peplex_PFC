USE master;
GO

/* Create the database */
DECLARE @device_directory NVARCHAR(520)
PRINT 'Fetching database path'
--SELECT @device_directory = SUBSTRING(filename, 1, CHARINDEX(N'master.mdf', LOWER(filename)) - 1)
--FROM master.dbo.sysaltfiles WHERE dbid = 1 AND fileid = 1

SET @device_directory = 'D:\\Peplex\\';

PRINT @device_directory

EXECUTE (N'CREATE DATABASE CENTRAL_PEPLEX_DB
  ON PRIMARY (NAME = N''CENTRAL_PEPLEX_DB'', FILENAME = N''' + @device_directory + N'CENTRAL_PEPLEX_DB.mdf'', SIZE = 100MB , MAXSIZE = UNLIMITED, FILEGROWTH = 100MB)
  LOG ON (NAME = N''CENTRAL_PEPLEX_DB_log'',  FILENAME = N''' + @device_directory + N'CENTRAL_PEPLEX_DB.ldf'')')

GO

/* Use the database in this script */
USE CENTRAL_PEPLEX_DB;
GO

ALTER DATABASE CENTRAL_PEPLEX_DB SET RECOVERY SIMPLE;
GO

