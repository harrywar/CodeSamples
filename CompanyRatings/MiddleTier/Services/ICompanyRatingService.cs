using System.Collections.Generic;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;

namespace Sabio.Web.Services
{
    public interface ICompanyRatingService
    {
        CompanyRatingsDomain GetRatingAverageByCompanyId(int companyId);
        List<CompanyRatingsDomain> GetRatingsByCompanyId(int companyId);
        int InsertRating(CompanyRatingInsertRequest model);
    }
}