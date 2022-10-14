using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class UpdateFilmeDto
    {
        [Display(Name = "título")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Titulo { get; set; }

        [Display(Name = "diretor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Diretor { get; set; }

        [Display(Name = "gênero")]
        [StringLength(30, ErrorMessage = "O {0} não pode passar de {1} caracteres")]
        public string Genero { get; set; }

        [Display(Name = "duração")]
        [Range(1, 600, ErrorMessage = "A {0} deve ter no mínimo {1} e no máximo {2} minutos")]
        public int Duracao { get; set; }
    }
}
