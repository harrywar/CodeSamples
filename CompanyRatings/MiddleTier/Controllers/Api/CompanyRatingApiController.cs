using Microsoft.Practices.Unity;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sabio.Web.Controllers
{
    [RoutePrefix("api/ratings")]

    public class CompanyRatingApiController : ApiController

    {

        // Dependency Injection 

        // Add an instance variable that has the type of the Interface
        [Dependency]
        public ICompanyRatingService _CompanyRatingService { get; set; }

        [Route("insert"), HttpPost]

        public HttpResponseMessage InsertRating(CompanyRatingInsertRequest model)

        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }



            int ratingId = _CompanyRatingService.InsertRating(model);

            ItemResponse<int> response = new ItemResponse<int> { Item = ratingId };

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }




        [Route("get/{companyId}"), HttpGet]

        public HttpResponseMessage GetRatingsByCompanyId(int companyId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<CompanyRatingsDomain> ratingList = _CompanyRatingService.GetRatingsByCompanyId(companyId);

            var response = new ItemsResponse<CompanyRatingsDomain> { Items = ratingList };

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }




        [Route("average/{companyId}"), HttpGet]

        public HttpResponseMessage GetAvgRatingById(int companyId)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            CompanyRatingsDomain averageRating = _CompanyRatingService.GetRatingAverageByCompanyId(companyId);

            var response = new ItemResponse<CompanyRatingsDomain> { Item = averageRating };

            return Request.CreateResponse(HttpStatusCode.OK, response);

        }

    }
}
