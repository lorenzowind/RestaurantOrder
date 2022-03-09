using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantOrder.API.Domain
{
    public static class RestaurantMenu
    {
        public const string ERROR = "error";

        private enum DishType { MORNING, NIGHT }

        // Item '1'
        private enum Entree { EGGS, STEAK }

        // Item '2'
        private enum Side { TOAST, POTATO }

        // Item '3'
        private enum Drink { COFFEE, WINE }

        // Item '4'
        private enum Dessert { CAKE }

        public static bool IsDishTypeValid(string dishType) 
            => Enum.TryParse(dishType.ToUpper(), out DishType _);

        public static int GetItemRepetitions(int item, List<int> items) 
            => items.Count(it => it == item);

        public static bool CanItemBeRepeated(string dishType, int item)
        {
            if (dishType.ToUpper() == DishType.MORNING.ToString())
            {
                return item switch
                {
                    3 => true,
                    _ => false,
                };
            }

            if (dishType.ToUpper() == DishType.NIGHT.ToString())
            {
                return item switch
                {
                    2 => true,
                    _ => false,
                };
            }

            return false;
        }

        public static bool IsAllowedItem(string dishType, int item)
        {
            if (dishType.ToUpper() == DishType.MORNING.ToString())
            {
                return item switch
                {
                    1 or 2 or 3 => true,
                    _ => false,
                };
            }

            if (dishType.ToUpper() == DishType.NIGHT.ToString())
            {
                return item switch
                {
                    1 or 2 or 3 or 4 => true,
                    _ => false,
                };
            }

            return false;
        }

        public static string GetItem(string dishType, int item)
        {
            if (dishType.ToUpper() == DishType.MORNING.ToString())
            {
                return item switch
                {
                    1 => Entree.EGGS.ToString().ToLower(),
                    2 => Side.TOAST.ToString().ToLower(),
                    3 => Drink.COFFEE.ToString().ToLower(),
                    _ => ERROR,
                };
            }

            if (dishType.ToUpper() == DishType.NIGHT.ToString())
            {
                return item switch
                {
                    1 => Entree.STEAK.ToString().ToLower(),
                    2 => Side.POTATO.ToString().ToLower(),
                    3 => Drink.WINE.ToString().ToLower(),
                    4 => Dessert.CAKE.ToString().ToLower(),
                    _ => ERROR,
                };
            }

            return ERROR;
        }
    }
}
