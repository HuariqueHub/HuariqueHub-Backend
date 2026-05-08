using System.Linq;
using PuntoSabor_Backend.Shared.Infrastructure.Persistence.EFC;
using PuntoSabor_Backend.Auth.Domain.Model;
using PuntoSabor_Backend.Discovery.Domain.Model;
using PuntoSabor_Backend.Memberships.Domain.Model;
using PuntoSabor_Backend.Promotions.Domain.Model;
using PuntoSabor_Backend.Reviews.Domain.Model;

namespace PuntoSabor_Backend.Shared.Infrastructure.Persistence.EFC;

/**
 * <summary>
 * Pobla la base de datos con datos iniciales para prueba y demo.
 * </summary>
 */

public static class DataSeeder
{
    public static void Seed(AppDbContext context)
    {
        // CATEGORIES
        if (!context.Categories.Any())
        {
            context.Categories.AddRange(
                new Category { Id = 1, Name = "Pollo" },
                new Category { Id = 2, Name = "Marina" },
                new Category { Id = 3, Name = "Criolla" },
                new Category { Id = 4, Name = "Chifa" },
                new Category { Id = 5, Name = "Postres" },
                new Category { Id = 6, Name = "Menú" },
                new Category { Id = 7, Name = "Café" },
                new Category { Id = 8, Name = "Parrillas" }
            );
        }

        // HUARIQUES
if (!context.Huariques.Any())
{
    context.Huariques.AddRange(
        new Huarique {
            Id = 1,
            Name = "El Brasero",
            CategoryId = 1,
            Category = "Pollo",
            Price = 22,
            Rating = 4.6,
            District = "Surco",
            Near = true
        },

        new Huarique {
            Id = 2,
            Name = "Rincón Marino",
            CategoryId = 2,
            Category = "Marina",
            Price = 28,
            Rating = 4.8,
            District = "Chorrillos",
            Near = false
        },

        new Huarique {
            Id = 3,
            Name = "Doña Peta Criolla",
            CategoryId = 3,
            Category = "Criolla",
            Price = 25,
            Rating = 4.5,
            District = "Barranco",
            Near = true
        },

        new Huarique {
            Id = 4,
            Name = "Chifa San Joy Lao",
            CategoryId = 4,
            Category = "Chifa",
            Price = 21,
            Rating = 4.4,
            District = "Miraflores",
            Near = false
        },

        new Huarique {
            Id = 5,
            Name = "La Dulcería",
            CategoryId = 5,
            Category = "Postres",
            Price = 15,
            Rating = 4.9,
            District = "San Isidro",
            Near = false
        },

        new Huarique {
            Id = 6,
            Name = "La Esquinita del Menú",
            CategoryId = 6,
            Category = "Menú",
            Price = 12,
            Rating = 4.2,
            District = "San Borja",
            Near = true
        },

        new Huarique {
            Id = 7,
            Name = "Café Aroma & Sabor",
            CategoryId = 7,
            Category = "Café",
            Price = 10,
            Rating = 4.7,
            District = "Miraflores",
            Near = false
        },

        new Huarique {
            Id = 8,
            Name = "Pollos Don Tito",
            CategoryId = 1,
            Category = "Pollo",
            Price = 24,
            Rating = 4.7,
            District = "La Molina",
            Near = false
        },

        new Huarique {
            Id = 9,
            Name = "Mar & Tierra",
            CategoryId = 2,
            Category = "Marina",
            Price = 30,
            Rating = 4.3,
            District = "San Miguel",
            Near = true
        },

        new Huarique {
            Id = 10,
            Name = "Café Central",
            CategoryId = 7,
            Category = "Café",
            Price = 11,
            Rating = 4.5,
            District = "Centro de Lima",
            Near = false
        },

        new Huarique {
            Id = 11,
            Name = "Parrilladas Don Mario",
            CategoryId = 8,
            Category = "Parrillas",
            Price = 35,
            Rating = 4.8,
            District = "Surquillo",
            Near = true
        },

        new Huarique {
            Id = 12,
            Name = "Brasa y Carbón",
            CategoryId = 8,
            Category = "Parrillas",
            Price = 38,
            Rating = 4.6,
            District = "Lince",
            Near = false
        },

        new Huarique {
            Id = 13,
            Name = "Fuego Criollo",
            CategoryId = 8,
            Category = "Parrillas",
            Price = 36,
            Rating = 4.7,
            District = "San Juan de Miraflores",
            Near = true
        },

        new Huarique {
            Id = 14,
            Name = "La Parrilla del Norte",
            CategoryId = 8,
            Category = "Parrillas",
            Price = 33,
            Rating = 4.5,
            District = "Los Olivos",
            Near = false
        },

        new Huarique {
            Id = 15,
            Name = "Punto Grill",
            CategoryId = 8,
            Category = "Parrillas",
            Price = 40,
            Rating = 4.9,
            District = "San Isidro",
            Near = false
        },

        new Huarique {
            Id = 16,
            Name = "La Picantería Peruana",
            CategoryId = 3,
            Category = "Criolla",
            Price = 27,
            Rating = 4.7,
            District = "Surquillo",
            Near = true
        },

        new Huarique {
            Id = 17,
            Name = "La Casa del Postre",
            CategoryId = 5,
            Category = "Postres",
            Price = 13,
            Rating = 4.7,
            District = "Surquillo",
            Near = true
        },

        new Huarique {
            Id = 18,
            Name = "Chifa Ping Chung Long",
            CategoryId = 4,
            Category = "Chifa",
            Price = 20,
            Rating = 4.2,
            District = "Lince",
            Near = false
        },

        new Huarique {
            Id = 19,
            Name = "El Sabor Norteño",
            CategoryId = 3,
            Category = "Criolla",
            Price = 23,
            Rating = 4.2,
            District = "Los Olivos",
            Near = false
        },

        new Huarique {
            Id = 20,
            Name = "La Ola Marina",
            CategoryId = 2,
            Category = "Marina",
            Price = 29,
            Rating = 4.4,
            District = "Magdalena",
            Near = true
        },

        new Huarique {
            Id = 21,
            Name = "Menu Don Lucho",
            CategoryId = 6,
            Category = "Menú",
            Price = 12,
            Rating = 4.2,
            District = "El Agustino",
            Near = true
        }
    );
}


        // PROMOS
        if (!context.Promos.Any())
        {
            context.Promos.AddRange(
                new Promo { Id = 1, Title = "2x1 Pollo Hoy",        Note = "Locales seleccionados" },
                new Promo { Id = 2, Title = "Menú Marino S/20",     Note = "Lun–Vie 12:00–16:00" },
                new Promo { Id = 3, Title = "Café + Brownie",       Note = "Solo en Aroma & Sabor" },
                new Promo { Id = 4, Title = "Parrillada Familiar",  Note = "15% en fines de semana" },
                new Promo { Id = 5, Title = "Postres 3x2",          Note = "En La Dulcería todo el mes" },
                new Promo { Id = 6, Title = "Descuento Criollo",    Note = "Platos criollos -10%" }
            );
        }

        // PLANS
        if (!context.Plans.Any())
        {
            context.Plans.AddRange(
                new Plan { Id = "premium",   Name = "Premium",   Price = 35 },
                new Plan { Id = "basic",     Name = "Básico",    Price = 0 },
                new Plan { Id = "exclusive", Name = "Exclusive", Price = 50 }
            );
        }

        // USERS demo
        if (!context.Users.Any())
        {
            context.Users.AddRange(
                new User { Id = 1, Name = "demo",        Email = "demo@upc.edu.pe" },
                new User { Id = 2, Name = "SoyElPepe",   Email = "wa@gmail.com" },
                new User { Id = 3, Name = "Luna C.",     Email = "luna@puntosabor.pe" },
                new User { Id = 4, Name = "Sebastián",   Email = "sebastian@upc.pe" }
            );
        }

        // REVIEWS
        if (!context.Reviews.Any())
        {
            context.Reviews.AddRange(
                new Review { Id = 1,  HuariqueId = 1,  UserId = 1, Rating = 5, Comment = "Buenazo y barato.",                                      CreatedAt = DateTime.Parse("2025-11-01T15:00:00Z") },
                new Review { Id = 2,  HuariqueId = 2,  UserId = 2, Rating = 4, Comment = "Rincón Marino excelente, pero demora un poco.",          CreatedAt = DateTime.Parse("2025-11-02T12:10:00Z") },
                new Review { Id = 3,  HuariqueId = 5,  UserId = 3, Rating = 5, Comment = "La Dulcería, los mejores postres del sur.",              CreatedAt = DateTime.Parse("2025-11-02T18:40:00Z") },
                new Review { Id = 4,  HuariqueId = 11, UserId = 2, Rating = 4, Comment = "Fuego Criollo espectacular, carnes suaves.",            CreatedAt = DateTime.Parse("2025-11-03T10:05:00Z") },
                new Review { Id = 5,  HuariqueId = 15, UserId = 4, Rating = 5, Comment = "Punto Grill es otro nivel, todo premium.",              CreatedAt = DateTime.Parse("2025-11-03T21:25:00Z") },
                new Review { Id = 6,  HuariqueId = 1,  UserId = 3, Rating = 3, Comment = "El Brasero normalito, pero buen precio.",               CreatedAt = DateTime.Parse("2025-11-04T09:15:00Z") },
                new Review { Id = 7,  HuariqueId = 3,  UserId = 1, Rating = 4, Comment = "Doña Peta Criolla buen sazón, menú variado.",           CreatedAt = DateTime.Parse("2025-11-04T14:30:00Z") },
                new Review { Id = 8,  HuariqueId = 11, UserId = 2, Rating = 5, Comment = "Don Mario la rompe con su parrillada familiar.",        CreatedAt = DateTime.Parse("2025-11-05T11:50:00Z") },
                new Review { Id = 9,  HuariqueId = 12, UserId = 4, Rating = 4, Comment = "Brasa y Carbón deliciosa, atención rápida.",            CreatedAt = DateTime.Parse("2025-11-05T19:00:00Z") },
                new Review { Id = 10, HuariqueId = 10, UserId = 3, Rating = 5, Comment = "Café Central con el mejor espresso.",                   CreatedAt = DateTime.Parse("2025-11-06T08:45:00Z") }
            );
        }

        context.SaveChanges();
    }
}
