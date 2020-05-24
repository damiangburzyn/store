using System;
using System.Collections.Generic;
using System.Linq;
using Store.Data.Database;
using Microsoft.AspNetCore.Identity;
using Store.Data.Entities.Identity;
using Store.Data.EF.Entities;
using Store.Contracts;
using Microsoft.Extensions.Logging;

namespace Store.Data.Seeds
{
    public static class InitialApplicationIdentitySeed
    {
        public static void Run(
           ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager
      //     , ILogger logger
            )
        {
            AcceptanceTypesSeed(dbContext);

            dbContext.SaveChanges();
            
            AcceptanceFormulasSeed(dbContext);

            RolesSeed(dbContext, roleManager);

            UsersSeed(dbContext, userManager);


            dbContext.SaveChanges();
        }

        private static void UsersSeed(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            const string ownerMail = "admin@niepodam.pl";
            var owner = new ApplicationUser("admin", ownerMail, EUserStatus.Active, true);

            if (dbContext.Users.Any(x => x.Email.ToLower() == ownerMail))
            {
                owner = dbContext.Users.First(x => x.Email.ToLower() == ownerMail);
            }
            else
            {
                userManager.CreateAsync(owner).Wait();
                userManager.AddPasswordAsync(owner, "password").Wait();
            }

            userManager.AddToRoleAsync(owner, ERole.User.ToString()).Wait();
            userManager.AddToRoleAsync(owner, ERole.Administrator.ToString()).Wait();
        }

        private static void RolesSeed(ApplicationDbContext dbContext, RoleManager<ApplicationRole> roleManager)
        {
            var dbRoles = dbContext.Roles.ToList();

            foreach (ERole role in Enum.GetValues(typeof(ERole)))
            {
                var roleName = role.ToString();

                if (!dbRoles.Any(x => string.Equals(x.Name, roleName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    var newRole = new ApplicationRole(roleName);
                    roleManager.CreateAsync(newRole).Wait();
                }
            }
        }



        private static void AcceptanceTypesSeed(ApplicationDbContext dbContext)
        {
            try
            {
                var acceptanceTypes = new List<AcceptanceType>() {
                new AcceptanceType() { Id=1, Name = "Regulamin" },
                new AcceptanceType() { Id=2, Name = "Lowoganie do strony" },
                new AcceptanceType() { Id=3, Name = "Przetwarzanie firma" },
                new AcceptanceType() { Id=4, Name = "Informacje o usługach" },
                new AcceptanceType() { Id=5, Name = "Przetwarzanie marketing" },
                new AcceptanceType() { Id=6, Name = "Otrzymywanie informacji" }
            };

                foreach (var acceptanceType in acceptanceTypes)
                {
                    if (!dbContext.AcceptanceTypes.Any(x => x.Id == acceptanceType.Id))
                    {
                        dbContext.AcceptanceTypes.Add(acceptanceType);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private static void AcceptanceFormulasSeed(ApplicationDbContext dbContext)
        {
            var acceptanceFormulas = new List<AcceptanceFormula>
            {
                new AcceptanceFormula
                {
                    AcceptanceTypeId = 1,
                    JsonText ="Zapoznałem się i akceptuję regulamin korzystania z serwisu.",
                },
                new AcceptanceFormula
                {
                    AcceptanceTypeId = 2,
                    JsonText =" Wyrażam zgodę na dodanie mojego adresu email do listy użytkowników na potrzeby logowania się do strony  oraz jestem świadomy/a o możliwości wycofania zgody zgodnie z art. 7, pkt 3 RODO.",
                },
                new AcceptanceFormula
                {
                    AcceptanceTypeId = 3,
                    JsonText =  "Wyrażam zgodę na przetwarzanie moich danych osobowych  w celach marketingowych, zgodnie z art. 6 ust. 1 lit. a ogólnego rozporządzenia o ochronie danych osobowych z dnia 27 kwietnia 2016 r.",

                },
                new AcceptanceFormula
                {
                    AcceptanceTypeId = 4,
                    JsonText = "Wyrażam zgodę na otrzymywanie drogą elektroniczną na wskazany przeze mnie w formularzu adres e-mail informacji handlowych, dotyczących produktów i usług oferowanych  w rozumieniu ustawy o świadczeniu usług drogą elektroniczną z dnia 18 lipca 2002 r. (Dz.U. z 2013 r., poz. 1422 ze zm.).",
                },
                new AcceptanceFormula
                {
                    AcceptanceTypeId = 5,
                    JsonText = "Wyrażam zgodę na przetwarzanie moich danych osobowych w celu marketingu bezpośredniego, dotyczącego produktów i usług, wykonywanego przy użyciu telekomunikacyjnych urządzeń końcowych oraz automatycznych systemów wywołujących, zgodnie z art. 172 ustawy z dnia 16 lipca 2004r. Prawo telekomunikacyjne (Dz.U. z 2014 r. poz. 243 ze zm.).",
                },
                new AcceptanceFormula
                {
                    AcceptanceTypeId = 6,
                    JsonText = "Wyrażam zgodę na otrzymywanie informacji marketingowych drogą elektroniczną na podany podczas rejestracji adres poczty e-mail.",
                },

            };

            foreach (var acceptanceFormula in acceptanceFormulas)
            {
                if (!dbContext.AcceptanceFormulas.Any(x => x.AcceptanceTypeId == acceptanceFormula.AcceptanceTypeId && x.JsonText == acceptanceFormula.JsonText))
                {
                    dbContext.AcceptanceFormulas.Add(acceptanceFormula);
                }
            }
        }
    }
}