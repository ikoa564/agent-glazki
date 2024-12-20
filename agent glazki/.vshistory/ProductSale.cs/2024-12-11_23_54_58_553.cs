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

    public partial class ProductSale
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int AgentID { get; set; }
        public System.DateTime SaleDate { get; set; }
        public int ProductCount { get; set; }

        public int Discount
        {
            get
            {
                int totalCost = 0;

                // Получаем список продаж и связанных продуктов
                var sales = AbdeevGlazkiSaveEntities.GetContext().ProductSale
                    .Include("Product") // Подгружаем связанные данные о продукте
                    .Where(ps => ps.AgentID == this.ID) // Фильтруем по агенту
                    .ToList();

                // Вычисляем общую стоимость
                foreach (var sale in sales)
                {
                    var product = AbdeevGlazkiSaveEntities.GetContext().Product.FirstOrDefault(p => p.ID == sale.ProductID);
                    if (product != null)
                    {
                        totalCost += (int)(product.MinCostForAgent * sale.ProductCount); // Приведение к int
                    }
                }

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

        public virtual Agent Agent { get; set; }
        public virtual Product Product { get; set; }
    }
}
