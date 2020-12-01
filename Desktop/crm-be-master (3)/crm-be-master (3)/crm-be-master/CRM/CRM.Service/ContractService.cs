using CRM.Data.Infrastructure;
using CRM.Data.Repositories;
using CRM.Model;
using CRM.Service.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
//using Word = Microsoft.Office.Interop.Word;
namespace CRM.Service
{
    public interface IContractService
    {
        IEnumerable<Contract> GetContracts();
        IEnumerable<Contract> GetContracts(Expression<Func<Contract, bool>> where);
        Contract GetContractById(Guid Id);
        void Create(Contract contract);
        void DeleteContract(Contract contract);
        void UpdateContract(Contract contract);
        void SaveContract();
        double FormulaCaculator(double[] numbers, string operations);
        string NumberToText(double d, ref string number);
    }
    public class ContractService : IContractService
    {
        private readonly IContractRepository _ContractRepositry;
        private readonly IUnitOfWork _UnitOfWork;

        public ContractService(IContractRepository _ContractRepositry, IUnitOfWork _UnitOfWork)
        {
            this._ContractRepositry = _ContractRepositry;
            this._UnitOfWork = _UnitOfWork;
        }

        public IEnumerable<Contract> GetContracts()
        {
            return _ContractRepositry.GetAll();
        }

        public void DeleteContract(Contract contract)
        {
            _ContractRepositry.Delete(contract);
        }

        public void UpdateContract(Contract contract)
        {
            _ContractRepositry.Update(contract);
        }

        public Contract GetContractById(Guid Id)
        {
            return _ContractRepositry.GetById(Id);
        }

        public void SaveContract()
        {
            _UnitOfWork.Commit();
        }

        public string NumberToText(double d, ref string number)
        {
            number = String.Format("{0:0,0.00}", d);
            string temp;
            string result = number[0] == '-' ? "âm " : "";
            string[] numbers = number.Replace("-", "").Split(".")[0].Split(",");

            switch (numbers.Length)
            {
                case 4:
                    result += CharArrayToText(numbers[numbers.Length - 4].ToCharArray()) + " tỷ ";
                    goto case 3;
                case 3:
                    temp = CharArrayToText(numbers[numbers.Length - 3].ToCharArray());
                    result += string.IsNullOrEmpty(temp) ? "" : temp + " triệu ";
                    goto case 2;
                case 2:
                    temp = CharArrayToText(numbers[numbers.Length - 2].ToCharArray());
                    result += string.IsNullOrEmpty(temp) ? "" : temp + " nghìn ";
                    goto default;
                default:
                    result += CharArrayToText(numbers[numbers.Length - 1].ToCharArray());
                    break;
            }

            char[] charArray = number.Split(".")[1].ToCharArray();
            if (charArray[0] == '0' && charArray[1] == '0')
            {
                number = String.Format("{0:0,0}", d);
                if (d < 10) number = number[1].ToString();
            }
            else if (charArray[0] == '0') result += " phẩy không " + CharArrayToText(charArray[1]);
            else if (charArray[1] == '0') result += " phẩy " + CharArrayToText(charArray[0]);
            else result += " phẩy " + CharArrayToText(charArray);

            return result;
        }

        private string CharArrayToText(params char[] charArray)
        {
            if (charArray.Length == 1)
            {
                switch (charArray[0])
                {
                    case '0': return "không";
                    case '1': return "một";
                    case '2': return "hai";
                    case '3': return "ba";
                    case '4': return "bốn";
                    case '5': return "năm";
                    case '6': return "sáu";
                    case '7': return "bảy";
                    case '8': return "tám";
                    case '9': return "chín";
                    default: return "";
                }
            }
            if (charArray.Length == 2)
            {
                if (charArray[0] == '0')
                    return CharArrayToText(charArray[1]);
                if (charArray[0] == '1')
                    if (charArray[1] == '0') return "mười";
                    else if (charArray[1] == '5') return "mười lăm";
                    else return "mười " + CharArrayToText(charArray[1]);
                else
                {
                    string result = CharArrayToText(charArray[0]) + " mươi";
                    if (charArray[1] == '0') return result;
                    else if (charArray[1] == '1') return result + " mốt";
                    else if (charArray[1] == '5') return result + " lăm";
                    else return result + " " + CharArrayToText(charArray[1]);
                }
            }
            else
            {
                if (charArray[0] == '0' && charArray[1] == '0' && charArray[2] == '0') return "";
                string result = CharArrayToText(charArray[0]) + " trăm";
                if (charArray[1] == '0')
                    if (charArray[2] == '0') return result;
                    else return result + " lẻ " + CharArrayToText(charArray[2]);
                else return result + " " + CharArrayToText(charArray[1], charArray[2]);
            }
        }

        public double FormulaCaculator(double[] numbers, string operations)
        {
            double result = numbers[0];
            for (int i = 0; i < operations.Length; i++)
            {
                switch (operations[i])
                {
                    case '+': result = result + numbers[i + 1]; break;
                    case '-': result = result - numbers[i + 1]; break;
                    case '*': result = result * numbers[i + 1]; break;
                    default: result = result / numbers[i + 1]; break;
                }
            }

            return result;
        }

        public void Create(Contract contract)
        {
            _ContractRepositry.Add(contract);
        }

        public IEnumerable<Contract> GetContracts(Expression<Func<Contract, bool>> where)
        {
            return _ContractRepositry.GetMany(where);
        }

        
    }
}
