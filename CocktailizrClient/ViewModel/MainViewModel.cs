using System.Collections.Generic;
using System.Linq;
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
        public MainViewModel()
        {
            using (var client = new CocktailServiceClient())
            {
                client.ClientCredentials.UserName.UserName = "Admin";
                client.ClientCredentials.UserName.Password = "Cocktailizor";
                client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
                var zutaten = client.GetAllZutaten();

                var apfel = zutaten.Single(z => z.Name.ToLowerInvariant().Equals("äpfel"));
                var eisWürfel = zutaten.Single(z => z.Name.ToLowerInvariant().Equals("eiswürfel"));
                var limette = zutaten.Single(z => z.Name.ToLowerInvariant().Equals("limette"));
                var salz = zutaten.Single(z => z.Name.ToLowerInvariant().Equals("salz"));
                var pfeffer = zutaten.Single(z => z.Name.ToLowerInvariant().Equals("pfeffer"));
                var tomatens = zutaten.Single(z => z.Name.ToLowerInvariant().Equals("tomatensaft"));

                var cocktailsByIngredients = client.GetCocktailsByIndigrents(new[] { apfel, eisWürfel, limette, salz, pfeffer, tomatens });
            }
            using (var client = new AdminServiceClient())
            {

            }
        }
    }
}