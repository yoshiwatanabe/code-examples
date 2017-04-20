using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryAsCSharp
{
    public enum Privilege { Owner, Editor, Viwer }
    
    public class Manifest
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool AccessReistricted { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<ImageRef> ImageRefs { get; set; }
    }

    public class ImageRef
    {
        public string ImageSourceUri { get; set; }
        public int DimensionHeigh { get; set; }
        public int DimensionWidth { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }        
        public ICollection<Assortment> Assortments { get; set; }
        public ICollection<ImageSelection> ImageSelections { get; set; }
    }

    public class Assortment
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public ICollection<LaunchMarket> LaunchMarkets { get; set; }
        public ICollection<ImageSelection> ImageSelections { get; set; }
    }

    public class LaunchMarket
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public ICollection<LocaleSpec> LocaleSpecs { get; set; }
        public ICollection<ImageSelection> ImageSelections { get; set; }
    }

    public class LocaleSpec
    {
        public string Locale { get; set; }
        public ICollection<ImageSelection> ImageSelections { get; set; }
    }

    public class ImageSelection
    {
        public string ImageRefId { get; set; }
        public string AltText { get; set; }
    }
}
