using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.ViewModels
{
    #region Form Data 
    public class FormDataCM
    {
        public FormCM Form { get; set; }
        public List<FormGroupVM> FormGroups { get; set; }
    }

    public class FormDataVM
    {
        public FormDataVM()
        {
            FormData = null;
        }
        public FormVM Form { get; set; }

        public List<FormGroupVM> FormGroups { get; set; }
        public object FormData { get; set; }
    }

    public class FormDataUM
    {
        public FormUM Form { get; set; }

        public List<FormGroupVM> FormGroups { get; set; }
    }
    #endregion

    #region Form 
    public class FormCM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public Formular[] Formulas { get; set; }
        public string[] NumbertoWordFields { get; set; }
        public Guid? HsWorkflowId { get; set; }
    }

    public class FormVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Method { get; set; }
        public Formular[] Formulas { get; set; }
        public string[] NumbertoWordFields { get; set; }
		public Guid? HsWorkflowId { get; set; }
	}
    
    public class FormUM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Method { get; set; }
        public Formular[] Formulas { get; set; }
        public string[] NumbertoWordFields { get; set; }
    }

    public class Formular
    {
        public string Name { get; set; }
        public string[] Fields { get; set; }
    }

    #endregion

    #region Form Group
    public class FormGroupVM
    {
        public String Type { get; set; }
        public String Label { get; set; }
        public String SubLabel { get; set; }
        public String InputType { get; set; }
        public String InputMask { get; set; }
        public String Name { get; set; }
        public String Placeholder { get; set; }
        public String DateFormat { get; set; }
        public String[] Class { get; set; }
        public List<String> Options { get; set; }
        public bool IsMultiple { get; set; }
        public IEnumerable<ValidationVM> Validations { get; set; }
        public int Order { get; set; }
        public Guid? GlobalVariableId { get; set; }
        public bool? SelectMultiple { get; set; }
    }

    public class FormGroupDataVM : FormGroupVM
    {
        public FileConfig FileConfig { get; set; }
        public string FieldType { get; set; }
    }

    public class FileConfig
    {
        public FileConfig()
        {
            //FileType = "input-file";    read-write
            //FileType = "download-file"; read
            //r-w//r
        }
        public FileConfig(string fileType)
        {
            FileType = fileType;
            //FileType = "input-file";    read-write
            //FileType = "download-file"; read
            //r-w//r
        }
        public string FileType { get; set; }
        public List<FileVM> FileList { get; set; }
    }

    public class FileVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }

    public class ValidationVM
    {
        public String Name { get; set; }
        public String Validator { get; set; }
        public String Message { get; set; }
    }
    #endregion
}
