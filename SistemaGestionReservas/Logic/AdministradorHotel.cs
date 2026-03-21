using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionReservas.Logic
{
    public class AdministradorHotel
    {

        private List<Reserva> reservas;

        public void AgregarReserva(Reserva nueva)
        {
            DateTime inicioNueva = nueva.FechaReserva.Date;
            DateTime finNueva = inicioNueva.AddDays(nueva.DuracionEstadia);

            bool ocupada = reservas.Any(r =>
                r.NumeroHabitacion == nueva.NumeroHabitacion &&
                inicioNueva < r.FechaReserva.AddDays(r.DuracionEstadia).Date &&
                finNueva > r.FechaReserva.Date
            );

            if (ocupada)
            {
                throw new Exception($"La habitación {nueva.NumeroHabitacion} ya está ocupada " + $"entre las fechas seleccionadas.");
            }

        }





    }
}
