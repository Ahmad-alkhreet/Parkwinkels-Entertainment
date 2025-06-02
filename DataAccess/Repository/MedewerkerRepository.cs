using Domain;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccess.Repositories
{
    public class MedewerkerRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public MedewerkerRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<Medewerker>> GetAllAsync()
        {
            var medewerkers = new List<Medewerker>();
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "SELECT * FROM Medewerker";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        medewerkers.Add(new Medewerker(
                            reader.GetInt32("MedewerkerID"),
                            reader.GetString("Naam"),
                            reader.GetString("Rol"),
                            reader.GetString("Email")
                        ));
                    }
                }
            }
            return medewerkers;
        }

        public async Task AddAsync(Medewerker medewerker)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "INSERT INTO Medewerker (Naam, Rol, Email) VALUES (@Naam, @Rol, @Email)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Naam", medewerker.Naam);
                    command.Parameters.AddWithValue("@Rol", medewerker.Rol);
                    command.Parameters.AddWithValue("@Email", medewerker.Email);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
