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

-- Дамп структуры для таблица Students.Groups
CREATE TABLE IF NOT EXISTS "Groups" (
	"IdGroup" NUMERIC(18,0) NOT NULL,
	"Name" VARCHAR(10) NOT NULL COLLATE 'Cyrillic_General_CI_AS',
	"Kurator" VARCHAR(255) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"IdF" NUMERIC(18,0) NOT NULL,
	"IdFormaTime" NUMERIC(18,0) NOT NULL,
	"Year" SMALLINT NOT NULL,
	"IdCaptain" NUMERIC(18,0) NULL DEFAULT NULL,
	"rowguid" UNIQUEIDENTIFIER NOT NULL,
	"IdEduType" NUMERIC(18,0) NOT NULL,
	FOREIGN KEY INDEX "FK_Groups_EduType" ("IdEduType"),
	FOREIGN KEY INDEX "FK_Groups_Facultets" ("IdF"),
	FOREIGN KEY INDEX "FK_Groups_FormaTime" ("IdFormaTime"),
	FOREIGN KEY INDEX "FK_Groups_Years" ("Year"),
	PRIMARY KEY ("IdGroup"),
	CONSTRAINT "FK_Groups_FormaTime" FOREIGN KEY ("IdFormaTime") REFERENCES "FormaTime" ("IdFormaTime") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK_Groups_Years" FOREIGN KEY ("Year") REFERENCES "Years" ("Year") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK_Groups_EduType" FOREIGN KEY ("IdEduType") REFERENCES "EduType" ("IdEduType") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK_Groups_Facultets" FOREIGN KEY ("IdF") REFERENCES "Facultets" ("IdF") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Экспортируемые данные не выделены.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
