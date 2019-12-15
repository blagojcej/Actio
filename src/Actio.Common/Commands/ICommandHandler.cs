using System.Threading.Tasks;

namespace Actio.Common.Commands
{
    /// <summary>
    /// Handler for commands
    /// </summary>
    public interface ICommandHandler<in T> where T : ICommand
    {
        /// <summary>
        /// Take command and perform some business logic
        /// </summary>
        /// <param name="command">ICommand object</param>
        /// <returns></returns>
        Task HandleAsync(T command);
    }
}