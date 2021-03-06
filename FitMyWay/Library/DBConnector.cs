﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FitMyWay.Library
{
	public  class DBConnector
	{
		private const string connString = "Server=tcp:testfit.database.windows.net,1433;" +
			"Initial Catalog=testfit;Persist Security Info=False;User ID=testfit;Password=Ny22quistP;" +
			"MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

		public void SaveFitBitAccessTokens(string accessToken, string refreshToken)
		{
			const string procedureName = "SaveFitbitTokens";

			var parameters = new DynamicParameters();
			parameters.Add("@accessToken", accessToken);
			parameters.Add("@refreshToken", refreshToken);
			
			using (var connection = new SqlConnection(connString))
			{
				connection.Open();

				connection.Execute(procedureName,
					parameters,
					commandType: CommandType.StoredProcedure);
			}
		}

		public IDictionary<string,string> GetFitbitAccessTokens()
		{
			var sql = "Select top 1 fitbitaccesstoken, fitbitrefreshtoken from fituser";
			using (var conn = new SqlConnection(connString))
			{
				return (IDictionary<string, string>)conn.Query(sql).First();
			}
		}

		//
		public async Task<IEnumerable<Activity>> GetUserActivitySuggestions(int userId)
		{
			const string procedureName = "GetUserActivitySuggestions";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);

			return await ExecuteAsync(async conn =>
			{
				return await conn.QueryAsync<Activity>(
					procedureName,
					parameters,
					commandType: CommandType.StoredProcedure
				).ConfigureAwait(false);
			}).ConfigureAwait(false);
		}


		public async Task<IEnumerable<PlanPrescription>> GetPhysicalPlanPrescriptions(int userId)
		{
			const string procedureName = "GetPhysicalPlanPrescriptions";
			var parameters = new DynamicParameters();
			parameters.Add("@userId", userId);

			return await ExecuteAsync(async conn =>
			{
				return await conn.QueryAsync<PlanPrescription>(
					procedureName,
					parameters,
					commandType: CommandType.StoredProcedure
				).ConfigureAwait(false);
			}).ConfigureAwait(false);
		}


		protected async Task<T> ExecuteAsync<T>(Func<SqlConnection, Task<T>> sqlFunc)
		{
			using (var connection = new SqlConnection(connString))
			{
				connection.Open();
				return await sqlFunc(connection)
					.ConfigureAwait(false);
			}
		}
	}
}