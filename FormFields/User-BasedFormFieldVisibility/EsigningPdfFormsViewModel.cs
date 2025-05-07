using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Resources;

namespace User_BasedFormFieldVisibility
{
    public class EsigningPdfFormsViewModel
    {
        private Stream m_documentStream;
        public EsigningPdfFormsViewModel()
        {
            this.Employees = new ObservableCollection<Employee>();
            string andrewFilePath = "../../Data/profile1.png";
            string anneFilePath = "../../Data/profile2.png";

            Employees.Add(new Employee
            {
                Name = "Andrew Fuller",
                ProfilePicture = GetFileStream("profile1.png"),
                Mail = "andrew@mycompany.com",
                BorderColor = true,

            });
            Employees.Add(new Employee
            {
                Name = "Anne Dodsworth",
                ProfilePicture = GetFileStream("profile2.png"),
                Mail = "anne@mycompany.com",
                BorderColor = false,

            });
        }

        /// <summary>
        /// Gets or sets the document path.
        /// </summary>
        public Stream DocumentStream
        {
            get
            {
                return m_documentStream;
            }
            set
            {
                m_documentStream = value;
            }
        }
        /// <summary>
        /// Gets or sets the collection of Employees.
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }
        private Stream GetFileStream(string filePath)
        {
            Uri uriResource = new Uri("/User-BasedFormFieldVisibility;component/Data/" + filePath, UriKind.Relative);
            StreamResourceInfo streamResourceInfo = Application.GetResourceStream(uriResource);
            return streamResourceInfo.Stream;
        }
    }
    /// <summary>
    /// Class to represent the details of the Employees
    /// </summary>
    public class Employee
    {
        public string Name { get; set; }
        public Stream ProfilePicture { get; set; }
        public string Mail { get; set; }
        public bool BorderColor { get; set; }

    }
}
