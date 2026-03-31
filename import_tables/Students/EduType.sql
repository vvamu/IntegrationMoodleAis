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

-- Дамп структуры для таблица Students.EduType
CREATE TABLE IF NOT EXISTS "EduType" (
	"IdEduType" NUMERIC(18,0) NOT NULL,
	"Name" VARCHAR(50) NOT NULL COLLATE 'Cyrillic_General_CI_AS',
	"periodEdu" NUMERIC(5,2) NOT NULL,
	"rowguid" UNIQUEIDENTIFIER NOT NULL,
	"SortOrder" TINYINT NULL DEFAULT NULL,
	PRIMARY KEY ("IdEduType")
);

-- Экспортируемые данные не выделены.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
