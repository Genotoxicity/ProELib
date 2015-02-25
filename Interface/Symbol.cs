using System.Collections.Generic;
using System.Windows;
using System;
using e3;

namespace ProELib
{
    public class Symbol
    {
        private e3Symbol symbol;
        private bool isAreaGot;
        private Area area;
        private Point position;
        private int sheetId;
        private bool isLocationVariablesSet;

        public int SheetId
        {
            get
            {
                if (!isLocationVariablesSet)
                    SetLocationVariables();
                return sheetId;
            }
        }

        public bool IsConnected
        {
            get
            {
                return symbol.IsConnected()==1;
            }
        }

        public Area Area
        {
            get
            {
                if (!isAreaGot)
                {
                    area = GetArea();
                    isAreaGot = true;
                }
                return area;
            }
        }

        public int Id
        {
            get
            {
                return symbol.GetId();
            }
            set
            {
                symbol.SetId(value);
                isAreaGot = false;
                isLocationVariablesSet = false;
            }
        }

        public string Name
        {
            get
            {
                return symbol.GetName();
            }
        }

        public string Version
        {
            get
            {
                return symbol.GetVersion();
            }
        }

        public string Type
        {
            get
            {
                return symbol.GetType();
            }
        }

        public string TypeName
        {
            get
            {
                return symbol.GetTypeName();
            }
        }

        public string SymbolTypeName
        {
            get
            {
                return symbol.GetSymbolTypeName();
            }
        }

        public List<int> PinIds
        {
            get
            {
                dynamic pinIds = default(dynamic);
                int pinCount = symbol.GetPinIds(ref pinIds);
                List<int> ids = new List<int>(pinCount);
                for (int i = 1; i <= pinCount; i++)
                    ids.Add(pinIds[i]);
                return ids;
            }
        }

        public int PinCount
        {
            get
            {
                return symbol.GetPinCount();
            }
        }

        public SheetReferenceInfo SheetReferenceInfo
        {
            get
            {
                dynamic inout, type, refnam, signam;
                if (symbol.GetSheetReferenceInfo(out inout, out type, out refnam, out signam) == 1)
                    return new SheetReferenceInfo((int)inout, (int)type, (string)signam, (string)refnam);
                return null;
            }
        }

        internal Symbol(e3Job job)
        {
            symbol = job.CreateSymbolObject();
            isAreaGot = false;
            isLocationVariablesSet = false;
        }

        public List<int> GetTextIdsOfType(int textType)
        {
            dynamic textIds = default(dynamic);
            int textCount = symbol.GetTextIds(ref textIds, textType);
            List<int> ids = new List<int>(textCount);
            for (int i = 1; i <= textCount; i++)
                ids.Add(textIds[i]);
            return ids;
        }

        public bool IsSchematicTypeOf(int schematicTypeCode)
        {
            dynamic schematicTypes = default(dynamic);
            int schematicTypeCount = symbol.GetSchematicTypes(ref schematicTypes);
            if (schematicTypeCount == 0)
                return false;
            for (int i = 1; i <= schematicTypeCount; i++)
                if (schematicTypes[i] == schematicTypeCode)
                    return true;
            return false;
        }

        public int Place(int sheetId, double x, double y)
        {
            return symbol.Place(sheetId, x, y);
        }

        public int Place(int sheetId, double x, double y, SymbolTransformation transformation)
        {
            string rotation = String.Empty;
            if (transformation == SymbolTransformation.HorizontallyMirrored)
                rotation = "X0";
            return symbol.Place(sheetId, x, y,rotation);
        }

        public int PlaceAsGraphic(int sheetId, double x, double y)
        {
            return symbol.PlaceAsGraphic(sheetId, x, y, null, 0, 0, 0);
        }

        public int Delete()
        {
            return symbol.Delete();
        }

        public int Jump()
        {
            return symbol.Jump();
        }

        public int Load(string name, string version)
        {
            return symbol.Load(name, version);
        }

        public int PlaceInteractively()
        {
            return symbol.PlaceInteractively();
        }

        private void SetLocationVariables()
        {
            isLocationVariablesSet = true;
            dynamic dx = default(dynamic);
            dynamic dy = default(dynamic);
            dynamic grid = default(dynamic);
            sheetId = symbol.GetSchemaLocation(ref dx, ref dy, ref grid);
            position = new Point((double)dx, (double)dx);
        }

        public void SetAttribute(string attribute, string value)
        {
            symbol.SetAttributeValue(attribute, value);
        }

        public string GetAttribute(string attribute)
        {
            return symbol.GetAttributeValue(attribute);
        }

        private Area GetArea()
        {
            dynamic xMin = default(dynamic), yMin = default(dynamic), xMax = default(dynamic), yMax = default(dynamic);
            symbol.GetArea(ref xMin, ref yMin, ref xMax, ref yMax);
            return new Area(xMin, xMax, yMax, yMin);
        }

    }
}
