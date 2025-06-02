using Domain;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class NieuwsberichtRepository : INieuwsberichtRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public NieuwsberichtRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<Nieuwsbericht>> GetAllAsync()
        {
            var lijst = new List<Nieuwsbericht>();
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "SELECT * FROM Nieuwsbericht";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lijst.Add(new Nieuwsbericht(
                            reader.GetInt32("NieuwsID"),
                            reader.GetString("Titel"),
                            reader.GetString("Inhoud"),
                            reader.GetDateTime("Publicatiedatum")
                        ));
                    }
                }
            }
            return lijst;
        }

        public async Task AddAsync(Nieuwsbericht nieuws)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = @"INSERT INTO Nieuwsbericht (Titel, Inhoud, Publicatiedatum) 
                              VALUES (@Titel, @Inhoud, @Datum)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Titel", nieuws.Titel);
                    command.Parameters.AddWithValue("@Inhoud", nieuws.Inhoud);
                    command.Parameters.AddWithValue("@Datum", nieuws.Publicatiedatum);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
