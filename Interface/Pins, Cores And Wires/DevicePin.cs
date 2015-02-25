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
                int coreCount = pin.GetCoreIds(ref connectedCoreIds);
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
                return pin.GetConnectedPinId();
            }
        }

        public int SequenceNumber
        {
            get
            {
                return pin.GetSequenceNumber();
            }
        }

        public int PhysicalId
        {
            get
            {
                return pin.GetPhysicalID();
            }
        }

        public bool IsPlaced
        {
            get
            {
                return pin.HasDevice() == 1;
            }
        }

        public PinPanelLocation PanelLocation
        {
            get
            {

                dynamic dx = default(dynamic);
                dynamic dy = default(dynamic);
                dynamic dz = default(dynamic);
                int sheetId = pin.GetPanelLocation(ref dx, ref dy, ref dz);
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
                return pin.GetLogicalEquivalenceID();
            }
        }

        public int NameEquivalence
        {
            get
            {
                return pin.GetNameEquivalenceID();
            }
        }

        public bool IsOnPanel
        {
            get
            {
                dynamic dx = default(dynamic);
                dynamic dy = default(dynamic);
                dynamic dz = default(dynamic);
                return pin.GetPanelLocation(ref dx, ref dy, ref dz) > 0;
            }
        }

        public bool IsView
        {
            get
            {
                return pin.IsView() == 1;
            }
        }

        public bool IsPinView
        {
            get
            {
                return pin.IsPinView() == 1;
            }
        }

        internal DevicePin(e3Job job) : base(job)
        {
            isLocationVariablesSet = false;
        }

        

        private void SetLocationVariables()
        {
            isLocationVariablesSet = true;
            dynamic xCoordinate = default(dynamic), yCoordinate = default(dynamic), grid = default(dynamic);
            sheetId = pin.GetSchemaLocation(ref xCoordinate, ref yCoordinate, ref grid);
            if (xCoordinate != null && yCoordinate != null)
                position = new Point(xCoordinate, yCoordinate);
        }

    }
}
