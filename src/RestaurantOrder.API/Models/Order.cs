using RestaurantOrder.API.Domain;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.API.Models
{
    public class Order
    {
        public string DishType { get; private set; }
        public List<int> Items { get; private set; }

        public Order(string dishType, List<int> items)
        {
            DishType = dishType;
            Items = items;
        }

        public string GenerateOutput()
        {
            List<string> itemsOutput = new();

            foreach (int item in Items.Distinct().OrderBy(item => item))
            {
                string it = RestaurantMenu.GetItem(DishType, item);

                if (it == RestaurantMenu.ERROR)
                {
                    itemsOutput.Add(it);
                    break;
                }

                int itemRepetitions = RestaurantMenu.GetItemRepetitions(item, Items);

                bool canItemBeRepeated = RestaurantMenu.CanItemBeRepeated(DishType, item);

                if (itemRepetitions > 1)
                {
                    if (!canItemBeRepeated)
                    {
                        itemsOutput.Add(it);
                        itemsOutput.Add(RestaurantMenu.ERROR);
                        break;
                    }

                    it += $"({itemRepetitions}x)";
                } 

                itemsOutput.Add(it);
            }

            return string.Join(", ", itemsOutput);
        }
    }
}
