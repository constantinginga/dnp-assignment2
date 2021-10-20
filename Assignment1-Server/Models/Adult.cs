using System.ComponentModel.DataAnnotations;

namespace Assignment1_Server.Models {
public class Adult : Person {
        public Job JobTitle { get; set; }
}
}