using ArDesignForminhas_Web.Models;
using System.Collections.Generic;


namespace ArDesignForminhas_Web.Interfaces
{
    public interface IProdutoRepositorio
    {
        Produto ObeterPorCodigo(int codProduto);
        List<Produto> Listar();
        List<Produto> ListarPorCategoria(int codCategoria);
        List<Produto> ListarDestaque();
        int Adicionar(Produto objProduto);
        void AdicionarImagemProduto(IEnumerable<ImagemProduto> lstImagemProduto);
        void ExcluirImagemProduto(int codProduto);
        int Editar(Produto objProduto);
        void Excluir(int codProduto);
    }
}
