using System.ComponentModel.DataAnnotations;

namespace Week11Practice1_Crazy_Musicians.Entities
{
    public class Musician
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage ="İsim girmek zorunludur" )]
        [StringLength(100,MinimumLength =3, ErrorMessage = "İsim 3 ile 100 karakter arasında olmalı")]
        public string FullName { get; set; }

        [Display(Name ="Meslek")]
        [Required(ErrorMessage ="Meslek girmeniz gerekmektedir")]
        public string Job { get; set; }

        [StringLength(300,MinimumLength =3, ErrorMessage = "3 ile 300 karakter arasında olmalı")]
        public string FunFeature { get; set; }      //Eğlenceli özellik
        public bool IsDeleted { get; set; }
    }
}
