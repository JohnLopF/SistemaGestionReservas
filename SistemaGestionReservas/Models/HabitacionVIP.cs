using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionReservas.Logic
{
    public class HabitacionVIP : Reserva
    {

        public override double CalcularCostoTotal()
        {
            double total = DuracionEstadia * TarifaNoche;

            //Descuento del 20% si son más de 5 noches (Regla 4)
            if (DuracionEstadia > 5)
            {
                total *= 0.80;
            }
            return total;
        }

    }
}
