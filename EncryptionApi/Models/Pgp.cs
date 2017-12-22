namespace EncryptionApi.Models {
    public class Pgp {
        public string EncryptionKey { get; set; }
        public string SigningKey { get; set; }
        public string Password { get; set; }

        public string SecureParameters { get; set; }
    }
}
