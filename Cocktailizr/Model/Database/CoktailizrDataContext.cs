using System;
using System.Collections.Generic;
using System.Drawing;
using Cocktailizr.Security;
using CocktailizrTypes.Model.Entities;
using MongoDB.Driver;

namespace Cocktailizr.Model.Database
{
    public class CocktailizrDataContext
    {
        private readonly IMongoDatabase _mongoDb;

        public IMongoCollection<Cocktail> Cocktails { get { return _mongoDb.GetCollection<Cocktail>(Properties.Resources.CocktailsCollectionName); } }

        public IMongoCollection<Benutzer> Benutzer { get { return _mongoDb.GetCollection<Benutzer>(Properties.Resources.BenutzerCollectionName); } }

        public CocktailizrDataContext()
        {
            _mongoDb = MongoClientFactory.DatabaseConnection;

            Seed();
        }

        private void Seed()
        {
            if ((Benutzer.Find(b => b.Id.Equals(new Guid("fc748c3e-72ce-4d23-be9d-4af064f60a66"))).CountAsync().Result == 0))
            {
                #region Admin
                Benutzer.InsertOneAsync(new Benutzer()
                {
                    Id = new Guid("fc748c3e-72ce-4d23-be9d-4af064f60a66"),
                    Name = "Admin",
                    Role = "ADMIN",
                    HashedPassword = PasswordHashHelper.HashPassword("Cocktailizor", PasswordHashHelper.GenerateSaltValue()),
                }).Wait();

                #endregion
            }
            if (Cocktails.Find(c => c.Id.Equals(new Guid("6bd35665-0109-4e94-806c-baa9cf58584a"))).CountAsync().Result == 0)
            {
                #region Apfelsaft-DemoCocktail
                Cocktails.InsertOneAsync(new Cocktail()
                {
                    Id = new Guid("6bd35665-0109-4e94-806c-baa9cf58584a"),
                    Alcoholic = false,
                    DrinkColor = Color.Yellow,
                    Name = "Apfelsaft",
                    Tags = new List<string>() { "Sweet", "Summer", "Germany" },
                    Rezept = new Rezept()
                    {
                        Zubereitungszeit = TimeSpan.FromHours(1),
                        ZubereitungsSchritte = new List<Step>()
                    {
                        new Step()
                        {
                            Headline = "Vorbereitung",
                           Beschreibung = "Äpfel pflücken oder kaufen und schälen"
                        },
                        new Step()
                        {
                            Headline = "Zubereitung",
                            Beschreibung = "Äpfel pressen und den Saft auffangen"
                        }
                    }
                    },
                    Zutaten = new Dictionary<Zutat, decimal>()
                    {
                        {new Zutat()
                            {
                                Name = "Äpfel",
                                IsOptional = false,
                                Skala = ZutatenSkala.Stueck
                            }, 30
                        },
                        {new Zutat()
                            {
                                Name = "Eiswürfel",
                                IsOptional = true,
                                Skala = ZutatenSkala.Stueck
                            }, 3
                        },
                    },
                });
                #endregion
            }

            if (Cocktails.Find(c => c.Id.Equals(new Guid("6bd35665-0109-4e94-806c-baa9cf58984a"))).CountAsync().Result == 0)
            {
                #region BloodyMarry-DemoCocktail
                Cocktails.InsertOneAsync(new Cocktail()
                {
                    Id = new Guid("6bd35665-0109-4e94-806c-baa9cf58984a"),
                    Alcoholic = true,
                    DrinkColor = Color.Red,
                    Name = "Bloody Marry",
                    Tags = new List<string>() { "Spicy", "Salty", "Tomato" },
                    Rezept = new Rezept()
                    {
                        Zubereitungszeit = TimeSpan.FromMinutes(10),
                        ZubereitungsSchritte = new List<Step>()
                    {
                        new Step()
                        {
                            Headline = "Vorbereitung",
                           Beschreibung = "Limette auspressen und den Saft in einem Longdrink-Glas mit Eiswüfeln sammeln,"
                        },
                        new Step()
                        {
                            Headline = "Zubereitung /1",
                            Beschreibung = "Salz und Pfeffer zugeben, Tabasqo drauf und WertschisterSchaier-Soße,"
                        },                      new Step()
                        {
                            Headline = "Zubereitung /2",
                            Beschreibung = "Mit Tomatensaft und Wodka nach Maß auffüllen."
                        }
                    }
                    },
                    Zutaten = new Dictionary<Zutat, decimal>()
                    {
                        {new Zutat()
                            {
                                Name = "Limette",
                                IsOptional = false,
                                Skala = ZutatenSkala.Stueck
                            }, 0.5m
                        },
                        {new Zutat()
                            {
                                Name = "Eiswürfel",
                                IsOptional = true,
                                Skala = ZutatenSkala.Stueck
                            }, 3
                        },
                        {new Zutat()
                            {
                                Name = "Salz",
                                IsOptional = false,
                                Skala = ZutatenSkala.Priese
                            }, 1m
                        },
                        {new Zutat()
                            {
                                Name = "Pfeffer",
                                IsOptional = false,
                                Skala = ZutatenSkala.Priese
                            }, 1m
                        },
                        {new Zutat()
                            {
                                Name = "Tomatensaft",
                                IsOptional = false,
                                Skala = ZutatenSkala.Cl
                            }, 20m
                        },
                        {new Zutat()
                            {
                                Name = "WertschisterSchaier-Soße",
                                IsOptional = false,
                                Skala = ZutatenSkala.Cl
                            }, 2m
                        },
                        {new Zutat()
                            {
                                Name = "Wodka",
                                IsOptional = false,
                                Skala = ZutatenSkala.Cl
                            }, 5m
                        },
                    },
                });
                #endregion
            }
        }
    }
}