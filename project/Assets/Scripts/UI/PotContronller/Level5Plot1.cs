using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Plot1 : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Level5Plot1Dialog","/Level5Plot1.txt","Canvas","Text","Image",new DialogUI());
    }
}
