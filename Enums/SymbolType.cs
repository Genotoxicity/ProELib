using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProELib
{
    public enum SymbolType 
    { 
        Undefined =0,
        Border=1,
        Normal=2,
        SignalCarrying=3,
        Reference4=4,
        Reference5=5,
        ConnectorOrMaster=6,
        Block=8,
        ConnectorOnBlockBoundary=9,
        FreeConnector10=10,
        FreeConnector11=11,
        Field=13,
        Dynamic=14,
        ASIC=15,
        FormboardTable=16,
        ContactArrangement=17,
        HierarchicalBlock=38,
        PortOnHierarchicalBlock=39,
        PortOnHierarchical=40,
        TerminalRow=50,
        Mount=60,
        Panel=61,
        CableDuct=62,
        TemplateForPins=100,
        TemplateForTexts=101,
        AttributeTemplate=102,
        PinTemplate=103,
        PadTemplate=104
    }
}
