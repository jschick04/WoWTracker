using System.ComponentModel.DataAnnotations;
using Tracker.Api.Contracts.Helpers;

namespace Tracker.Api.Contracts.V1.Requests {

    public class UpdateRequest {

        private string _firstName;
        private string _lastName;
        private string _username;
        private string _password;
        private string _confirmPassword;

        public string FirstName {
            get => _firstName;
            set => _firstName = ReplaceEmptyWithNull(value);
        }

        public string LastName {
            get => _lastName;
            set => _lastName = ReplaceEmptyWithNull(value);
        }

        [EmailAddress]
        public string Username {
            get => _username;
            set => _username = ReplaceEmptyWithNull(value);
        }

        [Password]
        [DataType(DataType.Password)]
        public string Password {
            get => _password;
            set => _password = ReplaceEmptyWithNull(value);
        }

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword {
            get => _confirmPassword;
            set => _confirmPassword = ReplaceEmptyWithNull(value);
        }

        private static string ReplaceEmptyWithNull(string value) => string.IsNullOrWhiteSpace(value) ? null : value;

    }

}