using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSECosaveCleaner
{
    // Section of actor data that we wish to preserve!
    internal class ActorSection
    {
        public readonly int m_startIndex;
        public readonly int m_endIndex;

        public ActorSection(int startIndex, int endIndex)
        {
            m_startIndex = startIndex;
            m_endIndex = endIndex;
        }
    }
}
