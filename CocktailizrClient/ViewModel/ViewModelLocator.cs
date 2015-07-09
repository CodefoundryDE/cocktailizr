/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:CocktailizrClient"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.ServiceModel.Description;
using System.ServiceModel.Security;
using System.Web.UI.WebControls;
using CocktailizrClient.CocktailServiceReference;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using CocktailizrClient.AdminServiceReference;
using CocktailizrClient.View;

namespace CocktailizrClient.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            Cleanup();

            // CocktailService - Client
            SimpleIoc.Default.Register(() =>
            {
                var client = new CocktailServiceClient();
                client.ClientCredentials.UserName.UserName = "ANONYMOUS";
                client.ClientCredentials.UserName.Password = "ANONYMOUS";
                return client;
            });

            // AdminService - Client
            SimpleIoc.Default.Register(() =>
            {
                var client = new AdminServiceClient();
                client.ClientCredentials.UserName.UserName = "ANONYMOUS";
                client.ClientCredentials.UserName.Password = "ANONYMOUS";
                return client;
            });

            SimpleIoc.Default.Register<ViewModelLocator>(() => this);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CocktailViewModel>();
            SimpleIoc.Default.Register<SearchViewModel>();
            SimpleIoc.Default.Register<ExtendedSearchViewModel>();
            SimpleIoc.Default.Register<AdminViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public CocktailViewModel Cocktail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CocktailViewModel>();
            }
        }

        public SearchViewModel Search
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SearchViewModel>();
            }
        }

        public ExtendedSearchViewModel ExtendedSearch
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ExtendedSearchViewModel>();
            }
        }

        public AdminViewModel Admin
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdminViewModel>();
            }
        }
        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public AdminServiceClient AdminServiceClient
        {
            get { return ServiceLocator.Current.GetInstance<AdminServiceClient>(); }
        }

        public CocktailServiceClient CocktailServiceClient
        {
            get { return ServiceLocator.Current.GetInstance<CocktailServiceClient>(); }
        }

        public void LoginServiceClients(ClientCredentials credentials)
        {
            SimpleIoc.Default.Unregister<AdminServiceClient>();
            // AdminService - Client
            SimpleIoc.Default.Register(() =>
            {
                var client = new AdminServiceClient();
                client.ClientCredentials.UserName.UserName = credentials.UserName.UserName;
                client.ClientCredentials.UserName.Password = credentials.UserName.Password;
                return client;
            });

            SimpleIoc.Default.Unregister<CocktailServiceClient>();
            // AdminService - Client
            SimpleIoc.Default.Register(() =>
            {
                var client = new CocktailServiceClient();
                client.ClientCredentials.UserName.UserName = credentials.UserName.UserName;
                client.ClientCredentials.UserName.Password = credentials.UserName.Password;
                return client;
            });
        }

        public static void Cleanup()
        {
            SimpleIoc.Default.Reset();
        }
    }
}