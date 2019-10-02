using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Movies.Core.Contract.Request
{
    public class MovieSearchRequest : IValidatableObject
    {
        public string Title { get; set; }
        public int? YearOfRelease { get; set; }
        public IList<string> Genres { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Title) && !YearOfRelease.HasValue && (Genres == null || !Genres.Any()))
                yield return new ValidationResult(
                    "At least one of the search fields need to be populated",
                    new[] {"Title", "YearOfRelease", "Genres"});
        }
    }
}