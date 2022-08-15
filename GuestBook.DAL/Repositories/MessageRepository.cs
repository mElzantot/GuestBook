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
    public class MessageRepository : IMessageRepository
    {
        private readonly string _connectionString;

        public MessageRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sqlCommand = $"UPDATE {nameof(Message)} SET {nameof(Message.IsDeleted)} = 1 WHERE {nameof(Message.Id)} = @{nameof(Message.Id)}";
                var result = await conn.ExecuteAsync(sqlCommand, new { id = (int)id });
                return result > 0;
            }
        }


        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var result = await conn.QueryAsync<Message>($"SELECT * FROM {nameof(Message)} WHERE {nameof(Message.IsDeleted)} != 1 ");
                return result;
            }
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var result = await conn.QueryFirstOrDefaultAsync<Message>($"SELECT * FROM {nameof(Message)} WHERE {nameof(Message.IsDeleted)} = @id AND {nameof(Message.IsDeleted)} != 1 ", new { Id = id });
                return result;
            }
        }

        public async Task<Message> InsertAsync(Message item)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                try
                {

                    conn.Open();
                    string sqlCommand = $"INSERT INTO {nameof(Message)} ( [{nameof(Message.MessageBody)}] , [{nameof(Message.GuestId)}] , [{nameof(Message.CreationDate)}] , [{nameof(Message.ParentMessageId)}])" +
                                         " OUTPUT INSERTED.Id " +
                                         $" Values( @{nameof(Message.MessageBody)} , @{nameof(Message.GuestId)} , @{nameof(Message.CreationDate)} , @{nameof(Message.ParentMessageId)})";

                    var result = await conn.ExecuteScalarAsync<int>(sqlCommand, item);
                    item.Id = result;
                    return item;
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }

        public async Task<bool> UpdateAsync(Message entity)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sqlCommand = $"UPDATE {nameof(Message)} SET {nameof(Message.MessageBody)} = @{nameof(Message.MessageBody)} WHERE {nameof(Message.Id)} = @{nameof(Message.Id)}";
                var result = await conn.ExecuteAsync(sqlCommand, entity);
                return result > 0;
            }
        }

    }
}
