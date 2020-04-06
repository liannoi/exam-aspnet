using System.Reflection;
using AutoMapper;
using Exam.Application.Common.Behaviour;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Exam.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection self)
        {
            self.AddAutoMapper(Assembly.GetExecutingAssembly());
            self.AddMediatR(Assembly.GetExecutingAssembly());
            self.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return self;
        }
    }
}