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

        public virtual void Validar()
        {
            if (string.IsNullOrEmpty(NombreCliente) || NumeroHabitacion <= 0)
                throw new Exception("El nombre y el número de habitación son obligatorios.");

            if (DuracionEstadia <= 1)
                throw new Exception("La reserva debe ser mayor a 1 noche (Regla 2).");

            if (TarifaNoche <= 0)
                throw new Exception("La tarifa debe ser mayor a cero (Regla 3).");
        }

    }
}
