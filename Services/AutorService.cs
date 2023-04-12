using AT.Entidade;
using AT.Repositorio;
using Microsoft.EntityFrameworkCore;
using Services.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AutorService
    {
        private AtContext _context;

        public AutorService(AtContext context)
        {
            this._context = context;
        }

        public IEnumerable<Autor> ObterAutores() 
        {
            return _context.Autores.Include(x => x.Livros);
        }

        public Autor ObterAutorPorId(int id)
        {
            return _context.Autores.Include(x => x.Livros).FirstOrDefault(x => x.Id == id);
        }

        public Autor SalvarAutor(Autor autor)
        {
            var autorDb = _context.Autores.FirstOrDefault(x => x.Email == autor.Email);

            if (autorDb != null)
                throw new BusinessException("Email já cadastrado na base de dados, por favor utilize outro");

            _context.Autores.Add(autor);
            _context.SaveChanges();

            return autor;
        }

        public Autor AtualizarAutor(Autor autorNew)
        {
            var autor = _context.Autores.FirstOrDefault(x => x.Id == autorNew.Id);

            autor.Nome = autorNew.Nome;
            autor.Sobrenome = autorNew.Sobrenome;
            autor.Email = autorNew.Email;
            autor.DataNascimento = autorNew.DataNascimento;

            _context.Autores.Update(autor);
            _context.SaveChanges();

            return autorNew;
        }

        public void ExcluirAutor(Autor autor)
        {
            _context.Autores.Remove(autor);
            _context.SaveChanges();
        }
    }
}
