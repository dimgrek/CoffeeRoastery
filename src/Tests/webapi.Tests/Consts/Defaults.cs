using System;
using CoffeeRoastery.DAL.Domain.Models;

namespace webapi.Tests.Consts;

public class Defaults
{
    public static readonly Product[] Products =
    {
        new Product
        {
            Id = new Guid("0B647B6B-9FFE-4C37-B412-A652450D65C5"),
            Name = "Nensebo",
            CountryOfOrigin = "Ethiopia",
        },
        new Product
        {
            Id = new Guid("2E5CE2E0-F580-469E-BB49-C3E3F725ACA2"),
            Name = "Santa Ana",
            CountryOfOrigin = "Salvador",
        },
        new Product
        {
            Id = new Guid("F0B98F57-7FD8-4843-BF25-A64E16D158FF"),
            Name = "Yirga Chefe",
            CountryOfOrigin = "Ethiopia",
        },
        new Product
        {
            Id = new Guid("D30C4C1D-E67A-4928-88CB-D0416DB85188"),
            Name = "Cordillera Cacahuatique",
            CountryOfOrigin = "El Salvador",
        },
    };
}