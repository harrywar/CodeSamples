using Microsoft.Practices.Unity;
using Sabio.Web.Domain;
using Sabio.Web.Domain.Quotes;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.company;
using Sabio.Web.Models.Responses;
using Sabio.Web.Services;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace Sabio.Web.Controllers.Api
{
    [RoutePrefix("api/companies")]
    [Authorize]
    public class CompanyApiController : ApiController

    {

        [Dependency]
        public IQuoteRequestService _QuoteRequestService { get; set; }

        [Dependency]
        public ICompanyService _CompanyService { get; set; }

        [Dependency]
        public IQuoteService _QuoteService { get; set; }

        [Dependency]
        public IBidService _BidService { get; set; }

        // =========================================================================================

        [Route(), HttpPost]
        public HttpResponseMessage CompanyInsert(CompanyInsertRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<int> response = new ItemResponse<int>();

            response.Item = _CompanyService.InsertCompany(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //==========================================================================================

        [Route(), HttpGet]
        public HttpResponseMessage CompanyGetAll()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<CompanyDomain> response = new ItemsResponse<CompanyDomain>();

            response.Items = _CompanyService.GetAllCompanies();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        //==========================================================================================

        [Route("buyers"), HttpGet]
        public HttpResponseMessage CompanyGetAllBuyers()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<CompanyDomain> response = new ItemsResponse<CompanyDomain>();

            response.Items = _CompanyService.GetAllBuyers();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        //==========================================================================================


        [Route("suppliers/categoryId"), HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetByCategoryIdAndRadiusAndDesignation([FromUri] ProductByCategoryRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<SupplierDomain> response = new ItemsResponse<SupplierDomain>();

            response.Items = _CompanyService.GetByCategoryIdAndRadiusAndDesignation(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        //==========================================================================================

        [Route("suppliers"), HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage CompanyGetAllSuppliers()
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemsResponse<CompanyDomain> response = new ItemsResponse<CompanyDomain>();

            response.Items = _CompanyService.GetAllSuppliers();

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }



        //==========================================================================================
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage CompanyGetById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            CompanyDomain company = _CompanyService.GetByIdCompany(id);

            var response = new ItemResponse<CompanyDomain> { Item = company };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //==========================================================================================

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage CompanyEdit(CompanyUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<bool> response = new ItemResponse<bool>();

            response.Item = _CompanyService.UpdateCompany(model);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //==========================================================================================

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage CompanyDelete(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            ItemResponse<bool> response = new ItemResponse<bool>();

            response.Item = _CompanyService.DeleteCompany(id);

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //==========================================================================================
        //- QUOTE REQUEST TABLE - GET ALL
        [Route("{id:int}/quoterequesttable"), HttpGet]
        public HttpResponseMessage CompanyQuoteRequestTable(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<QuoteRequestDomain> quoteRequestList = _QuoteRequestService.GetQuoteRequestsByCompanyId(id);

            ItemsResponse<QuoteRequestDomain> response = new ItemsResponse<QuoteRequestDomain> { Items = quoteRequestList };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //=============================================================================================

        [Route("{id:int}/activequoterequest"), HttpGet]
        public HttpResponseMessage CompanyQuoteRequestTablebyState(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<QuoteRequestDomain> quoteRequestList = _QuoteRequestService.GetQuoteRequestsByCompanyIdAndStatus(id);

            ItemsResponse<QuoteRequestDomain> response = new ItemsResponse<QuoteRequestDomain> { Items = quoteRequestList };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //==========================================================================================

        [Route("{id:int}/activebidrequest"), HttpGet]
        public HttpResponseMessage CompanyBidTablebyState(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<BidDomain> bidList =  _BidService.GetBidsByCompanyIdAndStatus(id);

            ItemsResponse<BidDomain> response = new ItemsResponse<BidDomain> { Items = bidList };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        //==========================================================================================

        [Route("{id:int}/media"), HttpPut]
        public HttpResponseMessage CompanyProfileMediaIdUpdate(CompanyMediaIdUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            bool isSuccessful = _CompanyService.UpdateCompanyProfileMediaId(model);

            var response = new ItemResponse<bool> { Item = isSuccessful };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        [Route("{id:int}/buyerquotes"), HttpGet]
        public HttpResponseMessage CompanyGetQuotesById(int id)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            List<QuoteDomain> quoteList = _QuoteService.QuoteGetByBuyerCompanyId(id);

            var response = new ItemsResponse<QuoteDomain> { Items = quoteList };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}

