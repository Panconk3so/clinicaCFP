
using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;



namespace clinicaCFP.Models

{

    [Table("t_registroCita")]

    public class RegistrarCita

    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Column("id")]

       
    public int Id { get; set; }
    public string? UserId { get; set; }

    
    public string? Nombre { get; set; }

  
    public string? ApellidoPaterno { get; set; }

   
    public string? ApellidoMaterno { get; set; }

 
    public string? Email { get; set; }

    
    public string? Phone { get; set; }
   
  
    public DateTime  FechaNacimiento { get; set; }
    

   
    public string? DNI { get; set; }

   

    public string? Departamento { get; set; }

 
    public string? Especialidad { get; set; }

    public Decimal Precio { get; set; }

    public DateTime FechaCitaDeseada { get; set; }
      public bool? PagoRealizado { get; set; }

    }

}
  