using Dapper;
using GuestBook.DAL.Entites;
using GuestBook.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuestBook.DAL.Repositories
{
    public class GuestRepository : IGuestRepository
    {
        private readonly string _connectionString;

        public GuestRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }


        public async Task<Guest> GetGuestAsync(string GuestName)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sqlCommand = $"SELECT * FROM {nameof(Guest)} WHERE Name = @Name";
                var GuestId = await conn.QueryFirstOrDefaultAsync<Guest>(sqlCommand, new { Name = GuestName });
                return GuestId;
            }

        }


        public async Task<IEnumerable<Guest>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sqlCommand = $"SELECT * FROM {nameof(Guest)}";
                var GuestId = await conn.QueryAsync<Guest>(sqlCommand);
                return GuestId;
            }
        }

        public async Task<Guest> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sqlCommand = $"SELECT * FROM {nameof(Guest)} WHERE Id = @Id";
                var GuestId = await conn.QueryFirstOrDefaultAsync<Guest>(sqlCommand, new { Id = id });
                return GuestId;
            }
        }

        public async Task<Guest> InsertAsync(Guest item)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sqlCommand = $"INSERT INTO {nameof(Guest)} ( [{nameof(Guest.GuestName)}], [{nameof(Guest.PasswordHash)}] , [{nameof(Guest.CreationDate)}]) "
                                   + " OUTPUT INSERTED.Id " +
                                   " Values( @Name , @PasswordHash , @CreationDate)";
                var GuestId = await conn.ExecuteScalarAsync<int>(sqlCommand, item);
                item.Id = GuestId;
            }
            return item;
        }

    }
}
