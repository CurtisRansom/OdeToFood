using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestsaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestsaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant {Id=1, Name="Scott's Pizza", Locaiton="Maryland", Cuisine=CuisineType.Italian},
                new Restaurant {Id=2, Name="Cinnamon Club", Locaiton="London England", Cuisine=CuisineType.Italian},
                new Restaurant {Id=3, Name="La Costa", Locaiton="California", Cuisine=CuisineType.Mexican}
            };
        }
        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            restaurants.Add(newRestaurant);
            
            return newRestaurant;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);

            if(restaurant != null)
            {
                restaurant.Name = updatedRestaurant.Name;
                restaurant.Locaiton = updatedRestaurant.Locaiton;
                restaurant.Cuisine = updatedRestaurant.Cuisine;
            }

            return restaurant;
        }

        public int Commit()
        {
            return 0;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);

            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }

            return restaurant;
        }
    }
}
