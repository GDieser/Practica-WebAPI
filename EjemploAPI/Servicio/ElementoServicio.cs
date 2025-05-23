﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Servicio
{
    public class ElementoServicio
    {
        public List<Elemento> listar()
        {
			List<Elemento> lista = new List<Elemento>();
			AccesoDatos datos = new AccesoDatos();

			try
			{
				datos.setearConsulta("SELECT Id, Descripcion FROM ELEMENTOS");
				datos.ejecutarLectura();

				while(datos.Lector.Read())
				{
					Elemento aux = new Elemento();
					aux.Id = (int)datos.Lector["Id"];
					aux.Descripcion = (string)datos.Lector["Descripcion"];

					lista.Add(aux);
				}

				return lista;
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
    }
}
