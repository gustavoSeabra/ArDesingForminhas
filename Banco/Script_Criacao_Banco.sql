CREATE DATABASE `ardesingforminhas` /*!40100 DEFAULT CHARACTER SET utf8 */;

use `ardesingforminhas`;

CREATE TABLE `categoria` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) DEFAULT NULL,
  `Descricao` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`Codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;


/***************************************************/

CREATE TABLE `produto` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(100) DEFAULT NULL,
  `Descricao` varchar(500) DEFAULT NULL,
  `Valor` decimal(18,2) NOT NULL,
  `CodCategoria` int(11) NOT NULL,
  PRIMARY KEY (`Codigo`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8;


/******************************************************/

CREATE TABLE `imagem` (
  `Codigo` int(11) NOT NULL AUTO_INCREMENT,
  `Caminho` varchar(500) DEFAULT NULL,
  `EhHome` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`Codigo`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


/******************************************************/

CREATE TABLE `imagemproduto` (
  `idimagem` int(11) NOT NULL AUTO_INCREMENT,
  `idProduto` int(11) NOT NULL,
  `nome` varchar(50) DEFAULT NULL,
  `caminho` varchar(1000) DEFAULT NULL,
  PRIMARY KEY (`idimagem`),
  KEY `iIdProduto` (`idProduto`),
  CONSTRAINT `fkCodigoProduto` FOREIGN KEY (`idProduto`) REFERENCES `produto` (`Codigo`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
