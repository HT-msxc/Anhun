using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Plot2 : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Level5Plot2Dialog","/Level5Plot2.txt","Canvas","Text","Image",new DialogUI());
    }
}
