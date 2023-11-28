using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.MoovIt
{
    public class MoovIt : IMoovIt
    {
        private Dictionary<string, Route> routesById = new Dictionary<string, Route>();
        private Dictionary<string, Route> routesByDistAndLocation = new Dictionary<string, Route>();

        public int Count => routesById.Count;

        public bool Contains(Route route)
        {
            return routesById.ContainsKey(route.Id) || routesByDistAndLocation.ContainsKey(GetKey((route)));
        }

        public void AddRoute(Route route)
        {
            if (Contains(route))
            {
                throw new ArgumentException();
            }

            routesById.Add(route.Id, route);
            routesByDistAndLocation.Add(GetKey(route), route);
        }

        public void ChooseRoute(string routeId)
        {
            if (!routesById.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }

            routesById[routeId].Popularity++;
        }

        public IEnumerable<Route> GetFavoriteRoutes(string destinationPoint)
            => routesById.Values
                .Where(r => r.IsFavorite)
                .Where(r => r.LocationPoints.Contains(destinationPoint) && 
                            r.LocationPoints[0] != destinationPoint)
                .OrderBy(r => r.Distance)
                .ThenByDescending(r => r.Popularity);        

        public Route GetRoute(string routeId)
        {
            if (!routesById.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }

            return routesById[routeId];
        }

        public IEnumerable<Route> GetTop5RoutesByPopularityThenByDistanceThenByCountOfLocationPoints()
            => routesById.Values
            .OrderByDescending(r => r.Popularity)
            .ThenBy(r => r.Distance)
            .ThenBy(r => r.LocationPoints.Count)
            .Take(5);

        public void RemoveRoute(string routeId)
        {
            if (!routesById.ContainsKey(routeId))
            {
                throw new ArgumentException();
            }
            var route = routesById[routeId];
            routesById.Remove(routeId);
            routesByDistAndLocation.Remove(GetKey(route));
        }

        public IEnumerable<Route> SearchRoutes(string startPoint, string endPoint)
            => routesById.Values
                .Where(r => 
                {
                    var startIndex = r.LocationPoints.IndexOf(startPoint);
                    var endIndex = r.LocationPoints.IndexOf(endPoint);

                    return startIndex >= 0 && endIndex > startIndex;
                })
                .OrderByDescending(r => r.IsFavorite)
                .ThenBy(r =>
                {
                    var startIndex = r.LocationPoints.IndexOf(startPoint);
                    var endIndex = r.LocationPoints.IndexOf(endPoint);
                    return endIndex - startIndex;
                })
                .ThenByDescending(r => r.Popularity);        

        private string GetKey(Route route)
        {
            return route.Distance.ToString() + route.LocationPoints[0] + route.LocationPoints[route.LocationPoints.Count - 1];
        }
    }
}
