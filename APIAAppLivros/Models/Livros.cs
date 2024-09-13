namespace APIAAppLivros.Models
{
        public class Livros
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Autor { get; set; }
            public int  AnoPublicacao { get; set; }
             public string Genero { get; set; }
            public int NumeroPaginas { get; set; }

            public bool? Ativo { get; set; }
        }
    
}