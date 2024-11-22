using MvcWebApp.Models;

namespace MvcWebApp.ViewModels;
public class RiddleCategoryDetail
{
    public RiddleCategory Category { get; set; }
    public int RiddleCount { get; set; }
    public List<Riddle> PreviewRiddles { get; set; } = [];

    public RiddleCategoryDetail(RiddleCategory category, int riddleCount, List<Riddle> previewRiddles)
    {
        Category = category;
        RiddleCount = riddleCount;
        PreviewRiddles = previewRiddles;
    }
}