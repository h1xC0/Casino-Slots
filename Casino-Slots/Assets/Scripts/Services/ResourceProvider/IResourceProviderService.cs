using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace ResourceProvider
{
    public interface IResourceProviderService
    {
        TResource LoadResource<TResource>(bool baseType = false) where TResource : Object, IResource;
        IEnumerable<TResource> LoadResources<TResource>(bool baseType = false)
            where TResource : Object, IResource;
    }
}