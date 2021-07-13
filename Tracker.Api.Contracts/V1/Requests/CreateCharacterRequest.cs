using System.ComponentModel.DataAnnotations;

namespace Tracker.Api.Contracts.V1.Requests {

    public class CreateCharacterRequest {

        private string _firstProfession;
        private string _secondProfession;

        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Class { get; set; }

        public string FirstProfession {
            get => _firstProfession;
            set => _firstProfession = ReplaceEmptyWithNull(value);
        }

        public string SecondProfession {
            get => _secondProfession;
            set => _secondProfession = ReplaceEmptyWithNull(value);
        }

        public bool HasCooking { get; set; }

        private static string ReplaceEmptyWithNull(string value) => string.IsNullOrWhiteSpace(value) ? null : value;

    }

}