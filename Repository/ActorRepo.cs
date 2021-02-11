using Dapper;
using TechnoDapperBlazor.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TechnoDapperBlazor.Repository
{
    public class ActorRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("SqlDbContext"));

        public static List<ActorInfo> GetActorById(int actorId)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnData.ConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", actorId, DbType.Int32);

            string sQuery = @"spActor_GetOne";
            var actor = sqlConnection.Query<ActorInfo>(sQuery, parameters, commandType: CommandType.StoredProcedure);
            return actor.ToList();
        }

        public static async Task<List<ActorInfo>> GetActorAsync()
        {
            IEnumerable<ActorInfo> actors;

            await using SqlConnection sqlConnection = new SqlConnection(ConnData.ConnectionString);
            string sQuery = @"spActor_GetAll";
            actors = sqlConnection.Query<ActorInfo>(sQuery, commandType: CommandType.StoredProcedure);
         
            return actors.ToList();
        }

        public static async Task<ActorInfo> AddActorAsync(ActorInfo actor)
        {
            await using SqlConnection sqlConnection = new SqlConnection(ConnData.ConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("FirstName", actor.FirstName);
            parameters.Add("FamilyName", actor.FamilyName);
            parameters.Add("DoB", actor.DoB);
            parameters.Add("DoD", actor.DoD);
            parameters.Add("GenderType", actor.GenderType);

            string sQuery = @"spActor_Insert";
            await sqlConnection.ExecuteAsync(sQuery, parameters, commandType: CommandType.StoredProcedure);

            return null;
        }

        public static async Task<ActorInfo> UpdateActorAsync(ActorInfo updatedActor)
        {
            await using SqlConnection sqlConnection = new SqlConnection(ConnData.ConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedActor.ActorID, DbType.Int32);
            parameters.Add("FirstName", updatedActor.FirstName);
            parameters.Add("FamilyName", updatedActor.FamilyName);
            parameters.Add("DoB", updatedActor.DoB);
            parameters.Add("DoD", updatedActor.DoD);
            parameters.Add("GenderType", updatedActor.GenderType);

            string sQuery = @"spActor_Update";
            await sqlConnection.ExecuteAsync(sQuery, parameters, commandType: CommandType.StoredProcedure);
            
            return updatedActor;
        }

        public static async Task<ActorInfo> DeleteActorAsync(int actorId)
        {
            await using SqlConnection sqlConnection = new SqlConnection(ConnData.ConnectionString);
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", actorId, DbType.Int32);

            string sQuery = @"spActor_Delete";
            await sqlConnection.ExecuteAsync(sQuery, parameters, commandType: CommandType.StoredProcedure);
           
            return null;
        }

        public static IEnumerable<ActorInfo> GetItems()
        {
            using IDbConnection dbConnection = ConnData;

            dbConnection.Open();

            var result = dbConnection.Query<ActorInfo>("spActor_GetAll").ToList();
            dbConnection.Close();
            return result;
        }
    }
}