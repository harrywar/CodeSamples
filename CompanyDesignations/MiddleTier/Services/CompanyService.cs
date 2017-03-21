using Microsoft.Practices.Unity;
using Sabio.Data;
using Sabio.Web.Domain;
using Sabio.Web.Enums;
using Sabio.Web.Models;
using Sabio.Web.Models.Requests;
using Sabio.Web.Models.Requests.company;
using Sabio.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Web.Services
{
	public class CompanyService : BaseService, ICompanyService
    {

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

		public int InsertCompany(CompanyInsertRequest model)
		{
			int id = 0;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Company_Insert"
                      , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                      {
                          paramCollection.AddWithValue("@ownerId", model.OwnerId);
                          paramCollection.AddWithValue("@Name", model.Name);
                          paramCollection.AddWithValue("@TypeId", model.TypeId);
						  paramCollection.AddWithValue("@Phone", model.Phone);
						  paramCollection.AddWithValue("@faxNumber", model.Fax);
						  paramCollection.AddWithValue("@email", model.Email);
						  paramCollection.AddWithValue("@url", model.Url);

                          SqlParameter p = new SqlParameter("@id", System.Data.SqlDbType.Int);
                          p.Direction = System.Data.ParameterDirection.Output;

                          paramCollection.Add(p);

                      }, returnParameters: delegate (SqlParameterCollection param)
                      {
                          int.TryParse(param["@id"].Value.ToString(), out id);
                      });
            }
            catch(Exception ex)
            {
                throw ex;
            }

			return id;
		}



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<CompanyDomain> GetAllCompanies()
		{
            List<CompanyDomain> companyList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Company_SelectAll"
                 , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                 {}
                 , map: delegate (IDataReader reader, short set)
                 {
                     CompanyDomain singleCompany = new CompanyDomain();
                     int startingIndex = 0; //startingOrdinal

                     singleCompany.CompanyId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.OwnerId = reader.GetSafeString(startingIndex++);
                     singleCompany.DateCreated = reader.GetSafeDateTime(startingIndex++);
                     singleCompany.Name = reader.GetSafeString(startingIndex++);
                     singleCompany.TypeId = reader.GetSafeInt32(startingIndex++);
					 singleCompany.Phone = reader.GetSafeString(startingIndex++);
					 singleCompany.Fax = reader.GetSafeString(startingIndex++);
					 singleCompany.Email = reader.GetSafeString(startingIndex++);
					 singleCompany.MediaId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.Url = reader.GetSafeString(startingIndex++);


                     if (companyList == null)
                     {
                         companyList = new List<CompanyDomain>();
                     }

                     companyList.Add(singleCompany);
                 }
              );
            }
            catch(Exception ex)
            {


				throw ex;
            }

			return companyList;
		}








        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<CompanyDomain> GetAllBuyers()
        {
            List<CompanyDomain> companyList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Company_GetAllBuyers"
                 , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                 { }
                 , map: delegate (IDataReader reader, short set)
                 {
                     CompanyDomain singleCompany = new CompanyDomain();
                     int startingIndex = 0; //startingOrdinal

                     singleCompany.CompanyId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.OwnerId = reader.GetSafeString(startingIndex++);
                     singleCompany.DateCreated = reader.GetSafeDateTime(startingIndex++);
                     singleCompany.Name = reader.GetSafeString(startingIndex++);
                     singleCompany.TypeId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.Phone = reader.GetSafeString(startingIndex++);
                     singleCompany.Fax = reader.GetSafeString(startingIndex++);
                     singleCompany.Email = reader.GetSafeString(startingIndex++);
                     singleCompany.MediaId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.Url = reader.GetSafeString(startingIndex++);
                     singleCompany.MediaUrl = reader.GetSafeString(startingIndex++);

                     if (companyList == null)
                     {
                         companyList = new List<CompanyDomain>();
                     }

                     companyList.Add(singleCompany);
                 }
              );
            }
            catch (Exception ex)
            {


                throw ex;
            }

            return companyList;
        }





        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<CompanyDomain> GetAllSuppliers()
        {
            List<CompanyDomain> companyList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Company_GetAllSuppliers"
                 , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                 { }
                 , map: delegate (IDataReader reader, short set)
                 {
                     CompanyDomain singleCompany = new CompanyDomain();
                     int startingIndex = 0; //startingOrdinal

                     singleCompany.CompanyId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.OwnerId = reader.GetSafeString(startingIndex++);
                     singleCompany.DateCreated = reader.GetSafeDateTime(startingIndex++);
                     singleCompany.CompanyName = reader.GetSafeString(startingIndex++);
                     singleCompany.TypeId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.Phone = reader.GetSafeString(startingIndex++);
                     singleCompany.Fax = reader.GetSafeString(startingIndex++);
                     singleCompany.Email = reader.GetSafeString(startingIndex++);
                     singleCompany.MediaId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.Url = reader.GetSafeString(startingIndex++);
                     singleCompany.MediaUrl = reader.GetSafeString(startingIndex++);
                     singleCompany.Designations = reader.GetSafeInt32(startingIndex++);
                     singleCompany.AddressType = reader.GetSafeInt32(startingIndex++);
                     singleCompany.Latitude = reader.GetSafeDecimal(startingIndex++);
                     singleCompany.Longitude = reader.GetSafeDecimal(startingIndex++);
                     singleCompany.Address1 = reader.GetSafeString(startingIndex++);
                     singleCompany.City = reader.GetSafeString(startingIndex++);
                     singleCompany.State = reader.GetSafeString(startingIndex++);
                     singleCompany.ZipCode = reader.GetSafeString(startingIndex++);

                     if (companyList == null)
                     {
                         companyList = new List<CompanyDomain>();
                     }

                     companyList.Add(singleCompany);
                 }
              );
            }
            catch (Exception ex)
            {


                throw ex;
            }

            return companyList;
        }




        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public List<SupplierDomain> GetByCategoryIdAndRadiusAndDesignation(ProductByCategoryRequest model)
        {
            List<SupplierDomain> companyList = null;

            try
            {
                DataProvider.ExecuteCmd(GetConnection, "dbo.Suppliers_GetByCategoryIdAndRadiusAndDesignation"
                 , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                 {
                     paramCollection.AddWithValue("@CategoryId", model.CategoryId);
                     paramCollection.AddWithValue("@latpoint", model.Latitude);
                     paramCollection.AddWithValue("@lngpoint", model.Longitude);
                     paramCollection.AddWithValue("@radius", model.Radius);
                     paramCollection.AddWithValue("@designations", model.Designations);

                 }
                 , map: delegate (IDataReader reader, short set)
                 {
                     SupplierDomain singleCompany = new SupplierDomain();

                     int startingIndex = 0; //startingOrdinal

                     singleCompany.CompanyName = reader.GetSafeString(startingIndex++);
                     singleCompany.Designations = reader.GetSafeInt32(startingIndex++);
                     singleCompany.URL = reader.GetSafeString(startingIndex++);
                     singleCompany.Email = reader.GetSafeString(startingIndex++);
                     singleCompany.MediaUrl = reader.GetSafeString(startingIndex++);
                     singleCompany.Phone = reader.GetSafeString(startingIndex++);
                     singleCompany.CompanyId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.ProductName = reader.GetSafeString(startingIndex++);
                     singleCompany.Category = reader.GetSafeString(startingIndex++);
                     singleCompany.ProductDescription = reader.GetSafeString(startingIndex++);
                     singleCompany.Cost = reader.GetSafeDecimal(startingIndex++);
                     singleCompany.Quantity = reader.GetSafeInt32(startingIndex++);
                     singleCompany.Threshold = reader.GetSafeInt32(startingIndex++);
                     singleCompany.MinPurchase = reader.GetSafeInt32(startingIndex++);
                     singleCompany.ProductMediaId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.CategoryId = reader.GetSafeInt32(startingIndex++);
                     singleCompany.Latitude = reader.GetSafeDecimal(startingIndex++);
                     singleCompany.Longitude = reader.GetSafeDecimal(startingIndex++);
                     singleCompany.Address1 = reader.GetSafeString(startingIndex++);
                     singleCompany.City = reader.GetSafeString(startingIndex++);
                     singleCompany.State = reader.GetSafeString(startingIndex++);
                     singleCompany.ZipCode = reader.GetSafeString(startingIndex++);
                     singleCompany.distance_in_mi = (decimal)reader.GetSafeDouble(startingIndex++);


                     if (companyList == null)
                     {
                         companyList = new List<SupplierDomain>();
                     }

                     companyList.Add(singleCompany);
                 }
              );
            }
            catch (Exception ex)
            {


                throw ex;
            }

            return companyList;
        }




        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public CompanyDomain GetByIdCompany(int id)
		{
			CompanyDomain singleCompany = null;

			try
			{
				DataProvider.ExecuteCmd(GetConnection, "dbo.Company_GetById"
			  , inputParamMapper: delegate (SqlParameterCollection paramCollection)
			  {
				  paramCollection.AddWithValue("@id", id);

			  }, map: delegate (IDataReader reader, short set)
			  {
				  singleCompany = new CompanyDomain();
				  int startingIndex = 0; //startingOrdinal

				  singleCompany.CompanyId = reader.GetSafeInt32(startingIndex++);
				  singleCompany.OwnerId = reader.GetSafeString(startingIndex++);
				  singleCompany.DateCreated = reader.GetSafeDateTime(startingIndex++);
				  singleCompany.Name = reader.GetSafeString(startingIndex++);
				  singleCompany.TypeId = reader.GetSafeInt32(startingIndex++);
				  singleCompany.Phone = reader.GetSafeString(startingIndex++);
				  singleCompany.Fax = reader.GetSafeString(startingIndex++);
				  singleCompany.Email = reader.GetSafeString(startingIndex++);
				  singleCompany.MediaId = reader.GetSafeInt32(startingIndex++);
                  singleCompany.Url = reader.GetSafeString(startingIndex++);
                  singleCompany.MediaUrl = reader.GetSafeString(startingIndex++);
                  singleCompany.Designations = reader.GetSafeInt32(startingIndex++);

              });
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return singleCompany;
		}



		// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

		public bool UpdateCompany(CompanyUpdateRequest model)
		{
			bool success = false;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Company_Update"
                , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                {
					paramCollection.AddWithValue("@id", model.CompanyId);
                    paramCollection.AddWithValue("@CName", model.Name);
                    paramCollection.AddWithValue("@TypeId", model.TypeId);
					paramCollection.AddWithValue("@Phone", model.Phone);
					paramCollection.AddWithValue("@faxNumber", model.Fax);
					paramCollection.AddWithValue("@email", model.Email);
                    paramCollection.AddWithValue("@url", model.Url);
                    paramCollection.AddWithValue("@designations", model.Designations);

                    success = true;
                });
            }
            catch(Exception ex)
            {
                throw ex;
            }
			

			return success;
		}



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public bool DeleteCompany(int id)
		{
			bool success = false;

            try
            {
                DataProvider.ExecuteNonQuery(GetConnection, "dbo.Company_Delete"
                      , inputParamMapper: delegate (SqlParameterCollection paramCollection)
                      {
                          paramCollection.AddWithValue("@id", id);

                          success = true;
                      });
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return success;
        }



        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        public int BuildCompanyOnRegistration(RegisterViewModel model, string ownerId)
        {
            int companyId = 0;


            // Create Company Account
            CompanyInsertRequest company = new CompanyInsertRequest
            {
                Name = model.CompanyName,
                OwnerId = ownerId,
                TypeId = model.CompanyRole
            };

            companyId = InsertCompany(company);

            return companyId;
        }

		

		// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

		public bool UpdateCompanyProfileMediaId(CompanyMediaIdUpdateRequest model)
		{
			bool success = false;

			try
			{
				DataProvider.ExecuteNonQuery(GetConnection, "dbo.Company_UpdateMediaId"
					  , inputParamMapper: delegate (SqlParameterCollection paramCollection)
					  {
						  paramCollection.AddWithValue("@Id", model.CompanyId);
						  paramCollection.AddWithValue("@mediaId", model.MediaId);

						  success = true;
					  });
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return success;
		}
	}
}
