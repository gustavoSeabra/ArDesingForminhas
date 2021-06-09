using ArDesignForminhas_Web.Models;
using System.Collections.Generic;


namespace ArDesignForminhas_Web.Interfaces
{
    public interface ICategoriaRepositorio
    {
        Categoria ObeterPorCodigo(int codCategoria);
        List<Categoria> Listar(string nome);
        void Adicionar(Categoria objCategoria);
        int Editar(Categoria objCategoria);
        void Excluir(int codCategoria);
    }
}
