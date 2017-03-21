using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
    public interface IContractService
    {
        int SaveContract(ContractInsertRequest model);

        bool UpdateContractURL(ContractInsertRequest model);

        ContractDomain GetContractByQuoteId(int quoteId);

    }
}
