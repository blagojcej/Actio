using System;
using System.Windows.Input;

namespace Actio.Common.Commands
{
    /// <summary>
    /// Interface that requires user to be authenticated
    /// </summary>
    public interface IAuthenticatedCommand : ICommand
    {
        Guid UserId { get; set; }
    }
}