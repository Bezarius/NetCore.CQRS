using System;
using System.Threading.Tasks;

namespace DbContextScope.UnitOfWork.Core.Interfaces
{
    /// <summary>
    /// Defines Unit Of Work methods.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Saves the changes made to the repositories.
        /// </summary>
        void Save();

        /// <summary>
        /// Saves the changes made to the repositories asynchronously.
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();
    }
}
