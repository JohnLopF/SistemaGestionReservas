using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionReservas.Logic
{
    public class HabitacionEstandar : Reserva
    {
        public override double CalcularCostoTotal()
        {
            return DuracionEstadia * TarifaNoche;
        }

    }
}
