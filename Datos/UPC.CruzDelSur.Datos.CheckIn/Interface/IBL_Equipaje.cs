﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UPC.CruzDelSur.Negocio.Modelo.CheckIn;

namespace UPC.CruzDelSur.Datos.CheckIn.Interface
{
    public interface IBL_Equipaje
    {
        List<BE_Equipaje> f_listarEquipajeBoleto(string nroboleto, string dni);
        List<BE_Equipaje> f_verificarEquipajeBoleto(string nroboleto, string dni);
        List<BE_Equipaje> f_actualizarEstadoEquipaje(string nroboleto, int accion);
        List<BE_Equipaje> f_verificarEstadoEquipaje(string nroboleto, int NroEquipaje);
        int f_RegistrarInfraccion(string nroboleto, string detalleinfraccion);
        
        BE_Tiket f_modificarEquipaje(int codequipaje, int codboleto);
    }
}
