using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Shared.Data
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NeuralIdealAttribute : System.Attribute
    {
        public int Index { get; private set; }

        public NeuralIdealAttribute(int iOutputNeuronIndex)
        {
            Index = iOutputNeuronIndex;
        }
    }
}
