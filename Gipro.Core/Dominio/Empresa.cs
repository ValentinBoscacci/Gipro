using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Core.dominio
{
    public class Empresa /*: IHasTenant*/
    {
        private string _connectionStringEncrypted;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        // Información Básica
        [Required(ErrorMessage = "El nombre de la empresa es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        // ConnectionString con encriptación automática
        [Required]
        [Column("ConnectionString", TypeName = "nvarchar(max)")] // Columna encriptada en BD
        public string ConnectionStringEncrypted
        {
            get => _connectionStringEncrypted;
            set => _connectionStringEncrypted = value;
        }

        [NotMapped]
        public string ConnectionString
        {
            get => Decrypt(_connectionStringEncrypted);
            set => _connectionStringEncrypted = Encrypt(value);
        }

        // Configuración de Tenant
        [Required]
        public TipoTenant TipoTenant { get; set; } = TipoTenant.Pequeña;

        // Campos para empresas grandes (tenant único)
        [StringLength(100)]
        [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Solo minúsculas, números y guiones")]
        public string? Subdominio { get; set; }

        [StringLength(100)]
        [Url(ErrorMessage = "Debe ser una URL válida")]
        public string? DominioPersonalizado { get; set; }

        // Información Fiscal
        [Required(ErrorMessage = "El CUIT es obligatorio")]
        [StringLength(13, MinimumLength = 11, ErrorMessage = "El CUIT debe tener entre 11 y 13 caracteres")]
        [RegularExpression(@"^\d{2}-\d{8}-\d{1}$", ErrorMessage = "Formato inválido (XX-XXXXXXXX-X)")]
        public string CUIT { get; set; }

        [StringLength(200)]
        public string? RazonSocial { get; set; }

        [Required]
        public SituacionFiscal SituacionFiscal { get; set; }

        // Ubicación
        [StringLength(200)]
        public string? DomicilioFiscal { get; set; }

        [StringLength(100)]
        public string? Localidad { get; set; }

        [StringLength(50)]
        public string? Provincia { get; set; }

        [StringLength(50)]
        public string? Pais { get; set; } = "Argentina";

        [StringLength(20)]
        [DataType(DataType.PostalCode)]
        public string? CodigoPostal { get; set; }

        // Contacto
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "El email no tiene un formato válido")]
        public string? EmailContacto { get; set; }

        [StringLength(50)]
        [Phone(ErrorMessage = "El teléfono no tiene un formato válido")]
        public string? TelefonoContacto { get; set; }

        [StringLength(100)]
        public string? PersonaContacto { get; set; }

        // Configuraciones
        public int? IdConfiguration { get; set; }

        [StringLength(10)]
        public string? MonedaPrincipal { get; set; } = "ARS";

        [StringLength(50)]
        public string? ZonaHoraria { get; set; } = "Argentina Standard Time";

        // Metadatos
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;

        public DateTime? FechaBaja { get; set; }

        public bool Activo { get; set; } = true;

        [StringLength(450)]
        public string? UsuarioCreacionId { get; set; }

        [StringLength(450)]
        public string? UsuarioModificacionId { get; set; }

        [Timestamp]
        public byte[]? Version { get; set; }

        // Relaciones
        [ForeignKey("IdConfiguration")]
        public virtual ConfiguracionEmpresa? Configuracion { get; set; }

        public virtual ICollection<Contratista> Contratistas { get; set; } = new List<Contratista>();
        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
        public virtual ICollection<DocumentoRequerido> DocumentosRequeridos { get; set; } = new List<DocumentoRequerido>();

        // Métodos de encriptación mejorados
        private static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;

            try
            {
                using var aes = Aes.Create();
                var key = GetEncryptionKey();
                var iv = GenerateRandomIv();

                using var encryptor = aes.CreateEncryptor(key, iv);
                using var ms = new MemoryStream();
                ms.Write(iv, 0, iv.Length); // Prepend IV

                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }

                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                throw new InvalidOperationException("Error en encriptación");
            }
        }

        private static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;

            try
            {
                var buffer = Convert.FromBase64String(cipherText);
                using var aes = Aes.Create();
                var key = GetEncryptionKey();
                var iv = new byte[16];

                Buffer.BlockCopy(buffer, 0, iv, 0, iv.Length); // Extract IV

                using var decryptor = aes.CreateDecryptor(key, iv);
                using var ms = new MemoryStream(buffer, iv.Length, buffer.Length - iv.Length);
                using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                using var sr = new StreamReader(cs);

                return sr.ReadToEnd();
            }
            catch
            {
                throw new InvalidOperationException("Error en desencriptación");
            }
        }

        private static byte[] GetEncryptionKey()
        {
            var key = Environment.GetEnvironmentVariable("DB_ENCRYPTION_KEY")
                      ?? throw new InvalidOperationException("Encryption key not configured");
            return SHA256.HashData(Encoding.UTF8.GetBytes(key));
        }

        private static byte[] GenerateRandomIv()
        {
            var iv = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(iv);
            return iv;
        }

        // Métodos de negocio
        public bool EsDominioValido(string host)
        {
            if (TipoTenant != TipoTenant.Grande) return false;

            return (!string.IsNullOrEmpty(DominioPersonalizado) &&
                   host.Equals(DominioPersonalizado, StringComparison.OrdinalIgnoreCase)) ||
                   (!string.IsNullOrEmpty(Subdominio) &&
                   host.StartsWith($"{Subdominio}.", StringComparison.OrdinalIgnoreCase));
        }

        public bool RequiereBaseDatosDedicada() =>
            TipoTenant == TipoTenant.Grande && !string.IsNullOrEmpty(ConnectionString);
    }

    public enum TipoTenant
    {
        Pequeña,    // Comparte base de datos (SaaS)
        Grande      // Base de datos dedicada
    }

    public enum SituacionFiscal
    {
        ResponsableInscripto,
        Monotributo,
        Exento,
        NoResponsable,
        ConsumidorFinal
    }
}