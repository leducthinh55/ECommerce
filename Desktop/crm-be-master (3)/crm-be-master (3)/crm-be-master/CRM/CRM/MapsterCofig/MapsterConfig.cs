
using CRM.Model;
using CRM.Service;
using CRM.ViewModels;
using Mapster;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace CRM.MapsterCofig
{
    public class MapsterConfig
    {
        public MapsterConfig()
        {
        }

        public void Run()
        {
            TypeAdapterConfig<HsRoleOfGroup, RoleViewModel>.NewConfig()
                                .Map(dest => dest.Id, src => src.RoleId)
                                .Map(dest => dest.Name, src => src.Role.Name);

            TypeAdapterConfig<HsGroupUser, UserViewModel>.NewConfig()
                                .Map(dest => dest.Id, src => src.UserId)
                                .Map(dest => dest.UserName, src => src.User.UserName)
                                .Map(dest => dest.FullName, src => src.User.FullName);

            TypeAdapterConfig<HsGroupUser, GroupViewModel>.NewConfig()
                                .Map(dest => dest.Id, src => src.GroupId)
                                .Map(dest => dest.Name, src => src.Group.Name);
            TypeAdapterConfig<FormUM, Form>.NewConfig()
                                .Map(dest => dest.Formulas, src => JsonConvert.SerializeObject(src.Formulas))
                                .Map(dest => dest.NumbertoWordFields, src => JsonConvert.SerializeObject(src.NumbertoWordFields));
            TypeAdapterConfig<FormCM, Form>.NewConfig()
                                .Map(dest => dest.Formulas, src => JsonConvert.SerializeObject(src.Formulas))
                                .Map(dest => dest.NumbertoWordFields, src => JsonConvert.SerializeObject(src.NumbertoWordFields));
            TypeAdapterConfig<Form, FormVM>.NewConfig()
                                .Map(dest => dest.Formulas, src => src.Formulas == null ? null : JsonConvert.DeserializeObject<Formular[]>(src.Formulas))
                                .Map(dest => dest.NumbertoWordFields, src => src.NumbertoWordFields == null ? null : JsonConvert.DeserializeObject<string[]>(src.NumbertoWordFields));
            //TypeAdapterConfig<UserCreateModel , HsUser>.NewConfig()
            //                    .Map(dest => dest.UserName, src => src.UserName);

            TypeAdapterConfig<GlobalVariableValue, FileCommonVM>.NewConfig()
                                .Map(dest => dest.Name, src => Path.GetFileName(src.Value).Substring(Path.GetFileName(src.Value).IndexOf('_') + 1))
                                .Map(dest => dest.Id, src => src.Id)
                                .Map(dest => dest.DateCreated, src => src.DateCreated)
                                .Map(dest => dest.Type, src => FileType.Global);

            TypeAdapterConfig<WorkFlowHistoryFile, FileCommonVM>.NewConfig()
                                .Map(dest => dest.Name, src => src.Name)
                                .Map(dest => dest.Id, src => src.Id)
                                .Map(dest => dest.DateCreated, src => src.Date)
                                .Map(dest => dest.Type, src => FileType.WorkFlowHistory);

            TypeAdapterConfig<ContractTelecom, ContractTelecomVM>.NewConfig()
                                .Map(dest => dest.Name, src => src.Customer.Owner != null ? src.Customer.Owner.Name : null)
                                .Map(dest => dest.Position, src => src.Customer.Owner != null ? src.Customer.Owner.Position : null)
                                .Map(dest => dest.BirthDate, src => src.Customer.Owner != null ? src.Customer.Owner.Birthday : null)
                                .Map(dest => dest.CustomerName, src => src.Customer.Name)
                                .Map(dest => dest.CustomerCode, src => src.Customer.Code)

                                .Map(dest => dest.Phone, src => src.Customer.Tel)
                                .Map(dest => dest.Email, src => src.Customer.Email);

            //.Map(dest => dest.DeputyName, )
            //.Map(dest => dest.DeputyPosition, );
            TypeAdapterConfig<Contract, ContractVM>.NewConfig()
                                .Map(dest => dest.CompanyName, src => src.Customer.Name)
                                .Map(dest => dest.CompanyCode, src => src.Customer.Code)

                                //.Map(dest => dest.DeputyName, src => src.Customer.DeputyName)
                                //.Map(dest => dest.DeputyPosition, src => src.Customer.DeputyPosition)
                                .Map(dest => dest.Tel, src => src.Customer.Tel)
                                .Map(dest => dest.Email, src => src.Customer.Email)
                                .Map(dest => dest.Fax, src => src.Customer.Fax)
                                ;
            TypeAdapterConfig<Contract, RevenueVM>.NewConfig()
                                .Map(dest => dest.CompanyName, src => src.Customer.Name);
            #region Customer
            TypeAdapterConfig<CustomerCM, Customer>.NewConfig()
                                .Map(dest => dest.Owner, src => src.OwnerUM);
            TypeAdapterConfig<Customer, CustomerDetailVM>.NewConfig()
                                .Map(dest => dest.OwnerVM, src => src.Owner)
                                .Map(dest => dest.PersonnelVM, src => src.Personnel)
                                .Map(dest => dest.DeputyVM, src => src.Deputy)
                                .Map(dest => dest.AmountVM, src => src.Amount);
                                //.Map(dest => dest.SignDayActivities, src => _contractService.GetContracts(c => c.CustomerId == src.Id).Select(c => c.StartDate).FirstOrDefault());
            TypeAdapterConfig<CustomerDetailVM, Customer>.NewConfig()
                                .Map(dest => dest.Owner, src => src.OwnerVM)
                                .Map(dest => dest.Personnel, src => src.PersonnelVM)
                                .Map(dest => dest.Deputy, src => src.DeputyVM)
                                .Map(dest => dest.Amount, src => src.AmountVM);
            TypeAdapterConfig<CustomerUM, Customer>.NewConfig()
                                .Map(dest => dest.Owner, src => src.OwnerUM)
                                .Map(dest => dest.Personnel, src => src.PersonnelUM)
                                .Map(dest => dest.Deputy, src => src.DeputyUM)
                                .Map(dest => dest.Amount, src => src.AmountUM);
            TypeAdapterConfig<CustomerCML, Customer>.NewConfig()
                                .Map(dest => dest.Owner, src => src.OwnerCML)
                                .Map(dest => dest.Deputy, src => src.DeputyCML)
                                .Map(dest => dest.Personnel, src => src.PersonnelCML)
                                .Map(dest => dest.Amount, src => src.AmountCML);
            TypeAdapterConfig<CustomerUM, Customer>.NewConfig()
                                .Map(dest => dest.Owner, src => src.OwnerUM)
                                .Map(dest => dest.Deputy, src => src.DeputyUM)
                                .Map(dest => dest.Personnel, src => src.PersonnelUM)
                                .Map(dest => dest.Amount, src => src.AmountUM);

            #endregion
        }
    }
}
