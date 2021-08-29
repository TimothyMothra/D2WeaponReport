namespace Tests
{
    using System.Collections.Generic;

    using DestinyLib.DataContract.Definitions;

    public static class ExampleRecords
    {
        /// <summary>
        /// (https://data.destinysets.com/i/InventoryItem:821154603).
        /// </summary>
        public static WeaponDefinition GetGnawingHunger()
        {
            var weaponMetaData = new WeaponMetaData
            {
                HashId = 821154603,
                Name = "Gnawing Hunger",
                AmmoTypeId = "1",
                TierTypeName = "Legendary",
                DefaultDamageTypeId = "4",
                DefaultDamageTypeHash = "3454344768",
                FlavorText = "Don't let pride keep you from a good meal.",
                ItemTypeId = "6",
                CollectibleHash = 1683333367,
                CollectionDefintitionIconPath = "/common/destiny2_content/icons/48037e6416c3c9da07030a72931e0ca9.jpg",
                ItemDefinitionIconPath = "/common/destiny2_content/icons/c4509acb76551495deac51bb29b61248.jpg",
                ScreenshotPath = "/common/destiny2_content/screenshots/821154603.jpg",
            };

            #region Stats
            var weaponStatsCollection = new WeaponStatsCollection();

            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 1480404414,
                    Name = "Attack",
                    Description = "Higher Attack allows your weapons to damage higher-level opponents.",
                },
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 1935470627,
                    Name = "Power",
                    Description = "Raising your Power increases the damage your abilities deal against higher-level combatants.",
                },
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 1885944937,
                    Name = string.Empty, // TODO: WHAT IS THIS?
                    Description = string.Empty,
                },
                Value = 0,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 3555269338,
                    Name = "Zoom",
                    Description = "How much the weapon's scope can zoom in on targets.",
                },
                Value = 16,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 4284893193,
                    Name = "Rounds Per Minute",
                    Description = "The number of shots per minute this weapon can fire.",
                },
                Value = 600,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 4043523819,
                    Name = "Impact",
                    Description = "Increases the damage inflicted by each round.",
                },
                Value = 21,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 1240592695,
                    Name = "Range",
                    Description = "Increases the effective range of this weapon.",
                },
                Value = 53,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 155624089,
                    Name = "Stability",
                    Description = "How much or little recoil you will experience while firing the weapon.",
                },
                Value = 49,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 3871231066,
                    Name = "Magazine",
                    Description = "The number of shots which can be fired before reloading.",
                },
                Value = 43,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 4188031367,
                    Name = "Reload Speed",
                    Description = "The time it takes to reload this weapon.",
                },
                Value = 61,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 1931675084,
                    Name = "Inventory Size",
                    Description = "How much ammo a player can hold in reserve.",
                },
                Value = 55,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 943549884,
                    Name = "Handling",
                    Description = "The speed with which the weapon can be readied and aimed.",
                },
                Value = 67,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 1345609583,
                    Name = "Aim Assistance",
                    Description = "The weapon's ability to augment your aim.",
                },
                Value = 65,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });
            weaponStatsCollection.Values.Add(new WeaponStatDefinition
            {
                MetaData = new WeaponStatMetaData
                {
                    HashId = 2715839340,
                    Name = "Recoil Direction",
                    Description = "The weapon's tendency to move while firing.",
                },
                Value = 54,
                MinValue = 0,
                MaxValue = 0,
                DisplayMaximum = 100,
            });

            #endregion

            #region Perks
            // Perk Sets are the unique collections of Perks that belongs to an individual weapon.

            var weaponPerksCollection = new WeaponPerkSetCollection();

            weaponPerksCollection.Values.Add(new WeaponPerkSetDefinition
            {
                SocketIndex = 1,
                SocketTypeHash = 3362409147,
                PlugSetHash = 295878355,
                Values = new List<WeaponPerkDefinition>
                {
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 839105230u,
                            Name = "Arrowhead Brake",
                            Description = "Lightly vented barrel.\n  •  Greatly controls recoil\n  •  Increases handling speed",
                            IconPath = "/common/destiny2_content/icons/7a0a23f9622636cea92387d50d368333.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 2715839340,
                                Value = 30,
                            },
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 943549884,
                                Value = 10,
                            },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 3661387068u,
                            Name = "Chambered Compensator",
                            Description = "Stable barrel attachment.\n  •  Increases stability\n  •  Moderately controls recoil\n  •  Slightly decreases handling speed",
                            IconPath = "/common/destiny2_content/icons/376aa9bd8c392567f501012fc3b3d4d0.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 155624089,
                                Value = 10,
                            },
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 2715839340,
                                Value = 10,
                            },
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 943549884,
                                Value = -5,
                            },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 4090651448u,
                            Name = "Corkscrew Rifling",
                            Description = "Balanced barrel. \n  •  Slightly increases range and stability\n  •  Slightly increases handling speed",
                            IconPath = "/common/destiny2_content/icons/18cc75bf9a17bb80b5109f1b8909521f.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                             {
                                StatHash = 155624089u,
                                Value = 5,
                             }, new WeaponPerkValueDefinition
                             {
                                StatHash = 1240592695u,
                                Value = 5,
                             }, new WeaponPerkValueDefinition
                             {
                                StatHash = 943549884u,
                                Value = 5,
                             },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 1467527085u,
                            Name = "Extended Barrel",
                            Description = "Weighty barrel extension.\n  •  Increases range\n  •  Decreases handling speed\n  •  Moderately controls recoil",
                            IconPath = "/common/destiny2_content/icons/7c81469db03f7111f8d248b54c83d7cf.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                             {
                                StatHash = 943549884u,
                                Value = -10,
                             }, new WeaponPerkValueDefinition
                             {
                                StatHash = 1240592695u,
                                Value = 10,
                             }, new WeaponPerkValueDefinition
                             {
                                StatHash = 2715839340u,
                                Value = 10,
                             },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 1840239774u,
                            Name = "Fluted Barrel",
                            Description = "Ultra-light barrel. \n  •  Greatly increases handling speed\n  •  Slightly increases stability",
                            IconPath = "/common/destiny2_content/icons/c2a4355c8db7eac12d6dec52d09a20fe.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
             {
                StatHash = 155624089u,
                Value = 5,
             }, new WeaponPerkValueDefinition
             {
                StatHash = 943549884u,
                Value = 15,
             },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 202670084u,
                            Name = "Full Bore",
                            Description = "Barrel optimized for distance.\n  •  Greatly increases range\n  •  Decreases stability\n  •  Slightly decreases handling speed",
                            IconPath = "/common/destiny2_content/icons/9307c5604f995d90ad5ed65281b64772.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
             {
                StatHash = 155624089u,
                Value = -10,
             }, new WeaponPerkValueDefinition
             {
                StatHash = 1240592695u,
                Value = 15,
             }, new WeaponPerkValueDefinition
             {
                StatHash = 943549884u,
                Value = -5,
             },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 3250034553u,
                            Name = "Hammer-Forged Rifling",
                            Description = "Durable ranged barrel.\n  •  Increases range",
                            IconPath = "/common/destiny2_content/icons/9acb073d4e85160f66f0d617d210cf61.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
             {
                StatHash = 1240592695u,
                Value = 10,
             },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 1392496348u,
                            Name = "Polygonal Rifling",
                            Description = "Barrel optimized for recoil reduction.\n  •  Increases stability",
                            IconPath = "/common/destiny2_content/icons/7cadd68b66ffd3bbedc09aa9c7ba6e03.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
             {
                StatHash = 155624089u,
                Value = 10,
             },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 1482024992u,
                            Name = "Smallbore",
                            Description = "Dual strength barrel.\n  •  Increases range\n  •  Increases stability",
                            IconPath = "/common/destiny2_content/icons/bc3d5d36c6b4627bdb8d37cd52711cf4.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
             {
                StatHash = 155624089u,
                Value = 7,
             }, new WeaponPerkValueDefinition
             {
                StatHash = 1240592695u,
                Value = 7,
             },
                        },
                    },
                },
            });

            weaponPerksCollection.Values.Add(new WeaponPerkSetDefinition
            {
                SocketIndex = 2,
                SocketTypeHash = 3815406785,
                PlugSetHash = 3964805173,
                Values = new List<WeaponPerkDefinition>
                {
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 3142289711u,
                            Name = "Accurized Rounds",
                            Description = "This weapon can fire longer distances.\n  •  Increases range",
                            IconPath = "/common/destiny2_content/icons/05e357cd152eb0f665ee986aaa3edc56.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 1240592695u,
                                Value = 10,
                            },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 4134353779u,
                            Name = "Drop Mag",
                            Description = "Increases reload speed but reduces magazine size.",
                            IconPath = "/common/destiny2_content/icons/d316be9e70844427e69034b0f06bec75.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                               StatHash = 4188031367u,
                               Value = 30,
                            },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 1087426260u,
                            Name = "Appended Mag",
                            Description = "This weapon's magazine is built for higher capacity.\n  •  Increases magazine size",
                            IconPath = "/common/destiny2_content/icons/9d8a7be4c2fef471a6fde38d02ab8dae.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                            StatHash = 3871231066u,
                            Value = 20,
                            },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 106909392u,
                            Name = "Tactical Mag",
                            Description = "This weapon has multiple tactical improvements.\n  •  Slightly increases stability\n  •  Increases reload speed\n  •  Slightly increases magazine size",
                            IconPath = "/common/destiny2_content/icons/9711e6d3de7a9cf2f844752992390a63.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 3871231066u,
                                Value = 10,
                            }, new WeaponPerkValueDefinition
                            {
                                StatHash = 4188031367u,
                                Value = 10,
                            }, new WeaponPerkValueDefinition
                            {
                                StatHash = 155624089u,
                                Value = 5,
                            },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 2420895100u,
                            Name = "Extended Mag",
                            Description = "This weapon has a greatly increased magazine size, but reloads much slower.\n  •  Greatly increases magazine size\n  •  Greatly decreases reload speed",
                            IconPath = "/common/destiny2_content/icons/4cd73ae5b1bc1ae0afc7adb34c2f2df6.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 3871231066u,
                                Value = 30,
                            }, new WeaponPerkValueDefinition
                            {
                                StatHash = 4188031367u,
                                Value = -20,
                            },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 3177308360u,
                            Name = "Steady Rounds",
                            Description = "This magazine is optimized for recoil control.\n  •  Greatly increases stability \n  •  Slightly decreases range",
                            IconPath = "/common/destiny2_content/icons/adaf4315dab749519c41a233e9954598.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 155624089u,
                                Value = 15,
                            }, new WeaponPerkValueDefinition
                            {
                                StatHash = 1240592695u,
                                Value = -5,
                            },
                        },
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 1431678320u,
                            Name = "Alloy Magazine",
                            Description = "Faster reloads when the magazine is empty.",
                            IconPath = "/common/destiny2_content/icons/932e7b84c233c6cc49a58058f8c5144d.png",
                        },
                        WeaponPerkList = null,
                    },
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 3230963543u,
                            Name = "Flared Magwell",
                            Description = "Optimized for fast reloading.\n  •  Slightly increases stability\n  •  Greatly increases reload speed",
                            IconPath = "/common/destiny2_content/icons/ec1bca64fc9709678ea5c297c0e64c19.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 4188031367u,
                                Value = 15,
                            }, new WeaponPerkValueDefinition
                            {
                                StatHash = 155624089u,
                                Value = 5,
                            },
                        },
                    },
                },
            });

            weaponPerksCollection.Values.Add(new WeaponPerkSetDefinition
            {
                SocketIndex = 3,
                SocketTypeHash = 2614797986,
                PlugSetHash = 2297212861,
                Values = new List<WeaponPerkDefinition>
                {
                    new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 1890422124u,
                            Name = "Tap the Trigger",
                            Description = "Grants a short period of increased stability and accuracy on initial trigger pull.",
                            IconPath = "/common/destiny2_content/icons/8e910e56aa6e36b2c406804e1d78d04f.png",
                        },
                        WeaponPerkList = null,
                    }, new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 1820235745u,
                            Name = "Subsistence",
                            Description = "Defeating targets partially reloads the magazine from reserves.",
                            IconPath = "/common/destiny2_content/icons/03d774525048e5eb5b6899b37d418920.png",
                        },
                        WeaponPerkList = null,
                    }, new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 2387244414u,
                            Name = "Zen Moment",
                            Description = "Causing damage with this weapon increases its stability.",
                            IconPath = "/common/destiny2_content/icons/49958f356ea1df930888d15fe6539fe1.png",
                        },
                        WeaponPerkList = null,
                    }, new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 2869569095u,
                            Name = "Field Prep",
                            Description = "Increased ammo reserves. Faster reload, stow, and ready when you're crouching.",
                            IconPath = "/common/destiny2_content/icons/fd815414532978ad2e5decbfc416b9e4.png",
                        },
                        WeaponPerkList = new List<WeaponPerkValueDefinition>
                        {
                            new WeaponPerkValueDefinition
                            {
                                StatHash = 1931675084u,
                                Value = 30,
                            },
                        },
                    }, new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 3300816228u,
                            Name = "Auto-Loading Holster",
                            Description = "The holstered weapon is automatically reloaded after a short period of time.",
                            IconPath = "/common/destiny2_content/icons/d355b1117307c9f187729249361cecc3.png",
                        },
                        WeaponPerkList = null,
                    },
                },
            });

            weaponPerksCollection.Values.Add(new WeaponPerkSetDefinition
            {
                SocketIndex = 4,
                SocketTypeHash = 2614797986,
                PlugSetHash = 1853656119,
                Values = new List<WeaponPerkDefinition>
                {
                    new WeaponPerkDefinition
                   {
                      MetaData = new WeaponPerkMetaData
                       {
                          HashId = 3425386926u,
                          Name = "Rampage",
                          Description = "Kills with this weapon temporarily grant increased damage. Stacks 3x.",
                          IconPath = "/common/destiny2_content/icons/e9aa2f479812bfabc8d48effde384737.png",
                       },
                      WeaponPerkList = null,
                   }, new WeaponPerkDefinition
                   {
                       MetaData = new WeaponPerkMetaData
                       {
                          HashId = 1015611457u,
                          Name = "Kill Clip",
                          Description = "Reloading after a kill grants increased damage.",
                          IconPath = "/common/destiny2_content/icons/03e17b5e24aa08bebda6bcbd405b8ada.png",
                       },
                       WeaponPerkList = null,
                   }, new WeaponPerkDefinition
                   {
                      MetaData = new WeaponPerkMetaData
                        {
                            HashId = 2458213969u,
                            Name = "Multikill Clip",
                            Description = "Reloading grants increased damage based on the number of rapid kills made beforehand.",
                            IconPath = "/common/destiny2_content/icons/a610c14dcf450e8c5b0c0903d5b05965.png",
                        },
                      WeaponPerkList = null,
                   }, new WeaponPerkDefinition
                   {
                      MetaData = new WeaponPerkMetaData
                        {
                            HashId = 3523296417u,
                            Name = "Demolitionist",
                            Description = "Kills with this weapon generate grenade energy. Activating your grenade ability reloads this weapon from reserves.",
                            IconPath = "/common/destiny2_content/icons/53ead908917c9156457c7c9dd453d649.png",
                        },
                      WeaponPerkList = null,
                   }, new WeaponPerkDefinition
                    {
                        MetaData = new WeaponPerkMetaData
                        {
                            HashId = 4082225868u,
                            Name = "Swashbuckler",
                            Description = "This weapon gains increased damage from melee kills and kills with this weapon.",
                            IconPath = "/common/destiny2_content/icons/ef89b89410de618e9cfa353ff35125d8.png",
                        },
                        WeaponPerkList = null,
                    },
                },
            });

            #endregion

            return new WeaponDefinition(weaponMetaData, weaponStatsCollection, weaponPerksCollection);
        }
    }
}
