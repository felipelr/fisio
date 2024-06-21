
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fisio.domain.Entities
{
    public class Patient : Base
    {
        public Patient(string name, string document, string document2, string gender, string cellphone, string photo, DateTime dateBirth, string workPlace, string spouseName, string fatherName, string motherName, string maritalStatus, string responsibleName1, string responsibleName2, string street, string streetNumber, string neighborhood, string city, string state, string zipCode, bool active, string userId)
        {
            Name = name;
            Document = document;
            Document2 = document2;
            Gender = gender;
            Cellphone = cellphone;
            Photo = photo;
            DateBirth = dateBirth;
            WorkPlace = workPlace;
            SpouseName = spouseName;
            FatherName = fatherName;
            MotherName = motherName;
            MaritalStatus = maritalStatus;
            ResponsibleName1 = responsibleName1;
            ResponsibleName2 = responsibleName2;
            Street = street;
            StreetNumber = streetNumber;
            Neighborhood = neighborhood;
            City = city;
            State = state;
            ZipCode = zipCode;
            Active = active;
            UserId = userId;
        }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(255, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 255 caracteres")]
        [Column("name")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(14, ErrorMessage = "Este campo deve conter 14 caracteres")]
        [MinLength(14, ErrorMessage = "Este campo deve conter 14 caracteres")]
        [Column("document")]
        public string Document { get; private set; }

        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        [Column("document_2")]
        public string Document2 { get; private set; }

        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        [Column("gender")]
        public string Gender { get; private set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 25 caracteres")]
        [Column("cellphone")]
        public string Cellphone { get; private set; }
        
        [MaxLength(255, ErrorMessage = "Este campo deve conter no máximo 255 caracteres")]
        [Column("photo")]
        public string Photo { get; private set; }

        [Column("datebirth")]
        public DateTime DateBirth { get; private set; }

        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [Column("work_place")]
        public string WorkPlace { get; private set; }

        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [Column("spouse_name")]
        public string SpouseName { get; private set; }

        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [Column("father_name")]
        public string FatherName { get; private set; }

        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [Column("mother_name")]
        public string MotherName { get; private set; }

        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [Column("marital_status")]
        public string MaritalStatus { get; private set; }

        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [Column("responsible_name_1")]
        public string ResponsibleName1 { get; private set; }

        [MaxLength(150, ErrorMessage = "Este campo deve conter no máximo 150 caracteres")]
        [Column("responsible_name_2")]
        public string ResponsibleName2 { get; private set; }

        [MaxLength(255, ErrorMessage = "Este campo deve conter no máximo 255 caracteres")]
        [Column("street")]
        public string Street { get; private set; }

        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        [Column("street_number")]
        public string StreetNumber { get; private set; }

        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [Column("neighborhood")]
        public string Neighborhood { get; private set; }

        [MaxLength(100, ErrorMessage = "Este campo deve conter no máximo 100 caracteres")]
        [Column("city")]
        public string City { get; private set; }

        [MaxLength(50, ErrorMessage = "Este campo deve conter no máximo 50 caracteres")]
        [Column("state")]
        public string State { get; private set; }

        [MaxLength(20, ErrorMessage = "Este campo deve conter no máximo 20 caracteres")]
        [Column("zip_code")]
        public string ZipCode { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DefaultValue(true)]
        [Column("active")]
        public bool Active { get; private set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Column("user_id")]
        [ForeignKey("User")]
        public string? UserId { get; private set; }
        public User? User { get; private set; }
    }
}
