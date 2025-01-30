using System.Collections.Generic;

namespace ConceptorUI.Services;

public class TransferService
{
    public void Transfer<T>(object sender, object data)
    {
        if (_objects == null) return;
        
        foreach (var item in _objects)
        {
            if (sender.ToString() != item.GetType().Name) continue;
            if (item.GetType().IsSubclassOf(typeof(TransferControlExtension)))
                ((TransferControlExtension)item).GetTransferData(sender, data);
            break;
        }
    }

    public static void SaveObject(object transferExtension)
    {
        _objects ??= [];
        var index = -1;

        for (var i = 0; i < _objects.Count; i++)
        {
            if (transferExtension.GetType().Name != _objects[i].GetType().Name) continue;
            index = i;
        }

        if (index != -1)
            _objects.RemoveAt(index);

        _objects.Add(transferExtension);
    }

    private static List<object> _objects;
}