using CommandLine;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Shared.Extensions;
using Shared.Feature;
using Shared.Models;

var serviceCollection = new ServiceCollection();

serviceCollection.AddShared();
serviceCollection.AddSingleton(args);



