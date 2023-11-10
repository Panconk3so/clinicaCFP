using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clinicaCFP.Models
{
   public class ServicioPago
{
    public bool ProcesarPago(decimal monto)
    {
        // Lógica de procesamiento de pago, utiliza la clase PasarelaPago internamente.
        PasarelaPago pasarelaPago = new PasarelaPago();
        return pasarelaPago.ProcesarPago(monto);
    }
}
}