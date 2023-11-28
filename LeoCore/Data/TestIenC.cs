using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LeoCore.Data
{
    public interface IService
    {
        void Serve();
    }


    public class Service1 : IService
    {
        public void Serve() { Debug.WriteLine("111111111"); }
    }
    public class Service2 : IService
    {
        public void Serve() { Debug.WriteLine("2222222222222"); }
    }

    public class Client {
        private IService _service;
        public Client(IService service)
        { 
            _service = service;
        }

        public void ServeMethod()
        {
            this._service.Serve();

        }
    
    }
}
