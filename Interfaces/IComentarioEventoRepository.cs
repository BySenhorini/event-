﻿using Eventplus_api_senai.Domains;

namespace Eventplus_api_senai.Interfaces
{
    public interface IComentariosEventosRepository
    {
        void Cadastrar(ComentariosEventos comentarioEvento);
        void Deletar(Guid id);
        List<ComentariosEventos> Listar(Guid id);
        ComentariosEventos BuscarPorIdUsuario(Guid idUsuario, Guid idEvento);

        List<ComentariosEventos> ListarSomenteExibe(Guid id);
    }
}