using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArDesignForminhas_Web.Infraestrutura.Repositorio
{
    public class BannerRepositorio : IBannerRepositorio
    {
        private const string SQL_LISTAR_IMAGEM = @"select	Codigo,
		                                                    Caminho,
		                                                    EhHome
                                                    from imagem";

        private const string SQL_OBTER_IMAGEM = @"select	Codigo,
		                                                    Caminho,
		                                                    EhHome
                                                    from imagem
                                                    WHERE Codigo = @Codigo";

        private const string SQL_INSERIR_IMAGEM = @"INSERT INTO imagem (`Caminho`,`EhHome`)
                                                    VALUES (@Caminho, @EhHome);
                                                    SELECT LAST_INSERT_ID();";

        private const string SQL_EXCLUIR_IMAGEM = @"DELETE FROM imagem WHERE Codigo = @Codigo";

        private const string SQL_EDITAR_IMAGEM = @"UPDATE imagem
                                                    SET
                                                    EhHome = @EhHome,
                                                    Caminho = @Caminho
                                                    WHERE Codigo = @Codigo;";

        public int Adicionar(Banner objBanner)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Caminho", objBanner.Caminho, System.Data.DbType.String, null, 500);
            parametros.Add("EhHome", objBanner.EhHome, System.Data.DbType.Boolean);

            return Contexto.ExecutarScalar(SQL_INSERIR_IMAGEM, parametros);
        }

        public int Editar(Banner objBanner)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Caminho", objBanner.Caminho, System.Data.DbType.String, null, 500);
            parametros.Add("Codigo", objBanner.Codigo, System.Data.DbType.Int32);
            parametros.Add("EhHome", objBanner.EhHome, System.Data.DbType.Boolean);

            return Contexto.Executar(SQL_EDITAR_IMAGEM, parametros);
        }

        public void Excluir(int codBanner)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", codBanner, System.Data.DbType.Int32);

             Contexto.Executar(SQL_EXCLUIR_IMAGEM, parametros);
        }

        public List<Banner> Listar()
        {
            return Contexto.Listar<Banner>(SQL_LISTAR_IMAGEM).ToList();
        }

        public Banner ObeterPorCodigo(int codBanner)
        {
            // SQL_OBTER_IMAGEM
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", codBanner, System.Data.DbType.Int32);

            return Contexto.Obter<Banner>(SQL_OBTER_IMAGEM, parametros);
        }
    }
}