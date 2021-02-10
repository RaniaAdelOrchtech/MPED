using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MPMAR.Business.Interfaces
{
    public interface IImportFromExcel
    {
        void ComponentConstant(string filePath, int rowsCount, int sheetNum = 0);
        void ComponentCurrent(string filePath, int rowsCount, int sheetNum = 1);
        void ActivityConstant(string filePath, int rowsCount, int sheetNum = 2);
        void ActivityCurrent(string filePath, int rowsCount, int sheetNum = 3);
        void RGDPGrowthRate(string filePath, int rowsCount, int sheetNum = 0);
        void RGDPGrowthRate1617(string filePath, int rowsCount, int sheetNum = 0);
        void SectorGrowthRate(string filePath, int rowsCount, int sheetNum = 0);
        void Investments(string filePath, int rowsCount, int sheetNum = 0);
    }
}
