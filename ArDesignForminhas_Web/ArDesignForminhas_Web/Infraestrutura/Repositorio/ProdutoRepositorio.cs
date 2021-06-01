﻿using ArDesignForminhas_Web.Interfaces;
using ArDesignForminhas_Web.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArDesignForminhas_Web.Infraestrutura.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        #region Querys´s

        private const string SQL_LISTAR_PRODUTO = @"select  Codigo,
                                                            Nome,
                                                            Descricao,
                                                            Valor,
                                                            CodCategoria,
                                                            CaminhoFoto
                                                    from produto";

        private const string SQL_SELECIONAR_PRODUTO = @"select  Codigo,
                                                                Nome,
                                                                Descricao,
                                                                Valor,
                                                                CodCategoria,
                                                                CaminhoFoto
                                                        from produto
                                                        where Codigo = @Codigo";

        private const string SQL_ADICIONAR_PRODUTO = @"insert into produto(Nome, Descricao, Valor, CodCategoria, CaminhoFoto)
                                                        values (@Nome, @Descricao, @Valor, @CodCategoria, @CaminhoFoto)";

        private const string SQL_EDITAR_PRODUTO = @"update produto set
	                                                    Nome = @Nome,
                                                        Descricao = @Descricao,
                                                        Valor = @Valor,
                                                        CodCategoria = @CodCategoria,
                                                        CaminhoFoto = @CaminhoFoto,
                                                    where Codigo = @Codigo";

        private const string SQL_EXCLUIR_PRODUTO = @"delete from produto where Codigo = @Codigo";


        #endregion

        public void Adicionar(Produto objProduto)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Nome", objProduto.Nome, System.Data.DbType.String, null, 100);
            parametros.Add("Descricao", objProduto.Descricao, System.Data.DbType.String, null, 500);
            parametros.Add("Valor", objProduto.Valor, System.Data.DbType.Decimal);
            parametros.Add("CodCategoria", objProduto.CodCategoria, System.Data.DbType.Int32);
            parametros.Add("CaminhoFoto", objProduto.CaminhoFoto, System.Data.DbType.String, null, 500);

            Contexto.Executar(SQL_ADICIONAR_PRODUTO, parametros);
        }

        public int Editar(Produto objProduto)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", objProduto.Codigo, System.Data.DbType.Int32);
            parametros.Add("Nome", objProduto.Nome, System.Data.DbType.String, null, 100);
            parametros.Add("Descricao", objProduto.Descricao, System.Data.DbType.String, null, 500);
            parametros.Add("Valor", objProduto.Valor, System.Data.DbType.Decimal);
            parametros.Add("CodCategoria", objProduto.CodCategoria, System.Data.DbType.Int32);
            parametros.Add("CaminhoFoto", objProduto.CaminhoFoto, System.Data.DbType.String, null, 500);

            return Contexto.Executar(SQL_EDITAR_PRODUTO, parametros);
        }

        public void Excluir(int codProduto)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", codProduto, System.Data.DbType.Int32);

            Contexto.Executar(SQL_EXCLUIR_PRODUTO, parametros);
        }

        public List<Produto> Listar()
        {
            var parametros = new DynamicParameters();

            return Contexto.Listar<Produto>(SQL_LISTAR_PRODUTO).ToList();
        }

        public List<Produto> ListarPorCategoria(int codCategoria)
        {
            var parametros = new DynamicParameters();
            var sql = SQL_LISTAR_PRODUTO;

            sql += "where CodCategoria = @CodCategoria";
            parametros.Add("CodCategoria", codCategoria, System.Data.DbType.Int32);

            return Contexto.Listar<Produto>(sql, parametros).ToList();
        }

        public Produto ObeterPorCodigo(int codProduto)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", codProduto, System.Data.DbType.Int32);
            return Contexto.Obter<Produto>(SQL_SELECIONAR_PRODUTO, parametros);
        }
    }
}