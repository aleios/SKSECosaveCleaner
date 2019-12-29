using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSECosaveCleaner
{
    internal class ActorID
    {
        private byte[] m_id;
        private bool m_isfound;

        public ActorID(string id)
        {
            try
            {
                if(id.Length != 8)
                {
                    throw new ArgumentException();
                }
                m_id = BitConverter.GetBytes(Convert.ToInt32(id, 16));
            }
            catch(Exception)
            {
                throw new ArgumentException("Invalid characters in actor form ID " + id);
            }
        }

        public byte[] ID
        {
            get { return m_id; }
        }

        public bool IDFound
        {
            get { return m_isfound; }
            set { m_isfound = value; }
        }
    }
}
