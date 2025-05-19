using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Dominio;

namespace EjemploAPI.Models
{
    public class PokemonDTO
    {
        public string Nombre { get; set; }
        public int Numero { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
        public int IdTipo { get; set; }
        public int IdDebilidad { get; set; }

    }
}