CREATE DATABASE `ardesingforminhas` /*!40100 DEFAULT CHARACTER SET utf8 */;

use `ardesingforminhas`;

CREATE TABLE `categoria` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `CodigoPai` int(11) DEFAULT NULL,
  `Nome` varchar(100) DEFAULT NULL,
  `Descricao` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/***************************************************/

CREATE TABLE `produto` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) DEFAULT NULL,
  `Descricao` varchar(500) DEFAULT NULL,
  `Valor` decimal(18,2) NOT NULL,
  PRIMARY KEY (`Codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


/******************************************************/

CREATE TABLE `imagem` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `Caminho` varchar(500) DEFAULT NULL,
  `EhHome` tinyint(1) NOT NULL,
  `Produto_Codigo` int(11) DEFAULT NULL,
  PRIMARY KEY (`Codigo`),
  KEY `IX_Produto_Codigo` (`Produto_Codigo`) USING HASH,
  CONSTRAINT `FK_Imagems_Produtoes_Produto_Codigo` FOREIGN KEY (`Produto_Codigo`) REFERENCES `produto` (`Codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;