using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionReservas.Logic
{
    public abstract class Reserva
    {

        public string NombreCliente { get; set; }
        public string DocumentoCliente { get; set; }
        public int NumeroHabitacion { get; set; }
        public DateTime FechaReserva { get; set; }
        public int DuracionEstadia { get; set; }
        public double TarifaNoche { get; set; }

        public abstract double CalcularCostoTotal();

        public virtual string MostrarInfo()
        {
            return $"Reserva para {NombreCliente} (Documento: {DocumentoCliente}), Habitación: {NumeroHabitacion}, Fecha: {FechaReserva.ToShortDateString()}, Duración: {DuracionEstadia} noches, Tarifa por noche: {TarifaNoche:C}";
        }
        
    }
}
