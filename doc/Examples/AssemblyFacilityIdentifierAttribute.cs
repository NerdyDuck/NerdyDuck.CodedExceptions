using System;
using System.Reflection;

#region example1part1
using NerdyDuck.CodedExceptions;

[assembly: AssemblyFacilityIdentifier(0x0305)]
#endregion

#region example1part2
using NerdyDuck.CodedExceptions;

namespace Example
{
    class Program
    {
        static void Main()
        {
            int facilityId = AssemblyFacilityIdentifierAttribute.FromAssembly(Assembly.GetExecutingAssembly()).FacilityId;
            // facilityId is 0x0305
            int baseHResult = HResultHelper.GetBaseHResult(facilityId);
            // baseHResult is 0xa3050000
            int myHResult = baseHResult | 0x42;
            // myHResult is 0xa3050042;
        }
    }
}
#endregion
