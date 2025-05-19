using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Diagnostics.SymbolStore;

namespace Servicio
{
    public class PokemonServicio
    {
        public List<Pokemon> listarConSP()
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();

            //string consulta = "SELECT Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D  WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad AND P.Activo = 1";

            try
            {
                //datos.setearConsulta(consulta);

                datos.seterarProcedimiento("StoredListar");
                
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0);/* Opcion 1 */
                    aux.Nombre = (string)datos.Lector["Nombre"]; /* Opcion 2 */
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    //Opciones para ingresos NULL (opcion1)
                    /*if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                        aux.UrlImagen = (string)lector["UrlImagen"];*/

                    //Opcion 2
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];

                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }

                datos.cerrarConexion();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void agregarConSP(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.seterarProcedimiento("storedAltaPokemon");
                datos.setearParametro("@numero", nuevo.Numero);
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@desc", nuevo.Descripcion);
                datos.setearParametro("@img", nuevo.UrlImagen);
                datos.setearParametro("@idTipo", nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);



                //datos.setearParametro("@idEvolucion", null);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<Pokemon> listar(string id = "")
        {
            List<Pokemon> lista = new List<Pokemon>();

            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            string consulta = "SELECT Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D  WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad ";

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = consulta;
                comando.Connection = conexion;
                if (id != "")
                    comando.CommandText += " and P.Id = " + id;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();

                    aux.Id = (int)lector["Id"];
                    aux.Numero = lector.GetInt32(0);/* Opcion 1 */
                    aux.Nombre = (string)lector["Nombre"]; /* Opcion 2 */
                    aux.Descripcion = (string)lector["Descripcion"];

                    //Opciones para ingresos NULL (opcion1)
                    /*if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                        aux.UrlImagen = (string)lector["UrlImagen"];*/

                    //Opcion 2
                    if (!(lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];

                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];

                    aux.Activo = bool.Parse(lector["Activo"].ToString());


                    lista.Add(aux);
                }
                
                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        public void agregar(Pokemon nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                //Opcion 1 (insertar)
                datos.setearConsulta("INSERT INTO POKEMONS(Numero, Nombre, Descripcion, Activo, IdTipo, IdDebilidad, UrlImagen) VALUES (" + nuevo.Numero + ", '" + nuevo.Nombre+ "', '" + nuevo.Descripcion + "', 1, @idTipo, @idDebilidad, @urlImagen)");

                //Opcion 2 (parametros)
                datos.setearParametro("@idTipo", nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                datos.setearParametro("@urlImagen", nuevo.UrlImagen);

                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }

        public void modificarConSp(Pokemon pokemon)
        {

            AccesoDatos acceso = new AccesoDatos();
            try
            {
                acceso.seterarProcedimiento("storedModificarPokemon");
                acceso.setearParametro("@numero", pokemon.Numero);
                acceso.setearParametro("@nombre", pokemon.Nombre);
                acceso.setearParametro("@desc", pokemon.Descripcion);
                acceso.setearParametro("@img", pokemon.UrlImagen);
                acceso.setearParametro("@idTipo", pokemon.Tipo.Id);
                acceso.setearParametro("@idDebilidad", pokemon.Debilidad.Id);
                acceso.setearParametro("@id", pokemon.Id);

                acceso.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                acceso.cerrarConexion();
            }
        }

        public void modificar(Pokemon pokemon)
        {
            //El objetpo podria ser un atributo privado que nace en servicio
            AccesoDatos acceso = new AccesoDatos();
            try
            {
                acceso.setearConsulta("UPDATE POKEMONS SET Numero = @numero, Nombre = @nombre, Descripcion = @descripcion, UrlImagen = @imagen, IdTipo = @idTipo, IdDebilidad = @idDebilidad WHERE Id = @id");
                acceso.setearParametro("@numero", pokemon.Numero);
                acceso.setearParametro("@nombre", pokemon.Nombre);
                acceso.setearParametro("@descripcion", pokemon.Descripcion);
                acceso.setearParametro("@imagen", pokemon.UrlImagen);
                acceso.setearParametro("@idTipo", pokemon.Tipo.Id);
                acceso.setearParametro("@idDebilidad", pokemon.Debilidad.Id);
                acceso.setearParametro("@id", pokemon.Id);

                acceso.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex; 
            }
            finally
            {
                acceso.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM POKEMONS WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminarLogico(int id, bool activo = false)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE POKEMONS SET Activo = @activo WHERE Id = @id");
                datos.setearParametro("@id", id);
                datos.setearParametro("@activo", activo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Pokemon> filtrar(string campo, string criterio, string filtro, string estado)
        {
            List<Pokemon> lista = new List<Pokemon>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT Numero, Nombre, P.Descripcion, UrlImagen, E.Descripcion Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id, P.Activo FROM POKEMONS P, ELEMENTOS E, ELEMENTOS D  WHERE E.Id = P.IdTipo AND D.Id = P.IdDebilidad AND ";

                if(campo == "Número")
                {
                    switch(criterio)
                    {
                        case "Mayor a":
                            consulta += "Numero > " + filtro;
                            break;
                        case "Menor a":
                            consulta += "Numero < " + filtro;
                            break;
                        default:
                            consulta += "Numero = " + filtro;
                            break;
                    }
                }
                else if(campo == "Nombre")
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "Nombre LIKE '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "Nombre LIKE '%"+ filtro + "'";
                            break;
                        default:
                            consulta += "Nombre LIKE '%"+ filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Empieza con":
                            consulta += "E.Descripcion LIKE '" + filtro + "%' ";
                            break;
                        case "Termina con":
                            consulta += "E.Descripcion LIKE '%" + filtro + "'";
                            break;
                        default:
                            consulta += "E.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }

                if (estado == "Activo")
                    consulta += " and P.Activo = 1";
                else if (estado == "Inactivo")
                    consulta += " and P.Activo = 0";
                

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pokemon aux = new Pokemon();

                    aux.Id = (int)datos.Lector["Id"];
                    aux.Numero = datos.Lector.GetInt32(0);/* Opcion 1 */
                    aux.Nombre = (string)datos.Lector["Nombre"]; /* Opcion 2 */
                    aux.Descripcion = (string)datos.Lector["Descripcion"];

                    //Opciones para ingresos NULL (opcion1)
                    /*if(!(lector.IsDBNull(lector.GetOrdinal("UrlImagen"))))
                        aux.UrlImagen = (string)lector["UrlImagen"];*/

                    //Opcion 2
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["UrlImagen"];

                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)datos.Lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)datos.Lector["Tipo"];

                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)datos.Lector["IdDebilidad"];
                    aux.Debilidad.Descripcion = (string)datos.Lector["Debilidad"];

                    aux.Activo = bool.Parse(datos.Lector["Activo"].ToString());

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
