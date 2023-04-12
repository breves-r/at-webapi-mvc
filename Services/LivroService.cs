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
    public class LivroService
    {
        private AtContext _context;

        public LivroService(AtContext context)
        {
            _context = context;
        }

        public IEnumerable<Livro> ObterLivros()
        {
            return _context.Livros.Include(x => x.Autores);
        }

        public Livro ObterLivroPorId(int id)
        {
            return _context.Livros.Include(x => x.Autores).FirstOrDefault(x => x.Id == id);
        }

        public Livro SalvarLivro(Livro livro)
        {
            var livroDb = _context.Livros.FirstOrDefault(x => x.Isbn == livro.Isbn);

            if (livroDb != null)
                throw new BusinessException("Isbn já cadastrado na base de dados, por favor utilize outro");

            _context.Livros.Add(livro);
            _context.SaveChanges();

            return livro;
        }

        public Livro AtualizarLivro(Livro livroNew)
        {
            var livro = _context.Livros.FirstOrDefault(x => x.Id == livroNew.Id);

            livro.Titulo = livroNew.Titulo;
            livro.Ano = livroNew.Ano;
            livro.Isbn = livroNew.Isbn;

            _context.Livros.Update(livro);
            _context.SaveChanges();

            return livro;
        }

        public void ExcluirLivro(Livro livro)
        {
            _context.Livros.Remove(livro);
            _context.SaveChanges();
        }
    }
}
