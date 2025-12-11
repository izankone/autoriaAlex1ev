using EntregaAlex.Models;
using MySqlConnector;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace EntregaAlex.Repository
{
    public class OpinionesRepository : IOpinionesRepository
    {
        private readonly string _connectionString;

        public OpinionesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        public async Task<List<Opiniones>> GetAllAsync()
        {
            var lista = new List<Opiniones>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                string query = "SELECT Id, NombreCompleto, FechaCreacion, Puntuacion, Mensaje, EventoId FROM Opiniones";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        lista.Add(new Opiniones
                        {
                            Id = reader.GetInt32(0),
                            NombreCompleto = reader.GetString(1),
                            FechaCreacion = reader.GetDateTime(2),
                            Puntuacion = reader.GetInt32(3),
                            Mensaje = reader.GetString(4),
                            EventoId = reader.GetInt32(5) 
                        });
                    }
                }
            }
            
            return lista;
        }

        public async Task<Opiniones?> GetByIdAsync(int id)
        {
            Opiniones? opiniones = null;

            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                
                string query = "SELECT Id, NombreCompleto, FechaCreacion, Puntuacion, Mensaje, EventoId FROM Opiniones WHERE Id = @Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            opiniones = new Opiniones
                            {
                                Id = reader.GetInt32(0),
                                NombreCompleto = reader.GetString(1),
                                FechaCreacion = reader.GetDateTime(2),
                                Puntuacion = reader.GetInt32(3),
                                Mensaje = reader.GetString(4),
                                EventoId = reader.GetInt32(5) 
                            };
                        }
                    }
                }
            }
            return opiniones;
        }

        public async Task<Opiniones> CreateAsync(Opiniones opiniones)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = @"INSERT INTO Opiniones (NombreCompleto, FechaCreacion, Puntuacion, Mensaje, EventoId) 
                                 VALUES (@NombreCompleto, @FechaCreacion, @Puntuacion, @Mensaje, @EventoId);
                                 SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreCompleto", opiniones.NombreCompleto);
                    command.Parameters.AddWithValue("@FechaCreacion", opiniones.FechaCreacion);
                    command.Parameters.AddWithValue("@Puntuacion", opiniones.Puntuacion);
                    command.Parameters.AddWithValue("@Mensaje", opiniones.Mensaje);
                    command.Parameters.AddWithValue("@EventoId", opiniones.EventoId);

                    var id = await command.ExecuteScalarAsync();
                    if (id != null) opiniones.Id = Convert.ToInt32(id);
                }
            }
            return opiniones;
        }

        public async Task<Opiniones?> UpdateAsync(Opiniones opiniones)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // CORREGIDO: Añadido EventoId en el UPDATE por si cambia de evento
                string query = @"UPDATE Opiniones 
                                 SET NombreCompleto=@NombreCompleto, FechaCreacion=@FechaCreacion, 
                                     Puntuacion=@Puntuacion, Mensaje=@Mensaje, EventoId=@EventoId 
                                 WHERE Id=@Id";

                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@NombreCompleto", opiniones.NombreCompleto);
                    command.Parameters.AddWithValue("@FechaCreacion", opiniones.FechaCreacion);
                    command.Parameters.AddWithValue("@Puntuacion", opiniones.Puntuacion);
                    command.Parameters.AddWithValue("@Mensaje", opiniones.Mensaje);
                    command.Parameters.AddWithValue("@EventoId", opiniones.EventoId); // Nuevo parámetro
                    command.Parameters.AddWithValue("@Id", opiniones.Id);

                    int filas = await command.ExecuteNonQueryAsync();
                    if (filas == 0) return null;
                }
            }
            return opiniones;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                string query = "DELETE FROM Opiniones WHERE Id = @Id";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    return await command.ExecuteNonQueryAsync() > 0;
                }
            }
        }
    }
}