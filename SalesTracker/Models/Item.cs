using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesTracker.Models
{
    [Table("Items")]
    public class Item
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemPrice { get; set; }
        public int SalesAssociateId { get; set; }

        public virtual SalesAssociate SalesAssociate { get; set; }

        public Item(string itemName, string itemDescription, int itemPrice, int salesAssociateId = 0)
        {
            ItemName = itemName;
            ItemDescription = itemDescription;
            ItemPrice = itemPrice;
            SalesAssociateId = salesAssociateId;
        }

        public Item() {}
    }
}