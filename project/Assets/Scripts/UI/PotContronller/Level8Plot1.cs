using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level8Plot1 : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Level8Plot1Dialog","/Level8Plot1.txt","Canvas","Text","Image",new DialogUI());
    }
}
