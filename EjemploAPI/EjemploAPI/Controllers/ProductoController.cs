using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EjemploAPI.Models;

namespace EjemploAPI.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: api/Producto
        public IEnumerable<Producto> Get()
        {
            return new Producto[] { new Producto { Id = 1, Nombre = "Compu" }, new Producto { Id = 2, Nombre = "Note" } };
        }

        // GET: api/Producto/5
        public Producto Get(int id)
        {
            List<Producto> lista = new List<Producto>();
            lista.Add(new Producto { Id = 1, Nombre = "Compu" });
            lista.Add(new Producto { Id = 2, Nombre = "Note" });
            lista.Add(new Producto { Id = 3, Nombre = "Celu" });

            Producto filtrado = lista.Find(x => x.Id == id);

            return filtrado;
        }

        // POST: api/Producto
        public void Post([FromBody]Producto producto)
        {
        }

        // PUT: api/Producto/5
        public void Put(int id, [FromBody] Producto producto)
        {
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }
    }
}
