using System.Collections.Generic;

namespace Patterns
{
    public interface IGridToLineConverter
    {
        void GetLineValuesFromGrid(LineType lineType, in IGrid grid, out List<int> valuesInLine);
    }
}

