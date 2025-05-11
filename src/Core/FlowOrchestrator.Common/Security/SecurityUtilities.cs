using System.Security.Cryptography;
using System.Text;

namespace FlowOrchestrator.Common.Security;

/// <summary>
/// Provides security utilities for the system.
/// </summary>
public static class SecurityUtilities
{
    /// <summary>
    /// Generates a random string of the specified length.
    /// </summary>
    /// <param name="length">The length of the random string.</param>
    /// <returns>A random string of the specified length.</returns>
    public static string GenerateRandomString(int length)
    {
        if (length <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than zero.");
        }
        
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var result = new StringBuilder(length);
        
        for (int i = 0; i < length; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }
        
        return result.ToString();
    }
    
    /// <summary>
    /// Generates a cryptographically secure random string of the specified length.
    /// </summary>
    /// <param name="length">The length of the random string.</param>
    /// <returns>A cryptographically secure random string of the specified length.</returns>
    public static string GenerateSecureRandomString(int length)
    {
        if (length <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than zero.");
        }
        
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var result = new StringBuilder(length);
        var random = new byte[length];
        
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(random);
        }
        
        for (int i = 0; i < length; i++)
        {
            result.Append(chars[random[i] % chars.Length]);
        }
        
        return result.ToString();
    }
    
    /// <summary>
    /// Computes the SHA-256 hash of the specified string.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>The SHA-256 hash of the specified string.</returns>
    public static string ComputeSha256Hash(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));
        }
        
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
    
    /// <summary>
    /// Computes the HMAC-SHA-256 hash of the specified string using the specified key.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <param name="key">The key to use for the HMAC.</param>
    /// <returns>The HMAC-SHA-256 hash of the specified string.</returns>
    public static string ComputeHmacSha256Hash(string input, string key)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));
        }
        
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentException("Key cannot be null or empty.", nameof(key));
        }
        
        using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = hmac.ComputeHash(bytes);
            
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
    
    /// <summary>
    /// Encrypts the specified string using AES encryption with the specified key and initialization vector.
    /// </summary>
    /// <param name="input">The string to encrypt.</param>
    /// <param name="key">The encryption key.</param>
    /// <param name="iv">The initialization vector.</param>
    /// <returns>The encrypted string.</returns>
    public static string EncryptAes(string input, byte[] key, byte[] iv)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));
        }
        
        if (key == null || key.Length != 32) // 256 bits
        {
            throw new ArgumentException("Key must be 32 bytes (256 bits).", nameof(key));
        }
        
        if (iv == null || iv.Length != 16) // 128 bits
        {
            throw new ArgumentException("IV must be 16 bytes (128 bits).", nameof(iv));
        }
        
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(input);
                    }
                    
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }
    
    /// <summary>
    /// Decrypts the specified string using AES encryption with the specified key and initialization vector.
    /// </summary>
    /// <param name="input">The string to decrypt.</param>
    /// <param name="key">The encryption key.</param>
    /// <param name="iv">The initialization vector.</param>
    /// <returns>The decrypted string.</returns>
    public static string DecryptAes(string input, byte[] key, byte[] iv)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentException("Input cannot be null or empty.", nameof(input));
        }
        
        if (key == null || key.Length != 32) // 256 bits
        {
            throw new ArgumentException("Key must be 32 bytes (256 bits).", nameof(key));
        }
        
        if (iv == null || iv.Length != 16) // 128 bits
        {
            throw new ArgumentException("IV must be 16 bytes (128 bits).", nameof(iv));
        }
        
        using (var aes = Aes.Create())
        {
            aes.Key = key;
            aes.IV = iv;
            
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            
            using (var ms = new MemoryStream(Convert.FromBase64String(input)))
            {
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Generates a random encryption key.
    /// </summary>
    /// <returns>A random encryption key.</returns>
    public static byte[] GenerateEncryptionKey()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var key = new byte[32]; // 256 bits
            rng.GetBytes(key);
            return key;
        }
    }
    
    /// <summary>
    /// Generates a random initialization vector.
    /// </summary>
    /// <returns>A random initialization vector.</returns>
    public static byte[] GenerateInitializationVector()
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var iv = new byte[16]; // 128 bits
            rng.GetBytes(iv);
            return iv;
        }
    }
}
