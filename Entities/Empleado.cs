namespace CrudAPI.Entities
{
    public class Empleado
    {
        public int idEmpleado { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int sueldo { get; set; }
        public int idPerfil { get; set; }
        public virtual Perfil PerfilReferencia { get; set; } 
    }

}
