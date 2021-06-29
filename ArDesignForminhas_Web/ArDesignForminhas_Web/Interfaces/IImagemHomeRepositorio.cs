using ArDesignForminhas_Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArDesignForminhas_Web.Interfaces
{
    public interface IBannerRepositorio
    {
        Banner ObeterPorCodigo(int codBanner);
        List<Banner> Listar();
        void Adicionar(Banner objBanner);
        int Editar(Banner objBanner);
        void Excluir(int codBanner);
    }
}
