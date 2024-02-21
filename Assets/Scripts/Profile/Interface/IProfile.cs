using Project.DependencyInjection;

namespace Project
{
    public interface IProfile : IAsyncDIModule
    {
        Coins Coins { get; }
        Meat Meat { get; }
        bool MusicIsActive { get; set; }
        bool SoundsIsActive { get; set; }
        float MusicValue { get; set; }
        float SoundsValue { get; set; }
    }
}