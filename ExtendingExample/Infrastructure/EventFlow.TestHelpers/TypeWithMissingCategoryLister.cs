using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventFlow.TestHelpers
{
    public class TypeWithMissingCategoryLister : MarshalByRefObject
    {
        private static readonly ISet<string> ValidCategories;

        static TypeWithMissingCategoryLister()
        {
            ValidCategories = new HashSet<string>(typeof(Categories)
                .GetFields()
                .Select(f => (string)f.GetValue(null)));
        }

        public List<string> GetTypesWithoutCategoryAttribute(string path)
        {
            return AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))
                .GetTypes()
                .Where(t => !t.IsAbstract)
                .Where(t => t.GetMethods().Any(mi =>
                    mi.GetCustomAttributes<TestAttribute>().Any() ||
                    mi.GetCustomAttributes<TestCaseAttribute>().Any()))
                .Select(t =>
                {
                    var categoryAttribute = t.GetCustomAttributes<CategoryAttribute>().SingleOrDefault();
                    if (categoryAttribute == null)
                    {
                        return $"{t.FullName} (no category)";
                    }
                    if (!ValidCategories.Contains(categoryAttribute.Name))
                    {
                        return $"{t.FullName} (invalid category '{categoryAttribute.Name}')";
                    }
                    return null;
                })
                .Where(e => !string.IsNullOrEmpty(e))
                .ToList();
        }
    }
}
