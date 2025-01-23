using System;
using ResourceProvider;
using UI.Credits;

namespace Services.ResourceProvider
{
    public class ResourceNames
    {
    
        private static readonly ResourceInfo[] Resources =
        {
            new(typeof(CreditsView), "Prefabs/CreditsView")
        };
        
        public static string GetPath<TResource>(bool baseType = false) where TResource : IResource
        {
            var path = string.Empty;
            var currentType = typeof(TResource);

            foreach (var resource in Resources)
            {
                var typeToSearch = resource.Type;
                if(typeToSearch == currentType)
                {
                    path = resource.Path;
                }
                
                if (!baseType) continue;
                if (typeToSearch.IsSubclassOf(currentType))
                {
                    path = resource.Path;
                }
                
            }

            if (string.IsNullOrEmpty(path))
                throw new NullReferenceException($"The is no path for resource with type {typeof(TResource)}.");

            return path;
        }
    }
}
