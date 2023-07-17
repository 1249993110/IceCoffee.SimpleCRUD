using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static readonly Lazy<UnitOfWorkFactory> _default = new(() => new UnitOfWorkFactory(DbConnectionFactory.Default), true);
        public static UnitOfWorkFactory Default => _default.Value;

        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IRepositoryFactory? _repositoryFactory;

        private UnitOfWorkFactory(IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        public UnitOfWorkFactory(IDbConnectionFactory dbConnectionFactory, IRepositoryFactory repositoryFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _repositoryFactory = repositoryFactory;
        }

        public IUnitOfWork Create()
        {
            return Create(string.Empty);
        }

        public IUnitOfWork Create(IsolationLevel il)
        {
            return Create(string.Empty, il);
        }

        public IUnitOfWork Create(string dbAliase)
        {
            var connection = _dbConnectionFactory.CreateConnection(dbAliase);
            return new UnitOfWork(connection, _repositoryFactory ?? new RepositoryFactory(dbAliase));
        }

        public IUnitOfWork Create(string dbAliase, IsolationLevel il)
        {
            var connection = _dbConnectionFactory.CreateConnection(dbAliase);
            return new UnitOfWork(connection, _repositoryFactory ?? new RepositoryFactory(dbAliase), il);
        }
    }
}