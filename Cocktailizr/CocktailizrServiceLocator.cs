using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cocktailizr.Security;
using Cocktailizr.Model.Service;
using GalaSoft.MvvmLight.Ioc;

namespace Cocktailizr
{
    public class CocktailizrServiceLocator
    {

        static CocktailizrServiceLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Services
            SimpleIoc.Default.Register<BenutzerDbService>();
            SimpleIoc.Default.Register<CocktailDbService>();
        }

        public static BenutzerDbService BenutzerDbService
        {
            get { return ServiceLocator.Current.GetInstance<BenutzerDbService>(); }
        }

        public static CocktailDbService CocktailDbService
        {
            get { return ServiceLocator.Current.GetInstance<CocktailDbService>(); }
        }

    }
}