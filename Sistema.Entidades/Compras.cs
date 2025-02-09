﻿using System;
using System.Data;

namespace sistema.Entidades
{
    public class Compras
    {
        public int IdIngreso { get; set; }
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public string TipoComprobante { get; set; }
        public string SerieComprobante { get; set; }
        public string NumCoprobante { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }
        public DataTable Detalles { get; set; }
    }
}

