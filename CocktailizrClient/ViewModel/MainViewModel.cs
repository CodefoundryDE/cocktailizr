using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Description;
using System.Windows.Media.Animation;
using GalaSoft.MvvmLight;
using System.Windows;
using System.ServiceModel.Security;
using CocktailizrClient.AdminServiceReference;
using CocktailizrClient.CocktailServiceReference;

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
        public MainViewModel()
        {
            using (var client = new CocktailServiceClient())
            {
                client.ClientCredentials.UserName.UserName = "tes1t";
                client.ClientCredentials.UserName.Password = "test123";
                client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
                MessageBox.Show(client.GetRandomCocktail().ToString());
            }

            using (var client = new AdminServiceClient())
            {

            }
        }
    }
}