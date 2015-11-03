using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModManager
{
    public class GameCollection
    {
        public List<Game> Games = new List<Game>();

        public void AddGameToColleciton(Game g)
        {
            Games.Add(g);
        }

        public void RemoveGameFromCollection(Game g)
        {
            Games.Remove(g);
        }
        
        public Game ReturnGameByName(string gameTitle)
        {
            foreach(Game g in Games)
            {
                if(g.Name == gameTitle)
                {
                    return g;
                }
            }
            return null;
            
        }

    }
}
