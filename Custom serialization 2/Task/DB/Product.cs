namespace Task.DB
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;

    [Serializable]
    public partial class Product : ISerializable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("icrosoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            Order_Details = new HashSet<Order_Detail>();
        }

        public Product(SerializationInfo information, StreamingContext context)
        {
            ProductID = information.GetInt32(nameof(ProductID));
            ProductName = information.GetString(nameof(ProductName));
            SupplierID = (int?)information.GetValue(nameof(SupplierID), typeof(int?));
            CategoryID = (int?)information.GetValue(nameof(CategoryID), typeof(int?));
            QuantityPerUnit = information.GetString(nameof(QuantityPerUnit));
            UnitPrice = (decimal?)information.GetValue(nameof(UnitPrice), typeof(decimal?));
            UnitsInStock = (short?)information.GetValue(nameof(UnitsInStock), typeof(short?));
            UnitsOnOrder = (short?)information.GetValue(nameof(UnitsOnOrder), typeof(short?));
            ReorderLevel = (short?)information.GetValue(nameof(ReorderLevel), typeof(short?));
            Discontinued = information.GetBoolean(nameof(Discontinued));
            Category = (Category)information.GetValue(nameof(Category), typeof(Category));
            Order_Details = (ICollection<Order_Detail>)information.GetValue(nameof(Order_Details), typeof(ICollection<Order_Detail>));
            Supplier = (Supplier)information.GetValue(nameof(Supplier), typeof(Supplier));
        }

        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        public int? SupplierID { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public decimal? UnitPrice { get; set; }

        public short? UnitsInStock { get; set; }

        public short? UnitsOnOrder { get; set; }

        public short? ReorderLevel { get; set; }

        public bool Discontinued { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order_Detail> Order_Details { get; set; }

        public virtual Supplier Supplier { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            var dbContext = (context.Context as IObjectContextAdapter).ObjectContext;

            dbContext.LoadProperty(this, x => x.Category);
            dbContext.LoadProperty(this, x => x.Order_Details);
            dbContext.LoadProperty(this, x => x.Supplier);
            info.AddValue(nameof(ProductID), ProductID);
            info.AddValue(nameof(ProductName), ProductName);
            info.AddValue(nameof(SupplierID), SupplierID);
            info.AddValue(nameof(CategoryID), CategoryID);
            info.AddValue(nameof(QuantityPerUnit), QuantityPerUnit);
            info.AddValue(nameof(UnitPrice), UnitPrice);
            info.AddValue(nameof(UnitsInStock), UnitsInStock);
            info.AddValue(nameof(UnitsOnOrder), UnitsOnOrder);
            info.AddValue(nameof(ReorderLevel), ReorderLevel);
            info.AddValue(nameof(Discontinued), Discontinued);
            info.AddValue(nameof(Category), Category);
            info.AddValue(nameof(Order_Details), Order_Details);
            info.AddValue(nameof(Supplier), Supplier);
        }
    }
}
