using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Api.AppCore.SISS_Integration.Models
{
    /* --> † 31/07/2024 - Luiz Lenire. <-- */

    [Table("connectlogin")]
    public sealed class ConnectLogin
    {
        #region --> Public properties. <--

        [JsonIgnore]
        [Column("identifier")]
        [Key]
        public long Identifier { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [JsonIgnore]
        [Column("role")]
        public string? Role { get; set; }

        [JsonIgnore]
        [Column("included")]
        public DateTime Included { get; set; }

        [JsonIgnore]
        [Column("count")]
        public long Count { get; set; }

        [JsonIgnore]
        [Column("last")]
        public DateTime? Last { get; set; }

        [JsonIgnore]
        [Column("active")]
        public bool Active { get; set; }

        #endregion --> Public properties. <--

        #region --> Constructors. <--

        public ConnectLogin() { }

        public ConnectLogin(string username, string password)
        {
            Username = username;
            Password = password;
        }

        #endregion --> Constructors. <--
    }
}
