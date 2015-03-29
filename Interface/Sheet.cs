using System;
using System.Collections.Generic;
using e3;

namespace ProELib
{
    public class Sheet
    {
        private e3Sheet e3Sheet;
        private OrdinateDirection ordinateDirection;
        private AbscissaDirection abscissaDirection;
        private Area drawingArea;
        private bool isDrawingAreaGot;

        public int Id
        {
            get
            {
                return e3Sheet.GetId();
            }
            set
            {
                e3Sheet.SetId(value);
                SetAxesDirections();
                isDrawingAreaGot = false;
            }
        }   

        public string Name
        {
            get
            {
                return e3Sheet.GetName();
            }
            set
            {
                if (String.IsNullOrEmpty(value))
                    e3Sheet.SetName(value.Replace(' ', '_'));
            }
        }

        public List<int> NetSegmentIds
        {
            get
            {
                dynamic netSegmentIds = default(dynamic);
                int netSegmentCount = e3Sheet.GetNetSegmentIds(ref netSegmentIds);
                List<int> ids = new List<int>(netSegmentCount);
                for (int i = 1; i <= netSegmentCount; i++)
                    ids.Add(netSegmentIds[i]);
                return ids;
            }
        }

        public List<int> SchematicTypes
        {
            get
            {
                dynamic schematicTypes = default(dynamic);
                int schematicTypeCount = e3Sheet.GetSchematicTypes(ref schematicTypes);
                List<int> types = new List<int>(schematicTypeCount);
                for (int i = 1; i <= schematicTypeCount; i++)
                    types.Add(schematicTypes[i]);
                return types;
            }
        }

        public List<int> SymbolIds
        {
            get
            {
                dynamic symbolIds = default(dynamic);
                int symbolCount = e3Sheet.GetSymbolIds(ref symbolIds);
                List<int> ids = new List<int>(symbolCount);
                for (int i = 1; i <= symbolCount; i++)
                    ids.Add(symbolIds[i]);
                return ids;
            }
        }

        public List<int> InsideSymbolIds
        {
            get
            {
                dynamic insideSymbolIds = default(dynamic);
                int insideSymbolCount = e3Sheet.GetInsideSymbolIds(ref insideSymbolIds);
                List<int> ids = new List<int>(insideSymbolCount);
                for (int i = 1; i <= insideSymbolCount; i++)
                    ids.Add(insideSymbolIds[i]);
                return ids;
            }
        }

        public List<int> GraphicIds
        {
            get
            {
                dynamic graphicIds = default(dynamic);
                int graphicCount = e3Sheet.GetGraphIds(ref graphicIds);
                List<int> ids = new List<int>(graphicCount);
                for (int i = 1; i <= graphicCount; i++)
                    ids.Add(graphicIds[i]);
                return ids;
            }
        }

        public List<int> GroupIds
        {
            get
            {
                dynamic groupIds = default(dynamic);
                int groupCount = e3Sheet.GetGroupIds(ref groupIds);
                List<int> ids = new List<int>(groupCount);
                for (int i = 1; i <= groupCount; i++)
                    ids.Add(groupIds[i]);
                return ids;
            }
        }

        public List<int> EmbeddedSheetIds
        {
            get
            { 
                dynamic embeddedSheetIds = default(dynamic);
                int embeddedSheetCount = e3Sheet.GetEmbeddedSheetIds(ref embeddedSheetIds);
                List<int> ids = new List<int>(embeddedSheetCount);
                for (int i = 1; i <= embeddedSheetCount; i++)
                    ids.Add(embeddedSheetIds[i]);
                return ids;
            }
        }

        public int ParentSheetId
        {
            get
            {
                return e3Sheet.GetParentSheetId();
            }
        }

        public Area DrawingArea
        {
            get
            {
                if (!isDrawingAreaGot)
                {
                    drawingArea = GetDrawingArea();
                    isDrawingAreaGot = true;
                }
                return drawingArea;
            }
        }

        public bool IsPanel
        {
            get
            {
                return e3Sheet.IsPanel() > 0 ? true : false;
            }
        }

        internal Sheet(e3Sheet e3Sheet)
        {
            this.e3Sheet = e3Sheet;
            SetAxesDirections();
            isDrawingAreaGot = false;
        }

        private void SetAxesDirections()
        {
            dynamic xLeft = default(dynamic);
            dynamic yBottom = default(dynamic);
            dynamic xRight = default(dynamic);
            dynamic yTop = default(dynamic);
            e3Sheet.GetDrawingArea(ref xLeft, ref yBottom, ref xRight, ref yTop);
            if (yTop < yBottom)
                ordinateDirection = OrdinateDirection.TopToBottom;
            else
                ordinateDirection = OrdinateDirection.BottomToTop;
            if (xLeft < xRight)
                abscissaDirection = AbscissaDirection.LeftToRight;
            else
                abscissaDirection = AbscissaDirection.RightToLeft;
        }

        public bool IsSchematicTypeOf(int schematicTypeCode)
        {
            return SchematicTypes.Contains(schematicTypeCode);
        }

        public double MoveDown(double from, double offset)
        {
            if (ordinateDirection == OrdinateDirection.TopToBottom)
                return from + offset;
            return from - offset;
        }

        public double MoveUp(double from, double offset)
        {
            if (ordinateDirection == OrdinateDirection.BottomToTop)
                return from + offset;
            return from - offset;
        }

        public double MoveLeft(double from, double offset)
        {
            if (abscissaDirection == AbscissaDirection.RightToLeft)
                return from + offset;
            return from - offset;
        }

        public double MoveRight(double from, double offset)
        {
            if (abscissaDirection == AbscissaDirection.LeftToRight)
                return from + offset;
            return from - offset;
        }

        public bool IsUnderTarget(double target, double value)
        {
            if (ordinateDirection == OrdinateDirection.TopToBottom)
                return target < value;
            return target > value;
        }

        public bool IsAboveTarget(double target, double value)
        {
            if (ordinateDirection == OrdinateDirection.TopToBottom)
                return target > value;
            return target < value;
        }

        public bool IsUnderOrEqualTarget(double target, double value)
        {
            if (ordinateDirection == OrdinateDirection.TopToBottom)
                return target <= value;
            return target >= value;
        }

        public bool IsAboveOrEqualTarget(double target, double value)
        {
            if (ordinateDirection == OrdinateDirection.TopToBottom)
                return target >= value;
            return target <= value;
        }

        public bool IsRightOfTarget(double target, double value)
        {
            if (abscissaDirection == AbscissaDirection.LeftToRight)
                return target < value;
            return target > value;
        }

        public bool IsLeftOfTarget(double target, double value)
        {
            if (abscissaDirection == AbscissaDirection.LeftToRight)
                return target > value;
            return target < value;
        }

        public bool IsRightOfOrEqualTarget(double target, double value)
        {
            if (abscissaDirection == AbscissaDirection.LeftToRight)
                return target <= value;
            return target >= value;
        }

        public bool IsLeftOfOrEqualTarget(double target, double value)
        {
            if (abscissaDirection == AbscissaDirection.LeftToRight)
                return target >= value;
            return target <= value;
        }

        public int Create(string name, string format)
        {
            return Create(name, format, 0, InsertPosition.After);
        }

        public int Create(string name, string format, int targetSheetId, InsertPosition position)
        {
            int newSheetId = e3Sheet.Create(0, name, format, targetSheetId, (int)position);
            Id = newSheetId;
            return newSheetId;
        }

        public int Delete()
        {
            if (Id > 0)
            {
                int result = e3Sheet.Delete();
                Id = 0;
                return result;
            }
            return 0;
        }

        public void ExportImage(string fileName, string fileFormat)
        {
            e3Sheet.ExportImage(fileFormat, 0, fileName);
        }

        public string GetAttributeValue(string attribute)
        {
            return e3Sheet.GetAttributeValue(attribute);
        }

        public void SetAttribute(string attribute, string value)
        {
            e3Sheet.SetAttributeValue(attribute, value);
        }

        public List<int> GetTextIds(int typeCode)
        {
            dynamic textIds = default(dynamic);
            int textCount = e3Sheet.GetTextIds(ref textIds, typeCode);
            List<int> ids = new List<int>(textCount);
            for (int i = 1; i <= textCount; i++)
                ids.Add(textIds[i]);
            return ids;
        }

        private Area GetDrawingArea()
        { 
            dynamic xMin = default(dynamic), yMin = default(dynamic), xMax = default(dynamic), yMax = default(dynamic);
            e3Sheet.GetDrawingArea(ref xMin, ref yMin, ref xMax, ref yMax);
            return new Area(xMin, xMax, yMax, yMin);
        }
    }
}
