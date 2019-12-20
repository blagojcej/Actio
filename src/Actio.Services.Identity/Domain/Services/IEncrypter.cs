namespace Actio.Services.Identity.Domain.Services
{
    public interface IEncrypter
    {
        /// <summary>
        /// Get secured string
        /// </summary>
        /// <returns></returns>
        string GetSalt();

        /// <summary>
        /// Get hash for given password and salt
        /// </summary>
        /// <param name="value">Password</param>
        /// <param name="salt">Previously generated salt</param>
        /// <returns></returns>
        string GetHash(string value, string salt);
    }
}