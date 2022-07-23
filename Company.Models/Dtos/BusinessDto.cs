using System.Collections.Generic;

namespace Company.Models.Dtos
{
    public class BusinessDto
    {
        public string CompanyName { get; set; }
        public string Domain { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string Employees { get; set; }
        public string Revenue { get; set; }
        public string Industry { get; set; }
        public int SicCode { get; set; }
        public string SicDescription { get; set; }
        public int NaicsCode { get; set; }
        public string NaicsDescription { get; set; }
        public string Description { get; set; }
        public int YearFounded { get; set; }
        public string Logo { get; set; }
        public string LinkedinUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string YoutubeUrl { get; set; }
        public string CrunchbaseUrl { get; set; }
        public string YelpUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string Type { get; set; }
        public string Ticker { get; set; }
        public string Exchange { get; set; }
        public int AlexaRank { get; set; }
        public List<PersonDto> People { get; set; }
    }
}
