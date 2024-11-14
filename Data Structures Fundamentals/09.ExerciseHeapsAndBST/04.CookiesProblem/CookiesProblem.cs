using System;
using System.Linq;
using _03.MinHeap;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int minSweetness, int[] cookies)
        {
            OrderedBag<int> cookieBag = new OrderedBag<int>();

            cookieBag.AddMany(cookies);

            int steps = 0;
            int currentSweetness = cookieBag.GetFirst();

            while(currentSweetness < minSweetness && cookieBag.Count > 1)
            {
                int firstCookie = cookieBag.RemoveFirst();
                int secondCookie = cookieBag.RemoveFirst();
                
                int newCookie = firstCookie + 2 * secondCookie;
                cookieBag.Add(newCookie);
                currentSweetness = cookieBag.GetFirst();

                steps++;
            }

            return currentSweetness < minSweetness ? -1 : steps;
        }
    }
}
