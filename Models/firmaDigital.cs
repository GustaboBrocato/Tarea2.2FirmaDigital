using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Tarea2._2FirmaDigital.Models
{
    [SQLite.Table("FirmasDigitales")]
    public class firmaDigital
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(255), NotNull]
        public string? Descripcion { get; set; }

        [MaxLength(255), NotNull]
        public string? NombreFirma { get; set; }
        public string? FirmaDigital { get; set; }
    }
}
