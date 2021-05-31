using ArDesignForminhas_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArDesignForminhas_Web.Interfaces
{
    public interface ICategoriaRepositorio
    {
        Categoria ObeterPorCodigo(int codCategoria);
        List<Categoria> Listar(string nome);
        List<Categoria> ListarCategoriaPai();
        void Adicionar(Categoria objCategoria);
        int Editar(Categoria objCategoria);
        void Excluir(int codCategoria);
    }
}
