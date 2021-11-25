using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Plot1 : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Level6Plot1Dialog","/Level6Plot1.txt","Canvas","Text","Image",new DialogUI());
    }
}
