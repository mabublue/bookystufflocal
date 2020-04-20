using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using bookystufflocal.domain.DomainLayer.BaseModels;
using LanguageExt;

namespace bookystufflocal.domain.DomainLayer.Library
{
    public class Author
    {
        protected Author()
        {
            //EF ctor
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }

        private Author(string firstName, string middleName, string lastname)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastname;
        }

        public static Either<IEnumerable<ValidationError>, Author> TryCreate(string firstName, string middleName, string lastname)
        {
            var errors = ValidateCanCreateAuthor(lastname).ToList();

            if (errors.Any()) return errors;

            return new Author(firstName, middleName, lastname);
        }
        
        public string FullName()
        {
            var fullName = FirstName;

            fullName += fullName != string.Empty ? $" {MiddleName}" : MiddleName;
            fullName += fullName != string.Empty ? $" {LastName}" : LastName;

            return fullName;
        }

        private static IEnumerable<ValidationError> ValidateCanCreateAuthor(string lastname)
        {
            if (lastname == null)
                yield return new ValidationError(nameof(lastname), ValidationMessages.ThisFieldIsRequired);
        }
    }
}
