using System;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.Windows.Media.Animation;
using GalaSoft.MvvmLight;
using System.Windows;
using System.ServiceModel.Security;
using CocktailizrClient.AdminServiceReference;
using CocktailizrClient.CocktailServiceReference;
using CocktailizrTypes.Model.Entities;

namespace CocktailizrClient.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>



        
        private Cocktail _cocktail;
        public Cocktail Cocktail
        {
            get { return _cocktail; }
            set
            {
                _cocktail = value;
                RaisePropertyChanged(() => Cocktail);
            }
        }


        public MainViewModel()
        {
            using (var client = new CocktailServiceClient())
            {
                client.ClientCredentials.UserName.UserName = "Admin";
                client.ClientCredentials.UserName.Password = "Cocktailizor";
                client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
                Cocktail = client.GetRandomCocktail();

            }

            using (var client = new AdminServiceClient())
            {

            }
        }
    }
}