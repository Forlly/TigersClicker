using Project.DependencyInjection;

namespace Project
{
    public interface IProfile : IAsyncDIModule
    {
        Coins Coins { get; }
        Meat Meat { get; }
        bool MusicIsActive { get; set; }
        bool SoundsIsActive { get; set; }
        int CountOfBanks { get; set; }
        int CountOfButcheries { get; set; }
        int CountOfTigers { get; set; }
        bool IsIntroductionCompleted { get; set; }
        bool IsLearningCompleted { get; set; }
    }
}