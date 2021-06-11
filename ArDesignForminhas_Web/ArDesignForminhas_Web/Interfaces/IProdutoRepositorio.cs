using ArDesignForminhas_Web.Models;
using System.Collections.Generic;


namespace ArDesignForminhas_Web.Interfaces
{
    public interface IProdutoRepositorio
    {
        Produto ObeterPorCodigo(int codProduto);
        List<Produto> Listar();
        List<Produto> ListarPorCategoria(int codCategoria);
        void Adicionar(Produto objProduto);
        int Editar(Produto objProduto);
        void Excluir(int codProduto);
        int ObterProximoID();
    }
}
