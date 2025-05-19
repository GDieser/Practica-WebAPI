using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dominio;
using Servicio;
using EjemploAPI.Models;

namespace EjemploAPI.Controllers
{
    public class PokemonController : ApiController
    {
        // GET: api/Pokemon
        public IEnumerable<Pokemon> Get()
        {
            PokemonServicio servicio = new PokemonServicio();



            return servicio.listar();
        }

        // GET: api/Pokemon/5
        public Pokemon Get(int id)
        {
            PokemonServicio servicio = new PokemonServicio();
            List<Pokemon> lista = servicio.listar();


            return lista.Find(x => x.Id == id);
        }

        // POST: api/Pokemon
        public void Post([FromBody] PokemonDTO pokemon)
        {
            PokemonServicio servicio = new PokemonServicio();
            Pokemon aux = new Pokemon();

            aux.Nombre = pokemon.Nombre;
            aux.Numero = pokemon.Numero;
            aux.Descripcion = pokemon.Descripcion;
            aux.UrlImagen = pokemon.UrlImagen;
            aux.Tipo = new Elemento { Id = pokemon.IdTipo };
            aux.Debilidad = new Elemento { Id = pokemon.IdDebilidad };

            servicio.agregar(aux);

        }

        // PUT: api/Pokemon/5
        public void Put(int id, [FromBody]PokemonDTO pokemon)
        {
            PokemonServicio servicio = new PokemonServicio();
            Pokemon aux = new Pokemon();

            aux.Nombre = pokemon.Nombre;
            aux.Numero = pokemon.Numero;
            aux.Descripcion = pokemon.Descripcion;
            aux.UrlImagen = pokemon.UrlImagen;
            aux.Tipo = new Elemento { Id = pokemon.IdTipo };
            aux.Debilidad = new Elemento { Id = pokemon.IdDebilidad };
            aux.Id = id;

            servicio.modificar(aux);

        }

        // DELETE: api/Pokemon/5
        public void Delete(int id)
        {
            PokemonServicio servicio = new PokemonServicio();
            servicio.eliminarLogico(id);
        }
    }
}
