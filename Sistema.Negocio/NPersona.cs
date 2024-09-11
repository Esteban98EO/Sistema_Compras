using System.Data;
using Sistema.Datos;
using sistema.Entidades;

namespace Sistema.Negocio
{
    public class NPersona
    {
        public static DataTable Listar()
        {
            DPersonaProveedor Datos = new DPersonaProveedor();
            return Datos.Listar();
        }

        public static DataTable ListarProveedores()
        {
            DPersonaProveedor Datos = new DPersonaProveedor();
            return Datos.ListarProveedores();
        }

        public static DataTable ListarClientes()
        {
             DPersonaProveedor Datos = new DPersonaProveedor();
            return Datos.ListarClientes();
        }

        public static DataTable Buscar(string Valor)
        {
            DPersonaProveedor Datos = new DPersonaProveedor();
            return Datos.Buscar(Valor);
        }

        public static DataTable BuscarProveedores(string Valor)
        {
            DPersonaProveedor Datos = new DPersonaProveedor();
            return Datos.BuscarProveedores(Valor);
        }

        public static DataTable BuscarClientes(string Valor)
        {
            DPersonaProveedor Datos = new DPersonaProveedor();
            return Datos.BuscarClientes(Valor);
        }

        public static string Insertar(string TipoPersona, string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string Email)
        {
            DPersonaProveedor Datos = new DPersonaProveedor();

            string Existe = Datos.Existe(Nombre);
            if (Existe.Equals("1"))
            {
                return "La persona ya se encuentra registrada.";
            }

            else
            {
                Persona Obj = new Persona();
                Obj.TipoPersona = TipoPersona;
                Obj.Nombre = Nombre;
                Obj.TipoDocumento = TipoDocumento;
                Obj.NumDocumento = NumDocumento;
                Obj.Direccion = Direccion;
                Obj.Telefono = Telefono;
                Obj.Email = Email;
                return Datos.Insertar(Obj);
            }
        }

        public static string Actualizar(int Id, string TipoPersona, string NombreAnt, string Nombre, string TipoDocumento, string NumDocumento, string Direccion, string Telefono, string Email)
        {
            DPersonaProveedor Datos = new DPersonaProveedor();
            Persona Obj = new Persona();

            if (NombreAnt.Equals(Nombre))
            {
                Obj.IdPersona = Id;
                Obj.TipoPersona = TipoPersona;
                Obj.Nombre = Nombre;
                Obj.TipoDocumento = TipoDocumento;
                Obj.NumDocumento = NumDocumento;
                Obj.Direccion = Direccion;
                Obj.Telefono = Telefono;
                Obj.Email = Email;
                return Datos.Actualizar(Obj);
            }
            else
            {
                string Existe = Datos.Existe(Nombre);
                if (Existe.Equals("1"))
                {
                    return "Una persona con ese nombre ya existe.";
                }

                else
                {
                    Obj.IdPersona = Id;
                    Obj.TipoPersona = TipoPersona;
                    Obj.Nombre = Nombre;
                    Obj.TipoDocumento = TipoDocumento;
                    Obj.NumDocumento = NumDocumento;
                    Obj.Direccion = Direccion;
                    Obj.Telefono = Telefono;
                    Obj.Email = Email;
                    return Datos.Actualizar(Obj);
                }
            }


        }

        public static string Eliminar(int Id)
        {
            DPersonaProveedor Datos = new DPersonaProveedor();
            return Datos.Eliminar(Id);
        }
    }
}
