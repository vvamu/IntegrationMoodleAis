-- --------------------------------------------------------
-- Хост:                         172.16.0.248\bstusqlserver
-- Версия сервера:               Microsoft SQL Server 2019 (RTM) - 15.0.2000.5
-- Операционная система:         Windows Server 2019 Standard 10.0 <X64> (Build 17763: )
-- HeidiSQL Версия:              12.0.0.6468
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES  */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- Дамп структуры для таблица Students.OK_MoveTypeSub_D
CREATE TABLE IF NOT EXISTS "OK_MoveTypeSub_D" (
	"IdOperation" NUMERIC(18,0) NOT NULL,
	"IsDeleted" BIT NOT NULL,
	"EditUser_D" VARCHAR(128) NOT NULL DEFAULT '' COLLATE 'Cyrillic_General_CI_AS',
	"EditDate_D" DATETIME NOT NULL DEFAULT '',
	"IdMoveTypeSub" NUMERIC(18,0) NOT NULL,
	"IdMoveType" NUMERIC(18,0) NOT NULL,
	"Name" VARCHAR(255) NOT NULL COLLATE 'Cyrillic_General_CI_AS',
	"ShortName" VARCHAR(50) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"Notes" VARCHAR(255) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"SortOrder" TINYINT NOT NULL,
	PRIMARY KEY ("IdOperation")
);

-- Экспортируемые данные не выделены.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
