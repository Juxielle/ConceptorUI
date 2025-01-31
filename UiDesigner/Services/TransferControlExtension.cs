using System.Windows.Controls;

namespace ConceptorUI.Services;

public abstract class TransferControlExtension : UserControl
{
    public TransferControlExtension()
    {
        TransferService.SaveObject(this);
    }

    public abstract void GetTransferData(object sender, object data);
}