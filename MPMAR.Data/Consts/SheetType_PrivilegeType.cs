using MPMAR.Analytics.Data.Enums;
using MPMAR.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPMAR.Data.Consts
{
    public class SheetType_PrivilegeType
    {
        public static readonly Dictionary<int, PrivilegesPageType> SheetType_PrivilegeType_Map = new Dictionary<int, PrivilegesPageType>() {
            {(int)SheetTypeEnum.ActivityCurrent,PrivilegesPageType.ActivityCurrent },
            {(int)SheetTypeEnum.SectorGrowthRates,PrivilegesPageType.SectorGrowthRates },
            {(int)SheetTypeEnum.ComponentConst,PrivilegesPageType.ComponentConstant },
            {(int)SheetTypeEnum.ComponentCurrent,PrivilegesPageType.ComponentCurrent },
        };
    }
}
