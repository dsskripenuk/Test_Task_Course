﻿namespace Course_Test_Task.Models
{
    public class Catalog
    {
        public int CatalogId { get; set; }
        public string Name { get; set; }
        public int? ParentCatalogId { get; set; }
        public Catalog? ParentCatalog { get; set; }
        public List<Catalog> SubCatalogs { get; set; }
    }
}
