using System.Linq;
using System.Collections.Generic;
using HotChocolate.Language;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;

#nullable enable

namespace HotChocolate.Configuration
{
    internal sealed class SyntaxTypeReferenceHandler
        : ITypeRegistrarHandler
    {
        public void Register(
            ITypeRegistrar typeRegistrar,
            IEnumerable<ITypeReference> typeReferences)
        {
            foreach (SyntaxTypeReference typeReference in
                typeReferences.OfType<SyntaxTypeReference>())
            {
                if (Scalars.TryGetScalar(
                    typeReference.Type.NamedType().Name.Value,
                    out ClrTypeReference namedTypeReference))
                {
                    if (!typeRegistrar.IsResolved(namedTypeReference))
                    {
                        typeRegistrar.Register(
                            typeRegistrar.CreateInstance(namedTypeReference.Type.Type),
                            typeReference.Scope);
                    }
                }
            }
        }
    }
}
