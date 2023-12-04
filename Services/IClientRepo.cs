using TestPrepation.Data.Models;
using TestPrepation.Data.ViewModels;

namespace TestPrepation.Services
{
    public interface IClientRepo
    {
        IEnumerable<Client> GetAllClients();
        Client GetClientById(int clientId);
        void AddClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(int clientId);
        IEnumerable<MaritalStatus> GetAllMaritalStatuses();
    }
}
