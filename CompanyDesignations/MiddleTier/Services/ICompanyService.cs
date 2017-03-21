using Sabio.Web.Domain;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services.Interfaces
{
     public interface ICompanyService
    {
        int InsertCompany(CompanyInsertRequest model);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        List<CompanyDomain> GetAllCompanies();

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        List<CompanyDomain> GetAllBuyers();

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        List<CompanyDomain> GetAllSuppliers();

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        List<SupplierDomain> GetByCategoryIdAndRadiusAndDesignation(ProductByCategoryRequest model);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        CompanyDomain GetByIdCompany(int id);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        bool UpdateCompany(CompanyUpdateRequest model);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        bool DeleteCompany(int id);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        int BuildCompanyOnRegistration(RegisterViewModel model, string ownerId);

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        bool UpdateCompanyProfileMediaId(CompanyMediaIdUpdateRequest model);
        
    }
}
