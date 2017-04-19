namespace DataAccessFSharp

open System
open System.Data
open System.Data.Linq
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq
open FSharp.Data.TypeProviders

type dbSchema = SqlDataConnection<"Data Source=RTAPRDUSWSCDB1;Initial Catalog=CatalogReportingPortal;Integrated Security=SSPI;">

type SkuChange = 
    { 
        ChangeCategory: string
        CountryCode: string
        Price: decimal option
        OnlineStartDate : DateTime option
    }

module SkuDataAccess =
    let ToDateTimeOption (a: Nullable<DateTime>) = 
        if a.HasValue then Some(a.Value)
        else None
          
    let ToDecimalOption (a: Nullable<decimal>) =
        if a.HasValue then Some(a.Value)
        else None    

    let testFunc sku =
        use db = dbSchema.GetDataContext()
        db.DataContext.Log <- System.Console.Out
        let ancapChanges = db.ViewAllANCAPChanges
                
        let skuChanges sku =
            query {
              for row in db.ViewAllANCAPChanges do
              where (row.Sku = sku)
              select { 
                ChangeCategory = row.ChangeCategory
                CountryCode = row.CountryCode
                Price = (ToDecimalOption row.Price)
                OnlineStartDate = (ToDateTimeOption row.StartDate) 
              }
            } 

        let result = skuChanges sku
        result |> Seq.toArray
        
        