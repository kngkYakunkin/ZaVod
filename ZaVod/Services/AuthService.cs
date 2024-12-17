using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaVod.Models;
using ZaVod.Repositories;

namespace ZaVod.Services
{
    public class AuthService
    {
        private readonly WorkerRepository workerRepository;
        public AuthService()
        {
            this.workerRepository = new WorkerRepository();
        }

        public bool ValidateUser(string login, string password)
        {
            Worker worker = this.workerRepository.GetWorker(login);
            if (worker != null && worker.Password == password) 
            {
                return true;
            }
            return false;
        }
    }
}
