using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionReservas.Logic
{
    public class AdministradorHotel
    {

        private List<Reserva> reservas = new List<Reserva>();

        public void RegistrarReserva(Reserva nueva)
        {
            //Validar datos básicos
            nueva.Validar();

            //Validar disponibilidad
            if (ExisteConflicto(nueva.NumeroHabitacion, nueva.FechaReserva, nueva.DuracionEstadia))
            {
                throw new Exception($"Conflicto: La habitación {nueva.NumeroHabitacion} ya está ocupada en esas fechas.");
            }

            reservas.Add(nueva);
        }
        private bool ExisteConflicto(int habitacion, DateTime inicio, int noches)
        {
            DateTime finNueva = inicio.Date.AddDays(noches);

            return reservas.Any(r =>
                r.NumeroHabitacion == habitacion &&
                inicio.Date < r.FechaReserva.Date.AddDays(r.DuracionEstadia) &&
                finNueva > r.FechaReserva.Date
            );
        }
        public List<Reserva> ObtenerTodas()
        {
            return reservas;
        }




    }
}
