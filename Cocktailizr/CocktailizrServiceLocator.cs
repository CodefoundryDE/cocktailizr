﻿using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cocktailizr.Auth;
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
            SimpleIoc.Default.Register<BenutzerService>();
            SimpleIoc.Default.Register<CocktailDbService>();
        }

        public static BenutzerService BenutzerService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BenutzerService>();
            }
        }

        public static CocktailDbService CocktailDbService
        {
            get { return ServiceLocator.Current.GetInstance<CocktailDbService>(); }
        }

    }
}