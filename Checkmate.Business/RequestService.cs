using Checkmate.Data;
using Checkmate.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Checkmate.Business
{
    public class RequestService : IRequestService, IDb
    {
        public AppDataDb db { get; }
        public RequestService(AppDataDb dbContext)
        {
            this.db = dbContext;
        }

        public IQueryable<Request> GetRequests()
        {
            return this.db.Requests;
        }

        public Request GetByID(int Id)
        {
            return this.db.Requests.Where(requestId => requestId.Id == Id).FirstOrDefault();
        }

        public void Update(Request reqeust)
        {
            db.Entry(reqeust).State = EntityState.Modified;
            db.Requests.Update(reqeust);
            db.SaveChanges();
        }

        public bool UpdateRequest(List<int> requests, int typeId)
        {
            bool retVal = false;
            try
            {
                foreach (var item in requests)
                {
                    Request request = GetByID(item);
                    if (request != null)
                    {
                        request.RequestType = typeId;
                        Update(request);
                    }
                }
                retVal = true;
            }
            catch (Exception ex)
            { 
            
            }
            return retVal;
        }

        public void InsertEmployees()
        {
            //return this.db.Employees;


            for (int i = 1; i < 35; i++)
            {
                if (i % 2 == 0)
                {
                    this.db.Requests.Add(new Request()
                    {
                        //RequestID = i.ToString(),
                        //Code = "IT" + i,
                        //Name = "Claudio" + i,
                        BookletSize = 10,
                        RequestID = "BK" + i.ToString(),
                        RequestType = 2,
                        BookletAccountNumber = "007917918",
                        OperatorName = "UserOps",
                        PrinterBranchID = "ITL129",
                        PrinterClientID = "22111",
                        BookletStyleName = "style" + i.ToString(),
                        BookletAccountName = "accountName" + i.ToString()
                    });
                    this.db.SaveChanges();
                }
                else
                {
                    this.db.Requests.Add(new Request()
                    {
                        //req Id = i,
                        //Code = "IT" + i,
                        //Name = "Edoardo" + i,
                        BookletSize = 25,
                        RequestID = "BK" + i.ToString(),
                        RequestType = 1,
                        BookletAccountNumber = "001247919",
                        OperatorName = "SelfService",
                        PrinterBranchID = "ITL148",
                        PrinterClientID = "12311",
                        BookletStyleName = "style" + i.ToString(),
                        BookletAccountName = "accountName" + i.ToString()
                    });
                    this.db.SaveChanges();
                }
            }

        }
    }
}
