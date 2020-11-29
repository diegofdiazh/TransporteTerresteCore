﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransporteTerresteCore.Data.Entities
{
    public class ReservaTransporteTerrestre
    {
        public int Id { get; set; }
        public string CodigoTransporte { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Token { get; set; }
    }
}
