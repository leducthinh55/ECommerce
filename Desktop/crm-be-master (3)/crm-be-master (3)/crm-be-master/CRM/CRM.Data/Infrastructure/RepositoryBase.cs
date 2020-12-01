using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CRM.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Properties
        private CRMContext dataContext;
        private readonly DbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected CRMContext DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #region Implementation
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
  //          dbSet.FromSql("SELECT TOP (1000) Customers.Id
  //    +", Customers.No"
  //    +", Customers.Code"
  //    +", Customers.Name"
  //    +", Customers.ShortName"
  //    +", Customers.Country"
  //    +", Customers.CountryType"
  //    +", Customers.TimeOfChangeInvestmentCertificates"
  //    +", Customers.TimeOfChangeBusinessLicense"
  //    +", Customers.TransactionName"
  //    +", Customers.CompanyType"
  //    +", Customers.Agency"
  //    +", Customers.ObjectDetail"
  //    +", Customers.ProductHighlight"
  //    +", Customers.Address"
  //    +", Customers.AddressFloor"
  //    +", Customers.NumberOfActivities"
  //    +", Customers.StartYear"
  //    +", Customers.Tel"
  //    +", Customers.Fax"
  //    +", Customers.Email"
  //    +", Customers.Website"
  //    +", Customers.NumberOfBusinessLicense"
  //    +", Customers.NumberOfInvestmentCertificate"
  //    +", Customers.AddressProvince"
  //    +", Customers.DateOfIssuingBusinessLicense"
  //    +", Customers.AddressRoom"
  //    +", Customers.TaxCode"
  //    +", Customers.EndYear"
  //    +", Customers.MarketActive"
  //    +", Customers.MainCarrer"
  //    +", Customers.MemberOfQuangTrungSoftware"
  //    +", Customers.Carrer"
  //    +", Customers.BusinessType"
  //    +", Customers.ExpirationDateActivities"
  //    +", Customers.DateOfIssuingInvestmentCertificate"
  //    +", Customers.Note"
  //    +", Customers.MarketType"
  //    +", Customers.ObjectType"
  //    +", Customers.ProfilePicture"
  //    +", Customers.CustomerTypeId"
  //    +", Customers.IdentityCardId"
  //    +", Customers.ActiveDay"
  //    +", Customers.AddressBuilding"
  //    +", Customers.VondautuProcess"
  //    +", Customers.VondautuRegister"
  //    +", Customers.VondieuleUSD"
  //    +", Customers.VondieuleVND"
  //    +", Customers.IsReal"
  //    +", Customers.TableTel"
  //    +", Deputys.Name as DeputyName"
  //    +", Deputys.Position as DeputyPosition"
  //    +", Deputys.Gender as DeputyGender"
  //    +", Deputys.Birthday as DeputyBirthday"
  //    +", Deputys.Country as DeputyCountry"
  //    +", Deputys.PhoneNumber as DeputyPhoneNumber"
  //    +", Deputys.Email as DeputyEmail"
  //    +", Owners.Name as OwnerName"
  //    +", Owners.CompanyCode as OwnerCompanyCode"
  //    +", Owners.IssuingCompanyPlace as OwnerIssuingCompanyPlace"
  //    +", Owners.IssuingCompanyDate as OwnerIssuingCompanyDate"
  //    +", Owners.AddressMainTown as OwnerAddressMainTown"
  //    +", Owners.LegalRepresentativePeople as OwnerLegalRepresentativePeople"
  //    +", Owners.Position as OwnerPosition"
  //    +", Owners.Gender as OwnerGender"
  //    +", Owners.Birthday as OwnerBirthday"
  //    +", Owners.Country as OwnerCountry"
  //    +", Personnels.TotalEmployee as PersonnelTotalEmployee"
  //    +", Personnels.TotalEmployeeOutSide as PersonnelTotalEmployeeOutSide"
  //    +", Personnels.TotalEmployeeInSide as PersonnelTotalEmployeeInSide"
  //    +", Personnels.TotalDomesticLecturersOutside as PersonnelTotalDomesticLecturersOutside"
  //    +", Personnels.TotalDomesticLecturersOutsideCollege as PersonnelTotalDomesticLecturersOutsideCollege"
  //    +", Personnels.TotalDomesticLecturersOutsideAssociateProfessor as PersonnelTotalDomesticLecturersOutsideAssociateProfessor"
  //    +", Personnels.TotalDomesticEmployeeOutside as PersonnelTotalDomesticEmployeeOutside"
  //    +", Personnels.TotalDomesticEmployeeOutsideProfessor as PersonnelTotalDomesticEmployeeOutsideProfessor"
  //    +", Personnels.TotalDomesticEmployeeOutsideAssociateProfessor as PersonnelTotalDomesticEmployeeOutsideAssociateProfessor"
  //    +", Personnels.TotalDomesticLecturersOutsideDoctor as PersonnelTotalDomesticLecturersOutsideDoctor"
  //    +", Personnels.TotalDomesticLecturersOutsideMaster as PersonnelTotalDomesticLecturersOutsideMaster"
  //    +", Personnels.TotalDomesticLecturersOutsideIntermediate as PersonnelTotalDomesticLecturersOutsideIntermediate"
  //    +", Personnels.TotalInternationalEmployeeOutsideCollege as PersonnelTotalInternationalEmployeeOutsideCollege"
  //    +", Personnels.TotalInternationalEmployeeOutsideIntermediate as PersonnelTotalInternationalEmployeeOutsideIntermediate"
  //    +", Personnels.TotalInternationalEmployeeOutsideDoctor as PersonnelTotalInternationalEmployeeOutsideDoctor"
  //    +", Personnels.TotalInternationalEmployeeOutsideMaster as PersonnelTotalInternationalEmployeeOutsideMaster"
  //    +", Personnels.TotalInternationalEmployeeOutsideProfessor as PersonnelTotalInternationalEmployeeOutsideProfessor"
  //    +", Personnels.TotalInternationalEmployeeOutsideOrther as PersonnelTotalInternationalEmployeeOutsideOrther"
  //    +", Personnels.TotalDomesticLecturersOutsideOrther as PersonnelTotalDomesticLecturersOutsideOrther"
  //    +", Personnels.TotalDomesticLecturersOutsideUniversity as PersonnelTotalDomesticLecturersOutsideUniversity"
  //    +", Personnels.TotalDomesticLecturersOutsideProfessor as PersonnelTotalDomesticLecturersOutsideProfessor"
  //    +", Personnels.TotalDomesticLecturersInsideMaster as PersonnelTotalDomesticLecturersInsideMaster"
  //    +", Personnels.TotalDomesticEmployeeInsideMaster as PersonnelTotalDomesticEmployeeInsideMaster"
  //    +", Personnels.TotalFeMaleEmployee as PersonnelTotalFeMaleEmployee"
  //    +", Personnels.TotalInternationalEmployeeInside as PersonnelTotalInternationalEmployeeInside"
  //    +", Personnels.TotalFeMaleEmployeeInside as PersonnelTotalFeMaleEmployeeInside"
  //    +", Personnels.TotalFemaleLecturers as PersonnelTotalFemaleLecturers"
  //    +", Personnels.TotalFemaleLecturersOutside as PersonnelTotalFemaleLecturersOutside"
  //    +", Personnels.TotalInternationalEmployeeInsideAssociateProfessor as PersonnelTotalInternationalEmployeeInsideAssociateProfessor"
  //    +", Personnels.TotalFeMaleEmployeeOutside as PersonnelTotalFeMaleEmployeeOutside"
  //    +", Personnels.TotalFemaleLecturersInside as PersonnelTotalFemaleLecturersInside"
  //    +", Personnels.TotalInternationalEmployee as PersonnelTotalInternationalEmployee"
  //    +", Personnels.TotalDomesticEmployeeInsideOther as PersonnelTotalDomesticEmployeeInsideOther"
  //    +", Personnels.TotalDomesticEmployeeInsideUniversity as PersonnelTotalDomesticEmployeeInsideUniversity"
  //    +", Personnels.TotalDomesticEmployeeInsideProfessor as PersonnelTotalDomesticEmployeeInsideProfessor"
  //    +", Personnels.TotalDomesticEmployeeOutsideDoctor as PersonnelTotalDomesticEmployeeOutsideDoctor"
  //    +", Personnels.TotalDomesticEmployeeOutsideMaster as PersonnelTotalDomesticEmployeeOutsideMaster"
  //    +", Personnels.TotalDomesticEmployeeOutsideUniversity as PersonnelTotalDomesticEmployeeOutsideUniversity"
  //    +", Personnels.TotalDomesticEmployeeOutsideCollege as PersonnelTotalDomesticEmployeeOutsideCollege"
  //    +", Personnels.TotalDomesticEmployeeOutsideIntermediate as PersonnelTotalDomesticEmployeeOutsideIntermediate"
  //    +", Personnels.TotalDomesticEmployeeOutsideOther as PersonnelTotalDomesticEmployeeOutsideOther"
  //    +", Personnels.TotalInternationalEmployeeInsideCollege as PersonnelTotalInternationalEmployeeInsideCollege"
  //    +", Personnels.TotalInternationalEmployeeOutside as PersonnelTotalInternationalEmployeeOutside"
  //    +", Personnels.TotalInternationalEmployeeInsideDoctor as PersonnelTotalInternationalEmployeeInsideDoctor"
  //    +", Personnels.TotalInternationalEmployeeInsideMaster as PersonnelTotalInternationalEmployeeInsideMaster"
  //    +", Personnels.TotalInternationalEmployeeInsideProfessor as PersonnelTotalInternationalEmployeeInsideProfessor"
  //    +", Personnels.TotalInternationalEmployeeOutsideAssociateProfessor as PersonnelTotalInternationalEmployeeOutsideAssociateProfessor"
  //    +", Personnels.TotalInternationalEmployeeInsideIntermediate as PersonnelTotalInternationalEmployeeInsideIntermediate"
  //    +", Personnels.TotalInternationalEmployeeInsideOrther as PersonnelTotalInternationalEmployeeInsideOrther"
  //    +", Personnels.TotalInternationalEmployeeInsideUniversity as PersonnelTotalInternationalEmployeeInsideUniversity"
  //    +", Personnels.TotalDomesticLecturersInsideOrther as PersonnelTotalDomesticLecturersInsideOrther"
  //    +", Personnels.TotalDomesticLecturersInsideUniversity as PersonnelTotalDomesticLecturersInsideUniversity"
  //    +", Personnels.TotalDomesticLecturersInsideProfessor as PersonnelTotalDomesticLecturersInsideProfessor"
  //    +", Personnels.TotalDomesticLecturersInside as PersonnelTotalDomesticLecturersInside"
  //    +", Personnels.TotalDomesticLecturersInsideCollege as PersonnelTotalDomesticLecturersInsideCollege"
  //    +", Personnels.TotalDomesticLecturersInsideIntermediate as PersonnelTotalDomesticLecturersInsideIntermediate"
  //    +", Personnels.TotalDomesticLecturers as PersonnelTotalDomesticLecturers"
  //    +", Personnels.TotalDomesticLecturersInsideAssociateProfessor as PersonnelTotalDomesticLecturersInsideAssociateProfessor"
  //    +", Personnels.TotalDomesticLecturersInsideDoctor as PersonnelTotalDomesticLecturersInsideDoctor"
  //    +", Personnels.TotalInternationalEmployeeOutsideUniversity as PersonnelTotalInternationalEmployeeOutsideUniversity"
  //    +", Personnels.TotalOfficialEmployeeOutside as PersonnelTotalOfficialEmployeeOutside"
  //    +", Personnels.TotalInternationalLecturersInsideOrther as PersonnelTotalInternationalLecturersInsideOrther"
  //    +", Personnels.TotalMaleEmployeeOutside as PersonnelTotalMaleEmployeeOutside"
  //    +", Personnels.TotalMaleLecturersInside as PersonnelTotalMaleLecturersInside"
  //    +", Personnels.TotalMaleLecturers as PersonnelTotalMaleLecturers"
  //    +", Personnels.TotalInternationalLecturersInsideDoctor as PersonnelTotalInternationalLecturersInsideDoctor"
  //    +", Personnels.TotalInternationalLecturersInsideMaster as PersonnelTotalInternationalLecturersInsideMaster"
  //    +", Personnels.TotalInternationalLecturersInsideIntermediate as PersonnelTotalInternationalLecturersInsideIntermediate"
  //    +", Personnels.TotalMaleLecturersOutside as PersonnelTotalMaleLecturersOutside"
  //    +", Personnels.TotalOfficialEmployeeInside as PersonnelTotalOfficialEmployeeInside"
  //    +", Personnels.TotalOfficialEmployee as PersonnelTotalOfficialEmployee"
  //    +", Personnels.TotalSoftwareEmployee as PersonnelTotalSoftwareEmployee"
  //    +", Personnels.TotalSoftwareEmployeeOutside as PersonnelTotalSoftwareEmployeeOutside"
  //    +", Personnels.TotalSoftwareEmployeeInside as PersonnelTotalSoftwareEmployeeInside"
  //    +", Personnels.TotalLecturersInside as PersonnelTotalLecturersInside"
  //    +", Personnels.TotalInternationalLecturers as PersonnelTotalInternationalLecturers"
  //    +", Personnels.TotalOfficialLecturers as PersonnelTotalOfficialLecturers"
  //    +", Personnels.TotalOtherEmployeeInside as PersonnelTotalOtherEmployeeInside"
  //    +", Personnels.TotalOfficialLecturersInside as PersonnelTotalOfficialLecturersInside"
  //    +", Personnels.TotalOtherAlumnus as PersonnelTotalOtherAlumnus"
  //    +", Personnels.TotalOtherAlumnusOutside as PersonnelTotalOtherAlumnusOutside"
  //    +", Personnels.TotalOtherEmployeeOutside as PersonnelTotalOtherEmployeeOutside"
  //    +", Personnels.TotalOfficialLecturersOutside as PersonnelTotalOfficialLecturersOutside"
  //    +", Personnels.TotalOtherAlumnusInside as PersonnelTotalOtherAlumnusInside"
  //    +", Personnels.TotalOtherEmployee as PersonnelTotalOtherEmployee"
  //    +", Personnels.TotalInternationalLecturersInside as PersonnelTotalInternationalLecturersInside"
  //    +", Personnels.TotalInternationalLecturersInsideCollege as PersonnelTotalInternationalLecturersInsideCollege"
  //    +", Personnels.TotalInternationalLecturersInsideAssociateProfessor as PersonnelTotalInternationalLecturersInsideAssociateProfessor"
  //    +", Personnels.TotalInternationalLecturersInsideUniversity as PersonnelTotalInternationalLecturersInsideUniversity"
  //    +", Personnels.TotalInternationalLecturersOutsideAssociateProfessor as PersonnelTotalInternationalLecturersOutsideAssociateProfessor"
  //    +", Personnels.TotalInternationalLecturersOutsideDoctor as PersonnelTotalInternationalLecturersOutsideDoctor"
  //    +", Personnels.TotalInternationalLecturersInsideProfessor as PersonnelTotalInternationalLecturersInsideProfessor"
  //    +", Personnels.TotalInternationalLecturersOutside as PersonnelTotalInternationalLecturersOutside"
  //    +", Personnels.TotalInternationalLecturersOutsideCollege as PersonnelTotalInternationalLecturersOutsideCollege"
  //    +", Personnels.TotalPartTimeEmployee as PersonnelTotalPartTimeEmployee"
  //    +", Personnels.TotalSoftwareAlumnusInside as PersonnelTotalSoftwareAlumnusInside"
  //    +", Personnels.TotalPartTimeEmployeeInside as PersonnelTotalPartTimeEmployeeInside"
  //    +", Personnels.TotalPartTimeLecturers as PersonnelTotalPartTimeLecturers"
  //    +", Personnels.TotalPartTimeLecturersOutside as PersonnelTotalPartTimeLecturersOutside"
  //    +", Personnels.TotalSoftwareAlumnusOutside as PersonnelTotalSoftwareAlumnusOutside"
  //    +", Personnels.TotalPartTimeEmployeeOutside as PersonnelTotalPartTimeEmployeeOutside"
  //    +", Personnels.TotalPartTimeLecturersInside as PersonnelTotalPartTimeLecturersInside"
  //    +", Personnels.TotalSoftwareAlumnus as PersonnelTotalSoftwareAlumnus"
  //    +", Personnels.TotalLecturersOutside as PersonnelTotalLecturersOutside"
  //    +", Personnels.TotalMaleEmployeeInside as PersonnelTotalMaleEmployeeInside"
  //    +", Personnels.TotalMaleEmployee as PersonnelTotalMaleEmployee"
  //    +", Personnels.TotalInternationalLecturersOutsideMaster as PersonnelTotalInternationalLecturersOutsideMaster"
  //    +", Personnels.TotalInternationalLecturersOutsideProfessor as PersonnelTotalInternationalLecturersOutsideProfessor"
  //    +", Personnels.TotalLecturers as PersonnelTotalLecturers"
  //    +", Personnels.TotalInternationalLecturersOutsideIntermediate as PersonnelTotalInternationalLecturersOutsideIntermediate"
  //    +", Personnels.TotalInternationalLecturersOutsideOrther as PersonnelTotalInternationalLecturersOutsideOrther"
  //    +", Personnels.TotalInternationalLecturersOutsideUniversity as PersonnelTotalInternationalLecturersOutsideUniversity"
  //    +", Personnels.TotalAlumnus as PersonnelTotalAlumnus"
  //    +", Personnels.TotalAlumnusOutside as PersonnelTotalAlumnusOutside"
  //    +", Personnels.TotalAlumnusInside as PersonnelTotalAlumnusInside"
  //    +", Personnels.TotalDomesticEmployeeInsideIntermediate as PersonnelTotalDomesticEmployeeInsideIntermediate"
  //    +", Personnels.TotalDomesticEmployeeInsideDoctor as PersonnelTotalDomesticEmployeeInsideDoctor"
  //    +", Personnels.TotalDomesticEmployeeInside as PersonnelTotalDomesticEmployeeInside"
  //    +", Personnels.TotalDomesticEmployeeInsideAssociateProfessor as PersonnelTotalDomesticEmployeeInsideAssociateProfessor"
  //    +", Personnels.TotalDomesticEmployeeInsideCollege as PersonnelTotalDomesticEmployeeInsideCollege"
  //    +", Personnels.TotalDomesticEmployee as PersonnelTotalDomesticEmployee"
  //    +", Personnels.TotalStudent as PersonnelTotalStudent"
  //    +", Personnels.TotalStudentOutside as PersonnelTotalStudentOutside"
  //    +", Personnels.TotalStudentInside as PersonnelTotalStudentInside"
  //    , Amounts.TotalVndRevenue as AmountTotalVndRevenue
  //    , Amounts.TotalUsdRevenue as AmountTotalUsdRevenue
  //    , Amounts.TotalVndRevenueOutside as AmountTotalVndRevenueOutside
  //    , Amounts.TotalVndRevenueInside as AmountTotalVndRevenueInside
  //    , Amounts.TotalUsdRevenueOutside as AmountTotalUsdRevenueOutside
  //    , Amounts.TotalUsdRevenueInside as AmountTotalUsdRevenueInside
  //    , Amounts.TotalDomesticVndRevenue as AmountTotalDomesticVndRevenue
  //    , Amounts.TotalDomesticUsdRevenue as AmountTotalDomesticUsdRevenue
  //    , Amounts.TotalDomesticVndRevenueOutside as AmountTotalDomesticVndRevenueOutside
  //    , Amounts.TotalDomesticVndRevenueInside as AmountTotalDomesticVndRevenueInside
  //    , Amounts.TotalDomesticUsdRevenueOutside as AmountTotalDomesticUsdRevenueOutside
  //    , Amounts.TotalDomesticUsdRevenueInside as AmountTotalDomesticUsdRevenueInside
  //    , Amounts.TotalExportVndRevenue as AmountTotalExportVndRevenue
  //    , Amounts.TotalExportUsdRevenue as AmountTotalExportUsdRevenue
  //    , Amounts.TotalExportVndRevenueOutside as AmountTotalExportVndRevenueOutside
  //    , Amounts.TotalExportVndRevenueInside as AmountTotalExportVndRevenueInside
  //    , Amounts.TotalExportUsdRevenueOutside as AmountTotalExportUsdRevenueOutside
  //    , Amounts.TotalExportUsdRevenueInside as AmountTotalExportUsdRevenueInside
  //    , Amounts.RatioOfExportRevenue as AmountRatioOfExportRevenue
  //FROM Customers
  //INNER JOIN Deputys ON Deputys.CustomerId = Customers.Id
  //INNER JOIN Owners ON Owners.CustomerId = Customers.Id
  //INNER JOIN Personnels ON Personnels.CustomerId = Customers.Id
  //INNER JOIN Amounts ON Amounts.CustomerId = Customers.Id")
        }

        public virtual void Update(T entity)
        {
            
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T GetById(Guid id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public virtual IQueryable<T> _GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        #endregion

    }
}
