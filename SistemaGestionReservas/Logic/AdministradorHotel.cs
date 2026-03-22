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
        public List<Reserva> BuscarPorNombre(string nombre)
        {
            return reservas
                .Where(r => r.NombreCliente.ToLower().Contains(nombre.ToLower()))
                .ToList();
        }
        public List<Reserva> FiltrarPorTipo(string tipo)
        {
            //Filtra según el nombre de la clase (HabitacionVIP o HabitacionEstandar)
            return reservas
                .Where(r => r.GetType().Name.Contains(tipo))
                .ToList();
        }
        public void EditarReserva(string documentoOriginal, Reserva reservaEditada)
        {
            //Buscar si existe
            var existente = reservas.FirstOrDefault(r => r.DocumentoCliente == documentoOriginal);

            if (existente == null)
                throw new Exception("No se encontró la reserva para editar.");

            //Validar que el cambio de fecha/habitación no choque con otras excluyendo la actual
            bool choque = reservas.Any(r =>
                r != existente && //No compararse con ella misma
                r.NumeroHabitacion == reservaEditada.NumeroHabitacion &&
                reservaEditada.FechaReserva.Date < r.FechaReserva.Date.AddDays(r.DuracionEstadia) &&
                reservaEditada.FechaReserva.Date.AddDays(reservaEditada.DuracionEstadia) > r.FechaReserva.Date);

            if (choque)
                throw new Exception("Los nuevos cambios generan un conflicto de fechas con otra reserva.");

            //aplicar los cambios
            int index = reservas.IndexOf(existente);
            reservaEditada.Validar();
            reservas[index] = reservaEditada;
        }
        //Eliminar reservas
        public void EliminarReserva(string documento)
        {
            var r = reservas.FirstOrDefault(x => x.DocumentoCliente == documento);

            if (r == null)
                throw new Exception("La reserva no existe.");

            reservas.Remove(r);
        }


    }
}
