namespace devopsgenie.service.Common
{
    public interface IEncryption
    {
        string EncryptionKey { get; set; }

        string Decrypt(string cipherText);
        string encrypt(string encryptString);
    }
}