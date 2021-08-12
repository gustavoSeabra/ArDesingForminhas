using ArDesignForminhas_Web.Interfaces;
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

        private const string SQL_LISTAR_PRODUTO = @"SELECT	p.Codigo,
		                                                    p.CodCategoria,
		                                                    p.Nome,
                                                            p.Valor,
                                                            p.Descricao,
                                                            i.idProduto,
                                                            i.idProduto,
                                                            i.nome,
                                                            i.caminho
                                                    FROM produto p left join imagemproduto i
		                                                    on p.Codigo = i.idProduto";

        private const string SQL_SELECIONAR_PRODUTO = @"select  Codigo,
                                                                Nome,
                                                                Descricao,
                                                                Valor,
                                                                CodCategoria
                                                        from produto
                                                        where Codigo = @Codigo";

        private const string SQL_ADICIONAR_PRODUTO = @"insert into produto(Nome, Descricao, Valor, CodCategoria)
                                                        values (@Nome, @Descricao, @Valor, @CodCategoria);
                                                        SELECT LAST_INSERT_ID();";

        private const string SQL_EDITAR_PRODUTO = @"update produto set
	                                                    Nome = @Nome,
                                                        Descricao = @Descricao,
                                                        Valor = @Valor,
                                                        CodCategoria = @CodCategoria
                                                    where Codigo = @Codigo";

        private const string SQL_EXCLUIR_PRODUTO = @"delete from produto where Codigo = @Codigo";

        private const string SQL_OBTER_PRODUTO_IMAGEM = @"select	idimagem,
		                                                            idProduto,
		                                                            nome,
                                                                    caminho
                                                            from imagemproduto
                                                            where idProduto = @Codigo";

        private const string SQL_INSERT_PRODUTO_IMAGEM = @"INSERT INTO imagemproduto
                                                            (`idProduto`,
                                                            `nome`,
                                                            `caminho`)
                                                            VALUES
                                                            (@idProduto,
                                                             @nome,
                                                             @caminho)";

        private const string SQL_EXCLUIR_PRODUTO_IMAGEM = @"delete from imagemproduto where idProduto = @idProduto";

        #endregion

        public int Adicionar(Produto objProduto)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Nome", objProduto.Nome, System.Data.DbType.String, null, 100);
            parametros.Add("Descricao", objProduto.Descricao, System.Data.DbType.String, null, 500);
            parametros.Add("Valor", objProduto.Valor, System.Data.DbType.Decimal);
            parametros.Add("CodCategoria", objProduto.CodCategoria, System.Data.DbType.Int32);

            return Contexto.ExecutarScalar(SQL_ADICIONAR_PRODUTO, parametros);
        }

        public void AdicionarImagemProduto(IEnumerable<ImagemProduto> lstImagemProduto)
        {
            foreach (ImagemProduto objImagem in lstImagemProduto)
            {
                var parametros = new DynamicParameters();

                parametros.Add("idProduto", objImagem.IdProduto, System.Data.DbType.Int32);
                parametros.Add("nome", objImagem.Nome, System.Data.DbType.String, null, 50);
                parametros.Add("caminho", objImagem.Caminho, System.Data.DbType.String, null, 1000);

                Contexto.Executar(SQL_INSERT_PRODUTO_IMAGEM, parametros);
            }
        }

        public int Editar(Produto objProduto)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", objProduto.Codigo, System.Data.DbType.Int32);
            parametros.Add("Nome", objProduto.Nome, System.Data.DbType.String, null, 100);
            parametros.Add("Descricao", objProduto.Descricao, System.Data.DbType.String, null, 500);
            parametros.Add("Valor", objProduto.Valor, System.Data.DbType.Decimal);
            parametros.Add("CodCategoria", objProduto.CodCategoria, System.Data.DbType.Int32);

            return Contexto.Executar(SQL_EDITAR_PRODUTO, parametros);
        }

        public void Excluir(int codProduto)
        {
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", codProduto, System.Data.DbType.Int32);

            Contexto.Executar(SQL_EXCLUIR_PRODUTO, parametros);
        }

        public void ExcluirImagemProduto(int codProduto)
        {
            var parametros = new DynamicParameters();

            parametros.Add("idProduto", codProduto, System.Data.DbType.Int32);

            Contexto.Executar(SQL_EXCLUIR_PRODUTO_IMAGEM, parametros);
        }

        public List<Produto> Listar()
        {
            List<Produto> retorno = new List<Produto>();
            Contexto.Conexao.Query<Produto, ImagemProduto, Produto>(SQL_LISTAR_PRODUTO,
                (p, i) =>
                {
                    if(p.Imagens == null)
                        p.Imagens = new List<ImagemProduto>();

                    if(i!=null)
                    {
                        if (p.Codigo == i.IdProduto)
                            p.Imagens.Add(i);
                    }
                    

                    var result = retorno.FirstOrDefault(x => x.Codigo  == p.Codigo);

                    if (result != null)
                    {
                        if (i != null)
                            result.Imagens.Add(i);
                    }
                    else
                    {
                        retorno.Add(p);
                    }
                    return p;
                }, splitOn: "Codigo, idProduto");

            return retorno;
        }

        public List<Produto> ListarDestaque()
        {
            var lista = this.Listar();
            var random = new Random();
           
            return lista.Skip(random.Next(lista.Count)).Take(3).ToList();
        }

        public List<Produto> ListarPorCategoria(int codCategoria)
        {
            var parametros = new DynamicParameters();
            var sql = SQL_LISTAR_PRODUTO;
            var retorno = new List<Produto>();

            sql += " where CodCategoria = " + codCategoria;

            Contexto.Conexao.Query<Produto, ImagemProduto, Produto>(sql,
                (p, i) =>
                {
                    if (p.Imagens == null)
                        p.Imagens = new List<ImagemProduto>();

                    if (i != null)
                    {
                        if (p.Codigo == i.IdProduto)
                            p.Imagens.Add(i);
                    }


                    var result = retorno.FirstOrDefault(x => x.Codigo == p.Codigo);

                    if (result != null)
                    {
                        if (i != null)
                            result.Imagens.Add(i);
                    }
                    else
                    {
                        retorno.Add(p);
                    }
                    return p;
                }, splitOn: "Codigo, idProduto");

            return retorno;
        }

        public Produto ObeterPorCodigo(int codProduto)
        {
            
            var parametros = new DynamicParameters();

            parametros.Add("Codigo", codProduto, System.Data.DbType.Int32);
            var objProduto = Contexto.Obter<Produto>(SQL_SELECIONAR_PRODUTO, parametros);

            objProduto.Imagens = Contexto.Listar<ImagemProduto>(SQL_OBTER_PRODUTO_IMAGEM, parametros).ToList();

            return objProduto;
        }
    }
}