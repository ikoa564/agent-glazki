//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace agent_glazki
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public partial class Agent
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Agent()
        {
            this.AgentPriorityHistory = new HashSet<AgentPriorityHistory>();
            this.ProductSale = new HashSet<ProductSale>();
            this.Shop = new HashSet<Shop>();
        }

        public int ID { get; set; }
        public int AgentTypeID { get; set; }

        public string AgentTypeText
        {
            get
            {
                return AgentType.Title;
            }
        }

        public int Discount
        {
            get
            {
                var context = AbdeevGlazkiSaveEntities.GetContext();

                var totalCost = context.ProductSale
            .Where(ps => ps.AgentID == this.ID)
            .Join(context.Product,
                  ps => ps.ProductID,
                  p => p.ID,
                  (ps, p) => new { ps.ProductCount, p.MinCostForAgent })
            .Sum(x => x.ProductCount * x.MinCostForAgent);



                int disc = 0;
                if (totalCost >= 0 && totalCost < 10000)
                    disc = 0;
                if (totalCost >= 10000 && totalCost < 50000)
                    disc = 5;
                if (totalCost >= 50000 && totalCost < 150000)
                    disc = 10;
                if (totalCost >= 150000 && totalCost < 500000)
                    disc = 20;
                if (totalCost >= 500000)
                    disc = 25;

                return disc;
            }
        }

        public string Title { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }


        public string Logo { get; set; }
        public string Address { get; set; }
        public int Priority { get; set; }
        public string DirectorName { get; set; }
        public string INN { get; set; }
        public string KPP { get; set; }


        public virtual AgentType AgentType { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentPriorityHistory> AgentPriorityHistory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSale { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Shop> Shop { get; set; }
    }
}
