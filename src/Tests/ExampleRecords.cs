namespace Tests
{
    using System.Collections.Generic;

    using DestinyLib.DataContract;

    public static class ExampleRecords
    {
        /// <summary>
        /// (https://data.destinysets.com/i/InventoryItem:821154603).
        /// </summary>
        public static WeaponDefinition GetGnawingHunger()
        {
            var gnawingHunger = new WeaponDefinition
            {
                MetaData = new WeaponDefinition.WeaponMetaData
                {
                    Id = 821154603,
                    Name = "Gnawing Hunger",
                    AmmoTypeId = "1",
                    TierTypeName = "Legendary",
                    DefaultDamageTypeId = "4",
                    DefaultDamageTypeHash = "3454344768",
                    FlavorText = "Don't let pride keep you from a good meal.",
                    ItemTypeId = "6",
                    CollectibleHash = 1683333367,
                    Icon = new System.Uri("https://bungie.net/common/destiny2_content/icons/48037e6416c3c9da07030a72931e0ca9.jpg"),
                },
                Stats = new List<WeaponDefinition.WeaponStat>(),
                PerkSets = new List<WeaponDefinition.PerkSet>(),
            };

            #region Stats
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Attack",
                Description = "Higher Attack allows your weapons to damage higher-level opponents.",
                StatHash = 1480404414,
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Power",
                Description = "Raising your Power increases the damage your abilities deal against higher-level enemies.",
                StatHash = 1935470627,
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "", // TODO: WHAT IS THIS?
                Description = "",
                StatHash = 1885944937,
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Zoom",
                Description = "How much the weapon's scope can zoom in on targets.",
                StatHash = 3555269338,
                Value = 16,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Rounds Per Minute",
                Description = "The number of shots per minute this weapon can fire.",
                StatHash = 4284893193,
                Value = 600,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Impact",
                Description = "Increases the damage inflicted by each round.",
                StatHash = 4043523819,
                Value = 21,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Range",
                Description = "Increases the effective range of this weapon.",
                StatHash = 1240592695,
                Value = 53,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Stability",
                Description = "How much or little recoil you will experience while firing the weapon.",
                StatHash = 155624089,
                Value = 49,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Magazine",
                Description = "The number of shots which can be fired before reloading.",
                StatHash = 3871231066,
                Value = 43,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Reload Speed",
                Description = "The time it takes to reload this weapon.",
                StatHash = 4188031367,
                Value = 61,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Inventory Size",
                Description = "How much ammo a player can hold in reserve.",
                StatHash = 1931675084,
                Value = 55,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Handling",
                Description = "The speed with which the weapon can be readied and aimed.",
                StatHash = 943549884,
                Value = 67,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Aim Assistance",
                Description = "The weapon's ability to augment your aim.",
                StatHash = 1345609583,
                Value = 65,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            gnawingHunger.Stats.Add(new WeaponDefinition.WeaponStat
            {
                Name = "Recoil Direction",
                Description = "The weapon's tendency to move while firing.",
                StatHash = 2715839340,
                Value = 54,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });

            #endregion

            #region Perks
            // Perk Sets are the unique collections of Perks that belongs to an individual weapon.

            gnawingHunger.PerkSets.Add(new WeaponDefinition.PerkSet
            {
                SocketIndex = 1,
                SocketTypeHash = 3362409147,
                PlugSetHash = 295878355,
                Perks = new List<WeaponDefinition.Perk>
                {
                    new WeaponDefinition.Perk
                    {
                        Id = 839105230u,
                        Name = "Arrowhead Brake",
                        Description = "Lightly vented barrel.\n  •  Greatly controls recoil\n  •  Increases handling speed",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
                            {
                                StatHash = 2715839340,
                                Value = 30,
                            },
                            new WeaponDefinition.PerkValue
                            {
                                StatHash = 943549884,
                                Value = 10,
                            }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 3661387068u,
                        Name = "Chambered Compensator",
                        Description = "Stable barrel attachment.\n  •  Increases stability\n  •  Moderately controls recoil\n  •  Slightly decreases handling speed",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
                            {
                                StatHash = 155624089,
                                Value = 10,
                            },
                            new WeaponDefinition.PerkValue
                            {
                                StatHash = 2715839340,
                                Value = 10,
                            },
                            new WeaponDefinition.PerkValue
                            {
                                StatHash = 943549884,
                                Value = -5,
                            }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 4090651448u,
                        Name = "Corkscrew Rifling",
                        Description = "Balanced barrel. \n  •  Slightly increases range and stability\n  •  Slightly increases handling speed",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 155624089u,
                Value = 5,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 1240592695u,
                Value = 5,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 943549884u,
                Value = 5,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 1467527085u,
                        Name = "Extended Barrel",
                        Description = "Weighty barrel extension.\n  •  Increases range\n  •  Decreases handling speed\n  •  Moderately controls recoil",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 943549884u,
                Value = -10,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 1240592695u,
                Value = 10,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 2715839340u,
                Value = 10,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 1840239774u,
                        Name = "Fluted Barrel",
                        Description = "Ultra-light barrel. \n  •  Greatly increases handling speed\n  •  Slightly increases stability",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 155624089u,
                Value = 5,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 943549884u,
                Value = 15,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 202670084u,
                        Name = "Full Bore",
                        Description = "Barrel optimized for distance.\n  •  Greatly increases range\n  •  Decreases stability\n  •  Slightly decreases handling speed",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 155624089u,
                Value = -10,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 1240592695u,
                Value = 15,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 943549884u,
                Value = -5,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 3250034553u,
                        Name = "Hammer-Forged Rifling",
                        Description = "Durable ranged barrel.\n  •  Increases range",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 1240592695u,
                Value = 10,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 1392496348u,
                        Name = "Polygonal Rifling",
                        Description = "Barrel optimized for recoil reduction.\n  •  Increases stability",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 155624089u,
                Value = 10,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 1482024992u,
                        Name = "Smallbore",
                        Description = "Dual strength barrel.\n  •  Increases range\n  •  Increases stability",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 155624089u,
                Value = 7,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 1240592695u,
                Value = 7,
             }
                        }
                    },
                },
            });

            gnawingHunger.PerkSets.Add(new WeaponDefinition.PerkSet
            {
                SocketIndex = 2,
                SocketTypeHash = 3815406785,
                PlugSetHash = 3964805173,
                Perks = new List<WeaponDefinition.Perk>
                {
                    new WeaponDefinition.Perk
                    {
                        Id = 3142289711u,
                        Name = "Accurized Rounds",
                        Description = "This weapon can fire longer distances.\n  •  Increases range",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
                            {
                                StatHash = 1240592695u,
                                Value = 10,
                            }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 4134353779u,
                        Name = "Drop Mag",
                        Description = "Magazine drops on reload, wasting ammunition but greatly increasing reload speed.",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 4188031367u,
                Value = 30,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 1087426260u,
                        Name = "Appended Mag",
                        Description = "This weapon's magazine is built for higher capacity.\n  •  Increases magazine size",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 3871231066u,
                Value = 20,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 106909392u,
                        Name = "Tactical Mag",
                        Description = "This weapon has multiple tactical improvements.\n  •  Slightly increases stability\n  •  Increases reload speed\n  •  Slightly increases magazine size",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 3871231066u,
                Value = 10,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 4188031367u,
                Value = 10,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 155624089u,
                Value = 5,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 2420895100u,
                        Name = "Extended Mag",
                        Description = "This weapon has a greatly increased magazine size, but reloads much slower.\n  •  Greatly increases magazine size\n  •  Greatly decreases reload speed",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 3871231066u,
                Value = 30,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 4188031367u,
                Value = -20,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 3177308360u,
                        Name = "Steady Rounds",
                        Description = "This magazine is optimized for recoil control.\n  •  Greatly increases stability \n  •  Slightly decreases range",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 155624089u,
                Value = 15,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 1240592695u,
                Value = -5,
             }
                        }
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 1431678320u,
                        Name = "Alloy Magazine",
                        Description = "Faster reloads when the magazine is empty.",
                        PerkValues = null,
                    },
                    new WeaponDefinition.Perk
                    {
                        Id = 3230963543u,
                        Name = "Flared Magwell",
                        Description = "Optimized for fast reloading.\n  •  Slightly increases stability\n  •  Greatly increases reload speed",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
             {
                StatHash = 4188031367u,
                Value = 15,
             }, new WeaponDefinition.PerkValue
             {
                StatHash = 155624089u,
                Value = 5,
             }
                        }
                    }
                }
            });

            gnawingHunger.PerkSets.Add(new WeaponDefinition.PerkSet
            {
                SocketIndex = 3,
                SocketTypeHash = 2614797986,
                PlugSetHash = 2297212861,
                Perks = new List<WeaponDefinition.Perk>
                {
                    new WeaponDefinition.Perk
                    {
                        Id = 1890422124u,
                        Name = "Tap the Trigger",
                        Description = "Grants a short period of increased stability and accuracy on initial trigger pull.",
                        PerkValues = null
                    }, new WeaponDefinition.Perk
                    {
                        Id = 1820235745u,
                        Name = "Subsistence",
                        Description = "Defeating targets partially reloads the magazine from reserves.",
                        PerkValues = null
                    }, new WeaponDefinition.Perk
                    {
                        Id = 2387244414u,
                        Name = "Zen Moment",
                        Description = "Causing damage with this weapon increases its stability.",
                        PerkValues = null
                    }, new WeaponDefinition.Perk
                    {
                        Id = 2869569095u,
                        Name = "Field Prep",
                        Description = "Increased ammo reserves. Faster reload, stow, and ready when you're crouching.",
                        PerkValues = new List<WeaponDefinition.PerkValue>
                        {
                            new WeaponDefinition.PerkValue
                            {
                                StatHash = 1931675084u,
                                Value = 30,
                            }
                        }
                    }, new WeaponDefinition.Perk
                    {
                        Id = 3300816228u,
                        Name = "Auto-Loading Holster",
                        Description = "The holstered weapon is automatically reloaded after a short period of time.",
                        PerkValues = null
                    }
                }
            });

            gnawingHunger.PerkSets.Add(new WeaponDefinition.PerkSet
            {
                SocketIndex = 4,
                SocketTypeHash = 2614797986,
                PlugSetHash = 1853656119,
                Perks = new List<WeaponDefinition.Perk>
                {
                    new WeaponDefinition.Perk
                   {
                      Id = 3425386926u,
                      Name = "Rampage",
                      Description = "Kills with this weapon temporarily grant increased damage. Stacks 3x.",
                        PerkValues = null
                   }, new WeaponDefinition.Perk
                   {
                      Id = 1015611457u,
                      Name = "Kill Clip",
                      Description = "Reloading after a kill grants increased damage.",
                        PerkValues = null
                   }, new WeaponDefinition.Perk
                   {
                      Id = 2458213969u,
                      Name = "Multikill Clip",
                      Description = "Reloading grants increased damage based on the number of rapid kills made beforehand.",
                        PerkValues = null
                   }, new WeaponDefinition.Perk
                   {
                      Id = 3523296417u,
                      Name = "Demolitionist",
                      Description = "Kills with this weapon generate grenade energy. Activating your grenade ability reloads this weapon from reserves.",
                        PerkValues = null
                   }, new WeaponDefinition.Perk
                    {
                        Id = 4082225868u,
                        Name = "Swashbuckler",
                        Description = "This weapon gains increased damage from melee kills and kills with this weapon.",
                        PerkValues = null
                    },
                }
            });

            #endregion

            return gnawingHunger;
        }
    }
}
