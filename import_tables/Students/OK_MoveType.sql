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

-- Дамп структуры для таблица Students.OK_MoveType
CREATE TABLE IF NOT EXISTS "OK_MoveType" (
	"IdMoveType" NUMERIC(18,0) NOT NULL,
	"Name" VARCHAR(100) NOT NULL COLLATE 'Cyrillic_General_CI_AS',
	"IdPU" SMALLINT NULL DEFAULT NULL,
	"SortOrder" TINYINT NOT NULL,
	"rowguid" UNIQUEIDENTIFIER NOT NULL,
	"IsAffectOnCreditEvents " BIT NOT NULL,
	"IdTypeReCalculation" INT NULL DEFAULT NULL,
	"IdKeyword" NUMERIC(18,0) NULL DEFAULT NULL,
	"ControlProgId" VARCHAR(50) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	FOREIGN KEY INDEX "FK_OK_MoveType_O_Keyword" ("IdKeyword"),
	FOREIGN KEY INDEX "FK_OK_MoveType_OK_PU" ("IdPU"),
	FOREIGN KEY INDEX "FK_OK_MoveType_p_TypeReCalculation" ("IdTypeReCalculation"),
	PRIMARY KEY ("IdMoveType"),
	CONSTRAINT "FK_OK_MoveType_OK_PU" FOREIGN KEY ("IdPU") REFERENCES "OK_PU" ("IdPU") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK_OK_MoveType_p_TypeReCalculation" FOREIGN KEY ("IdTypeReCalculation") REFERENCES "p_TypeReCalculation" ("IdTypeReCalculation") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK_OK_MoveType_O_Keyword" FOREIGN KEY ("IdKeyword") REFERENCES "O_Keyword" ("IdKeyword") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Экспортируемые данные не выделены.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
