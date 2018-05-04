using System.Threading.Tasks;

namespace SecurityService.Core.Messaging
{
	public interface IMessageHandlerCallback
	{
		Task<bool> HandleMessageAsync(MessageTypes messageType, string message);
	}
}