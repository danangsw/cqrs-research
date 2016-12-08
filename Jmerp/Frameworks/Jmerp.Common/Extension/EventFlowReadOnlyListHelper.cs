using EventFlow.Core;
using EventFlow.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Commons.Extension
{
    public static class EventFlowReadOnlyListHelper
    {
        public static List<T> AddList<T, Z>(this IReadOnlyList<T> oldList, T addValue) where Z : IIdentity where T : Entity<Z>
        {
            var newList = new List<T>();
            if (oldList != null)
            {
                newList.AddRange(oldList);
            }

            newList.Add(addValue);

            return newList;
        }

        public static List<T> UpdateList<T, Z>(this IReadOnlyList<T> oldList, T updateValue) where Z : IIdentity where T : Entity<Z>
        {
            var newList = new List<T>();

            if (oldList != null)
            {
                newList.AddRange(oldList);
            }

            var oldValue = oldList.Where(x => x.Id.Equals(updateValue.Id)).Single();

            var indexOld = newList.IndexOf(oldValue);
            if (indexOld != -1)
            {
                newList[indexOld] = updateValue;
            }

            return newList;
        }

        public static List<T> DeleteFromList<T, Z>(this IReadOnlyList<T> oldList, T deleteValue) where Z : IIdentity where T : Entity<Z>
        {
            var newList = new List<T>();

            if (oldList != null)
            {
                newList.AddRange(oldList);
            }

            var oldValue = oldList.Where(x => x.Id.Equals(deleteValue.Id)).Single();
            newList.Remove(oldValue);

            return newList;

        }

        public static List<T> GetDataFromCollectionCompareWithInputBasedOnId<T, Z>(this IReadOnlyList<T> oldList, IReadOnlyList<T> inputsValue) where Z : IIdentity where T : Entity<Z>
        {
            var newList = new List<T>();

            foreach (var item in oldList)
            {
                if (!inputsValue.Contains(item, new GenericCompare<T>(x => x.Id)))
                {
                    newList.Add(item);
                }
            }

            return newList;
        }


    }
}
