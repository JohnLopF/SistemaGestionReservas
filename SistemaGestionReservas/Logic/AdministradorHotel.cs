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


        

        private bool ExisteConflicto(int habitacion, DateTime inicio, int noches)
        {
            DateTime finNueva = inicio.Date.AddDays(noches);

            return reservas.Any(r =>
                r.NumeroHabitacion == habitacion &&
                inicio.Date < r.FechaReserva.Date.AddDays(r.DuracionEstadia) &&
                finNueva > r.FechaReserva.Date
            );
        }




    }
}
