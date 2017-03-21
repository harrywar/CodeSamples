using Microsoft.Practices.Unity;
using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class ContractService : BaseService, IContractService
    {


        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public int SaveContract(ContractInsertRequest model)
        {
            int id = 0;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Contract_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@quoteRequestId", model.QuoteRequestId);
                    paramCollection.AddWithValue("@quoteId", model.QuoteId);

                    SqlParameter p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                    p.Direction = System.Data.ParameterDirection.Output;

                    paramCollection.Add(p);

                }, returnParameters: delegate (SqlParameterCollection param)
                {
                    int.TryParse(param["@Id"].Value.ToString(), out id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return id;

        }




        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        
        public bool UpdateContractURL(ContractInsertRequest model)
        {
            bool isSuccess = false;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Contract_UpdateURL"
                     , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                     {
                         paramCollection.AddWithValue("@id", model.ContractId);
                         paramCollection.AddWithValue("@url", model.URL);

                         isSuccess = true;
                     });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return isSuccess;
        }







        public ContractDomain GetContractByQuoteId(int id)
        {
            ContractDomain singleContract = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Contract_GetByQuoteId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@quoteId", id);
              },
              map: delegate (IDataReader reader, short set)
              {
                  singleContract = new ContractDomain();
                  int startingIndex = 0; //startingOrdinal

                  singleContract.ContractId = reader.GetSafeInt32(startingIndex++);
                  singleContract.ContractTerms = reader.GetSafeString(startingIndex++);
                  singleContract.QuoteRequestId = reader.GetSafeInt32(startingIndex++);
                  singleContract.QuoteId = reader.GetSafeInt32(startingIndex++);
                  singleContract.StateId = reader.GetSafeInt32(startingIndex++);
                  singleContract.CreateDate = reader.GetSafeDateTime(startingIndex++);
                  singleContract.URL = reader.GetSafeString(startingIndex++);

              });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return singleContract;
        }

    }
}

