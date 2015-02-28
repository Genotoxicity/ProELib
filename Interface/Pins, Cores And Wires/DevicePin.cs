using System.Collections.Generic;
using System.Windows;
using System;
using e3;

namespace ProELib
{
    public class DevicePin : Pin
    {
        private int sheetId;
        private Point position;
        private bool isLocationVariablesSet;

        public List<int> CoreIds
        {
            get
            {
                dynamic connectedCoreIds = default(dynamic);
                int coreCount = e3Pin.GetCoreIds(ref connectedCoreIds);
                List<int> ids = new List<int>(coreCount);
                for (int i = 1; i <= coreCount; i++)
                    ids.Add(connectedCoreIds[i]);
                return ids;
            }
        }

        public int ConnectedPinId
        {
            get
            {
                return e3Pin.GetConnectedPinId();
            }
        }

        public int SequenceNumber
        {
            get
            {
                return e3Pin.GetSequenceNumber();
            }
        }

        public int PhysicalId
        {
            get
            {
                return e3Pin.GetPhysicalID();
            }
        }

        public bool IsPlaced
        {
            get
            {
                return e3Pin.HasDevice() == 1;
            }
        }

        public PinPanelLocation PanelLocation
        {
            get
            {

                dynamic dx = default(dynamic);
                dynamic dy = default(dynamic);
                dynamic dz = default(dynamic);
                int sheetId = e3Pin.GetPanelLocation(ref dx, ref dy, ref dz);
                return new PinPanelLocation(sheetId, (double)dx, (double)dy, (double)dz);
            }
        }

        public override int Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
                isLocationVariablesSet = false;
            }
        }

        public int SheetId
        {
            get
            {
                if (!isLocationVariablesSet)
                    SetLocationVariables();
                return sheetId;
            }
        }

        public Point Position
        {
            get
            {
                if (!isLocationVariablesSet)
                    SetLocationVariables();
                return position;
            }
        }

        public int LogicalEquivalence
        {
            get
            {
                return e3Pin.GetLogicalEquivalenceID();
            }
        }

        public int NameEquivalence
        {
            get
            {
                return e3Pin.GetNameEquivalenceID();
            }
        }

        public bool IsOnPanel
        {
            get
            {
                dynamic dx = default(dynamic);
                dynamic dy = default(dynamic);
                dynamic dz = default(dynamic);
                return e3Pin.GetPanelLocation(ref dx, ref dy, ref dz) > 0;
            }
        }

        public bool IsView
        {
            get
            {
                return e3Pin.IsView() == 1;
            }
        }

        public bool IsPinView
        {
            get
            {
                return e3Pin.IsPinView() == 1;
            }
        }

        internal DevicePin(e3Pin e3Pin) : base(e3Pin)
        {
            isLocationVariablesSet = false;
        }

        

        private void SetLocationVariables()
        {
            isLocationVariablesSet = true;
            dynamic xCoordinate = default(dynamic), yCoordinate = default(dynamic), grid = default(dynamic);
            sheetId = e3Pin.GetSchemaLocation(ref xCoordinate, ref yCoordinate, ref grid);
            if (xCoordinate != null && yCoordinate != null)
                position = new Point(xCoordinate, yCoordinate);
        }

    }
}
