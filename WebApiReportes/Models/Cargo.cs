using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiReportes.models
{
    [Table("cargo")]  // Especifica que la tabla en la BD es 'cargo' en minúsculas
    public class Cargo
    {
        //la tbla cargo tiene los campos: id,nombre,created_at,updated_at
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
      

    }
}
