using TechnoDapperBlazor.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TechnoDapperBlazor.Repository
{
    public static class FilmRepo
    {
        public static IDbConnection ConnData => new SqlConnection(new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build().GetConnectionString("SqlDbContext"));
        public static async Task<Film> AddFilmAsync(Film film)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Title", film.Title);
            parameters.Add("ReleaseDate", film.ReleaseDate);
            parameters.Add("Review", film.Review);
            parameters.Add("RunTimeMinutes", film.RunTimeMinutes);
            parameters.Add("BudgetDollars", film.BudgetDollars);
            parameters.Add("BoxOfficeDollars", film.BoxOfficeDollars);
            parameters.Add("OscarNominations", film.OscarNominations);
            parameters.Add("OscarWins", film.OscarWins);

            try
            {
                await dbConnection.ExecuteAsync("spFilm_Insert", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return film;
        }

        public static async Task<Film> DeleteFilmAsync(int filmId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", filmId, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spFilm_Delete", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return null;
        }

        public static async Task<IEnumerable<Film>> GetFilmAsync()
        {
            IEnumerable<Film> films;
            using IDbConnection dbConnection = ConnData;

            try
            {
                films = await dbConnection.QueryAsync<Film>("spFilm_GetAll", commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return films;
        }

        public static async Task<Film> GetFilmByIdAsync(int filmId)
        {
            using IDbConnection dbConnection = ConnData;
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", filmId, DbType.Int32);

            Film film;

            try
            {
                film = await dbConnection.QueryFirstOrDefaultAsync<Film>("spFilm_GetOne", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return film;
        }

        public static async Task<Film> UpdateFilmAsync(Film updatedFilm)
        {
            using IDbConnection dbConnection = ConnData;

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", updatedFilm.FilmID, DbType.Int32);
            parameters.Add("Title", updatedFilm.Title, DbType.String);
            parameters.Add("ReleaseDate", updatedFilm.ReleaseDate);
            parameters.Add("Review", updatedFilm.Review, DbType.String);
            parameters.Add("RunTimeMinutes", updatedFilm.RunTimeMinutes, DbType.Int32);
            parameters.Add("BudgetDollars", updatedFilm.BudgetDollars, DbType.Int32);
            parameters.Add("BoxOfficeDollars", updatedFilm.BoxOfficeDollars, DbType.Int32);
            parameters.Add("OscarNominations", updatedFilm.OscarNominations, DbType.Int32);
            parameters.Add("OscarWins", updatedFilm.OscarWins, DbType.Int32);

            try
            {
                await dbConnection.ExecuteAsync("spFilm_Update", parameters, commandType: CommandType.StoredProcedure);
            }
            finally
            {
                if (dbConnection.State == ConnectionState.Open)
                    dbConnection.Close();
            }
            return updatedFilm;
        }
    }
}
