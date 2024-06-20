using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class GenericPool<T> where T : class, IPoolable
    {
        private List<T> objectsInUse = new List<T>();
        private List<T> objectsAvailable = new List<T>();

        public GenericPool(int size, Func<T> objectFactory)
        {
            for (int i = 0; i < size; i++)
            {
                T newObj = objectFactory.Invoke();
                newObj.OnDestroy += (obj) => RecycleObject((T)obj);
                objectsAvailable.Add(newObj);
            }
        }

        public T GetObject()
        {
            T newObj = default(T);

            if (objectsAvailable.Count > 0)
            {
                newObj = objectsAvailable[0];
                objectsAvailable.Remove(newObj);
                objectsInUse.Add(newObj);
            }
            return newObj;
        }

        private void RecycleObject(T obj)
        {
            if (objectsInUse.Contains(obj))
            {
                objectsInUse.Remove(obj);
                objectsAvailable.Add(obj);
            }
        }


        public void PrintObjects()
        {
            Console.WriteLine("Available: " + objectsAvailable.Count);
            Console.WriteLine("In Use: " + objectsInUse.Count);
        }
    }

}
