using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransporteTerresteCore.Models.Responses
{
    public class ResponseConsultaTransporteTerrestre
    {
        public List<ResponseBase> transportes { get; set; }
        public ResponseConsultaTransporteTerrestre()
        {
            transportes = new List<ResponseBase>();
        }
    }
    public partial class ResponseBase
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime Stardate { get; set; }
        public DateTime EndDate { get; set; }
        public long Price { get; set; }
        public string TicketCode { get; set; }
    }
}


