using CommanderGQL.Data;
using CommanderGQL.Models;

namespace CommanderGQL.GraphQL.Platforms;

public class PlatformType : ObjectType<Platform>
{
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
        descriptor.Description("Represents any software or service that has a CLI.");

        descriptor
            .Field(p => p.Commands)
            .ResolveWith<Resolvers>(r => r.GetCommands(default!, default!))
            .UseDbContext<AppDbContext>()
            .Description("This is the list of available commands for this platform.");
    }

    private class Resolvers
    {
        public IQueryable<Command> GetCommands([Parent]Platform platform, AppDbContext context)
        {
            return context.Commands.Where(p => p.PlatformId == platform.Id);
        }
    }
}