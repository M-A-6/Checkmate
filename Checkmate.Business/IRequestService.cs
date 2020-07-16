using Checkmate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkmate.Business
{
    public interface IRequestService
    {
        IQueryable<Request> GetRequests();

        Request GetByID(int Id);

        void Update(Request reqeust);
        void InsertEmployees();

        bool UpdateRequest(List<int> requests, int typeId);        

    }
}
