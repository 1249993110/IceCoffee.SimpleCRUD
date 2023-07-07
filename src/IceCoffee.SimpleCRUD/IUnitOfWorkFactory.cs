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
        /// <param name="dbAliase"></param>
        /// <returns></returns>
        IUnitOfWork Create(string dbAliase);

        /// <summary>
        /// Create a new unit of work.
        /// </summary>
        /// <param name="dbAliase"></param>
        /// <param name="il"></param>
        /// <returns></returns>
        IUnitOfWork Create(string dbAliase, IsolationLevel il);
    }
}
