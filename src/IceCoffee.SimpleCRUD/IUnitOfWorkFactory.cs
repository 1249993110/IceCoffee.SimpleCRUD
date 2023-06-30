using System.Data;

namespace IceCoffee.SimpleCRUD
{
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Create a new unit of work.
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Create();

        /// <summary>
        /// Create a new unit of work.
        /// </summary>
        /// <param name="il"></param>
        /// <returns></returns>
        IUnitOfWork Create(IsolationLevel il);

        /// <summary>
        /// Create a new unit of work.
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        IUnitOfWork Create(string connectionName);

        /// <summary>
        /// Create a new unit of work.
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="il"></param>
        /// <returns></returns>
        IUnitOfWork Create(string connectionName, IsolationLevel il);

        /// <summary>
        /// Create a new unit of work.
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        IUnitOfWork Create(Enum connectionName);

        /// <summary>
        /// Create a new unit of work.
        /// </summary>
        /// <param name="connectionName"></param>
        /// <param name="il"></param>
        /// <returns></returns>
        IUnitOfWork Create(Enum connectionName, IsolationLevel il);
    }
}
