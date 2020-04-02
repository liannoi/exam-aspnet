using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Exam.Application.Common.Mappings
{
    // ReSharper disable once UnusedType.Global
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
                type.GetMethod("Mapping")?.Invoke(Activator.CreateInstance(type), new object[] {this});
        }
    }
}