namespace CaarInsurance.ViewModels
   
{
    public class InsureeVm
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string EmailAddress { get; set; }
        public required DateTime DateOfBirth { get; set; }
        public int CarYear { get; set; }
        public required string CarMake { get; set; }
        public required string CarModel { get; set; }
        public bool HasDUI { get; set; }
        public int SpeedingTickets { get; set; }
        public required string CoverageType { get; set; }
        
    }
}
