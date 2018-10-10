using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUI.Transportations.Air.Source
{
    public static class PlaneKindFactory
    {
        private static Dictionary<PlaneType, Func<PlaneKind>> _planes
            = new Dictionary<PlaneType, Func<PlaneKind>>
            {
                {PlaneType.airbus, () => GetAirbusA380()},
                {PlaneType.boeing, () => GetBoeing()},
            };

        public static PlaneKind GetPlane(PlaneType type)
        {
            return _planes[type]();
        }

        private static PlaneKind GetAirbusA380()
        {
            var kind = new PlaneKind();
            kind.Name = @"airbus a380";
            kind.KmsHourSpeedAverage = 900;
            kind.Weight = 300000000;
            kind.EngineFactor = 0.0000007;

            kind.StartingEffort = 8;
            kind.RequiredStartingDistance = 20;
            return kind;
        }

        private static PlaneKind GetBoeing()
        {
            var kind = new PlaneKind();
            kind.Name = @"boeing";
            kind.KmsHourSpeedAverage = 930;
            kind.Weight = 280000000;
            kind.EngineFactor = 0.0000007;

            kind.StartingEffort = 8;
            kind.RequiredStartingDistance = 17;
            return kind;
        }
    }

    public enum PlaneType
    {
        boeing,
        airbus
    }
}
