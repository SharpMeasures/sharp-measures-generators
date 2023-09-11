namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

internal static class ReferenceLister
{
    public static IEnumerable<MetadataReference> List(Assembly assembly)
    {
        Queue<Assembly> unresolvedAssemblies = new();
        List<Assembly> resolvedAssemblies = new();

        HashSet<string> resolvedAssemblyNames = new();

        unresolvedAssemblies.Enqueue(assembly);

        while (unresolvedAssemblies.Any())
        {
            var targetAssembly = unresolvedAssemblies.Dequeue();

            foreach (var assemblyName in targetAssembly.GetReferencedAssemblies().Where((assemblyName) => resolvedAssemblyNames.Contains(assemblyName.FullName) is false))
            {
                resolvedAssemblyNames.Add(assemblyName.FullName);

                Assembly assemblyReference;

                try
                {
                    assemblyReference = Assembly.Load(assemblyName);
                }
                catch (Exception e) when (e is FileLoadException or FileNotFoundException)
                {
                    continue;
                }

                unresolvedAssemblies.Enqueue(assemblyReference);
                resolvedAssemblies.Add(assemblyReference);
            }
        }

        resolvedAssemblies.AddRange(AppDomain.CurrentDomain.GetAssemblies());

        return resolvedAssemblies.Where(static (assembly) => assembly.IsDynamic is false)
            .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();
    }
}
