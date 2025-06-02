using Domain;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccess.Repositories
{
    public class ActiviteitRepository : IActiviteitRepository
    {
        private readonly DatabaseHelper _dbHelper;

        public ActiviteitRepository(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<List<Activiteit>> GetAllAsync()
        {
            var activiteiten = new List<Activiteit>();
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = "SELECT * FROM Activiteit";
                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        activiteiten.Add(new Activiteit(
                            reader.GetInt32("ActiviteitID"),
                            reader.GetString("Naam"),
                            reader.GetString("Beschrijving"),
                            reader.GetString("Locatie"),
                            reader.GetDateTime("Starttijd"),
                            reader.GetDateTime("Eindtijd"),
                            reader.GetInt32("MaxDeelnemers")
                        ));
                    }
                }
            }
            return activiteiten;
        }

        public async Task AddAsync(Activiteit activiteit)
        {
            using (var connection = await _dbHelper.GetConnectionAsync())
            {
                var query = @"INSERT INTO Activiteit 
                (Naam, Beschrijving, Locatie, Starttijd, Eindtijd, MaxDeelnemers) 
                VALUES (@Naam, @Beschrijving, @Locatie, @Starttijd, @Eindtijd, @MaxDeelnemers)";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Naam", activiteit.GetNaam());
                    command.Parameters.AddWithValue("@Beschrijving", activiteit.GetBeschrijving());
                    command.Parameters.AddWithValue("@Locatie", activiteit.GetLocatie());
                    command.Parameters.AddWithValue("@Starttijd", activiteit.GetStarttijd());
                    command.Parameters.AddWithValue("@Eindtijd", activiteit.GetEindtijd());
                    command.Parameters.AddWithValue("@MaxDeelnemers", activiteit.GetMaxDeelnemers());
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
