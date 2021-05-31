using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArDesignForminhas_Web.Infraestrutura.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio
    {
        #region Querys

        private const string SQL_LISTAR_CATEGORIA = @"
            SELECT Codigo,
                   CodigoPai,
                   Nome,
                   Descricao
            FROM categoria";

        private const string SQL_LISTAR_CATEGORIA_PAI = @"
            SELECT Codigo as CodigoPai,
                   Nome
            FROM categoria
            WHERE CodigoPai is null";

        private const string SQL_ATUALIZAR_CATEGORIA = @"
            UPDATE categoria
            SET
                CodigoPai = @CodigoPai,
                Nome = @Nome,
                Descricao = @Descricao
            WHERE Codigo = @Codigo";

        private const string SQL_INSERIR_CATEGORIA = @"
            INSERT INTO categoria (CodigoPai, Nome, Descricao)
            VALUES
            (@CodigoPai, @Nome, @Descricao)";

        private const string SQL_EXCLUI_CATEGORIA = @"DELETE FROM categoria WHERE Codigo = @Codigo";

        #endregion

        public void Adicionar(Categoria objCategoria)
        {
            var parametros = new DynamicParameters();
            
            parametros.Add("CodigoPai", objCategoria.CodigoPai);
            parametros.Add("Nome", objCategoria.Nome, System.Data.DbType.String, null, 100);
            parametros.Add("Descricao", objCategoria.Descricao , System.Data.DbType.String, null, 500);

            Contexto.Executar(SQL_INSERIR_CATEGORIA, parametros);
        }

        public int Editar(Categoria objCategoria)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", objCategoria.Codigo, System.Data.DbType.Int32);

            if (objCategoria.CodigoPai != null)
                parametros.Add("CodigoPai", objCategoria.CodigoPai, System.Data.DbType.Int32);
            else
                parametros.Add("CodigoPai", objCategoria.CodigoPai);

            parametros.Add("Nome", objCategoria.Nome, System.Data.DbType.String, null, 100);
            parametros.Add("Descricao", objCategoria.Descricao, System.Data.DbType.String, null, 500);

            return Contexto.Executar(SQL_ATUALIZAR_CATEGORIA, parametros);
        }

        public void Excluir(int codCategoria)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", codCategoria, System.Data.DbType.Int32);

            Contexto.Executar(SQL_EXCLUI_CATEGORIA, parametros);
        }

        public List<Categoria> Listar(string nome)
        {
            var parametros = new DynamicParameters();
            var sql = SQL_LISTAR_CATEGORIA;

            if (!string.IsNullOrEmpty(nome))
            {
                sql += "where Nome like '%@Nome%'";
                parametros.Add("Nome", nome, System.Data.DbType.String, null, 100);
            }

            return Contexto.Listar<Categoria>(sql, parametros).ToList();
        }

        public List<Categoria> ListarCategoriaPai()
        {
            var sql = SQL_LISTAR_CATEGORIA_PAI;

            return Contexto.Listar<Categoria>(sql, null).ToList();
        }

        public Categoria ObeterPorCodigo(int codCategoria)
        {
            var parametros = new DynamicParameters();
            var sql = SQL_LISTAR_CATEGORIA + " where Codigo = @Codigo";

            parametros.Add("Codigo", codCategoria, System.Data.DbType.Int32);

            return Contexto.Obter<Categoria>(sql, parametros);
        }
    }
}