using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Models.Requests;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Sabio.Web.Services
{
    public class CompanyRatingService : BaseService, ICompanyRatingService
    {
        public int InsertRating(CompanyRatingInsertRequest model)
        {
            int id = 0;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.CompanyRating_Insert"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
                    paramCollection.AddWithValue("@companyId", model.CompanyId);
                    paramCollection.AddWithValue("@rating", model.Rating);
                    paramCollection.AddWithValue("@ratingComment", model.RatingComment);
                    paramCollection.AddWithValue("@raterId", model.RaterId);

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






        public List<CompanyRatingsDomain> GetRatingsByCompanyId(int companyId)
        {

            List<CompanyRatingsDomain> ratingList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.CompanyRating_GetByCompanyId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@companyId", companyId);
              },
                map: delegate (IDataReader reader, short set)
                {
                    var singleRating = new CompanyRatingsDomain();
                    int startingIndex = 0; //startingOrdinal

                    singleRating.CompanyRatingsId = reader.GetSafeInt32(startingIndex++);
                    singleRating.companyId = reader.GetSafeInt32(startingIndex++);
                    singleRating.rating = reader.GetSafeDecimal(startingIndex++);
                    singleRating.ratingComment = reader.GetSafeString(startingIndex++);
                    singleRating.raterId = reader.GetSafeString(startingIndex++);
                    singleRating.createdDate = reader.GetSafeDateTime(startingIndex++);
                    singleRating.reviewerFullName = reader.GetSafeString(startingIndex++);



                    if (ratingList == null)
                    {
                        ratingList = new List<CompanyRatingsDomain>();
                    }

                    ratingList.Add(singleRating);

                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ratingList;
        }






        public CompanyRatingsDomain GetRatingAverageByCompanyId(int companyId)
        {
            CompanyRatingsDomain singleRating = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.CompanyRating_GetAvgByCompanyId"
              , inputParamMapper: delegate (SqlParameterCollection paramCollection)
              {
                  paramCollection.AddWithValue("@companyId", companyId);
              },
              map: delegate (IDataReader reader, short set)
              {
                  singleRating = new CompanyRatingsDomain();
                  int startingIndex = 0; //startingOrdinal

                  singleRating.ratingAverage = reader.GetSafeDecimal(startingIndex++);

              });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return singleRating;
        }


    }
}