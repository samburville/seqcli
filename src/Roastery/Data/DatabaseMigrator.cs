﻿using Roastery.Model;

namespace Roastery.Data
{
    static class DatabaseMigrator
    {
        public static void Populate(Database database)
        {
            database.BulkLoad(
                new Product("Rocket Ship dark roast, whole beans", 100) {Id = "product-8908fd0sa"},
                new Product("Rocket Ship dark roast, whole beans", 250) {Id = "product-fsad890fj"},
                new Product("Rocket Ship dark roast, whole beans", 1000) {Id = "product-fsdjkljrw"},
                new Product("Rocket Ship dark roast, ground", 100) {Id = "product-2nkfkdsju"},
                new Product("Rocket Ship dark roast, ground", 250) {Id = "product-f8sa9newq"},
                new Product("Rocket Ship dark roast, ground", 1000) {Id = "product-cvsad9033"},
                new Product("1 AM medium roast, whole beans", 100) {Id = "product-i908jd0sf"},
                new Product("1 AM medium roast, whole beans", 250) {Id = "product-isadj90fd"},
                new Product("1 AM medium roast, whole beans", 1000) {Id = "product-isdjjljr3"},
                new Product("1 AM medium roast, ground", 100) {Id = "product-inkfjdsj2"},
                new Product("1 AM medium roast, ground", 250) {Id = "product-i8sajnew1"},
                new Product("1 AM medium roast, ground", 1000) {Id = "product-ivsaj903t"});
        }
    }
}