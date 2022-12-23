using Blazorise;
using Microsoft.AspNetCore.Components;

namespace quotes_web.View.Quoting;
public partial class ConfirmationModal
{
    private Modal? modalRef;
    [Parameter]
    public Func<Task> ConfirmationCallBack { get; set; }
    public Task ShowModal()
    {
        return modalRef.Show();
    }
    private Task HideModal()
    {
        return modalRef.Hide();
    }
    private async Task OnConfirmation()
    {
        await ConfirmationCallBack();
        modalRef.Hide();
    }
}
