﻿using Dapper;
using iTEC_Hackathon.DTOs.User;
using iTEC_Hackathon.Interfaces;
using iTEC_Hackathon.Interfaces.User;
using System.Data;

namespace iTEC_Hackathon.Repositories
{
    public class RegisterUserRepository : IRegisterUserRepository
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        public RegisterUserRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> RegisterUserAsyncRepo(UserCredentialsDTO userCredentialsDTO)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", userCredentialsDTO.Email);
            parameters.Add("@Password", userCredentialsDTO.Password);
            parameters.Add("@UserID", dbType: DbType.Int32, direction: ParameterDirection.Output);
            using (var connection = _dbConnectionFactory.ConnectToDataBase())
            {
                await connection.ExecuteAsync("RegisterUser", parameters, commandType: CommandType.StoredProcedure);
                return parameters.Get<int>("UserID");
            }
        }
    }
}