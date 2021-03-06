﻿using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using UPC.CruzDelSur.Datos.Contratos;
using UPC.CruzDelSur.Modelo.Abastecimiento;

namespace UPC.CruzDelSur.Datos.Abastecimiento
{
	public class InsumoRepositorio : Repositorio<InsumoRepositorio >,  IInsumoRepositorio
	{

		protected InsumoRepositorio() { }


		public IQueryable<Insumo> ObtenerTodos()
		{
			DbCommand DbCommand = Database.GetSqlStringCommand("select int_codigo_insumo, vch_descripcion, vch_tipo_unidad, dte_fecha_vencimiento from ta_insumo");
			IList<Insumo> ListadoInsumo = new List<Insumo>();

			using (IDataReader Reader = Database.ExecuteReader(DbCommand))
			{
				while (Reader.Read())
				{
					ListadoInsumo.Add(
						new Insumo()
						{
							Id = (!Reader.IsDBNull(0)) ? Reader.GetInt32(0) : 0,
							Descripcion = (!Reader.IsDBNull(1)) ? Reader.GetString(1) : String.Empty,
							TipoUnidad = (!Reader.IsDBNull(2)) ? Reader.GetString(2) : String.Empty,
							FechaVencimiento = (!Reader.IsDBNull(3)) ? Reader.GetDateTime(3) : Convert.ToDateTime("01/01/1900")
						});
				}
			}

			return ListadoInsumo.AsQueryable();
		}


		public Insumo ObtenerPorId(int id)
		{
			DbCommand DbCommand = Database.GetSqlStringCommand("select int_codigo_insumo, vch_descripcion, vch_tipo_unidad, dte_fecha_vencimiento from ta_insumo where int_codigo_insumo = @int_codigo_insumo");
			Database.AddInParameter(DbCommand, "@int_codigo_insumo", DbType.Int32, id);

			using (IDataReader Reader = Database.ExecuteReader(DbCommand))
			{
				if (Reader.Read())
				{
					return new Insumo()
						{
							Id = (!Reader.IsDBNull(0)) ? Reader.GetInt32(0) : 0,
							Descripcion = (!Reader.IsDBNull(1)) ? Reader.GetString(1) : String.Empty,
							TipoUnidad = (!Reader.IsDBNull(2)) ? Reader.GetString(2) : String.Empty,
							FechaVencimiento = (!Reader.IsDBNull(3)) ? Reader.GetDateTime(3) : Convert.ToDateTime("01/01/1900")
						};
				}
			}

			return null;
		}

		public void Insertar(Insumo entidad)
		{
			throw new NotImplementedException();
		}

		public void Actualizar(Insumo entidad)
		{
			throw new NotImplementedException();
		}

		public void Eliminar(Insumo entidad)
		{
			throw new NotImplementedException();
		}

		public void Eliminar(int id)
		{
			throw new NotImplementedException();
		}
	}
}
