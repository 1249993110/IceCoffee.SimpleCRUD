using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static readonly Lazy<UnitOfWorkFactory> _default = new(() => new UnitOfWorkFactory(DbConnectionFactory.Default), true);
        public static UnitOfWorkFactory Default => _default.Value;

        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IRepositoryFactory? _repositoryFactory;

        public UnitOfWorkFactory(IDbConnectionFactory dbConnectionFactory)
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

        public IUnitOfWork Create(string connectionName)
        {
            var connection = _dbConnectionFactory.CreateConnection(connectionName);
            return new UnitOfWork(connection, _repositoryFactory ?? new RepositoryFactory(connectionName));
        }

        public IUnitOfWork Create(string connectionName, IsolationLevel il)
        {
            var connection = _dbConnectionFactory.CreateConnection(connectionName);
            return new UnitOfWork(connection, _repositoryFactory ?? new RepositoryFactory(connectionName), il);
        }

        public IUnitOfWork Create(Enum connectionName)
        {
            return Create(connectionName.ToString());
        }

        public IUnitOfWork Create(Enum connectionName, IsolationLevel il)
        {
            return Create(connectionName.ToString(), il);
        }
    }
}