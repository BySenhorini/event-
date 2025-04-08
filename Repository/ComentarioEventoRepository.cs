using Eventplus_api_senai.Context;
using Eventplus_api_senai.Domains;
using Eventplus_api_senai.Domais;
using Eventplus_api_senai.Interfaces;

namespace Eventplus_api_senai.Repository
{
    public class ComentariosEventosRepository : IComentariosEventosRepository
    {

        private readonly Event_Context _context;

        public ComentariosEventosRepository(Event_Context context)
        {
            _context = context;
        }
        public ComentariosEventos BuscarPorIdUsuario(Guid idUsuario, Guid idEvento)
        {
            try
            {
                return _context.ComentariosEventos
                    .Select(c => new ComentariosEventos
                    {
                        ComentarioEvento = c.ComentarioEvento,
                        Descricao = c.Descricao,
                        Exibe = c.Exibe,
                        IdUsuario = c.IdUsuario,
                        IdEvento = c.IdEvento,

                        Usuario = new Usuario
                        {
                            TipoUsuario = c.Usuario!.TipoUsuario
                        },

                        Evento = new Evento
                        {
                            NomeEvento = c.Evento!.NomeEvento,
                        }

                    }).FirstOrDefault(c => c.IdUsuario == idUsuario && c.IdEvento == idEvento)!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Cadastrar(ComentariosEventos comentarioEvento)
        {
            try
            {
                comentarioEvento.ComentarioEvento = Guid.NewGuid();

                _context.ComentariosEventos.Add(comentarioEvento);

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Deletar(Guid id)
        {
            try
            {
                ComentariosEventos comentarioEventoBuscado = _context.ComentariosEventos.Find(id)!;

                if (comentarioEventoBuscado != null)
                {
                    _context.ComentariosEventos.Remove(comentarioEventoBuscado);
                }

                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ComentariosEventos> Listar(Guid id)
        {
            try
            {
                return _context.ComentariosEventos
                    .Select(c => new ComentariosEventos
                    {
                        ComentarioEvento = c.ComentarioEvento,
                        Descricao = c.Descricao,
                        Exibe = c.Exibe,
                        IdUsuario = c.IdUsuario,
                        IdEvento = c.IdEvento,

                        Usuario = new Usuario
                        {
                            TipoUsuario = c.Usuario!.TipoUsuario
                        },

                        Evento = new Evento
                        {
                            NomeEvento = c.Evento!.NomeEvento,
                        }

                    }).Where(c => c.IdEvento == id).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ComentariosEventos> ListarSomenteExibe(Guid id)
        {
            try
            {
                return _context.ComentariosEventos
                    .Select(c => new ComentariosEventos
                    {
                        ComentarioEvento = c.ComentarioEvento,
                        Descricao = c.Descricao,
                        Exibe = c.Exibe,
                        IdUsuario = c.IdUsuario,
                        IdEvento = c.IdEvento,

                        Usuario = new Usuario
                        {
                            TipoUsuario = c.Usuario!.TipoUsuario
                        },

                        Evento = new Evento
                        {
                            NomeEvento = c.Evento!.NomeEvento,
                        }

                    }).Where(c => c.Exibe == true && c.IdEvento == id).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}