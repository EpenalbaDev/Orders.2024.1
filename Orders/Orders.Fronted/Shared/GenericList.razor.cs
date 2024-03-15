﻿using Microsoft.AspNetCore.Components;

namespace Orders.Fronted.Shared
{
    public partial class GenericList<Titem>
    {
        [Parameter] public RenderFragment? Loading { get; set; }
        [Parameter] public RenderFragment? NoRecords { get; set; }
        [EditorRequired,Parameter] public RenderFragment? Body { get; set; }
        [EditorRequired, Parameter] public List<Titem>? MyList { get; set; }

    }
}
