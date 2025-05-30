﻿using Eventplus_api_senai.Context;
using Eventplus_api_senai.Domais;
using Eventplus_api_senai.Interfaces;
using webapi.event_.Utils;

namespace Eventplus_api_senai.Repository
{
    public class UsuariosRepository : IUsuarioRepository
    {
        private readonly Event_Context _context;

        public UsuariosRepository(Event_Context context)
        {
            _context = context;
        }

        public Usuario BuscarPorEmailESenha(string email, string senha)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuario
                    .Select(u => new Usuario
                    {
                        TipoUsuarioID = u.TipoUsuarioID,
                        UsuarioID = u.UsuarioID,
                        Email = u.Email,
                        Senha = u.Senha,

                        TipoUsuario = new TipoUsuario
                        {
                            TipoUsuarioID = u.TipoUsuarioID,
                            TituloTipoUsuario = u.TipoUsuario!.TituloTipoUsuario
                        }
                    }).FirstOrDefault(u => u.Email == email)!;

                if (usuarioBuscado != null)
                {
                    bool confere = Criptografia.CompararHash(senha, usuarioBuscado.Senha!);

                    if (confere)
                    {
                        return usuarioBuscado!;
                    }
                }
                return null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario BuscarPorId(Guid id)
        {
            try
            {
                Usuario usuarioBuscado = _context.Usuario
                    .Select(u => new Usuario
                    {
                        TipoUsuarioID = u.TipoUsuarioID,
                        UsuarioID = u.UsuarioID,
                        Email = u.Email,
                        Senha = u.Senha,

                        TipoUsuario = new TipoUsuario
                        {
                            TipoUsuarioID = u.TipoUsuario!.TipoUsuarioID,
                            TituloTipoUsuario = u.TipoUsuario!.TituloTipoUsuario
                        }

                    }).FirstOrDefault(u => u.UsuarioID == id)!;

                if (usuarioBuscado != null)
                {
                    return usuarioBuscado;

                }
                return null!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Cadastrar(Usuario usuario)
        {
            try
            {
                usuario.TipoUsuarioID = Guid.NewGuid();

                usuario.Senha = Criptografia.GerarHash(usuario.Senha!);


                _context.Usuario.Add(usuario);


                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}