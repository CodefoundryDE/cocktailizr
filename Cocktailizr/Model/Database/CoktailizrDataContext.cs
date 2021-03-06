﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Cocktailizr.Security;
using CocktailizrTypes.Model.Entities;
using MongoDB.Bson;
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
                var cocktail = new Cocktail()
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
                                Skala = EZutatenSkala.Stueck
                            }, 30
                        },
                        {new Zutat()
                            {
                                Name = "Eiswürfel",
                                IsOptional = true,
                                Skala = EZutatenSkala.Stueck
                            }, 3
                        },
                    },
                };
                try
                {
                    string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    string imgPath = path.Substring(6) + @"\Assets\apfelsaft.jpg";
                    cocktail.Image = Image.FromFile(imgPath);

                    Cocktails.InsertOneAsync(cocktail);
                }
                catch (Exception e)
                {

                }


                #endregion
            }

            if (Cocktails.Find(c => c.Id.Equals(new Guid("6bd35665-0109-4e94-806c-baa9cf58984a"))).CountAsync().Result == 0)
            {
                #region BloodyMary-DemoCocktail

                var cocktail = new Cocktail()
                {
                    Id = new Guid("6bd35665-0109-4e94-806c-baa9cf58984a"),
                    Alcoholic = true,
                    DrinkColor = Color.Red,
                    Name = "Bloody Mary",
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
                            Beschreibung = "Salz und Pfeffer zugeben, Tabasco drauf und Worcestershire-Sauce,"
                        },                      
                        new Step()
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
                                Skala = EZutatenSkala.Stueck
                            }, 0.5m
                        },
                        {new Zutat()
                            {
                                Name = "Eiswürfel",
                                IsOptional = true,
                                Skala = EZutatenSkala.Stueck
                            }, 3
                        },
                        {new Zutat()
                            {
                                Name = "Salz",
                                IsOptional = false,
                                Skala = EZutatenSkala.Priese
                            }, 1m
                        },
                        {new Zutat()
                            {
                                Name = "Pfeffer",
                                IsOptional = false,
                                Skala = EZutatenSkala.Priese
                            }, 1m
                        },
                        {new Zutat()
                            {
                                Name = "Tomatensaft",
                                IsOptional = false,
                                Skala = EZutatenSkala.Cl
                            }, 20m
                        },
                        {new Zutat()
                            {
                                Name = "Worcestershire-Sauce",
                                IsOptional = false,
                                Skala = EZutatenSkala.Cl
                            }, 2m
                        },
                        {new Zutat()
                            {
                                Name = "Wodka",
                                IsOptional = false,
                                Skala = EZutatenSkala.Cl
                            }, 5m
                        },
                    },
                };

                try
                {
                    string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    string imgPath = path.Substring(6) + @"\Assets\bloodymary.jpg";
                    cocktail.Image = Image.FromFile(imgPath);

                    Cocktails.InsertOneAsync(cocktail);
                }
                catch (Exception e)
                {

                }
                #endregion
            }

            if (Cocktails.Find(c => c.Id.Equals(new Guid("4C65CAA9-CD36-44BC-A1DF-8155D75464C9"))).CountAsync().Result == 0)
            {
                #region Caipirinha-DemoCocktail
                var cocktail = new Cocktail()
                {
                    Id = new Guid("4C65CAA9-CD36-44BC-A1DF-8155D75464C9"),
                    Alcoholic = true,
                    DrinkColor = Color.Green,
                    Name = "Caipirinha",
                    Tags = new List<string>() { "Sweet", "Cool" },
                    Rezept = new Rezept()
                    {
                        Zubereitungszeit = TimeSpan.FromMinutes(15),
                        ZubereitungsSchritte = new List<Step>()
                    {
                        new Step()
                        {
                            Headline = "Vorbereitung",
                           Beschreibung = "Die unbehandelten Limetten vierteln."
                        },
                        new Step()
                        {
                            Headline = "Zubereitung /1",
                            Beschreibung = "Im Glas die Limettenviertel mit dem Zucker zusammen zerdrücken."
                        },                      
                        new Step()
                        {
                            Headline = "Zubereitung /2",
                            Beschreibung = "Cachaca zugeben und mit gestoßenem Eis auffüllen und umrühren."
                        },
                        new Step()
                        {
                            Headline = "Zubereitung /3",
                            Beschreibung = "Evtl. Ginger Ale zugeben."
                        }
                    }
                    },
                    Zutaten = new Dictionary<Zutat, decimal>()
                    {
                        {new Zutat()
                            {
                                Name = "Limette",
                                IsOptional = false,
                                Skala = EZutatenSkala.Stueck
                            }, 1m
                        },
                        {new Zutat()
                            {
                                Name = "Crushed Ice",
                                IsOptional = false,
                                Skala = EZutatenSkala.Cl
                            }, 40m
                        },
                        {new Zutat()
                            {
                                Name = "Brauner Zucker",
                                IsOptional = false,
                                Skala = EZutatenSkala.Priese
                            }, 1m
                        },
                        {new Zutat()
                            {
                                Name = "Ginger Ale",
                                IsOptional = false,
                                Skala = EZutatenSkala.Cl
                            }, 10m
                        },
                        {new Zutat()
                            {
                                Name = "Cachaca",
                                IsOptional = false,
                                Skala = EZutatenSkala.Cl
                            }, 10m
                        },
                    },
                };

                try
                {
                    string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    string imgPath = path.Substring(6) + @"\Assets\caipirinha.jpg";
                    cocktail.Image = Image.FromFile(imgPath);

                    Cocktails.InsertOneAsync(cocktail);
                }
                catch (Exception e)
                {

                }
                #endregion
            }

            if (Cocktails.Find(c => c.Id.Equals(new Guid("EB577661-B70D-48A8-B5EB-C7A9CB49A2B6"))).CountAsync().Result == 0)
            {
                #region Piña Colada-DemoCocktail
                var cocktail = new Cocktail()
                {
                    Id = new Guid("EB577661-B70D-48A8-B5EB-C7A9CB49A2B6"),
                    Alcoholic = true,
                    DrinkColor = Color.Green,
                    Name = "Pina Colada",
                    Tags = new List<string>() { "Sweet", "Cool", "Beach" },
                    Rezept = new Rezept()
                    {
                        Zubereitungszeit = TimeSpan.FromMinutes(15),
                        ZubereitungsSchritte = new List<Step>()
                    {
                        new Step()
                        {
                            Headline = "Zubereitung /1",
                            Beschreibung = "Die Zutaten inklusive Eis im Shaker schütteln und in ein mit drei großen Eiswürfeln gefülltes Longdrinkglas abseihen."
                        },                      
                        new Step()
                        {
                            Headline = "Zubereitung /2",
                            Beschreibung = "Zwei Ananasstücke werden zur Dekoration auf den Glasrand gesteckt (evtl. mit Zuckerrand)."
                        },
                    }
                    },
                    Zutaten = new Dictionary<Zutat, decimal>()
                    {
                        {new Zutat()
                            {
                                Name = "Weißer Rum",
                                IsOptional = false,
                                Skala = EZutatenSkala.Cl
                            }, 3m
                        },
                        {new Zutat()
                            {
                                Name = "Kokosmilch",
                                IsOptional = false,
                                Skala = EZutatenSkala.Cl
                            }, 3m
                        },
                        {new Zutat()
                            {
                                Name = "Ananassaft",
                                IsOptional = false,
                                Skala = EZutatenSkala.Cl
                            }, 9m
                        },
                        {new Zutat()
                            {
                                Name = "Eiswürfel",
                                IsOptional = false,
                                Skala = EZutatenSkala.Stueck
                            }, 3 
                        },
                        {new Zutat()
                            {
                                Name = "Ananasstücke",
                                IsOptional = true,
                                Skala = EZutatenSkala.Stueck
                            }, 2 
                        },
                    },
                };

                try
                {
                    string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
                    string imgPath = path.Substring(6) + @"\Assets\pinacolada.jpg";
                    cocktail.Image = Image.FromFile(imgPath);

                    Cocktails.InsertOneAsync(cocktail);
                }
                catch (Exception e)
                {

                }
                #endregion
            }
        }
    }
}