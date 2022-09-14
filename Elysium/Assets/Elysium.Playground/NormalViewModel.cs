using PropertyChanged.SourceGenerator;

namespace Elysium.Playground
{
    public partial class NormalViewModel
    {
        [Notify] private string _fieldValue = string.Empty;
    }
}