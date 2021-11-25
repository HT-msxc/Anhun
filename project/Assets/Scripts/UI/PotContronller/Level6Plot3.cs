using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Plot3 : BaseDialogContronller
{
    public override void CreatePlot()
    {
        plotType = new PlotType("Level6Plot3Dialog","/Level6Plot3.txt","Canvas","Text","Image",new DialogUI());
    }
}
