﻿using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.CruzDelSur.Datos.Contratos;
using UPC.CruzDelSur.Modelo.Abastecimiento;

namespace UPC.CruzDelSur.Datos.Abastecimiento
{
    public class SolicitudCocinaRepositorio : Repositorio<SolicitudCocinaRepositorio>, ISolicitudCocinaRepositorio
    {

        protected Database Database = DatabaseFactory.CreateDatabase();

        public IQueryable<SolicitudCocina> ObtenerTodos()
        {
			string Query = "select a.int_codigo_solicitudcocina, a.int_codigo_refrigerio, a.int_codigo_programacion_ruta, a.int_cantidad, a.bln_estado, a.dte_fecha_solicitud, d.int_vehiculo, d.vch_placa, c.int_codigo_ruta, e.int_codigo_agencia, e.vch_nombre, f.int_codigo_agencia, f.vch_nombre from ta_solicitudcocina a left outer join ta_programacion_ruta b on(a.int_codigo_programacion_ruta = b.int_codigo_programacion_ruta) left outer join ta_ruta c on(b.int_codigo_ruta = c.int_codigo_ruta) left outer join ta_vehiculo d on(b.int_codigovehiculo = d.int_vehiculo) left outer join ta_agencia e on(c.int_codigo_agenciaorigen = e.int_codigo_agencia) left outer join ta_agencia f on(c.int_codigo_agenciadestino = f.int_codigo_agencia)";
			DbCommand DbCommand = Database.GetSqlStringCommand(Query);
			IList<SolicitudCocina> ListadoSolicitudes = new List<SolicitudCocina>();

			using (IDataReader Reader = Database.ExecuteReader(DbCommand))
			{
				while (Reader.Read())
				{
					ListadoSolicitudes.Add(new SolicitudCocina()
											{
												Id = (!Reader.IsDBNull(0)) ? Reader.GetInt32(0) : 0,
												Refrigerio = new Refrigerio()
												{
													Id = (!Reader.IsDBNull(1)) ? Reader.GetInt32(1) : 0,
												},

												ProgramacionRuta = new ProgramacionRuta()
												{
													Id = (!Reader.IsDBNull(2)) ? Reader.GetInt32(2) : 0,
													Vehiculo = new Vehiculo()
													{
														Id = (!Reader.IsDBNull(6)) ? Reader.GetInt32(6) : 0,
														Placa = (!Reader.IsDBNull(7)) ? Reader.GetString(7) : String.Empty
													},
													Ruta = new Ruta()
													{
														Id = (!Reader.IsDBNull(8)) ? Reader.GetInt32(8) : 0,
														AgenciaOrigen = new Agencia()
														{
															Id = (!Reader.IsDBNull(9)) ? Reader.GetInt32(9) : 0,
															Nombre = (!Reader.IsDBNull(10)) ? Reader.GetString(10) : String.Empty
														},
														AgenciaDestino = new Agencia()
														{
															Id = (!Reader.IsDBNull(11)) ? Reader.GetInt32(11) : 0,
															Nombre = (!Reader.IsDBNull(12)) ? Reader.GetString(12) : String.Empty
														}
													}
												},
												Cantidad = (!Reader.IsDBNull(3)) ? Reader.GetInt32(3) : 0,
												Estado = (!Reader.IsDBNull(4) && Reader.GetBoolean(4)),
												FechaSolicitud = (!Reader.IsDBNull(5)) ? Reader.GetDateTime(5) : Convert.ToDateTime("01/01/1900")
											}
					);
				}
			}

			return ListadoSolicitudes.AsQueryable();
        }


        public SolicitudCocina ObtenerPorId(int id)
        {
            string Query = "select int_codigo_solicitudcocina, int_codigo_refrigerio, int_codigo_programacion_ruta, int_cantidad, bln_estado from ta_solicitudcocina where int_codigo_solicitudcocina = @id";
            DbCommand DbCommand = Database.GetSqlStringCommand(Query);
            Database.AddInParameter(DbCommand, "@id", DbType.Int32, id);

            using (IDataReader Reader = Database.ExecuteReader(DbCommand))
            {
                if (Reader.Read())
                {
                    return new SolicitudCocina()
                    {
                        Id = (!Reader.IsDBNull(0)) ? Reader.GetInt32(0) : 0,
                        Refrigerio = new Refrigerio() { Id = (!Reader.IsDBNull(1)) ? Reader.GetInt32(1) : 0, },
                        ProgramacionRuta = new ProgramacionRuta() { Id = (!Reader.IsDBNull(2)) ? Reader.GetInt32(2) : 0, },
                        Cantidad = (!Reader.IsDBNull(3)) ? Reader.GetInt32(3) : 0,
                        Estado = (!Reader.IsDBNull(4) && Reader.GetBoolean(4))
                    };
                }
            }

            return null;
        }

        public void Insertar(SolicitudCocina solicitudCocina)
        {
            string Query = "insert into ta_solicitudcocina(int_codigo_refrigerio, int_codigo_programacion_ruta, dte_fecha_solicitud, int_cantidad, bln_estado) values(@int_codigo_refrigerio, @int_codigo_programacion_ruta, @dte_fecha_solicitud, @int_cantidad, @bln_estado) set @id = scope_identity()";
            DbCommand DbCommand = Database.GetSqlStringCommand(Query);
            Database.AddOutParameter(DbCommand, "@id", DbType.Int32, 4);
            Database.AddInParameter(DbCommand, "@int_codigo_refrigerio", DbType.Int32, solicitudCocina.Refrigerio.Id);
            Database.AddInParameter(DbCommand, "@int_codigo_programacion_ruta", DbType.Int32, solicitudCocina.ProgramacionRuta.Id);
			Database.AddInParameter(DbCommand, "@dte_fecha_solicitud", DbType.Date, solicitudCocina.FechaSolicitud);
			Database.AddInParameter(DbCommand, "@int_cantidad", DbType.Int32, solicitudCocina.Cantidad);
            Database.AddInParameter(DbCommand, "@bln_estado", DbType.Boolean, solicitudCocina.Estado);

            int RowsAffected = Database.ExecuteNonQuery(DbCommand);
        }

        public void Actualizar(SolicitudCocina entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(SolicitudCocina entidad)
        {
            throw new NotImplementedException();
        }

        public void Eliminar(int id)
        {
            throw new NotImplementedException();
        }

		public void AnularSolicitud(int id)
		{
			DbCommand DbCommand = Database.GetSqlStringCommand("update ta_solicitudcocina set bln_estado = 0 where int_codigo_solicitudcocina = @id");
			Database.AddInParameter(DbCommand, "@id", DbType.Int32, id);
			Database.ExecuteNonQuery(DbCommand);
		}
	}
}