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

-- Дамп структуры для таблица Students.Spec
CREATE TABLE IF NOT EXISTS "Spec" (
	"IdSpec" NUMERIC(18,0) NOT NULL,
	"IdSciences" NUMERIC(18,0) NULL DEFAULT NULL,
	"Sifr" VARCHAR(25) NOT NULL COLLATE 'Cyrillic_General_CI_AS',
	"IdSpecZ" NUMERIC(18,0) NULL DEFAULT NULL,
	"NameRus" VARCHAR(255) NOT NULL COLLATE 'Cyrillic_General_CI_AS',
	"ShortNameRus" VARCHAR(255) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"NameBel" VARCHAR(255) NOT NULL COLLATE 'Cyrillic_General_CI_AS',
	"ShortNameBel" VARCHAR(100) NOT NULL COLLATE 'Cyrillic_General_CI_AS',
	"NameEng" VARCHAR(255) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"ShortNameEng" VARCHAR(100) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"DateB" SMALLDATETIME NOT NULL,
	"DateE" SMALLDATETIME NULL DEFAULT NULL,
	"EditUser" VARCHAR(50) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"EditTime" SMALLDATETIME NULL DEFAULT NULL,
	"KodOld" CHAR(2) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"rowguid" UNIQUEIDENTIFIER NOT NULL,
	"Name_P_Roditelny" VARCHAR(255) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"Name_P_Datelny" VARCHAR(255) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"Name_P_Vinitelny" VARCHAR(255) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"Name_P_Tvoritelny" VARCHAR(255) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	"Name_P_Predlozhny" VARCHAR(255) NULL DEFAULT NULL COLLATE 'Cyrillic_General_CI_AS',
	FOREIGN KEY INDEX "FK_SPEC_REF_291_SPEC" ("IdSpecZ"),
	FOREIGN KEY INDEX "FK_Spec_Sciences" ("IdSciences"),
	PRIMARY KEY ("IdSpec"),
	CONSTRAINT "FK_SPEC_REF_291_SPEC" FOREIGN KEY ("IdSpecZ") REFERENCES "Spec" ("IdSpec") ON UPDATE NO_ACTION ON DELETE NO_ACTION,
	CONSTRAINT "FK_Spec_Sciences" FOREIGN KEY ("IdSciences") REFERENCES "Sciences" ("IdSciences") ON UPDATE NO_ACTION ON DELETE NO_ACTION
);

-- Экспортируемые данные не выделены.

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
