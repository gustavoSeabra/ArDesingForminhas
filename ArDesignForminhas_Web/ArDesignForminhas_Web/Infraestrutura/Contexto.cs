using System;
using MySql.Data.MySqlClient;
using Dapper;
using System.Collections.Generic;

namespace ArDesignForminhas_Web.Infraestrutura
{
    public static class Contexto
    {
        /// <summary>
        /// Propriedade que armazena a string de conexão com o Banco
        /// </summary>
        private static MySqlConnection Conexao
        {
            get 
            {
                var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["bancoLocal"].ConnectionString;

                return new MySqlConnection(connectionString);
            }
        }


        /// <summary>
        /// Método responsável por obter um registro do banco
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="sql">Consulta a ser executada</param>
        /// <param name="parameters">Parametros da consulta</param>
        /// <returns>Um registro do tipo informado</returns>
        public static T Obter<T>(string sql, DynamicParameters parameters)
        {
            using(var conn = Conexao)
            {
                return Conexao.QuerySingle<T>(sql, parameters);
            }
        }

        /// <summary>
        /// Método responsável por obter uma lista de registros do banco
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="sql">Consulta a ser executada</param>
        /// <param name="parameters">Parametros da consulta</param>
        /// <returns>Lista de objetos do banco</returns>
        public static IEnumerable<T> Listar<T>(string sql, DynamicParameters parameters)
        {
            using (var conn = Conexao)
            {
                return Conexao.Query<T>(sql, parameters);
            }
        }

        /// <summary>
        /// Método responsável por obter uma lista de registros do banco
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="sql">Consulta a ser executada</param>
        /// <returns>Lista de objetos do banco</returns>
        public static IEnumerable<T> Listar<T>(string sql)
        {
            using (var conn = Conexao)
            {
                return Conexao.Query<T>(sql);
            }
        }

        /// <summary>
        /// Método responsável por executar um comando no banco. Como Insert, Update e Delete
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns>Quantidade de linhas afetadas</returns>
        public static int Executar(string sql, DynamicParameters parameters)
        {
            using (var conn = Conexao)
            {
                return Conexao.Execute(sql, parameters);
            }
        }

        public static object ListarQueryMultiplo(string sql, DynamicParameters parameters)
        {
            using (var conn = Conexao)
            {
                return Conexao.QueryMultiple(sql, parameters);
            }
        }
    }
}