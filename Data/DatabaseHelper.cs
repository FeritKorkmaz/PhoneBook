using Microsoft.Data.SqlClient;

namespace PhoneBook.Data
{
    public static  class DatabaseHelper
    {
        // SQL sorguları veya komutları çalıştırmak için genel metod
        public static  async Task ExecuteNonQueryAsync(string connectionString, string comandText, params SqlParameter[] parameters)
        {
            using(var connection = new SqlConnection(connectionString))
            { // sql bağlantısı açılır.
              await connection.OpenAsync();

                // SQL komutu oluşturulur
                using (var command = new SqlCommand(comandText, connection))
                {
                    // Parametreler komuta eklenir
                    command.Parameters.AddRange(parameters);
                    // Komut, veri değiştirme işlemleri için (INSERT, UPDATE, DELETE) çalıştırılır
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        // SQL sorgularından veri okumak için genel metod
        public static async Task<List<T>> ExecuteReaderAsync<T>(string connectionString, string commandText, Func<SqlDataReader, T> readFunc, params SqlParameter[] parameters)
        {
            // Sonuçları tutmak için bir liste oluşturulur
            var result = new List<T>();
            
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                // SQL komutu oluşturulur
                using (var command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.AddRange(parameters);
                    // Komut, veri okuma işlemleri için (SELECT) çalıştırılır
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        // Okunan her satır için readFunc fonksiyonu kullanılarak sonuçlar listeye eklenir
                        while (await reader.ReadAsync())
                        {
                            result.Add(readFunc(reader));
                        }
                    }
                }
            }
            // Sonuç listesi döndürülür
            return result;
        }
    } 
}
