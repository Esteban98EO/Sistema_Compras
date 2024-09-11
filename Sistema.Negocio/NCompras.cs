using System.Data;
using Sistema.Datos;
using sistema.Entidades;

namespace Sistema.Negocio
{
    public class NCompras
    {
        public static DataTable Listar()
        {
            DCompras Datos = new DCompras();
            return Datos.Listar();
        }

        public static DataTable Buscar(string Valor)
        {
            DCompras Datos = new DCompras();
            return Datos.Buscar(Valor);
        }

        public static DataTable ListarDetalle(int Id)
        {
            DCompras Datos = new DCompras();
            return Datos.ListarDetalle(Id);
        }

        public static string Insertar(int IdProveedor, int IdUsuario, string TipoComprobante, string SerieComprobante, string NumComprobante, decimal Impuesto, decimal Total, DataTable Detalles)
        {
            DCompras Datos = new DCompras();
            Compras Obj = new Compras();
            Obj.IdProveedor = IdProveedor;
            Obj.IdUsuario = IdUsuario;
            Obj.TipoComprobante = TipoComprobante;
            Obj.SerieComprobante = SerieComprobante;
            Obj.NumCoprobante = NumComprobante;
            Obj.Impuesto = Impuesto;
            Obj.Total = Total;
            Obj.Detalles = Detalles;
            return Datos.Insertar(Obj);
        }

        public static string Anular(int Id)
        {
            DCompras Datos = new DCompras();
            return Datos.Anular(Id);
        }
    }
}
