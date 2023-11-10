using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinicaCFP.Models
{
      public class PasarelaPago
    {
        public bool ProcesarPago(decimal monto)
        {
            // Aquí puedes simular el procesamiento de pago.
            // Devuelve true si el pago fue exitoso, o false si hubo un error.

            // En un escenario real, integrarías con la pasarela de pago externa.
            // Aquí simplemente se simula el éxito del 90% del tiempo.
            Random random = new Random();
            bool pagoExitoso = random.Next(100) < 90;

            return pagoExitoso;
        }
    }
}




