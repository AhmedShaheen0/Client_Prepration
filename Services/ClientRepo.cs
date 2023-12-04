using Microsoft.EntityFrameworkCore;
using TestPrepation.Data;
using TestPrepation.Data.Models;
using TestPrepation.Data.ViewModels;

namespace TestPrepation.Services
{
    public class ClientRepo : IClientRepo
    {
        private readonly ApplicationDbContext _context;

        public ClientRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddClient(Client client)
        {
            _context.Client.Add(client);
            _context.SaveChanges();

        }
        public void DeleteClient(int clientId)
        {
            var CLIENT = GetClientById(clientId);
            if (CLIENT != null)
            {
                _context.Client.Remove(CLIENT);
                _context.SaveChanges();

            }
        }

        public IEnumerable<Client> GetAllClients()
        {
            var repo = _context.Client.Include(c => c.MaritalStatus).ToList();

            return repo;
        }

        public Client GetClientById(int clientId)
        {
            return _context.Client.Include(b => b.MaritalStatus).SingleOrDefault(c => c.ClientId == clientId)!;
        }
        public IEnumerable<MaritalStatus> GetAllMaritalStatuses()
        {
            return _context.MaritalStatuse.OrderBy(x=>x.MaritalStatusName).ToList();
        }

        public void UpdateClient(Client client)
        {
            _context.Client.Update(client);
            _context.SaveChanges();
        }
    }
}
