using System.Collections.Generic;

namespace Patterns
{
    public interface ILinePatternChecker
    {
        ILineResult GetResultFromLine(in List<int> itemsInsideLine);
    }
}