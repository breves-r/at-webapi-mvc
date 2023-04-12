using AT.Entidade;
using AT.Repositorio;
using Services.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UsuarioService
    {
        private AtContext _context;

        public UsuarioService(AtContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> ObterUsuario()
        {
            return _context.Usuarios;
        }


        public Usuario ObterUsuarioPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public Usuario SalvarUsuario(Usuario usuario)
        {
            var user = this._context.Usuarios.FirstOrDefault(x => x.Email == usuario.Email);

            if (user != null)
                throw new BusinessException("Email já cadastrado na base de dados, por favor utilize outro");

            // Criar o hash do password, baseado na propriedade
            usuario.CriptografarPassword();

            this._context.Usuarios.Add(usuario);
            this._context.SaveChanges();

            return usuario;
        }

        public Usuario AtualizarUsuario(Usuario usuarioNew)
        {
            var user = this._context.Usuarios.FirstOrDefault(x => x.Id == usuarioNew.Id);

            user.Email = usuarioNew.Email;
            user.Senha = usuarioNew.Senha;

            // Criar o hash do password, baseado na propriedade
            user.CriptografarPassword();

            this._context.Usuarios.Update(user);
            this._context.SaveChanges();

            return user;

        }

        public void ExcluirUsuario(Usuario usuario)
        {
            this._context.Usuarios.Remove(usuario);
            this._context.SaveChanges();
        }

        public Usuario AutenticarUsuario(string email, string password)
        {
            var passwordEncoded = Convert.ToBase64String(Encoding.Default.GetBytes(password));

            var user = this._context.Usuarios.FirstOrDefault(
                    x => x.Email == email && x.Senha == passwordEncoded
                );

            return user;
        }
    }
}
