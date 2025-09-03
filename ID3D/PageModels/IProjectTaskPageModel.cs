using CommunityToolkit.Mvvm.Input;
using ID3D.Models;

namespace ID3D.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}