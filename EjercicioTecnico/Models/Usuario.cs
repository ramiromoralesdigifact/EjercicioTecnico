namespace EjercicioTecnico.Models
{
    public class Usuario
    {
        public int?  Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? NIT { get; set; }
        public string? DPI { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public List<string>? Intereses { get; set; } = new List<string>();
    }
}
